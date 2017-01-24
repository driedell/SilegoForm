using GreenPAK_Library;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

public static class MainProgram
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //  Global variables
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class g
    {
        public static string nvmData = null;
        public static XElement ELEMENT;
        public static Microsoft.Office.Interop.Word.Application app;
        public static Microsoft.Office.Interop.Word.Document doc;
        public static OleDbConnection connection = new OleDbConnection();

        public static string GreenPAK_File = null;
        public static string DataSheet_File = null;
        public static string templatePath = @"P:\Apps_Tools\New_DS_Template\";
        public static string path;

        public static bool pin_labels_update = false;
        public static bool pin_settings_update = false;
        public static bool project_info_update = false;
        public static bool temp_vdd_update = false;
        public static bool CNTs_DLYs_update = false;
        public static bool ACMPs_update = false;
        public static bool I_Q_update = false;
        public static bool new_part_update = false;
        public static bool lock_status_update = false;
        public static bool TM_part_code_update = false;
        public static bool TM_revision_update = false;
        public static bool ext_clk_update = false;

        public static string I_Q = "1.0";
        public static string TM_part_code = "  ";
        public static string TM_revision = "  ";
        public static string DS_rev = "010";
        public static string DS_rev_change = null;
        public static string Date = null;
        public static string part_number = "SLG~~~~~";
        public static int ext_clk_freq = 0;

        public static PAK GreenPAK;

        public static bool VHYS;
        public static bool asm_used;

        // Holds the number of pins for different IO settings for the output summary on the first page.
        // We have both VDD & VDD2 for dual supply PAKs
        public static pin_IO_settings VDD = new pin_IO_settings();

        public static pin_IO_settings VDD2 = new pin_IO_settings();

        public struct pin_IO_settings
        {
            public byte PP1x;
            public byte PP2x;
            public byte PP4x;
            public byte ODN1x;
            public byte ODN2x;
            public byte ODN4x;
            public byte ODP1x;
            public byte ODP2x;
            public byte TRI1x;
            public byte TRI2x;
            public bool LVDI;
            public bool wSchmitt;
            public bool woSchmitt;
        };

        // Holds VDD/VDD2 range for populating the EC table.
        // VDD_EC[0] = 1.8v
        // VDD_EC[1] = 3.3v
        // VDD_EC[2] = 5.0v
        public static bool[] VDD_EC = new bool[3];

        public static bool[] VDD2_EC = new bool[3];

        // Global variables to reduce paramaters that need to be passed for the EC table
        public static Table table;                      // Word table

        public static int row = 0;                      // Row of the table
        public static string VDD_DB = null;             // Access Database column designator (1_8, 3_3, 5_0)
        public static string VDD_typ = null;            // VDD to print to table (1.8, 3.3, 5.0)
        public static string subSymbol = null;          // Removes "2" from Dual Supply symbols (VOH2 → VOH)
        public static string extraInfo = null;          // Holds additional info for the condition/note field

        public struct symbols
        {
            public string sym;
            public string sub;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //  Programmatically move form Window and Console Window
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private const int SWP_NOSIZE = 0x0001;

    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    private static IntPtr MyConsole = GetConsoleWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //  Main
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [STAThread]
    public static void Main()
    {
        int xpos = 800;
        int ypos = 500;
        SetWindowPos(MyConsole, 0, xpos, ypos, 0, 0, SWP_NOSIZE);

        // Start the SilegoForm which will begin the BackgroundWorker and enter theProgram
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        System.Windows.Forms.Application.Run(new SilegoForm.SilegoForm());
        // After SilegoForm closes, we return here:

        closeDontSave();

        Console.WriteLine("press a key");
        Console.ReadLine();
    }

    public static void closeDontSave()
    {
        //  Closes and quits the word document. Mainly used in instances where the program does not run to completion.

        try
        {
            g.doc.Close(WdSaveOptions.wdDoNotSaveChanges);
            g.app.Quit(WdSaveOptions.wdDoNotSaveChanges);
        }
        catch { Console.WriteLine("An error occured in closeDontSave(), or closeDontSave() was unnecessary"); }
    }

    private static void pin_cell_update(Table table, int i)
    {
        // Updates the pin labels in the first page pinout diagram

        if (g.GreenPAK.pin[i].name.Length <= 16)
        {
            table.Cell(1, 1).Range.Text = g.GreenPAK.pin[i].name;
        }
        else
        {
            table.Cell(1, 1).Range.Text = g.GreenPAK.pin[i].name.Substring(0, 16) + " " +
                                          g.GreenPAK.pin[i].name.Substring(16);
        }
    }

    private static void pin_cell_config(int i, int HP, int VP, char side)
    {
        // Configures the cells that hold the labels for the first page pinout diagram.
        // Alters the text orientation and text alignment.

        foreach (Table table in g.doc.Tables)
        {
            if (table.Title == "pin" + i.ToString() + "_label")
            {
                table.Rows.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                table.Rows.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                table.Rows.HorizontalPosition = HP;
                table.Rows.VerticalPosition = VP;

                pin_cell_update(table, i);

                table.Cell(1, 1).Range.Rows[1].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                table.Cell(1, 1).WordWrap = true;

                switch (side)
                {
                    case 'L':
                        table.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        table.Cell(1, 1).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
                        table.Cell(1, 1).Width = 100;
                        table.Cell(1, 1).Height = 30;
                        break;

                    case 'D':
                        table.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        table.Cell(1, 1).Range.Orientation = WdTextOrientation.wdTextOrientationUpward;
                        table.Cell(1, 1).Width = 30;
                        table.Cell(1, 1).Height = 100;
                        break;

                    case 'R':
                        table.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        table.Cell(1, 1).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
                        table.Cell(1, 1).Width = 100;
                        table.Cell(1, 1).Height = 30;
                        break;

                    case 'U':
                        table.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        table.Cell(1, 1).Range.Orientation = WdTextOrientation.wdTextOrientationUpward;
                        table.Cell(1, 1).Width = 30;
                        table.Cell(1, 1).Height = 100;
                        break;

                    default: break;
                }
                break;
            }
        }
    }

    private static void pin_config(int i)
    {
        // Uses GreenPAK_Library.cs to read the nvm and store pin settings in a struct.
        // Writes pin settings to the Pin Configuration table in the datasheet.
        // Stores the number of outputs of different types in g.VDD.xxxx for the output summary on
        // the first page.

        switch (g.GreenPAK.pin[i].PT)
        {
            case "GPI":
                g.GreenPAK.pin[i].type = "Digital Input";
                pin_config_resistor(i);
                pin_config_OE_input(i);
                break;

            case "GPIO_OE":
                int OE = g.GreenPAK.pin[i].OE;

                pin_config_resistor(i);

                if (g.nvmData.Substring(OE, g.GreenPAK.matrix_VDD.Length).Equals(g.GreenPAK.matrix_GND))
                {
                    g.GreenPAK.pin[i].type = "Digital Input";
                }
                else if (g.nvmData.Substring(OE, g.GreenPAK.matrix_VDD.Length).Equals(g.GreenPAK.matrix_VDD) ||
                         g.nvmData.Substring(OE, g.GreenPAK.matrix_VDD.Length).Equals(g.GreenPAK.matrix_POR))
                {
                    g.GreenPAK.pin[i].type = "Digital Output";
                }
                else
                {
                    g.GreenPAK.pin[i].type = "Digital I/O";
                }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i); break;

                    case "Digital Output": pin_config_OE_output(i); break;

                    case "Digital I/O":
                        pin_config_OE_input(i);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i);
                        break;

                    default: break;
                }
                break;

            case "GPIO":
                pin_config_resistor(i);
                pin_config_GPIO(i);
                break;

            case "I2C":
                pin_config_resistor(i);
                switch (g.nvmData[g.GreenPAK.pin[i].UD].ToString())  // Check if we're using I2C
                {
                    case "0":                           // I2C is used
                        g.GreenPAK.pin[i].type = "I2C";
                        g.GreenPAK.pin[i].description = "Digital Input without Schmitt Trigger";
                        break;

                    case "1":                            // I2C is not used
                        pin_config_GPIO(i);
                        break;
                }
                break;

            case "SD_OE":
                OE = g.GreenPAK.pin[i].OE;

                pin_config_resistor(i);

                if (g.nvmData.Substring(OE, 6).Equals("000000")) { g.GreenPAK.pin[i].type = "Digital Input"; }
                else if (g.nvmData.Substring(OE, 6).Equals("111111")) { g.GreenPAK.pin[i].type = "Digital Output"; }
                else { g.GreenPAK.pin[i].type = "Digital I/O"; }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i); break;

                    case "Digital Output": pin_config_OE_output(i); break;

                    case "Digital I/O":
                        pin_config_OE_input(i);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i);
                        break;

                    default: break;
                }
                break;

            case "SD":
                pin_config_resistor(i);
                pin_config_GPIO(i);
                break;

            default: break;
        }
        if (g.GreenPAK.pin[i].VS.Equals(1))
        {
            switch (g.GreenPAK.pin[i].description)
            {
                case "Push Pull 1x": g.VDD.PP1x++; break;
                case "Digital Input with Schmitt trigger": g.VHYS = true; g.VDD.wSchmitt = true; break;
                case "Push Pull 2x": g.VDD.PP2x++; break;
                case "Push Pull 4x": g.VDD.PP4x++; break;
                case "Open Drain NMOS 1x": g.VDD.ODN1x++; break;
                case "Open Drain NMOS 2x": g.VDD.ODN2x++; break;
                case "Open Drain NMOS 4x": g.VDD.ODN4x++; break;
                case "Open Drain PMOS 1x": g.VDD.ODP1x++; break;
                case "Open Drain PMOS 2x": g.VDD.ODP2x++; break;
                case "3-State Output 1x": g.VDD.TRI1x++; break;
                case "3-State Output 2x": g.VDD.TRI2x++; break;
                case "Digital Input without Schmitt trigger": g.VDD.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }
        else if (g.GreenPAK.pin[i].VS.Equals(2))
        {
            switch (g.GreenPAK.pin[i].description)
            {
                case "Push Pull 1x": g.VDD2.PP1x++; break;
                case "Digital Input with Schmitt trigger": g.VHYS = true; g.VDD2.wSchmitt = true; break;
                case "Push Pull 2x": g.VDD2.PP2x++; break;
                case "Push Pull 4x": g.VDD2.PP4x++; break;
                case "Open Drain NMOS 1x": g.VDD2.ODN1x++; break;
                case "Open Drain NMOS 2x": g.VDD2.ODN2x++; break;
                case "Open Drain NMOS 4x": g.VDD2.ODN4x++; break;
                case "Open Drain PMOS 1x": g.VDD2.ODP1x++; break;
                case "Open Drain PMOS 2x": g.VDD2.ODP2x++; break;
                case "3-State Output 1x": g.VDD2.TRI1x++; break;
                case "3-State Output 2x": g.VDD2.TRI2x++; break;
                case "Digital Input without Schmitt trigger": g.VDD2.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD2.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }

        Console.WriteLine("Pin" + i.ToString() + ": " + g.GreenPAK.pin[i].type + ", " + g.GreenPAK.pin[i].description);
    }

    private static void pin_config_name(int i)
    {
        foreach (XElement pin in g.ELEMENT.Descendants("item")
            .Where(pin => pin.Attribute("caption").ToString().StartsWith("caption=\"PIN " + i.ToString())))
        {
            g.GreenPAK.pin[i].name = pin.Element("textLabel").Value;
        }
    }

    private static void pin_config_resistor(int i)
    {
        // Determines if a pin is floating or if it has a resistor. Stores that information in the
        // pin's struct.

        bool floating = false;

        switch (g.nvmData[g.GreenPAK.pin[i].RV + 1].ToString() +
                g.nvmData[g.GreenPAK.pin[i].RV + 0].ToString())
        {
            case "00": g.GreenPAK.pin[i].resistor = "floating"; floating = true; break;
            case "01": g.GreenPAK.pin[i].resistor = "10kΩ"; break;
            case "10": g.GreenPAK.pin[i].resistor = "100kΩ"; break;
            case "11": g.GreenPAK.pin[i].resistor = "1MΩ"; break;
        }
        if (floating.Equals(false))
        {
            if (g.GreenPAK.pin[i].UD.Equals(0))
            {
                g.GreenPAK.pin[i].resistor += " pulldown";
            }
            else
            {
                switch (g.nvmData[g.GreenPAK.pin[i].UD].ToString())
                {
                    case "0": g.GreenPAK.pin[i].resistor += " pulldown"; break;
                    case "1": g.GreenPAK.pin[i].resistor += " pullup"; break;
                }
            }
        }
    }

    private static void pin_config_OE_input(int i)
    {
        // Stores a pin's input settings for pins with OE's.

        switch (g.nvmData[g.GreenPAK.pin[i].IM + 1].ToString() +
                g.nvmData[g.GreenPAK.pin[i].IM + 0].ToString())
        {
            case "00": g.GreenPAK.pin[i].description += "Digital Input without Schmitt trigger"; break;
            case "01": g.GreenPAK.pin[i].description += "Digital Input with Schmitt trigger"; break;
            case "10": g.GreenPAK.pin[i].description += "Low Voltage Digital Input"; break;
            case "11":
                if (g.GreenPAK.pin[i].type == "Digital I/O")
                {
                    g.GreenPAK.pin[i].type = "3-State";
                    break;
                }
                else
                {
                    g.GreenPAK.pin[i].description += "Analog Input/Output";
                    g.GreenPAK.pin[i].type = "Analog I/O";
                    break;
                }
        }
    }

    private static void pin_config_OE_output(int i)
    {
        // Stores a pin's output settings for pins with OE's.

        if (g.GreenPAK.pin[i].SD > 0 &&
            g.nvmData[g.GreenPAK.pin[i].SD].ToString().Equals("1"))
        {
            g.GreenPAK.pin[i].description += "Open Drain NMOS 4x";
        }
        else
        {
            switch (g.nvmData[g.GreenPAK.pin[i].OM + 1].ToString() +
                    g.nvmData[g.GreenPAK.pin[i].OM + 0].ToString())
            {
                case "00":
                    if (g.GreenPAK.pin[i].type == "3-State")
                    {
                        g.GreenPAK.pin[i].description = "3-State Output 1x";
                        break;
                    }
                    else
                    {
                        g.GreenPAK.pin[i].description += "Push Pull 1x";
                        break;
                    }
                case "01":
                    if (g.GreenPAK.pin[i].type == "3-State")
                    {
                        g.GreenPAK.pin[i].description = "3-State Output 2x";
                        break;
                    }
                    else
                    {
                        g.GreenPAK.pin[i].description += "Push Pull 2x";
                        break;
                    }
                case "10": g.GreenPAK.pin[i].description += "Open Drain NMOS 1x"; break;
                case "11": g.GreenPAK.pin[i].description += "Open Drain NMOS 2x"; break;
            }
        }
    }

    private static void pin_config_GPIO(int i)
    {
        // Stores a pin's IO settings for pins without OE's.

        switch (g.nvmData[g.GreenPAK.pin[i].IO + 2].ToString() +
                g.nvmData[g.GreenPAK.pin[i].IO + 1].ToString() +
                g.nvmData[g.GreenPAK.pin[i].IO + 0].ToString())
        {
            case "000": g.GreenPAK.pin[i].type = "Digital Input"; g.GreenPAK.pin[i].description = "Digital Input without Schmitt trigger"; break;
            case "001": g.GreenPAK.pin[i].type = "Digital Input"; g.GreenPAK.pin[i].description = "Digital Input with Schmitt trigger"; break;
            case "010": g.GreenPAK.pin[i].type = "Digital Input"; g.GreenPAK.pin[i].description = "Low Voltage Digital Input"; break;
            case "011": g.GreenPAK.pin[i].type = "Analog I/O"; g.GreenPAK.pin[i].description = "Analog Input/Output"; break;
            case "100": g.GreenPAK.pin[i].type = "Digital Output"; g.GreenPAK.pin[i].description = ("Push Pull"); break;
            case "101": g.GreenPAK.pin[i].type = "Digital Output"; g.GreenPAK.pin[i].description = ("Open Drain NMOS"); break;
            case "110": g.GreenPAK.pin[i].type = "Digital Output"; g.GreenPAK.pin[i].description = ("Open Drain PMOS"); break;
            case "111": g.GreenPAK.pin[i].type = "Digital Output"; g.GreenPAK.pin[i].description = ("Open Drain NMOS"); break;
        }
        if (g.GreenPAK.pin[i].type.Equals("Digital Output") &&
            g.GreenPAK.pin[i].SD > 0 &&
            g.nvmData[g.GreenPAK.pin[i].SD].ToString().Equals("1"))
        {
            g.GreenPAK.pin[i].description = "Open Drain NMOS 4x";
        }
        else if (g.GreenPAK.pin[i].type.Equals("Digital Output"))
        {
            switch (g.nvmData[g.GreenPAK.pin[i].DR].ToString())
            {
                case "0": g.GreenPAK.pin[i].description += " 1x"; break;
                case "1": g.GreenPAK.pin[i].description += " 2x"; break;
            }
        }
    }

    private static string TryGetElementValue(this XElement parentEl, string elementName)
    {
        // Checks if an element exists. If so, it reterns the element's value. If not, it returns null.

        var foundEl = parentEl.Element(elementName);

        if (foundEl != null)
        {
            return foundEl.Value;
        }
        return null;
    }

    private static void acmp_config(int i)
    {
        // Uses GreenPAK_Library.cs and nvm to store ACMP information in a struct.
        // Later this information will be written to the EC table.

        int acmpVIH = 0;
        int acmpVIL = 0;
        byte multiplier = 1;
        string low_bw = null;
        byte hysteresis = 0;

        if (g.GreenPAK.acmp[i].GN > 0)
        {
            switch (g.nvmData[g.GreenPAK.acmp[i].GN + 1].ToString() +
                    g.nvmData[g.GreenPAK.acmp[i].GN + 0].ToString())
            {
                case "00": multiplier = 1; break;
                case "01": multiplier = 2; break;
                case "10": multiplier = 3; break;
                case "11": multiplier = 4; break;
            }
        }

        switch (g.nvmData[g.GreenPAK.acmp[i].LB].ToString())  // ### still need this? unused
        {
            case "0": low_bw = "OFF"; break;
            case "1": low_bw = "ON"; break;
        }

        switch (g.nvmData[g.GreenPAK.acmp[i].TH + 4].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].TH + 3].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].TH + 2].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].TH + 1].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].TH + 0].ToString())
        {
            case "00000": acmpVIH = 0050 * multiplier; acmpVIL = 0050 * multiplier; break;
            case "00001": acmpVIH = 0100 * multiplier; acmpVIL = 0100 * multiplier; break;
            case "00010": acmpVIH = 0150 * multiplier; acmpVIL = 0150 * multiplier; break;
            case "00011": acmpVIH = 0200 * multiplier; acmpVIL = 0200 * multiplier; break;
            case "00100": acmpVIH = 0250 * multiplier; acmpVIL = 0250 * multiplier; break;
            case "00101": acmpVIH = 0300 * multiplier; acmpVIL = 0300 * multiplier; break;
            case "00110": acmpVIH = 0350 * multiplier; acmpVIL = 0350 * multiplier; break;
            case "00111": acmpVIH = 0400 * multiplier; acmpVIL = 0400 * multiplier; break;
            case "01000": acmpVIH = 0450 * multiplier; acmpVIL = 0450 * multiplier; break;
            case "01001": acmpVIH = 0500 * multiplier; acmpVIL = 0500 * multiplier; break;
            case "01010": acmpVIH = 0550 * multiplier; acmpVIL = 0550 * multiplier; break;
            case "01011": acmpVIH = 0600 * multiplier; acmpVIL = 0600 * multiplier; break;
            case "01100": acmpVIH = 0650 * multiplier; acmpVIL = 0650 * multiplier; break;
            case "01101": acmpVIH = 0700 * multiplier; acmpVIL = 0700 * multiplier; break;
            case "01110": acmpVIH = 0750 * multiplier; acmpVIL = 0750 * multiplier; break;
            case "01111": acmpVIH = 0800 * multiplier; acmpVIL = 0800 * multiplier; break;
            case "10000": acmpVIH = 0850 * multiplier; acmpVIL = 0850 * multiplier; break;
            case "10001": acmpVIH = 0900 * multiplier; acmpVIL = 0900 * multiplier; break;
            case "10010": acmpVIH = 0950 * multiplier; acmpVIL = 0950 * multiplier; break;
            case "10011": acmpVIH = 1000 * multiplier; acmpVIL = 1000 * multiplier; break;
            case "10100": acmpVIH = 1050 * multiplier; acmpVIL = 1050 * multiplier; break;
            case "10101": acmpVIH = 1100 * multiplier; acmpVIL = 1100 * multiplier; break;
            case "10110": acmpVIH = 1150 * multiplier; acmpVIL = 1150 * multiplier; break;
            case "10111": acmpVIH = 1200 * multiplier; acmpVIL = 1200 * multiplier; break;
            case "11000":
                acmpVIH = (int)Convert.ToDouble(g.GreenPAK.vdd.typ) * 1000 / 3;
                acmpVIL = (int)Convert.ToDouble(g.GreenPAK.vdd.typ) * 1000 / 3;
                break;

            case "11001":
                acmpVIH = (int)Convert.ToDouble(g.GreenPAK.vdd.typ) * 1000 / 4;
                acmpVIL = (int)Convert.ToDouble(g.GreenPAK.vdd.typ) * 1000 / 4;
                break;
                //case "11010": acmpVIH = EXT_VREF; break;
                //case "11011": acmpVIH = EXT_VREF; break;
                //case "11100": acmpVIH = EXT_VREF / 2; break;
                //case "11101": acmpVIH = EXT_VREF / 2; break;
        }

        if (g.GreenPAK.base_die.Equals("SLG50003"))
        {
            switch (g.nvmData[g.GreenPAK.acmp[i].HY + 2].ToString() +
                    g.nvmData[g.GreenPAK.acmp[i].HY + 1].ToString() +
                    g.nvmData[g.GreenPAK.acmp[i].HY + 0].ToString())
            {
                case "000":
                    hysteresis = 0;
                    break;

                case "001":
                    hysteresis = 25;
                    acmpVIH = (int)(acmpVIH + (12.5 * multiplier));
                    acmpVIL = (int)(acmpVIL - (12.5 * multiplier));
                    break;

                case "010":
                    hysteresis = 50;
                    acmpVIL = acmpVIL - (50 * multiplier);
                    break;

                case "011":
                    hysteresis = 200;
                    acmpVIL = acmpVIL - (200 * multiplier);
                    break;

                case "110":
                    hysteresis = 100;
                    acmpVIL = acmpVIL - (100 * multiplier);
                    break;

                case "111":
                    hysteresis = 150;
                    acmpVIL = acmpVIL - (150 * multiplier);
                    break;
            }
        }
        else
        {
            switch (g.nvmData[g.GreenPAK.acmp[i].HY + 1].ToString() +
                    g.nvmData[g.GreenPAK.acmp[i].HY + 0].ToString())
            {
                case "00":
                    hysteresis = 0;
                    break;

                case "01":
                    hysteresis = 25;
                    acmpVIH = (int)(acmpVIH + (12.5 * multiplier));
                    acmpVIL = (int)(acmpVIL - (12.5 * multiplier));
                    break;

                case "10":
                    hysteresis = 50;
                    acmpVIL = acmpVIL - (50 * multiplier);
                    break;

                case "11":
                    hysteresis = 200;
                    acmpVIL = acmpVIL - (200 * multiplier);
                    break;
            }
        }
        if (acmpVIL < 0)
        {
            acmpVIL = 0;
        }

        g.GreenPAK.acmp[i].low_bw = low_bw;
        g.GreenPAK.acmp[i].hysteresis = hysteresis.ToString();
        g.GreenPAK.acmp[i].acmpVIH = acmpVIH.ToString();
        g.GreenPAK.acmp[i].acmpVIL = acmpVIL.ToString();
    }

    private static void counter_config(int i)
    {
        // Uses GreenPAK_Library.cs and nvm to store counter information in a struct.
        // Later this information will be written to the EC table.

        double freq = 0;
        double time = 0;
        string mode = null;
        string mode_alt = "dly";

        switch (g.GreenPAK.PAK_family)
        {
            case 5:
                switch (g.nvmData[g.GreenPAK.cnt[i].MD + 1].ToString() +
                        g.nvmData[g.GreenPAK.cnt[i].MD + 0].ToString())
                {
                    case "00": mode = "Delay"; break;
                    case "01": mode = "One-Shot"; break;
                    case "10": mode = "Frequency Detector"; break;
                    case "11": mode = "Counter"; mode_alt = "cnt"; break;
                }

                switch (g.nvmData[g.GreenPAK.cnt[i].CK + 2].ToString() +
                        g.nvmData[g.GreenPAK.cnt[i].CK + 1].ToString() +
                        g.nvmData[g.GreenPAK.cnt[i].CK + 0].ToString())
                {
                    case "000": freq = g.GreenPAK.RC_osc_freq; break;
                    case "001": freq = g.GreenPAK.RC_osc_freq / 4; break;
                    case "010": freq = g.GreenPAK.RC_osc_freq / 12; break;
                    case "011": freq = g.GreenPAK.RC_osc_freq / 24; break;
                    case "100": freq = g.GreenPAK.RC_osc_freq / 64; break;
                    case "101":
                        if (g.GreenPAK.RING_osc_freq > 0)
                        {
                            freq = g.GreenPAK.RING_osc_freq; break;
                        }
                        else if (g.GreenPAK.LF_osc_freq > 0)
                        {
                            freq = g.GreenPAK.LF_osc_freq; break;
                        }
                        else break;
                    case "110": freq = -1; break;
                    case "111": freq = -1; g.GreenPAK.cnt[i].used = false; return;
                }
                break;

            case 4:
                switch (g.nvmData[g.GreenPAK.cnt[i].MD].ToString())
                {
                    case "0": mode = "Delay"; break;
                    case "1": mode = "Counter"; mode_alt = "cnt"; break;
                }

                if (g.GreenPAK.cnt[i].CS.Equals(4))
                {
                    switch (g.nvmData[g.GreenPAK.cnt[i].CK + 3].ToString() +
                            g.nvmData[g.GreenPAK.cnt[i].CK + 2].ToString() +
                            g.nvmData[g.GreenPAK.cnt[i].CK + 1].ToString() +
                            g.nvmData[g.GreenPAK.cnt[i].CK + 0].ToString())
                    {
                        case "0000": freq = g.GreenPAK.RC_osc_freq / 1; break;
                        case "0001": freq = g.GreenPAK.RC_osc_freq / 4; break;
                        case "0010": freq = g.GreenPAK.RC_osc_freq / 12; break;
                        case "0011": freq = g.GreenPAK.RC_osc_freq / 24; break;
                        case "0100": freq = g.GreenPAK.RC_osc_freq / 64; break;
                        case "0101": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // DLY_out
                        case "0110": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // matrix_out
                        case "0111": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // matrix_out / 8
                        case "1000": freq = g.GreenPAK.RING_osc_freq; break;
                        case "1001": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // matrix_out
                        case "1010": freq = g.GreenPAK.LF_osc_freq; break;
                        case "1011": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // ??
                        case "1100": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // ??
                    }
                }
                else if (g.GreenPAK.cnt[i].CS.Equals(3))
                {
                    switch (g.nvmData[g.GreenPAK.cnt[i].CK + 2].ToString() +
                            g.nvmData[g.GreenPAK.cnt[i].CK + 1].ToString() +
                            g.nvmData[g.GreenPAK.cnt[i].CK + 0].ToString())
                    {
                        case "000": freq = g.GreenPAK.RC_osc_freq / 1; break;
                        case "001": freq = g.GreenPAK.RC_osc_freq / 4; break;
                        case "010": freq = g.GreenPAK.RC_osc_freq / 24; break;
                        case "011": freq = g.GreenPAK.RC_osc_freq / 64; break;
                        case "100": freq = g.GreenPAK.LF_osc_freq; break;
                        case "101": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // DLY_out
                        case "110": freq = g.GreenPAK.RING_osc_freq; break;
                        case "111": freq = -1; g.GreenPAK.cnt[i].used = false; return;     // matrix_out
                    }
                }
                break;

            case 3:
                switch (g.nvmData[g.GreenPAK.cnt[i].MD].ToString())
                {
                    case "0": mode = "Delay"; break;
                    case "1": mode = "Counter"; mode_alt = "cnt"; break;
                }

                switch (g.nvmData[g.GreenPAK.cnt[i].CK + 2].ToString() +
                        g.nvmData[g.GreenPAK.cnt[i].CK + 1].ToString() +
                        g.nvmData[g.GreenPAK.cnt[i].CK + 0].ToString())
                {
                    case "000": freq = g.GreenPAK.RC_osc_freq / 1; break;
                    case "001": freq = g.GreenPAK.RC_osc_freq / 4; break;
                    case "010": freq = g.GreenPAK.RC_osc_freq / 12; break;
                    case "011": freq = g.GreenPAK.RC_osc_freq / 24; break;
                    case "100": freq = g.GreenPAK.RC_osc_freq / 64; break;
                    case "101": freq = -1; g.GreenPAK.cnt[i].used = false; return;       // External Clock
                    case "110": freq = -1; g.GreenPAK.cnt[i].used = false; return;       // External Clock / 8
                    case "111": freq = -1; g.GreenPAK.cnt[i].used = false; return;       // CounterX Overflow
                }
                break;

            default:
                break;
        }

        string bin = Reverse(g.nvmData.Substring(g.GreenPAK.cnt[i].DA, g.GreenPAK.cnt[i].LN));

        int counter_data = Convert.ToInt32(bin, 2);

        if (mode.Equals("Counter"))        // ### Build in support for min/max values?
        {
            time = ((counter_data + 1) * (1 / freq));
        }
        else
        {
            time = ((counter_data + 2) * (1 / freq));
        }

        // SI Prefixes
        if (time < 0.001) { time = time * 1000000; g.GreenPAK.cnt[i].timeSI = "µs"; }
        else if (time < 1) { time = time * 1000; g.GreenPAK.cnt[i].timeSI = "ms"; }
        else { g.GreenPAK.cnt[i].timeSI = "s"; }

        g.GreenPAK.cnt[i].mode = mode;
        g.GreenPAK.cnt[i].mode_alt = mode_alt;
        g.GreenPAK.cnt[i].time.min = "--";        // ### Build in support for min/max values?
        if (freq < 0)
        {
            g.GreenPAK.cnt[i].time.typ = "Ext Clk";
            g.GreenPAK.cnt[i].timeSI = "s";
        }
        else
        {
            g.GreenPAK.cnt[i].time.typ = Math.Round(time, 3).ToString();
        }
        g.GreenPAK.cnt[i].time.max = "--";        // ### Build in support for min/max values?
    }

    private static string HexStringToBinary(string hex)
    {
        // Converts a hex string into a binary value.

        string result = null;
        foreach (char c in hex)
        {
            result = result + hexCharacterToBinary[char.ToLower(c)];
        }
        return result.ToString();
    }

    private static string Reverse(string s)
    {
        // Reverses an array of chars.

        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static void updateFields()
    {
        // Updates fields in the Word document, including headers and footers.

        foreach (Section section in g.doc.Sections)
        {
            g.doc.Fields.Update();  // update each section

            HeadersFooters headers = section.Headers;
            foreach (HeaderFooter header in headers)
            {
                Fields fields = header.Range.Fields;
                foreach (Field field in fields)
                {
                    field.Update();
                }
            }

            HeadersFooters footers = section.Footers;
            foreach (HeaderFooter footer in footers)
            {
                Fields fields = footer.Range.Fields;
                foreach (Field field in fields)
                {
                    field.Update();
                }
            }
        }
    }

    private static void EC_clearSection()
    {
        // Merges together all the rows in the EC table with the symbolRow so that the only row left
        // is the symbolRow.

        while (g.row < g.table.Rows.Count)
        {
            try
            {
                if (g.table.Cell(g.row + 1, 1).Range.Text != "")
                {
                    return;
                }
            }
            catch
            {
                g.table.Cell(g.row, 3).Merge(g.table.Cell(g.row + 1, 3));
                g.table.Cell(g.row, 4).Merge(g.table.Cell(g.row + 1, 4));
                g.table.Cell(g.row, 5).Merge(g.table.Cell(g.row + 1, 5));
                g.table.Cell(g.row, 6).Merge(g.table.Cell(g.row + 1, 6));
            }
        }
    }

    private static int EC_row_symbol(string symbol)
    {
        // Searches the EC table for the symbol. If the symbol is present, return its row index.
        // If the symbol is not already present, add it.

        for (int i = 1; i <= g.table.Rows.Count; i++)
        {
            // if the cell doesn't exist skip it
            try
            {
                if (g.table.Cell(i, 1).Range.Text.Contains(symbol))
                {
                    return i;
                }
            }
            catch { Console.WriteLine("Error occured in EC_row_symbol()"); }
        }
        int new_row;
        if (symbol.Equals("VDD2"))
        {
            g.table.Rows.Add(g.table.Rows[3]);
            new_row = 3;
        }
        else
        {
            g.table.Rows.Add();
            new_row = g.table.Rows.Count;
        }
        Range range = g.table.Cell(new_row, 1).Range;
        range.Text = symbol;
        range.SetRange(range.Start + 1, range.End);
        range.Select();
        range.Font.Subscript = 1;
        return new_row;
    }

    private static void EC_row_populate(Table table, int row, string param, string min, string typ, string max)
    {
        // Populates the EC table with four string values.

        table.Cell(row, 3).Range.Text = param;
        table.Cell(row, 4).Range.Text = min;
        table.Cell(row, 5).Range.Text = typ;
        table.Cell(row, 6).Range.Text = max;
    }

    private static void EC_row_split()
    {
        // Splits the current row in two by adding a row directly underneath the row's index, then
        // increments the row index.

        //Console.WriteLine("row count1 = " + g.table.Rows.Count);
        try
        {
            g.table.Rows[g.row].Range.Cells.Split(2, 1);
        }
        catch { Console.WriteLine("Error occured in EC_row_split()"); }
        //Console.WriteLine("row count2 = " + g.table.Rows.Count);

        //for (int i = 1; i <= g.table.Columns.Count; i++)
        //{
        //    g.table.Cell(g.row, i).Split(2, 1);
        //}
        g.row++;
    }

    private static void EC_row_merge(int symbolRow)
    {
        // Unused right now.

        //g.table.Cell(g.row, 1).Merge(g.table.Cell(symbolRow, 1));
        //g.table.Cell(g.row, 2).Merge(g.table.Cell(symbolRow, 2));
        //g.table.Cell(g.row, 3).Merge(g.table.Cell(g.row - 1, 3));
        //g.table.Cell(g.row, 4).Merge(g.table.Cell(g.row - 1, 4));
        //g.table.Cell(g.row, 5).Merge(g.table.Cell(g.row - 1, 5));
        //g.table.Cell(g.row, 6).Merge(g.table.Cell(g.row - 1, 6));
        //g.table.Cell(g.row, 7).Merge(g.table.Cell(symbolRow, 7));
    }

    private static void EC_cleanup1()
    {
        // After the EC table is populated, merge all empty cells with the cell above to remove blanks.

        for (int i = 1; i < g.table.Rows.Count; i++)
        {
            try
            {
                if (g.table.Cell(i, 5).Range.Text.Length < 3)
                {
                    Console.WriteLine("blank line: " + i.ToString());
                    g.table.Rows[i].Delete();
                }
            }
            catch { Console.WriteLine("Error occured in EC_cleanup1()"); }
        }
    }

    private static void EC_cleanup2()
    {
        foreach (Cell c in g.table.Range.Cells)
        {
            try
            {
                if (c.Range.Text.Length >= 3 &&
                    g.table.Cell(c.RowIndex + 1, c.ColumnIndex).Range.Text.Length < 3)
                {
                    Console.WriteLine(c.RowIndex.ToString() + ", " + c.ColumnIndex.ToString() + ", " + c.Range.Text);
                    int i = 1;

                    while (g.table.Cell(c.RowIndex + i, c.ColumnIndex).Range.Text.Length < 3
                        && i <= g.table.Rows.Count)
                    {
                        i++;
                    }
                    Console.WriteLine((i - 1).ToString());
                    c.Merge(g.table.Cell(c.RowIndex + i - 1, c.ColumnIndex));
                }
            }
            catch { Console.WriteLine("Error occured in EC_cleanup2()"); }
        }
    }

    private static void EC_cleanup3()
    {
        Console.WriteLine("setting row height");
        for (int i = 0; i < g.table.Rows.Count; i++)
        {
            try
            {
                Console.WriteLine(i.ToString());
                g.table.Cell(i, 5).SetHeight((float)0.1, WdRowHeightRule.wdRowHeightAtLeast);
            }
            catch { Console.WriteLine("Error occured in EC_cleanup3()"); }
        }
    }

    private static void EC_IH_IL(string symbol, bool[] VDD_EC, g.pin_IO_settings VDD_IOs)
    {
        // Query the Access Database and Write VIH/VIL settings to the EC table.

        int symbolRow = EC_row_symbol(symbol);
        g.subSymbol = symbol.Substring(0, 3);
        g.row = symbolRow;
        EC_clearSection();

        if (symbol.StartsWith("VIH"))
        {
            g.table.Cell(g.row, 2).Range.Text = "HIGH-Level Output Voltage";
            g.table.Cell(g.row, 7).Range.Text = "V";
        }
        if (symbol.StartsWith("VIL"))
        {
            g.table.Cell(g.row, 2).Range.Text = "LOW-Level Output Voltage";
            g.table.Cell(g.row, 7).Range.Text = "V";
        }

        if (g.GreenPAK.dual_supply_PAK && !symbol.Contains("2"))
        {
            g.table.Cell(g.row, 2).Range.Text += g.GreenPAK.dual_supply_vdd_pins;
        }
        else if (g.GreenPAK.dual_supply_PAK && symbol.Contains("2"))
        {
            g.table.Cell(g.row, 2).Range.Text += g.GreenPAK.dual_supply_vdd2_pins;
        }
        g.table.Cell(g.row, 7).Range.Text = "V";

        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0: g.VDD_DB = "1_8"; g.extraInfo = " at VDD = 1.8v"; break;
                case 1: g.VDD_DB = "3_3"; g.extraInfo = " at VDD = 3.3v"; break;
                case 2: g.VDD_DB = "5_0"; g.extraInfo = " at VDD = 5.0v"; break;
            }

            if (VDD_IOs.woSchmitt)
            {
                queryAndWrite("Logic Input", "woSchmitt");
                EC_row_split();
            }
            if (VDD_IOs.wSchmitt)
            {
                queryAndWrite("Logic Input with Schmitt Trigger", "wSchmitt");
                EC_row_split();
            }
            if (VDD_IOs.LVDI)
            {
                queryAndWrite("LVDI Logic Input", "LVDI");
                EC_row_split();
            }
        }
        EC_row_merge(symbolRow);
    }

    private static void EC_OH_OL(string symbol, bool[] VDD_EC, g.pin_IO_settings VDD_IOs)
    {
        // Query the Access Database, and write VOH/VOL/IOH/IOL settings to the EC table.

        string highlow = null;

        int symbolRow = EC_row_symbol(symbol);
        g.subSymbol = symbol.Substring(0, 3);
        g.row = symbolRow;
        EC_clearSection();

        if (symbol.StartsWith("VOH"))
        {
            g.table.Cell(g.row, 2).Range.Text = "HIGH-Level Output Voltage";
            g.table.Cell(g.row, 7).Range.Text = "V";
        }
        else if (symbol.StartsWith("VOL"))
        {
            g.table.Cell(g.row, 2).Range.Text = "LOW-Level Output Voltage";
            g.table.Cell(g.row, 7).Range.Text = "V";
        }
        else if (symbol.StartsWith("IOH"))
        {
            g.table.Cell(g.row, 2).Range.Text = "HIGH-Level Output Current";
            g.table.Cell(g.row, 7).Range.Text = "mA";
        }
        else if (symbol.StartsWith("IOL"))
        {
            g.table.Cell(g.row, 2).Range.Text = "LOW-Level Output Current";
            g.table.Cell(g.row, 7).Range.Text = "mA";
        }

        if (g.GreenPAK.dual_supply_PAK && !symbol.Contains("2"))
        {
            g.table.Cell(g.row, 2).Range.Text += g.GreenPAK.dual_supply_vdd_pins;
        }
        else if (g.GreenPAK.dual_supply_PAK && symbol.Contains("2"))
        {
            g.table.Cell(g.row, 2).Range.Text += g.GreenPAK.dual_supply_vdd2_pins;
        }

        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0: g.VDD_DB = "1_8"; g.extraInfo = " at VDD = 1.8v"; break;
                case 1: g.VDD_DB = "3_3"; g.extraInfo = " at VDD = 3.3v"; break;
                case 2: g.VDD_DB = "5_0"; g.extraInfo = " at VDD = 5.0v"; break;
            }

            if (symbol.StartsWith("VOH"))
            {
                highlow = "HIGH";
                switch (g.VDD_DB)
                {
                    case "1_8": g.extraInfo += ", IOH = 100µA"; break;
                    case "3_3": g.extraInfo += ", IOH = 3mA"; break;
                    case "5_0": g.extraInfo += ", IOH = 5mA"; break;
                }
            }
            else if (symbol.StartsWith("VOL"))
            {
                highlow = "LOW";
                switch (g.VDD_DB)
                {
                    case "1_8": g.extraInfo += ", IOL = 100µA"; break;
                    case "3_3": g.extraInfo += ", IOL = 3mA"; break;
                    case "5_0": g.extraInfo += ", IOL = 5mA"; break;
                }
            }
            else if (symbol.StartsWith("IOH"))
            {
                highlow = "HIGH";
                switch (g.VDD_DB)
                {
                    case "1_8": g.extraInfo += ", VOH = VDD - 0.2v"; break;
                    case "3_3": g.extraInfo += ", VOH = 2.4v"; break;
                    case "5_0": g.extraInfo += ", VOH = 2.4v"; break;
                }
            }
            else if (symbol.StartsWith("IOL"))
            {
                highlow = "LOW";
                switch (g.VDD_DB)
                {
                    case "1_8": g.extraInfo += ", VOL = 0.15v"; break;
                    case "3_3": g.extraInfo += ", VOL = 0.4v"; break;
                    case "5_0": g.extraInfo += ", VOL = 0.4v"; break;
                }
            }

            if (highlow.Equals("HIGH"))
            {
                if (VDD_IOs.PP1x > 0 || VDD_IOs.ODP1x > 0)
                {
                    queryAndWrite("Push Pull & PMOS OD 1x Driver", "PP1x");
                    EC_row_split();
                }
                if (VDD_IOs.PP2x > 0 || VDD_IOs.ODP2x > 0)
                {
                    queryAndWrite("Push Pull & PMOS OD 2x Driver", "PP2x");
                    EC_row_split();
                }
            }
            else if (highlow.Equals("LOW"))
            {
                if (VDD_IOs.PP1x > 0)
                {
                    queryAndWrite("Push Pull 1x Driver", "PP1x");
                    EC_row_split();
                }
                if (VDD_IOs.PP2x > 0)
                {
                    queryAndWrite("Push Pull 2x Driver", "PP2x");
                    EC_row_split();
                }
                if (VDD_IOs.ODN1x > 0)
                {
                    queryAndWrite("Open Drain 1x Driver", "ODN1x");
                    EC_row_split();
                }
                if (VDD_IOs.ODN2x > 0)
                {
                    queryAndWrite("Open Drain 2x Driver", "ODN2x");
                    EC_row_split();
                }
                if (VDD_IOs.ODN4x > 0)
                {
                    queryAndWrite("Open Drain 4x Driver", "ODN4x");
                    EC_row_split();
                }
            }
        }
        EC_row_merge(symbolRow);
    }

    private static g.symbols[] symbolArray;

    private static void EC_subscripts2()
    {
        symbolArray = new g.symbols[]
        {
            new g.symbols() { sym = "V", sub = "DD" },
            new g.symbols() { sym = "V", sub = "DD2" },
            new g.symbols() { sym = "V", sub = "OH" },
            new g.symbols() { sym = "V", sub = "OH2" },
            new g.symbols() { sym = "V", sub = "OL" },
            new g.symbols() { sym = "V", sub = "OL2" },
            new g.symbols() { sym = "I", sub = "OH" },
            new g.symbols() { sym = "I", sub = "OH2" },
            new g.symbols() { sym = "I", sub = "OL" },
            new g.symbols() { sym = "I", sub = "OL2" },
            new g.symbols() { sym = "PON", sub = "THR" },
            new g.symbols() { sym = "POFF", sub = "THR" },
        };

        foreach (Cell cell in g.table.Range.Cells)
        {
            for (int i = 0; i < symbolArray.Length; i++)
            {
                if (cell.Range.Text.Contains(symbolArray[i].sym + symbolArray[i].sub))
                {
                    Console.WriteLine(cell.Range.Text);
                    Console.WriteLine("Found " + symbolArray[i].sym + symbolArray[i].sub + " in cell " + cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString());

                    Range r = cell.Range;

                    r.SetRange(r.Start + r.Text.IndexOf(symbolArray[i].sym),
                               r.Start + r.Text.IndexOf(symbolArray[i].sym) + symbolArray[i].sym.Length);
                    r.Select();
                    r.Font.Subscript = 0;

                    r = cell.Range;
                    r.SetRange(r.Start + r.Text.IndexOf(symbolArray[i].sub),
                               r.Start + r.Text.IndexOf(symbolArray[i].sub) + symbolArray[i].sub.Length);
                    r.Select();
                    r.Font.Subscript = 1;
                }
            }
        }
    }

    private static void EC_subscripts(string symbol, string subscript)
    {
        // Make sure that all the symbols in the EC table have the proper subscripts.

        if (subscript.EndsWith("2"))
        {
            return;
        }

        foreach (Cell cell in g.table.Range.Cells)
        {
            if (cell.Range.Text.Contains(symbol + subscript))
            {
                Console.WriteLine(cell.Range.Text);
                Console.WriteLine("Found " + symbol + subscript + " in cell " + cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString());

                Range r = cell.Range;

                r.SetRange(r.Start + r.Text.IndexOf(symbol),
                           r.Start + r.Text.IndexOf(symbol) + symbol.Length);
                r.Select();
                r.Font.Subscript = 0;

                r = cell.Range;
                r.SetRange(r.Start + r.Text.IndexOf(subscript),
                           r.Start + r.Text.IndexOf(subscript) + subscript.Length);
                r.Select();
                r.Font.Subscript = 1;
            }
        }
    }

    private static void saveFileAndOpen()
    {
        // Updates all the fields in the Word document, saves the file to the proper location
        // with the proper filename, closes and quits word, then opens the new DS file.

        try
        {
            updateFields();
            //string path = g.DataSheet_File.Substring(0, g.DataSheet_File.LastIndexOf('\\') + 1);
            Console.WriteLine("\n\n" + g.path + g.part_number + "_DS_r" + g.DS_rev + "_" + DateTime.Now.ToString("MMddyyyy") + ".docx");
            g.doc.SaveAs2(g.path + g.part_number + "_DS_r" + g.DS_rev + "_" + DateTime.Now.ToString("MMddyyyy") + ".docx");

            g.doc.Close();
            g.app.Quit();

            Process.Start(g.path + g.part_number + "_DS_r" + g.DS_rev + "_" + DateTime.Now.ToString("MMddyyyy") + ".docx");

            return;
        }
        catch
        {
            Console.WriteLine("Error occured in saveFileAndOpen()");
            MessageBox.Show("Error: Could not save DataSheet file!\nPlease close the DataSheet file then click \"OK\".");
            saveFileAndOpen();
        }
    }

    private static void InlineShapeReplace(string title)
    {
        // Replaces the top marking, package size, and Tape and Reel pictures at the end of the DS.

        foreach (InlineShape shape in g.doc.InlineShapes)
        {
            if (shape.Title.Equals(title))
            {
                Range range = shape.Range;
                shape.Delete();
                InlineShape newShape;

                Console.WriteLine(g.templatePath + @"Resources\" + (g.templatePath + @"Resources\" + g.GreenPAK.package + "_" + title + ".png"));

                try
                {
                    newShape = range.InlineShapes.AddPicture(g.templatePath + @"Resources\" + g.GreenPAK.package + "_" + title + ".png");
                    newShape.Title = title;
                }
                catch
                {
                    Console.WriteLine("Error occured in InlineShapeReplace()");
                    MessageBox.Show("Could not find " + g.templatePath + @"Resources\" + g.GreenPAK.package + "_" + title + ".png");
                }
                return;
            }
        }
    }

    //### Maybe make a more succint version that queries min/typ/max all at once?
    private static string accessQuery(string returnField, string param1, string param2 = "-1", string param3 = "-1")
    {
        // Returns a single value from the Access Database based on the string parameters.

        string returnValue = null;
        //g.connection.Open();
        OleDbCommand command = new OleDbCommand();
        command.Connection = g.connection;
        command.CommandText = "SELECT * FROM " + g.GreenPAK.base_die + " WHERE param1 = '" + param1 + "'";
        if (param2 != "-1" && param2 != null) { command.CommandText += " AND param2 = '" + param2 + "'"; }
        if (param3 != "-1" && param3 != null) { command.CommandText += " AND param3 = '" + param3 + "'"; }

        try
        {
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                returnValue = reader[returnField].ToString();
                //Console.WriteLine(returnValue);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured in accessQuery()");
            MessageBox.Show("Error " + ex);
        }

        //g.connection.Close();
        return returnValue;
    }

    private static void queryAndWrite(string note, string param2 = "-1")
    {
        // Queries the Access Database for min/typ/max, then writes the returned values to the EC table.

        //g.connection.Open();
        OleDbCommand command = new OleDbCommand();
        command.Connection = g.connection;
        command.CommandText = "SELECT * FROM " + g.GreenPAK.base_die + " WHERE param1 = '" + g.subSymbol + "'";
        if (param2 != "-1" && param2 != null) { command.CommandText += " AND param2 = '" + param2 + "'"; }

        g.table.Cell(g.row, 3).Range.Text = note + g.extraInfo;

        try
        {
            OleDbDataReader reader = command.ExecuteReader();

            int i = 4;
            while (reader.Read())
            {
                Console.WriteLine(reader[g.VDD_DB].ToString());
                g.table.Cell(g.row, i).Range.Text = reader[g.VDD_DB].ToString();

                i++;
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured in queryAndWrite()");
            MessageBox.Show("Error " + ex);
        }

        //g.connection.Close();
    }

    public static void theProgram(BackgroundWorker worker, DoWorkEventArgs e)
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Reinitialize variables
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        g.VDD.PP1x = 0;
        g.VDD.PP2x = 0;
        g.VDD.PP4x = 0;
        g.VDD.ODN1x = 0;
        g.VDD.ODN2x = 0;
        g.VDD.ODN4x = 0;
        g.VDD.ODP1x = 0;
        g.VDD.ODP2x = 0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Load the GP and DS files that were selected in the gui. Populate the Date, I_Q, and DS_rev.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        var form = Form.ActiveForm as SilegoForm.SilegoForm;
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Loading files...");

        object oMissing = System.Reflection.Missing.Value;

        try
        {
            g.ELEMENT = XElement.Load(g.GreenPAK_File);
        }
        catch
        {
            Console.WriteLine("Error occured in theProgram: could not load GreenPAK file");
            form.backgroundWorker.ReportProgress(0, "Error: Could not load GreenPAK file!");
            e.Cancel = true;
            return;
        }

        try
        {
            g.app = new Microsoft.Office.Interop.Word.Application();
            if (g.new_part_update)
            {
                g.doc = g.app.Documents.Add(@"P:\Apps_Tools\New_DS_Template\New_DS_Template.docx");
            }
            else
            {
                g.doc = g.app.Documents.Add(g.DataSheet_File);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured in theProgram: could not load Datasheet file");
            MessageBox.Show(ex.ToString());
            form.backgroundWorker.ReportProgress(0, "Error: Could not load DataSheet file!");
            e.Cancel = true;
            return;
        }

        try
        {
            g.path = g.GreenPAK_File.Substring(0, g.GreenPAK_File.LastIndexOf("\\Program Files"));
            Console.WriteLine(g.path);
        }
        catch
        {
            form.backgroundWorker.ReportProgress(0, "Error: Support Case directory structure is not there!");
            e.Cancel = true;
            return;
        }
        g.path += @"\Datasheet Files\";

        Console.WriteLine(g.path);

        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Getting Date / I_Q / DS_rev");

        g.doc.Variables["Date"].Value = DateTime.Now.ToString("MM/dd/yyyy");
        g.doc.Variables["Date_long"].Value = DateTime.Now.ToString("MMMM dd, yyyy");

        if (g.I_Q_update)
        {
            g.doc.Variables["I_Q"].Value = g.I_Q;
        }

        g.doc.Variables["DS_rev"].Value = g.DS_rev;
        g.doc.Variables["DS_rev_alt"].Value = g.DS_rev.Insert(1, ".");

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Determine what chip this is for and assign to g.GreenPAK
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Creating PAKs");

        // GreenPAK5
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "05"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "1": PAK.createSLG46531(); g.GreenPAK = PAKs.SLG46531; break;
                case "2": PAK.createSLG46532(); g.GreenPAK = PAKs.SLG46532; break;
                case "3": PAK.createSLG46533(); g.GreenPAK = PAKs.SLG46533; break;
                case "4": PAK.createSLG46534(); g.GreenPAK = PAKs.SLG46534; break;
                case "5": PAK.createSLG46535(); g.GreenPAK = PAKs.SLG46535; break;
                case "6": PAK.createSLG46536(); g.GreenPAK = PAKs.SLG46536; break;
                case "7": PAK.createSLG50003(); g.GreenPAK = PAKs.SLG50003; break;
                case "10": PAK.createSLG46533M(); g.GreenPAK = PAKs.SLG46533M; break;
                case "11": PAK.createSLG46537(); g.GreenPAK = PAKs.SLG46537; break;
                case "12": PAK.createSLG46538(); g.GreenPAK = PAKs.SLG46538; break;
                case "13": PAK.createSLG46537M(); g.GreenPAK = PAKs.SLG46537M; break;
                case "14": PAK.createSLG46538M(); g.GreenPAK = PAKs.SLG46538M; break;
                default: break;
            }
        }
        // GreenPAK4
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "04"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "1": PAK.createSLG46140(); g.GreenPAK = PAKs.SLG46140; break;
                case "2": PAK.createSLG46620(); g.GreenPAK = PAKs.SLG46620; break;
                case "6": PAK.createSLG46621(); g.GreenPAK = PAKs.SLG46621; break;
                default: break;
            }
        }
        // GreenPAK3
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "03"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "2": PAK.createSLG46721(); g.GreenPAK = PAKs.SLG46721; break;
                case "3": PAK.createSLG46722(); g.GreenPAK = PAKs.SLG46722; break;
                case "4": PAK.createSLG46110(); g.GreenPAK = PAKs.SLG46110; break;
                case "5": PAK.createSLG46120(); g.GreenPAK = PAKs.SLG46120; break;
                case "6": PAK.createSLG46116(); g.GreenPAK = PAKs.SLG46116; break;
                case "7": PAK.createSLG46117(); g.GreenPAK = PAKs.SLG46117; break;
                case "11": PAK.createSLG46121(); g.GreenPAK = PAKs.SLG46121; break;
                case "12": PAK.createSLG46108(); g.GreenPAK = PAKs.SLG46108; break;
                case "13": PAK.createSLG46169(); g.GreenPAK = PAKs.SLG46169; break;
                case "14": PAK.createSLG46170(); g.GreenPAK = PAKs.SLG46170; break;
                default: break;
            }
        }

        g.doc.Variables["GreenPAK_Base_Die"].Value = g.GreenPAK.base_die;
        if (g.GreenPAK.package.Contains("_"))
        {
            g.doc.Variables["GreenPAK_Package"].Value = g.GreenPAK.package.Substring(0, g.GreenPAK.package.IndexOf("_"));
        }
        else
        {
            g.doc.Variables["GreenPAK_Package"].Value = g.GreenPAK.package;
        }
        g.doc.Variables["GreenPAK_Package_alt"].Value = g.GreenPAK.package.Substring(0, g.GreenPAK.package.IndexOf("-"));
        g.doc.Variables["GreenPAK_Package_size"].Value = g.GreenPAK.package_size;
        g.doc.Variables["GreenPAK_Family"].Value = g.GreenPAK.PAK_family.ToString();
        g.doc.Variables["GreenPAK_Pin_Count"].Value = (g.GreenPAK.pin.Length - 1).ToString();

        Console.WriteLine(g.GreenPAK.base_die);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Grab NVM, store in g.nvmData
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Loading NVM");

        foreach (XElement xEle in g.ELEMENT.Descendants("nvmData"))
        {
            string nvmData1 = (string)xEle;
            string[] nvmData2 = nvmData1.Split(' ');

            for (int i = 0; i < nvmData2.Length; i++)
            {
                if (nvmData2[i].Length.Equals(1))
                {
                    nvmData2[i] = "0" + nvmData2[i];
                }
                g.nvmData = g.nvmData + Reverse(HexStringToBinary(nvmData2[i]));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  VDD / Temp Specs
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Grabbing VDD / Temp specs");

        // VDD specs
        foreach (XElement xEle in g.ELEMENT.Descendants("vddSpecs"))
        {
            try
            {
                g.doc.Variables["vddMin"].Value = xEle.Attribute("vddMin").Value;
                g.doc.Variables["vddTyp"].Value = xEle.Attribute("vddTyp").Value;
                g.doc.Variables["vddMax"].Value = xEle.Attribute("vddMax").Value;
            }
            catch
            {
                Console.WriteLine("Error occured in theProgram: missing VDD Specs");
                e.Cancel = true;
                form.backgroundWorker.ReportProgress(2, "Error: Missing VDD Specs");
                return;
            }
            g.GreenPAK.vdd = new PAK.mTM();
            g.GreenPAK.vdd.min = xEle.Attribute("vddMin").Value;
            g.GreenPAK.vdd.typ = xEle.Attribute("vddTyp").Value;
            g.GreenPAK.vdd.max = xEle.Attribute("vddMax").Value;
        }

        // VDD2 specs
        if (g.GreenPAK.dual_supply_PAK)
        {
            foreach (XElement xEle in g.ELEMENT.Descendants("vdd2Specs"))
            {
                try
                {
                    g.doc.Variables["vdd2Min"].Value = xEle.Attribute("vdd2Min").Value;
                    g.doc.Variables["vdd2Typ"].Value = xEle.Attribute("vdd2Typ").Value;
                    g.doc.Variables["vdd2Max"].Value = xEle.Attribute("vdd2Max").Value;
                }
                catch
                {
                    Console.WriteLine("Error occured in theProgram: Missing VDD2 Specs");
                    e.Cancel = true;
                    form.backgroundWorker.ReportProgress(2, "Error: Missing VDD2 Specs");
                    return;
                }

                g.GreenPAK.vdd2 = new PAK.mTM();
                g.GreenPAK.vdd2.min = xEle.Attribute("vdd2Min").Value;
                g.GreenPAK.vdd2.typ = xEle.Attribute("vdd2Typ").Value;
                g.GreenPAK.vdd2.max = xEle.Attribute("vdd2Max").Value;
            }
        }

        // Temp specs
        foreach (XElement xEle in g.ELEMENT.Descendants("tempSpecs"))
        {
            try
            {
                g.doc.Variables["tempMin"].Value = xEle.Attribute("tempMin").Value;
                g.doc.Variables["tempTyp"].Value = xEle.Attribute("tempTyp").Value;
                g.doc.Variables["tempMax"].Value = xEle.Attribute("tempMax").Value;
            }
            catch
            {
                Console.WriteLine("Error occured in theProgram: Missing TEMP Specs");
                e.Cancel = true;
                form.backgroundWorker.ReportProgress(2, "Error: Missing TEMP Specs");
                return;
            }

            g.GreenPAK.temp = new PAK.mTM();
            g.GreenPAK.temp.min = xEle.Attribute("tempMin").Value;
            g.GreenPAK.temp.typ = xEle.Attribute("tempTyp").Value;
            g.GreenPAK.temp.max = xEle.Attribute("tempMax").Value;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Project Info
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Gathering Project Info");

        if (g.project_info_update)
        {
            // Customer Info (Name, project name, part number, version number)
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                 .Where(xEle => (string)xEle.Attribute("id") == "2"))
            {
                if (xEle.Attribute("text").Value.Length > 0)
                {
                    g.doc.Variables["Customer_Name"].Value = xEle.Attribute("text").Value;
                }
                else
                {
                    e.Cancel = true;
                    form.backgroundWorker.ReportProgress(0, "Error: Missing Customer Name");
                    return;
                }
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                 .Where(xEle => (string)xEle.Attribute("id") == "3"))
            {
                if (xEle.Attribute("text").Value.Length > 0)
                {
                    g.doc.Variables["Customer_Project_Name"].Value = xEle.Attribute("text").Value;
                }
                else
                {
                    e.Cancel = true;
                    form.backgroundWorker.ReportProgress(0, "Error: Missing Customer Project Name");
                    return;
                }
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                 .Where(xEle => (string)xEle.Attribute("id") == "4"))
            {
                if (xEle.Attribute("text").Value.Length > 0)
                {
                    g.doc.Variables["Customer_Part_Number"].Value = xEle.Attribute("text").Value;
                    g.part_number = xEle.Attribute("text").Value;
                }
                else
                {
                    e.Cancel = true;
                    form.backgroundWorker.ReportProgress(0, "Error: Missing Customer Part Number");
                    return;
                }
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                .Where(xEle => (string)xEle.Attribute("id") == "5"))
            {
                g.doc.Variables["Customer_Version_Number"].Value = xEle.Attribute("text").Value;
            }
        }
        else
        {
            g.part_number = g.DataSheet_File.Substring(g.DataSheet_File.LastIndexOf("\\") + 1);
            g.part_number = g.part_number.Substring(0, g.part_number.IndexOf("_"));
            Console.WriteLine("Part Number: " + g.part_number);
            g.doc.Variables["Customer_Part_Number"].Value = g.part_number;
        }

        // Pattern ID
        string pattern = Reverse(g.nvmData.Substring(g.GreenPAK.pattern_id_address, 8));
        g.doc.Variables["Pattern_ID"].Value = Convert.ToInt32(pattern, 2).ToString().PadLeft(3, '0');

        // Locked/Unlocked
        if (g.new_part_update)
        {
            foreach (Table table in g.doc.Tables)
            {
                if (table.Title == "lock_table")
                {
                    if (g.GreenPAK.PAK_family.Equals(5))
                    {
                        while (table.Rows.Count < 6)
                        {
                            table.Rows.Add();
                        }

                        table.Cell(1, 2).Range.Text = "Unlocked";
                        table.Cell(2, 2).Range.Text = "Locked for read, bits <0:1535> ";
                        table.Cell(3, 2).Range.Text = "Locked for write, bits <0:1535>";
                        table.Cell(4, 2).Range.Text = "Locked for write, bits <0:2047>";
                        table.Cell(5, 2).Range.Text = "Locked for read and write, bits <0:1535>";
                        table.Cell(6, 2).Range.Text = "Locked for read, bits <0:1535>, locked for write, bits <0:2047>";

                        break;
                    }
                    else if (g.GreenPAK.PAK_family.Equals(4) ||
                             g.GreenPAK.PAK_family.Equals(3))
                    {
                        foreach (Paragraph p in g.doc.Paragraphs)
                        {
                            if (p.Range.Text.StartsWith("Lock coverage "))
                            {
                                Console.WriteLine("Found lock coverage");
                                p.Range.Select();
                                g.app.Selection.Delete();
                                p.Range.Select();
                                g.app.Selection.Delete();
                                break;
                            }
                        }

                        table.Delete();
                        break;
                    }
                }
            }
        }

        if (g.lock_status_update)
        {
            string nvm_lock = null;

            if (g.GreenPAK.PAK_family.Equals(5))
            {
                foreach (Table table in g.doc.Tables)
                {
                    if (table.Title == "lock_table")
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            table.Cell(i, 1).Range.Text = "";
                        }

                        switch (g.nvmData[g.GreenPAK.lock_read].ToString() +
                                g.nvmData[g.GreenPAK.lock_write_0].ToString() +
                                g.nvmData[g.GreenPAK.lock_write_1].ToString())
                        {
                            case "000": nvm_lock = "U"; table.Cell(1, 1).Range.Text = "√"; break;
                            case "001": nvm_lock = "L"; table.Cell(4, 1).Range.Text = "√"; break;
                            case "010": nvm_lock = "L"; table.Cell(3, 1).Range.Text = "√"; break;
                            case "011": nvm_lock = "L"; table.Cell(4, 1).Range.Text = "√"; break;   // Superfluous
                            case "100": nvm_lock = "L"; table.Cell(2, 1).Range.Text = "√"; break;
                            case "101": nvm_lock = "L"; table.Cell(6, 1).Range.Text = "√"; break;
                            case "110": nvm_lock = "L"; table.Cell(5, 1).Range.Text = "√"; break;
                            case "111": nvm_lock = "L"; table.Cell(6, 1).Range.Text = "√"; break;   // Superfluous
                        }
                    }
                }
            }
            else if (g.GreenPAK.PAK_family.Equals(4) ||
                     g.GreenPAK.PAK_family.Equals(3))
            {
                switch (g.nvmData[g.GreenPAK.lock_status].ToString())
                {
                    case "0": nvm_lock = "U"; break;
                    case "1": nvm_lock = "L"; break;
                }
            }
            g.doc.Variables["NVM_lock"].Value = nvm_lock;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Pin Labels and Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Creating Pins");

        if (g.pin_labels_update || g.pin_settings_update)
        {
            for (int i = 1; i < g.GreenPAK.pin.Length; i++)
            {
                switch (g.GreenPAK.pin[i].PT)
                {
                    case "VDD":
                        g.GreenPAK.pin[i].name = "VDD";
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "PWR";
                        g.GreenPAK.pin[i].description = "Supply Voltage";
                        break;

                    case "GND":
                        g.GreenPAK.pin[i].name = "GND";
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "GND";
                        g.GreenPAK.pin[i].description = "Ground";
                        break;

                    case "VDD2":
                        g.GreenPAK.pin[i].name = "VDD2";
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "PWR";
                        g.GreenPAK.pin[i].description = "Supply Voltage";
                        break;

                    case "AGND":
                        g.GreenPAK.pin[i].name = "AGND";
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "AGND";
                        g.GreenPAK.pin[i].description = "Analog Ground";
                        break;

                    case "LDO_IN":
                        pin_config_name(i);
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "LDO Input";
                        g.GreenPAK.pin[i].description = "LDO Regulator Input";
                        break;

                    case "LDO_OUT":
                        pin_config_name(i);
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "LDO Output";
                        g.GreenPAK.pin[i].description = "LDO Regulator Output";
                        break;

                    case "PWR_SW_ON":
                        pin_config_name(i);
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "FET On";
                        g.GreenPAK.pin[i].description = "P-FET Power Switch On";
                        break;

                    case "VIN":
                        pin_config_name(i);
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "P-FET Input";
                        g.GreenPAK.pin[i].description = "P-FET Power Switch VIN";
                        break;

                    case "VOUT":
                        pin_config_name(i);
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "P-FET Output";
                        g.GreenPAK.pin[i].description = "P-FET Power Switch VOUT";
                        break;

                    case "NA":
                        g.GreenPAK.pin[i].name = "NC";
                        g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                        g.GreenPAK.pin[i].resistor = "--";
                        g.GreenPAK.pin[i].type = "--";
                        g.GreenPAK.pin[i].description = "Keep Floating or Connect to GND";
                        break;

                    default:
                        // Look for xml elements called "item" with caption "PIN _" and check if they have textLabel Element
                        foreach (XElement pin in g.ELEMENT.Descendants("item")
                            .Where(pin => pin.Attribute("caption").ToString().StartsWith("caption=\"PIN " + i.ToString())))
                        {
                            if (pin.Element("graphics").Attribute("hidden").Value.Equals("0") &&
                                TryGetElementValue(pin, "textLabel") == null)
                            {
                                form.backgroundWorker.ReportProgress(0, "Error: Pin" + i.ToString() + " is used but is not labeled!");
                                e.Cancel = true;
                                return;
                            }
                            if (pin.Element("graphics").Attribute("hidden").Value.Equals("1") &&
                                TryGetElementValue(pin, "textLabel") != null)
                            {
                                form.backgroundWorker.ReportProgress(0, "Error: Pin" + i.ToString() + " is labeled but is not used!");
                                e.Cancel = true;
                                return;
                            }

                            if (pin.Element("textLabel") != null)
                            {
                                pin_config(i);
                                g.GreenPAK.pin[i].name = pin.Element("textLabel").Value;
                                g.doc.Variables["pin" + i.ToString() + "_label"].Value = g.GreenPAK.pin[i].name;
                            }
                            else
                            {
                                g.doc.Variables["pin" + i.ToString() + "_label"].Value = "NC";
                                g.GreenPAK.pin[i].name = "NC";
                                g.GreenPAK.pin[i].type = "--";
                                g.GreenPAK.pin[i].description = "Keep Floating or Connect to GND";
                                g.GreenPAK.pin[i].resistor = "--";
                            }
                            break;
                        }
                        break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Output Summary
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Creating Output Summary");

        if (g.pin_settings_update)
        {
            string output_summary = ".";

            if (g.VDD.PP1x > 0 || g.VDD2.PP1x > 0) { output_summary += "\n\r" + (g.VDD.PP1x + g.VDD2.PP1x).ToString() + " Output \u2014 Push Pull 1x"; }
            if (g.VDD.PP2x > 0 || g.VDD2.PP2x > 0) { output_summary += "\n\r" + (g.VDD.PP2x + g.VDD2.PP2x).ToString() + " Output \u2014 Push Pull 2x"; }
            if (g.VDD.PP4x > 0 || g.VDD2.PP4x > 0) { output_summary += "\n\r" + (g.VDD.PP4x + g.VDD2.PP4x).ToString() + " Output \u2014 Push Pull 4x"; }
            if (g.VDD.ODN1x > 0 || g.VDD2.ODN1x > 0) { output_summary += "\n\r" + (g.VDD.ODN1x + g.VDD2.ODN1x).ToString() + " Output \u2014 Open Drain NMOS 1x"; }
            if (g.VDD.ODN2x > 0 || g.VDD2.ODN2x > 0) { output_summary += "\n\r" + (g.VDD.ODN2x + g.VDD2.ODN2x).ToString() + " Output \u2014 Open Drain NMOS 2x"; }
            if (g.VDD.ODN4x > 0 || g.VDD2.ODN4x > 0) { output_summary += "\n\r" + (g.VDD.ODN4x + g.VDD2.ODN4x).ToString() + " Output \u2014 Open Drain NMOS 4x"; }
            if (g.VDD.ODP1x > 0 || g.VDD2.ODP1x > 0) { output_summary += "\n\r" + (g.VDD.ODP1x + g.VDD2.ODP1x).ToString() + " Output \u2014 Open Drain PMOS 1x"; }
            if (g.VDD.ODP2x > 0 || g.VDD2.ODP2x > 0) { output_summary += "\n\r" + (g.VDD.ODP2x + g.VDD2.ODP2x).ToString() + " Output \u2014 Open Drain PMOS 2x"; }
            if (g.VDD.TRI1x > 0 || g.VDD2.TRI1x > 0) { output_summary += "\n\r" + (g.VDD.TRI1x + g.VDD2.TRI1x).ToString() + " Output \u2014 3-State Output 1x"; }
            if (g.VDD.TRI2x > 0 || g.VDD2.TRI2x > 0) { output_summary += "\n\r" + (g.VDD.TRI2x + g.VDD2.TRI2x).ToString() + " Output \u2014 3-State Output 2x"; }

            try { g.doc.Variables["output_summary"].Value = output_summary.Substring(3); }
            catch
            {
                Console.WriteLine("Error occured in theProgram: could not create output_summary");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Pin Configuration Table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating Pin Configuration Table");

        if (g.pin_labels_update || g.pin_settings_update)
        {
            foreach (Table table in g.doc.Tables)
            {
                if (table.Title == "pin_configuration")
                {
                    while (table.Rows.Count < g.GreenPAK.pin.Length)
                    {
                        table.Rows.Add();
                    }

                    // Assign values to each cell in the table
                    for (int i = 1; i < g.GreenPAK.pin.Length; i++)
                    {
                        table.Cell(i + 1, 1).Range.Text = i.ToString();
                        table.Cell(i + 1, 2).Range.Text = g.GreenPAK.pin[i].name;
                        table.Cell(i + 1, 3).Range.Text = g.GreenPAK.pin[i].type;
                        table.Cell(i + 1, 4).Range.Text = g.GreenPAK.pin[i].description;
                        table.Cell(i + 1, 5).Range.Text = g.GreenPAK.pin[i].resistor;
                    }
                    table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitContent);
                    table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);
                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  First Page Pinout Diagram
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating First Page Pinout Diagram");

        if (g.new_part_update)
        {
            foreach (Shape shape in g.doc.Shapes)
            {
                if (shape.Title == "pinout_diagram")
                {
                    int left = (int)shape.Left;
                    int top = (int)shape.Top;
                    int width = (int)shape.Width;
                    int height = (int)shape.Height;

                    //Console.WriteLine("Left: " + left.ToString() + " Top: " + top.ToString());
                    //Console.ReadLine();
                    shape.Delete();

                    g.doc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToFirst);
                    //Range r = g.doc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, 1);

                    Shape newShape = g.doc.Shapes.AddPicture(g.templatePath + @"Resources\" + g.GreenPAK.package + ".png");
                    newShape.Title = "pinout_diagram";
                    newShape.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage;
                    newShape.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionPage;
                    newShape.Left = left;
                    newShape.Top = top;
                    newShape.Width = width;
                    newShape.Height = height;

                    break;
                }
            }

            switch (g.GreenPAK.package)
            {
                case "STQFN-20L":
                    pin_cell_config(01, 264, 222, 'L');
                    pin_cell_config(02, 264, 242, 'L');
                    pin_cell_config(03, 264, 262, 'L');
                    pin_cell_config(04, 264, 282, 'L');
                    pin_cell_config(05, 264, 302, 'L');
                    pin_cell_config(06, 264, 322, 'L');
                    pin_cell_config(07, 264, 342, 'L');
                    pin_cell_config(08, 380, 378, 'D');
                    pin_cell_config(09, 400, 378, 'D');
                    pin_cell_config(10, 420, 378, 'D');
                    pin_cell_config(11, 472, 342, 'R');
                    pin_cell_config(12, 472, 322, 'R');
                    pin_cell_config(13, 472, 302, 'R');
                    pin_cell_config(14, 472, 282, 'R');
                    pin_cell_config(15, 472, 262, 'R');
                    pin_cell_config(16, 472, 242, 'R');
                    pin_cell_config(17, 472, 222, 'R');
                    pin_cell_config(18, 420, 120, 'U');
                    pin_cell_config(19, 400, 120, 'U');
                    pin_cell_config(20, 380, 120, 'U');
                    break;

                case "STQFN-22L":
                    pin_cell_config(01, 264, 227, 'L');
                    pin_cell_config(02, 264, 247, 'L');
                    pin_cell_config(03, 264, 267, 'L');
                    pin_cell_config(04, 264, 287, 'L');
                    pin_cell_config(05, 264, 307, 'L');
                    pin_cell_config(06, 264, 327, 'L');
                    pin_cell_config(07, 372, 370, 'D');
                    pin_cell_config(08, 392, 370, 'D');
                    pin_cell_config(09, 412, 370, 'D');
                    pin_cell_config(10, 432, 370, 'D');
                    pin_cell_config(11, 470, 327, 'R');
                    pin_cell_config(12, 470, 307, 'R');
                    pin_cell_config(13, 470, 287, 'R');
                    pin_cell_config(14, 470, 267, 'R');
                    pin_cell_config(15, 470, 247, 'R');
                    pin_cell_config(16, 470, 227, 'R');
                    pin_cell_config(17, 432, 120, 'U');
                    pin_cell_config(18, 412, 120, 'U');
                    pin_cell_config(19, 392, 120, 'U');
                    pin_cell_config(20, 372, 120, 'U');
                    break;

                case "MSTQFN-22L":
                    pin_cell_config(01, 264, 222, 'L');
                    pin_cell_config(02, 264, 242, 'L');
                    pin_cell_config(03, 264, 272, 'L');
                    pin_cell_config(04, 264, 302, 'L');
                    pin_cell_config(05, 264, 332, 'L');
                    pin_cell_config(06, 380, 365, 'D');
                    pin_cell_config(07, 400, 365, 'D');
                    pin_cell_config(08, 420, 365, 'D');
                    pin_cell_config(09, 472, 332, 'R');
                    pin_cell_config(10, 472, 302, 'R');
                    pin_cell_config(11, 472, 272, 'R');
                    pin_cell_config(12, 472, 242, 'R');
                    pin_cell_config(13, 472, 222, 'R');
                    pin_cell_config(14, 420, 120, 'U');
                    pin_cell_config(15, 400, 120, 'U');
                    pin_cell_config(16, 380, 120, 'U');
                    pin_cell_config(17, 264, 257, 'L');
                    pin_cell_config(18, 264, 287, 'L');
                    pin_cell_config(19, 264, 317, 'L');
                    pin_cell_config(20, 472, 317, 'R');
                    pin_cell_config(21, 472, 287, 'R');
                    pin_cell_config(22, 472, 257, 'R');

                    break;

                case "STQFN-14L_a":
                case "STQFN-14L_c":
                    pin_cell_config(01, 264, 222, 'L');
                    pin_cell_config(02, 264, 242, 'L');
                    pin_cell_config(03, 264, 262, 'L');
                    pin_cell_config(04, 264, 282, 'L');
                    pin_cell_config(05, 264, 302, 'L');
                    pin_cell_config(06, 380, 335, 'D');
                    pin_cell_config(07, 400, 335, 'D');
                    pin_cell_config(08, 450, 302, 'R');
                    pin_cell_config(09, 450, 282, 'R');
                    pin_cell_config(10, 450, 262, 'R');
                    pin_cell_config(11, 450, 242, 'R');
                    pin_cell_config(12, 450, 222, 'R');
                    pin_cell_config(13, 400, 120, 'U');
                    pin_cell_config(14, 380, 120, 'U');
                    break;

                case "STQFN-14L_b":
                    pin_cell_config(01, 264, 225, 'L');
                    pin_cell_config(02, 264, 251, 'L');
                    pin_cell_config(03, 264, 276, 'L');
                    pin_cell_config(04, 264, 302, 'L');
                    pin_cell_config(05, 372, 340, 'D');
                    pin_cell_config(06, 392, 340, 'D');
                    pin_cell_config(07, 412, 340, 'D');
                    pin_cell_config(08, 450, 302, 'R');
                    pin_cell_config(09, 450, 276, 'R');
                    pin_cell_config(10, 450, 251, 'R');
                    pin_cell_config(11, 450, 225, 'R');
                    pin_cell_config(12, 412, 120, 'U');
                    pin_cell_config(13, 392, 120, 'U');
                    pin_cell_config(14, 372, 120, 'U');
                    break;

                case "STQFN-12L":
                    pin_cell_config(01, 264, 222, 'L');
                    pin_cell_config(02, 264, 242, 'L');
                    pin_cell_config(03, 264, 262, 'L');
                    pin_cell_config(04, 264, 282, 'L');
                    pin_cell_config(05, 380, 318, 'D');
                    pin_cell_config(06, 400, 318, 'D');
                    pin_cell_config(07, 450, 282, 'R');
                    pin_cell_config(08, 450, 262, 'R');
                    pin_cell_config(09, 450, 242, 'R');
                    pin_cell_config(10, 450, 222, 'R');
                    pin_cell_config(11, 400, 120, 'U');
                    pin_cell_config(12, 380, 120, 'U');
                    break;

                case "STQFN-8L":
                    pin_cell_config(01, 264, 222, 'L');
                    pin_cell_config(02, 264, 242, 'L');
                    pin_cell_config(03, 264, 262, 'L');
                    pin_cell_config(04, 380, 300, 'D');
                    pin_cell_config(05, 430, 262, 'R');
                    pin_cell_config(06, 430, 242, 'R');
                    pin_cell_config(07, 430, 222, 'R');
                    pin_cell_config(08, 380, 120, 'U');
                    break;

                default: break;
            }
        }
        else if (g.pin_labels_update)
        {
            int i = 1;
            foreach (Table table in g.doc.Tables)
            {
                try
                {                                                          // Is this try/catch necessary?
                    if (table.Title == "pin" + i.ToString() + "_label")
                    {
                        pin_cell_update(table, i);
                        i++;
                    }
                }
                catch
                {
                    Console.WriteLine("Error occured in theProgram: could not update pin label " + i.ToString());
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Oscillators
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Loading OSC Settings");

        if (g.CNTs_DLYs_update)
        {
            //////////////////////////////////////////////////
            //  GreenPAK5
            //////////////////////////////////////////////////
            if (g.GreenPAK.PAK_family.Equals(5))
            {
                //////////////////////////////////////////////////
                //  OSC0
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.RC_osc_src].ToString())
                {
                    case "0": break;
                    case "1": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq_alt; break;
                }
                switch (g.nvmData[g.GreenPAK.RC_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.RC_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 1; break;
                    case "01": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 2; break;
                    case "10": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 4; break;
                    case "11": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 8; break;
                }

                //////////////////////////////////////////////////
                //  OSC1
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.RING_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.RING_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 1; break;
                    case "01": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 2; break;
                    case "10": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 4; break;
                    case "11": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 8; break;
                }

                switch (g.nvmData[g.GreenPAK.LF_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.LF_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 1; break;
                    case "01": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 2; break;
                    case "10": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 4; break;
                    case "11": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 16; break;
                }
            }

            //////////////////////////////////////////////////
            //  GreenPAK4
            //////////////////////////////////////////////////
            else if (g.GreenPAK.PAK_family.Equals(4))
            {
                //////////////////////////////////////////////////
                //  LF OSC
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.LF_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.LF_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 1; break;
                    case "01": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 2; break;
                    case "10": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 4; break;
                    case "11": g.GreenPAK.LF_osc_freq = g.GreenPAK.LF_osc_freq / 16; break;
                }

                //////////////////////////////////////////////////
                //  RC OSC
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.RC_osc_src].ToString())
                {
                    case "0": break;
                    case "1": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq_alt; break;
                }
                switch (g.nvmData[g.GreenPAK.RC_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.RC_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 1; break;
                    case "01": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 2; break;
                    case "10": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 4; break;
                    case "11": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 8; break;
                }

                //////////////////////////////////////////////////
                //  RING OSC
                //////////////////////////////////////////////////
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.RING_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.RING_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 1; break;
                    case "01": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 4; break;
                    case "10": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 8; break;
                    case "11": g.GreenPAK.RING_osc_freq = g.GreenPAK.RING_osc_freq / 16; break;
                }
            }

            //////////////////////////////////////////////////
            //  GreenPAK3
            //////////////////////////////////////////////////
            else if (g.GreenPAK.PAK_family.Equals(3))
            {
                //////////////////////////////////////////////////
                //  RC OSC
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.RC_osc_src].ToString())
                {
                    case "0": break;
                    case "1": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq_alt; break;
                }
                switch (g.nvmData[g.GreenPAK.RC_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.RC_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 1; break;
                    case "01": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 2; break;
                    case "10": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 4; break;
                    case "11": g.GreenPAK.RC_osc_freq = g.GreenPAK.RC_osc_freq / 8; break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Counters
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Loading Counter settings");

        if (g.CNTs_DLYs_update)
        {
            for (int i = 0; i < g.GreenPAK.cnt.Length; i++)
            {
                foreach (XElement counter in g.ELEMENT.Descendants("item")
                    .Where(counter => counter.Attribute("caption").Value.Contains("CNT" + i.ToString())))
                {
                    if (counter.Element("graphics").Attribute("hidden").Value.Equals("0"))
                    {
                        if (g.GreenPAK.PAK_family.Equals(5) &&
                            g.nvmData[g.GreenPAK.cnt[i].SL].ToString().Equals("1"))
                        {
                            g.GreenPAK.cnt[i].used = true;
                            counter_config(i);
                            break;
                        }
                        else if (g.GreenPAK.PAK_family.Equals(4))
                        {
                            if (g.GreenPAK.cnt[i].SL == 0)
                            {
                                g.GreenPAK.cnt[i].used = true;
                                counter_config(i);
                            }
                            else
                            {
                                switch (g.nvmData[g.GreenPAK.cnt[i].SL + 1].ToString() +
                                        g.nvmData[g.GreenPAK.cnt[i].SL + 0].ToString())
                                {
                                    case "00":
                                    case "01":
                                        g.GreenPAK.cnt[i].used = true;
                                        counter_config(i);
                                        break;

                                    case "10": g.GreenPAK.cnt[i].used = false; break;
                                    case "11": g.GreenPAK.cnt[i].used = false; break;
                                }
                            }

                            break;
                        }
                        else if (g.GreenPAK.PAK_family.Equals(3))
                        {
                            if (g.GreenPAK.cnt[i].SL == 0 || g.nvmData[g.GreenPAK.cnt[i].SL].ToString().Equals("1"))
                            {
                                g.GreenPAK.cnt[i].used = true;
                                counter_config(i);
                            }
                        }
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  ACMPs
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Loading ACMP settings");

        if (g.ACMPs_update && (g.GreenPAK.acmp.Length > 0))
        {
            for (int i = 0; i < g.GreenPAK.acmp.Length; i++)
            {
                foreach (XElement acmp in g.ELEMENT.Descendants("item")
                    .Where(acmp => acmp.Attribute("caption").Value.Equals("A CMP" + (i).ToString())))
                {
                    if (acmp.Element("graphics").Attribute("hidden").Value.Equals("0"))
                    {
                        g.GreenPAK.acmp[i].used = true;
                        acmp_config(i);
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  ASM
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        foreach (XElement asm in g.ELEMENT.Descendants("item")
            .Where(asm => asm.Attribute("caption").Value.Equals("ASM")))
        {
            if (asm.Element("graphics").Attribute("hidden").Value.Equals("0"))
            {
                g.asm_used = true;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Absolute Maximum Conditions Table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating Absolute Maximum Conditions Table");

        if (g.GreenPAK.VDD_bypass_enable > 0 &&
            g.nvmData[g.GreenPAK.VDD_bypass_enable].ToString().Equals("1"))
        {
            g.doc.Variables["VDD_MAX"].Value = "3";
        }
        else
        {
            g.doc.Variables["VDD_MAX"].Value = "7";
        }

        g.doc.Variables["ESD_cdm"].Value = g.GreenPAK.ESD_cdm.ToString();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Electrical Characteristics Table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating EC Table");

        int symbolRow = 0;

        double VDD_min = Convert.ToDouble(g.GreenPAK.vdd.min);          //### Change this so that it includes all VDDs within Min/Max range?
        double VDD_max = Convert.ToDouble(g.GreenPAK.vdd.max);

        if (VDD_min <= 1.8)
        {
            g.VDD_EC[0] = true;
            g.VDD_EC[1] = true;
            g.VDD_EC[2] = true;
        }
        else if (VDD_min <= 3.3)
        {
            g.VDD_EC[1] = true;
            g.VDD_EC[2] = true;
        }
        else if (VDD_min <= 5.0)
        {
            g.VDD_EC[2] = true;
        }
        if (VDD_max < 3.3)
        {
            g.VDD_EC[1] = false;
            g.VDD_EC[2] = false;
        }
        else if (VDD_max < 5.0)
        {
            g.VDD_EC[2] = false;
        }

        double VDD2_min = Convert.ToDouble(g.GreenPAK.vdd2.min);
        double VDD2_max = Convert.ToDouble(g.GreenPAK.vdd2.max);

        if (VDD2_min <= 1.8)
        {
            g.VDD2_EC[0] = true;
            g.VDD2_EC[1] = true;
            g.VDD2_EC[2] = true;
        }
        else if (VDD2_min <= 3.3)
        {
            g.VDD2_EC[1] = true;
            g.VDD2_EC[2] = true;
        }
        else if (VDD2_min <= 5.0)
        {
            g.VDD2_EC[2] = true;
        }
        if (VDD2_max < 3.3)
        {
            g.VDD2_EC[1] = false;
            g.VDD2_EC[2] = false;
        }
        else if (VDD2_max < 5.0)
        {
            g.VDD2_EC[2] = false;
        }

        g.connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + g.templatePath + "Base_Die_DB.accdb;";
        while (true)
        {
            try
            {
                g.connection.Open();
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Connection Successful");
                g.connection.Close();
                break;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occured in theProgram: could not connect to AccessDB");
                MessageBox.Show(exception.ToString());
                form.backgroundWorker.ReportProgress(0, "Error: Connection Unsuccessful. Try closing the Database!");
                e.Cancel = true;
                return;
            }
        }

        g.connection.Open();

        foreach (Table EC_TABLE in g.doc.Tables)
        {
            if (EC_TABLE.Title == "ec_table")
            {
                g.table = EC_TABLE;
                if (g.pin_settings_update || g.temp_vdd_update)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  VDD2
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (g.GreenPAK.dual_supply_PAK)
                    {
                        symbolRow = EC_row_symbol("VDD2");
                        g.row = symbolRow;

                        g.table.Cell(g.row, 2).Range.Text = "Supply Voltage";
                        g.table.Cell(g.row, 7).Range.Text = "V";

                        EC_row_populate(g.table, g.row, "VDD2 ≤ VDD",
                            g.GreenPAK.vdd2.min,
                            g.GreenPAK.vdd2.typ,
                            g.GreenPAK.vdd2.max);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_IH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: V_IH");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI)
                    {
                        EC_IH_IL("VIH", g.VDD_EC, g.VDD);
                    }
                    else if (g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        EC_IH_IL("VIH2", g.VDD2_EC, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_IL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: V_IL");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI)
                    {
                        EC_IH_IL("VIL", g.VDD_EC, g.VDD);
                    }
                    else if (g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        EC_IH_IL("VIL2", g.VDD2_EC, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_IH / I_IL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: I_IH");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI ||
                        g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        symbolRow = EC_row_symbol("IIH");
                        g.row = symbolRow;

                        g.table.Cell(g.row, 2).Range.Text = "HIGH-Level Input Current";
                        g.table.Cell(g.row, 7).Range.Text = "µA";

                        EC_row_populate(g.table, g.row, "Logic Input PINs; VIN = VDD",
                            "-1.0",
                            "--",
                            "1.0");

                        symbolRow = EC_row_symbol("IIL");
                        g.row = symbolRow;

                        g.table.Cell(g.row, 2).Range.Text = "LOW-Level Input Current";
                        g.table.Cell(g.row, 7).Range.Text = "µA";

                        EC_row_populate(g.table, g.row, "Logic Input PINs; VIN = 0v",
                            "-1.0",
                            "--",
                            "1.0");
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_HYS
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: V_HYS");
                    if (g.VHYS)
                    {
                        symbolRow = EC_row_symbol("VHYS");
                        g.row = symbolRow;

                        g.table.Cell(g.row, 2).Range.Text = "Schmitt Trigger Hysteresis Voltage";
                        g.table.Cell(g.row, 7).Range.Text = "V";
                        for (int i = 0; i < 3; i++)
                        {
                            string VDD_DB = null;
                            string VDD_typ = null;
                            switch (i)
                            {
                                case 0: VDD_DB = "1_8"; VDD_typ = " at VDD = 1.8v"; break;
                                case 1: VDD_DB = "3_3"; VDD_typ = " at VDD = 3.3v"; break;
                                case 2: VDD_DB = "5_0"; VDD_typ = " at VDD = 5.0v"; break;
                            }

                            EC_row_populate(g.table, g.row, "Logic Input with Schmitt Trigger" + VDD_typ,
                                accessQuery(VDD_DB, "VHYS", null, "min"),
                                accessQuery(VDD_DB, "VHYS", null, "typ"),
                                accessQuery(VDD_DB, "VHYS", null, "max"));
                        }
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_OH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: V_OH");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODP1x > 0 || g.VDD.ODP2x > 0)
                    {
                        EC_OH_OL("VOH", g.VDD_EC, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODP1x > 0 || g.VDD2.ODP2x > 0)
                    {
                        EC_OH_OL("VOH2", g.VDD2_EC, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_OL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: V_OL");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODN1x > 0 || g.VDD.ODN2x > 0 || g.VDD.ODN4x > 0)
                    {
                        EC_OH_OL("VOL", g.VDD_EC, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODN1x > 0 || g.VDD2.ODN2x > 0 || g.VDD2.ODN4x > 0)
                    {
                        EC_OH_OL("VOL2", g.VDD2_EC, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_OH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: I_OH");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODP1x > 0 || g.VDD.ODP2x > 0)
                    {
                        EC_OH_OL("IOH", g.VDD_EC, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODP1x > 0 || g.VDD2.ODP2x > 0)
                    {
                        EC_OH_OL("IOH2", g.VDD2_EC, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_OL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(2, "Populating EC Table: I_OL");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODN1x > 0 || g.VDD.ODN2x > 0 || g.VDD.ODN4x > 0)
                    {
                        EC_OH_OL("IOL", g.VDD_EC, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODN1x > 0 || g.VDD2.ODN2x > 0 || g.VDD2.ODN4x > 0)
                    {
                        EC_OH_OL("IOL2", g.VDD2_EC, g.VDD2);
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  CNTs/DLYs
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // ### Update this with Access Data at some point

                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: CNTs/DLYs");
                if (g.CNTs_DLYs_update)
                {
                    for (int i = 0; i < g.GreenPAK.cnt.Length; i++)
                    {
                        if (g.GreenPAK.cnt[i].used)
                        {
                            symbolRow = EC_row_symbol("t" + g.GreenPAK.cnt[i].mode_alt + i.ToString());
                            g.row = symbolRow;
                            g.table.Cell(g.row, 2).Range.Text = g.GreenPAK.cnt[i].mode + " " + i.ToString() + " Time";
                            g.table.Cell(g.row, 7).Range.Text = g.GreenPAK.cnt[i].timeSI;

                            Console.WriteLine(g.GreenPAK.cnt[i].time.typ);
                            EC_row_populate(g.table, g.row, "At temperature " + g.GreenPAK.temp.typ + "\u00B0C",
                                "--",
                                g.GreenPAK.cnt[i].time.typ,
                                "--");
                            EC_row_split();

                            EC_row_merge(symbolRow);
                        }
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  ACMPs
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // ### Update this with Access Data at some point

                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: ACMPs");
                if (g.ACMPs_update)
                {
                    for (int i = 0; i < g.GreenPAK.acmp.Length; i++)
                    {
                        if (g.GreenPAK.acmp[i].used)
                        {
                            symbolRow = EC_row_symbol("VACMP" + i.ToString());
                            g.row = symbolRow;
                            EC_clearSection();
                            g.table.Cell(g.row, 2).Range.Text = "Analog Comparator" + i.ToString() + " Threshold Voltage";
                            g.table.Cell(g.row, 7).Range.Text = "mV";

                            EC_row_populate(g.table, g.row, "Low to High transition at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].acmpVIH, "--");
                            EC_row_split();
                            //EC_row_populate(table, row, "Low to High transition at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].threshold, "--");
                            //EC_row_split(table, row);
                            //row++;

                            EC_row_populate(g.table, g.row, "High to Low transition at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].acmpVIL, "--");
                            EC_row_split();
                            //EC_row_populate(table, row, "High to Low transition at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].threshold, "--");
                            //EC_row_split(table, row);
                            //row++;

                            EC_row_merge(symbolRow);

                            if (g.GreenPAK.acmp[i].hysteresis != "0")
                            {
                                symbolRow = EC_row_symbol("VHYST" + i.ToString());
                                g.row = symbolRow;
                                EC_clearSection();
                                g.table.Cell(g.row, 2).Range.Text = "Analog Comparator" + i.ToString() + " Hysteresis Voltage (note 1)";
                                g.table.Cell(g.row, 7).Range.Text = "mV";

                                EC_row_populate(g.table, g.row, "ACMP" + i.ToString() + " at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].hysteresis, "--");
                                EC_row_split();
                                //EC_row_populate(table, row, "ACMP" + i.ToString() + " at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].hysteresis, "--");
                                //EC_row_split(table, row);
                                //row++;

                                EC_row_merge(symbolRow);
                            }
                        }
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  TSU / PONTHR / POFFTHR
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: T_SU / P_ONTHR / P_OFFTHR");

                if (g.temp_vdd_update || g.new_part_update)
                {
                    symbolRow = EC_row_symbol("TSU");
                    g.row = symbolRow;
                    g.table.Cell(g.row, 2).Range.Text = "Start up Time";
                    g.table.Cell(g.row, 7).Range.Text = "ms";

                    EC_row_populate(g.table, g.row, "From VDD rising past PONTHR",
                        accessQuery("1_8", "TSU", null, "min"),
                        accessQuery("1_8", "TSU", null, "typ"),
                        accessQuery("1_8", "TSU", null, "max"));

                    symbolRow = EC_row_symbol("PONTHR");
                    g.row = symbolRow;
                    g.table.Cell(g.row, 2).Range.Text = "Power On Threshold";
                    g.table.Cell(g.row, 7).Range.Text = "V";

                    EC_row_populate(g.table, g.row, "VDD Level Required to Start Up the Chip",
                        accessQuery("1_8", "PONTHR", null, "min"),
                        accessQuery("1_8", "PONTHR", null, "typ"),
                        accessQuery("1_8", "PONTHR", null, "max"));

                    symbolRow = EC_row_symbol("POFFTHR");
                    g.row = symbolRow;
                    g.table.Cell(g.row, 2).Range.Text = "Power OFF Threshold";
                    g.table.Cell(g.row, 7).Range.Text = "V";

                    EC_row_populate(g.table, g.row, "VDD Level Required to Switch Off the Chip",
                        accessQuery("1_8", "POFFTHR", null, "min"),
                        accessQuery("1_8", "POFFTHR", null, "typ"),
                        accessQuery("1_8", "POFFTHR", null, "max"));
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  ASM Asynchronous State Machine
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: ASM");
                if (g.asm_used && (g.new_part_update || g.temp_vdd_update))
                {
                    g.row = EC_row_symbol("tst_delay");
                    g.table.Cell(g.row, 2).Range.Text = "State Machine Output Delay";
                    g.table.Cell(g.row, 7).Range.Text = "ns";
                    for (int i = 0; i < 3; i++)
                    {
                        string VDD_DB = null;
                        string VDD_typ = null;
                        switch (i)
                        {
                            case 0: VDD_DB = "1_8"; VDD_typ = "1.8v"; break;
                            case 1: VDD_DB = "3_3"; VDD_typ = "3.3v"; break;
                            case 2: VDD_DB = "5_0"; VDD_typ = "5.0v"; break;
                        }
                        EC_row_populate(g.table, g.row, "At VDD = " + VDD_typ,
                        accessQuery(VDD_DB, "tst_out_delay", null, "min"),
                        accessQuery(VDD_DB, "tst_out_delay", null, "typ"),
                        accessQuery(VDD_DB, "tst_out_delay", null, "max"));
                        EC_row_split();
                    }

                    g.row = EC_row_symbol("tst_out");
                    g.table.Cell(g.row, 2).Range.Text = "State Machine Output Transition Time";
                    g.table.Cell(g.row, 7).Range.Text = "ns";
                    for (int i = 0; i < 3; i++)
                    {
                        string VDD_DB = null;
                        string VDD_typ = null;
                        switch (i)
                        {
                            case 0: VDD_DB = "1_8"; VDD_typ = "1.8v"; break;
                            case 1: VDD_DB = "3_3"; VDD_typ = "3.3v"; break;
                            case 2: VDD_DB = "5_0"; VDD_typ = "5.0v"; break;
                        }
                        EC_row_populate(g.table, g.row, "At VDD = " + VDD_typ,
                        accessQuery(VDD_DB, "tst_out", null, "min"),
                        accessQuery(VDD_DB, "tst_out", null, "typ"),
                        accessQuery(VDD_DB, "tst_out", null, "max"));
                        EC_row_split();
                    }

                    g.row = EC_row_symbol("tst_pulse");
                    g.table.Cell(g.row, 2).Range.Text = "State Machine Input Pulse Acceptance Time";
                    g.table.Cell(g.row, 7).Range.Text = "ns";
                    for (int i = 0; i < 3; i++)
                    {
                        string VDD_DB = null;
                        string VDD_typ = null;
                        switch (i)
                        {
                            case 0: VDD_DB = "1_8"; VDD_typ = "1.8v"; break;
                            case 1: VDD_DB = "3_3"; VDD_typ = "3.3v"; break;
                            case 2: VDD_DB = "5_0"; VDD_typ = "5.0v"; break;
                        }
                        EC_row_populate(g.table, g.row, "At VDD = " + VDD_typ,
                        accessQuery(VDD_DB, "tst_pulse", null, "min"),
                        accessQuery(VDD_DB, "tst_pulse", null, "typ"),
                        accessQuery(VDD_DB, "tst_pulse", null, "max"));
                        EC_row_split();
                    }

                    g.row = EC_row_symbol("tst_comp");
                    g.table.Cell(g.row, 2).Range.Text = "State Machine Input Complete Time";
                    g.table.Cell(g.row, 7).Range.Text = "ns";
                    for (int i = 0; i < 3; i++)
                    {
                        string VDD_DB = null;
                        string VDD_typ = null;
                        switch (i)
                        {
                            case 0: VDD_DB = "1_8"; VDD_typ = "1.8v"; break;
                            case 1: VDD_DB = "3_3"; VDD_typ = "3.3v"; break;
                            case 2: VDD_DB = "5_0"; VDD_typ = "5.0v"; break;
                        }
                        EC_row_populate(g.table, g.row, "At VDD = " + VDD_typ,
                        accessQuery(VDD_DB, "tst_comp", null, "min"),
                        accessQuery(VDD_DB, "tst_comp", null, "typ"),
                        accessQuery(VDD_DB, "tst_comp", null, "max"));
                        EC_row_split();
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  PowerPAK Specs
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: PowerPAK");

                if (g.new_part_update &&
                   (g.GreenPAK.base_die.Equals("SLG46116") || g.GreenPAK.base_die.Equals("SLG46117")))
                {
                    Document TABLES = g.app.Documents.Add(@"P:\Apps_Tools\New_DS_Template\Resources\TABLES.docx");

                    foreach (Table newTable in TABLES.Tables)
                    {
                        if (newTable.Title.Equals("PowerPAK"))
                        {
                            newTable.Range.Copy();
                            g.table.Rows.Add();
                            g.row = g.table.Rows.Count;
                            g.table.Rows[g.table.Rows.Count].Range.PasteAppendTable();
                            //EC_row_merge(g.table.Rows.Count - 1, g.table.Rows.Count);
                            //EC_row_merge(g.table.Rows.Count - 1);

                            break;
                        }
                    }
                    TABLES.Close();

                    for (int i = g.row; i < g.table.Rows.Count; i++)
                    {
                        g.table.Rows[i].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    }

                    //for (int i = 1; i < g.table.Columns.Count; i++)
                    //{
                    //    if (i == 1 || i > 3)
                    //    {
                    //        g.table.Columns[i].Cells.
                    //    }
                    //}
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  Format EC table subscripts and exit EC table
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(2, "Populating EC Table: Fixing Subscripts");

                if (g.new_part_update || g.pin_settings_update || g.temp_vdd_update)
                {
                    EC_subscripts2();
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VOH Subscripts");
                    //EC_subscripts("V", "OH");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VOH2 Subscripts");
                    //EC_subscripts("V", "OH2");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VOL Subscripts");
                    //EC_subscripts("V", "OL");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VOL2 Subscripts");
                    //EC_subscripts("V", "OL2");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: IOH Subscripts");
                    //EC_subscripts("I", "OH");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: IOH2 Subscripts");
                    //EC_subscripts("I", "OH2");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: IOL Subscripts");
                    //EC_subscripts("I", "OL");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: IOL2 Subscripts");
                    //EC_subscripts("I", "OL2");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VDD Subscripts");
                    //EC_subscripts("V", "DD");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: VDD2 Subscripts");
                    //EC_subscripts("V", "DD2");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: PONTHR Subscripts");
                    //EC_subscripts("PON", "THR");
                    //if (worker.CancellationPending) { e.Cancel = true; return; }
                    //form.backgroundWorker.ReportProgress(2, "Populating EC Table: POFFTHR Subscripts");
                    //EC_subscripts("POFF", "THR");
                }

                g.connection.Close();

                form.backgroundWorker.ReportProgress(2, "Populating EC Table: Formatting for prettiness 1");
                EC_cleanup1();
                form.backgroundWorker.ReportProgress(0, "Populating EC Table: Formatting for prettiness 2");
                EC_cleanup2();
                form.backgroundWorker.ReportProgress(0, "Populating EC Table: Formatting for prettiness 3");
                EC_cleanup3();
                break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Top Marking, Tape & Reel
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating Images");

        if (g.new_part_update)
        {
            g.doc.Variables["GreenPAK_Net_Weight"].Value = g.GreenPAK.package_weight;

            if (g.GreenPAK.base_die.Equals("SLG46108"))
            {
                g.doc.Variables["TM_note"].Value = g.GreenPAK.TM_note;
            }
            else
            {
                g.doc.Variables["TM_note"].Value = " ";
            }

            InlineShapeReplace("TM");
            InlineShapeReplace("size");
            InlineShapeReplace("TR_specs");
            InlineShapeReplace("TR");
            InlineShapeReplace("LP");
        }

        if (g.TM_part_code_update)
        {
            g.doc.Variables["TM_part_code"].Value = g.TM_part_code;
        }
        if (g.TM_revision_update)
        {
            g.doc.Variables["TM_revision"].Value = g.TM_revision;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Datasheet Revision History table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating Datasheet Revision History table");

        foreach (Table table in g.doc.Tables)
        {
            if (table.Title == "DRH")
            {
                table.Rows.Add();
                int DRH_row = table.Rows.Count - 1;

                table.Cell(DRH_row, 1).Range.Text = DateTime.Now.ToString("MM/dd/yyyy");
                table.Cell(DRH_row, 2).Range.Text = g.DS_rev.Insert(1, ".");
                table.Cell(DRH_row, 3).Range.Text = g.DS_rev_change;

                break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  I2C Slave Address table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(2, "Populating I2C Chip Address Table");

        if (g.GreenPAK.i2c_PAK)
        {
            foreach (XElement i2c_block in g.ELEMENT.Descendants("item")
                .Where(i2c_block => i2c_block.Attribute("caption").ToString().StartsWith("caption=\"I2C")))
            {
                if (i2c_block.Element("graphics").Attribute("hidden").Value.Equals("1"))
                {
                    foreach (Table table in g.doc.Tables)
                    {
                        if (table.Title == "i2c_chip_address")
                        {
                            table.Delete();
                            break;
                        }
                    }
                    foreach (Paragraph p in g.doc.Paragraphs)
                    {
                        if (p.Range.Text.StartsWith("I2C Chip Address"))
                        {
                            p.Range.Select();
                            g.app.Selection.Delete();
                            break;
                        }
                    }
                }
                else
                {
                    string SA_bin = Reverse(g.nvmData.Substring(g.GreenPAK.i2c_slave_address, 4));
                    SA_bin = "0" + SA_bin + "000";
                    string SA_dec = Convert.ToInt32(SA_bin, 2).ToString();
                    string SA_hex = "0x" + Convert.ToInt32(SA_bin, 2).ToString("X");

                    Console.WriteLine(SA_bin + " " + SA_dec + " " + SA_hex);

                    g.doc.Variables["Slave_Address_BIN"].Value = SA_bin;
                    g.doc.Variables["Slave_Address_DEC"].Value = SA_dec;
                    g.doc.Variables["Slave_Address_HEX"].Value = SA_hex;
                }
            }
        }
        else if (g.new_part_update)
        {
            foreach (Table table in g.doc.Tables)
            {
                if (table.Title == "i2c_chip_address")
                {
                    table.Delete();
                    break;
                }
            }
            foreach (Paragraph p in g.doc.Paragraphs)
            {
                if (p.Range.Text.StartsWith("I2C Chip Address"))
                {
                    p.Range.Select();
                    g.app.Selection.Delete();
                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  End of parsing, update fields, close the file, quit Word, open the file for user
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(100, "Saving and Opening DataSheet File");

        saveFileAndOpen();
        // BackgroundWorker stops, return control to gui
        return;
    }

    private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'a', "1010" },
    { 'b', "1011" },
    { 'c', "1100" },
    { 'd', "1101" },
    { 'e', "1110" },
    { 'f', "1111" }};
} // End of GreenPAK_DS_Creation