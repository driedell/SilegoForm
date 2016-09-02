using GreenPAK_Library;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

public static class MainProgram
{
    public static class g       // for global variables
    {
        public static string nvmData = null;
        public static XElement ELEMENT;
        public static Microsoft.Office.Interop.Word.Application app;
        public static Microsoft.Office.Interop.Word.Document doc;
        public static OleDbConnection connection = new OleDbConnection();

        // ### update the paths below
        public static string GreenPAK_File = null;

        public static string DataSheet_File = null;
        public static string templatePath = @"P:\Apps_Tools\New_DS_Template\";

        public static bool pin_labels_update = false;
        public static bool pin_settings_update = false;
        public static bool metadata_update = false;
        public static bool temp_vdd_update = false;
        public static bool CNTs_DLYs_update = false;
        public static bool ACMPs_update = false;
        public static bool I_Q_update = false;
        public static bool new_part_update = false;
        public static bool TM_part_code_update = false;
        public static bool TM_revision_update = false;

        public static string I_Q = "1.0";
        public static string TM_part_code = "~~";
        public static string TM_revision = "~~";
        public static string DS_rev = "010";
        public static string Date = null;
        public static string part_number = "SLG~~~~~";

        //public static PAK GreenPAK;
        public static PAK GreenPAK;

        public static bool VHYS;
        public static bool asm_used;

        public static pin_IOs VDD = new pin_IOs();
        public static pin_IOs VDD2 = new pin_IOs();

        public struct pin_IOs
        {
            public byte PP1x;
            public byte PP2x;
            public byte PP4x;
            public byte ODN1x;
            public byte ODN2x;
            public byte ODN4x;
            public byte ODP1x;
            public byte ODP2x;
            public bool LVDI;
            public bool wSchmitt;
            public bool woSchmitt;
        };
    }

    [STAThread]
    public static void Main()
    {
        // Start the SilegoForm which will begin the BackgroundWorker and enter theProgram
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        System.Windows.Forms.Application.Run(new SilegoForm.SilegoForm());
        // After SilegoForm closes, we return here:

        closeDontSave();

        //Console.WriteLine("press a key");
        //Console.ReadLine();
    }

    public static void closeDontSave()
    {
        try
        {
            g.doc.Close(WdSaveOptions.wdDoNotSaveChanges);
            g.app.Quit(WdSaveOptions.wdDoNotSaveChanges);
        }
        catch { }
    }

    private static void pin_cell_update(Table table, int i)
    {
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
        foreach (Table table in g.doc.Tables)
        {
            if (table.Title.Equals("pin" + i.ToString() + "_label"))
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

    private static void pin_config_PAK5(int i, int address, string pin_type)
    {
        int bit = address * 8;

        switch (pin_type)
        {
            case "GPI":
                g.GreenPAK.pin[i].type = "Digital Input";
                pin_config_resistor(i, bit + 5, bit + 4);
                pin_config_OE_input(i, bit + 7, bit + 6);
                break;

            case "GPIO_OE":
                int OE = g.GreenPAK.pin[i].OE * 8;

                pin_config_resistor(i, bit + 3, bit + 2, bit + 1);

                // ### check this section
                if (g.nvmData.Substring(OE, 6).Equals("000000")) { g.GreenPAK.pin[i].type = "Digital Input"; }
                else if (g.nvmData.Substring(OE, 6).Equals("111111")) { g.GreenPAK.pin[i].type = "Digital Output"; }
                else { g.GreenPAK.pin[i].type = "Digital Input/Output"; }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i, bit + 5, bit + 4); break;

                    case "Digital Output": pin_config_OE_output(i, bit + 7, bit + 6); break;

                    case "Digital Input/Output":
                        pin_config_OE_input(i, bit + 5, bit + 4);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i, bit + 7, bit + 6);
                        break;

                    default: break;
                }
                break;

            case "GPIO":
                pin_config_resistor(i, bit + 4, bit + 3, bit + 2);
                pin_config_GPIO(i, bit + 7, bit + 6, bit + 5, bit + 1);
                break;

            case "I2C":
                switch (g.nvmData[bit + 2].ToString())  // Check if we're using I2C ### check if works
                {
                    case "0":                           // I2C is used
                        g.GreenPAK.pin[i].type = "I2C";
                        //g.GreenPAK.pin[i].description = "I2C";
                        switch (g.nvmData[bit + 1].ToString())
                        {
                            //case "0": g.GreenPAK.pin[i].description += " 1x up to 400kHz"; break;
                            //case "1": g.GreenPAK.pin[i].description += " 2x up to 1MHz"; break;
                            case "0": g.GreenPAK.pin[i].description = "Digital Input without Schmitt Trigger"; break;
                            case "1": g.GreenPAK.pin[i].description = "Digital Input without Schmitt Trigger"; break;
                        }
                        break;

                    case "1":                            // I2C is not used
                        pin_config_GPIO(i, bit + 7, bit + 6, bit + 5, bit + 1);
                        break;
                }

                pin_config_resistor(i, bit + 4, bit + 3);

                break;

            case "SD_OE":
                OE = g.GreenPAK.pin[i].OE * 8;

                pin_config_resistor(i, bit + 3, bit + 2, bit + 1);

                if (g.nvmData.Substring(OE, 6).Equals("000000")) { g.GreenPAK.pin[i].type = "Digital Input"; }
                else if (g.nvmData.Substring(OE, 6).Equals("111111")) { g.GreenPAK.pin[i].type = "Digital Output"; }
                else { g.GreenPAK.pin[i].type = "Digital Input/Output"; }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i, bit + 5, bit + 4); break;

                    case "Digital Output": pin_config_OE_output(i, bit + 7, bit + 6, bit + 0); break;

                    case "Digital Input/Output":
                        pin_config_OE_input(i, bit + 5, bit + 4);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i, bit + 7, bit + 6, bit + 0);
                        break;

                    default: break;
                }
                break;

            case "SD":
                pin_config_resistor(i, bit + 4, bit + 3, bit + 2);
                pin_config_GPIO(i, bit + 7, bit + 6, bit + 5, bit + 1, bit + 0);
                break;

            default: break;
        }
        if (g.GreenPAK.pin[i].vdd_src.Equals(1))
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
                case "Digital Input without Schmitt trigger": g.VDD.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }
        else if (g.GreenPAK.pin[i].vdd_src.Equals(2))
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
                case "Digital Input without Schmitt trigger": g.VDD2.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD2.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }

        Console.WriteLine("Pin" + i.ToString() + ": " + g.GreenPAK.pin[i].type + ", " + g.GreenPAK.pin[i].description);
    }

    private static void pin_config_PAK4(int i, int address, string pin_type)
    {
        int bit = address;

        switch (pin_type)
        {
            case "GPI":
                g.GreenPAK.pin[i].type = "Digital Input";
                pin_config_resistor(i, bit + 3, bit + 2, bit + 4);
                pin_config_OE_input(i, bit + 1, bit + 0);
                break;

            case "GPIO_OE":
                int OE = g.GreenPAK.pin[i].OE;

                pin_config_resistor(i, bit + 5, bit + 4, bit + 6);

                // ### check this section
                if (g.nvmData.Substring(OE, 6).Equals("000000")) { g.GreenPAK.pin[i].type = "Digital Input"; }
                else if (g.nvmData.Substring(OE, 6).Equals("111111")) { g.GreenPAK.pin[i].type = "Digital Output"; }
                else { g.GreenPAK.pin[i].type = "Digital Input/Output"; }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i, bit + 1, bit + 0); break;

                    case "Digital Output": pin_config_OE_output(i, bit + 3, bit + 2); break;

                    case "Digital Input/Output":
                        pin_config_OE_input(i, bit + 1, bit + 0);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i, bit + 3, bit + 2);
                        break;
                }
                break;

            case "GPIO":
                pin_config_resistor(i, bit + 4, bit + 3, bit + 5);
                pin_config_GPIO(i, bit + 2, bit + 1, bit + 0, bit + 6);
                break;

            case "SD_OE":
                OE = g.GreenPAK.pin[i].OE;

                pin_config_resistor(i, bit + 5, bit + 4, bit + 6);

                if (g.nvmData.Substring(OE, 6).Equals("000000")) { g.GreenPAK.pin[i].type = "Digital Input"; }
                else if (g.nvmData.Substring(OE, 6).Equals("111111")) { g.GreenPAK.pin[i].type = "Digital Output"; }
                else { g.GreenPAK.pin[i].type = "Digital Input/Output"; }

                switch (g.GreenPAK.pin[i].type)
                {
                    case "Digital Input": pin_config_OE_input(i, bit + 1, bit + 0); break;

                    case "Digital Output": pin_config_OE_output(i, bit + 3, bit + 2, bit + 7); break;

                    case "Digital Input/Output":
                        pin_config_OE_input(i, bit + 1, bit + 0);
                        g.GreenPAK.pin[i].description += " /\n";
                        pin_config_OE_output(i, bit + 3, bit + 2, bit + 7);
                        break;
                }
                break;

            case "SD":
                pin_config_resistor(i, bit + 4, bit + 3, bit + 5);
                pin_config_GPIO(i, bit + 2, bit + 1, bit + 0, bit + 6, bit + 7);
                break;

            default: break;
        }
        if (g.GreenPAK.pin[i].vdd_src.Equals(1))
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
                case "Digital Input without Schmitt trigger": g.VDD.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }
        else if (g.GreenPAK.pin[i].vdd_src.Equals(2))
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
                case "Digital Input without Schmitt trigger": g.VDD2.woSchmitt = true; break;
                case "Low Voltage Digital Input": g.VDD2.LVDI = true; break;

                //### TriState?

                default: break;
            }
        }

        Console.WriteLine("Pin" + i.ToString() + ": " + g.GreenPAK.pin[i].type + ", " + g.GreenPAK.pin[i].description);
    }

    private static void pin_config_resistor(int i, int a, int b, int c = 0)
    {
        bool floating = false;
        switch (g.nvmData[a].ToString() + g.nvmData[b].ToString())
        {
            case "00": g.GreenPAK.pin[i].resistor = "floating"; floating = true; break;
            case "01": g.GreenPAK.pin[i].resistor = "10kΩ"; break;
            case "10": g.GreenPAK.pin[i].resistor = "100kΩ"; break;
            case "11": g.GreenPAK.pin[i].resistor = "1MΩ"; break;
        }
        if (floating.Equals(false))
        {
            switch (g.nvmData[c].ToString())
            {
                case "0": g.GreenPAK.pin[i].resistor += "\npulldown"; break;
                case "1": g.GreenPAK.pin[i].resistor += "\npullup"; break;
            }
        }
    }

    private static void pin_config_OE_input(int i, int a, int b)
    {
        switch (g.nvmData[a].ToString() + g.nvmData[b].ToString())
        {
            case "00": g.GreenPAK.pin[i].description += "Digital Input without Schmitt trigger"; break;
            case "01": g.GreenPAK.pin[i].description += "Digital Input with Schmitt trigger"; break;
            case "10": g.GreenPAK.pin[i].description += "Low Voltage Digital Input"; break;
            case "11": g.GreenPAK.pin[i].description += "Analog Input/Output"; g.GreenPAK.pin[i].type = "Analog I/O"; break;
        }
    }

    private static void pin_config_OE_output(int i, int a, int b, int c = -1)
    {
        if (c >= 0 && g.nvmData[c].ToString().Equals("1"))
        {
            g.GreenPAK.pin[i].description += "Open Drain NMOS 4x";
        }
        else
        {
            switch (g.nvmData[a].ToString() + g.nvmData[b].ToString())
            {
                case "00": g.GreenPAK.pin[i].description += "Push Pull 1x"; break;
                case "01": g.GreenPAK.pin[i].description += "Push Pull 2x"; break;
                case "10": g.GreenPAK.pin[i].description += "Open Drain NMOS 1x"; break;
                case "11": g.GreenPAK.pin[i].description += "Open Drain NMOS 2x"; break;
            }
        }
    }

    private static void pin_config_GPIO(int i, int a, int b, int c, int d, int e = -1)
    {
        switch (g.nvmData[a].ToString() + g.nvmData[b].ToString() + g.nvmData[c].ToString())
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
        if (g.GreenPAK.pin[i].type.Equals("Digital Output") && e >= 0 && g.nvmData[e].ToString().Equals("1"))
        {
            g.GreenPAK.pin[i].description = "Open Drain NMOS 4x";
        }
        else if (g.GreenPAK.pin[i].type.Equals("Digital Output"))
        {
            switch (g.nvmData[d].ToString())
            {
                case "0": g.GreenPAK.pin[i].description += " 1x"; break;
                case "1": g.GreenPAK.pin[i].description += " 2x"; break;
            }
        }
    }

    private static string TryGetElementValue(this XElement parentEl, string elementName)
    {
        var foundEl = parentEl.Element(elementName);

        if (foundEl != null)
        {
            return foundEl.Value;
        }
        return null;
    }

    private static void acmp_config_PAK5(int i)
    {
        int bit = g.GreenPAK.acmp[i].control * 8;
        int acmpVIH = 0;
        int acmpVIL = 0;
        byte multiplier = 1;
        string low_bw = null;
        byte hysteresis = 0;

        switch (g.nvmData[bit + 6].ToString() + g.nvmData[bit + 5].ToString())
        {
            case "00": multiplier = 1; break;
            case "01": multiplier = 2; break;
            case "10": multiplier = 3; break;
            case "11": multiplier = 4; break;
        }

        switch (g.nvmData[bit + 7].ToString())  // ### still need this? unused
        {
            case "0": low_bw = "OFF"; break;
            case "1": low_bw = "ON"; break;
        }

        switch (g.nvmData[bit + 4].ToString() +
                g.nvmData[bit + 3].ToString() +
                g.nvmData[bit + 2].ToString() +
                g.nvmData[bit + 1].ToString() +
                g.nvmData[bit + 0].ToString())
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
                //### include field for External VREF??
        }

        switch (g.nvmData[g.GreenPAK.acmp[i].hyst + 1].ToString() +
    g.nvmData[g.GreenPAK.acmp[i].hyst].ToString())
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
        if (acmpVIL < 0)
        {
            acmpVIL = 0;
        }

        g.GreenPAK.acmp[i].low_bw = low_bw;
        g.GreenPAK.acmp[i].hysteresis = hysteresis.ToString();
        g.GreenPAK.acmp[i].acmpVIH = acmpVIH.ToString();
        g.GreenPAK.acmp[i].acmpVIL = acmpVIL.ToString();
    }

    private static void acmp_config_PAK4(int i)
    {
        int bit = g.GreenPAK.acmp[i].control;
        int acmpVIH = 0;
        int acmpVIL = 0;
        byte multiplier = 1;
        string low_bw = null;
        byte hysteresis = 0;

        switch (g.nvmData[g.GreenPAK.acmp[i].gain + 1].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].gain + 0].ToString())
        {
            case "00": multiplier = 1; break;
            case "01": multiplier = 2; break;
            case "10": multiplier = 3; break;
            case "11": multiplier = 4; break;
        }

        switch (g.nvmData[g.GreenPAK.acmp[i].low_bandwidth].ToString())  // ### still need this? unused
        {
            case "0": low_bw = "OFF"; break;
            case "1": low_bw = "ON"; break;
        }

        switch (g.nvmData[bit + 4].ToString() +
                g.nvmData[bit + 3].ToString() +
                g.nvmData[bit + 2].ToString() +
                g.nvmData[bit + 1].ToString() +
                g.nvmData[bit + 0].ToString())
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
                //### include field for External VREF??
        }

        switch (g.nvmData[g.GreenPAK.acmp[i].hyst + 1].ToString() +
                g.nvmData[g.GreenPAK.acmp[i].hyst + 0].ToString())
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
        if (acmpVIL < 0)
        {
            acmpVIL = 0;
        }

        g.GreenPAK.acmp[i].low_bw = low_bw;
        g.GreenPAK.acmp[i].hysteresis = hysteresis.ToString();
        g.GreenPAK.acmp[i].acmpVIH = acmpVIH.ToString();
        g.GreenPAK.acmp[i].acmpVIL = acmpVIL.ToString();
    }

    private static void counter_config_PAK5(int i)
    {
        int bit = g.GreenPAK.cnt[i].control * 8;

        double freq = 0;
        double time = 0;
        string mode = null;
        string mode_alt = "dly";

        Console.WriteLine("pakfamily = 5");
        switch (g.nvmData[bit + 7].ToString() + g.nvmData[bit + 6].ToString())
        {
            case "00": mode = "Delay"; break;
            case "01": mode = "One-Shot"; break;
            case "10": mode = "Frequency Detector"; break;
            case "11": mode = "Counter"; mode_alt = "cnt"; break;
        }

        switch (g.nvmData[bit + 4].ToString() + g.nvmData[bit + 3].ToString() + g.nvmData[bit + 2].ToString())
        {
            case "000": freq = g.GreenPAK.PAK5_osc0; break;
            case "001": freq = g.GreenPAK.PAK5_osc0 / 4; break;
            case "010": freq = g.GreenPAK.PAK5_osc0 / 12; break;
            case "011": freq = g.GreenPAK.PAK5_osc0 / 24; break;
            case "100": freq = g.GreenPAK.PAK5_osc0 / 64; break;
            case "101": freq = g.GreenPAK.PAK5_osc1; break;
                //case "110": break;                        //### include field for External Clock?
                //case "111": clk_src = "CNT1_Overflow"; break;
        }
        //Console.WriteLine("freq = " + freq.ToString());

        string bin = Reverse(g.nvmData.Substring(g.GreenPAK.cnt[i].data * 8, g.GreenPAK.cnt[i].length));
        //Console.WriteLine("bin = " + bin.ToString());

        int counter_data = Convert.ToInt32(bin, 2);
        //Console.WriteLine("counter_data = " + counter_data.ToString());

        if (mode.Equals("Counter"))        // ### Build in support for min/max values?
        {
            time = ((counter_data + 1) * (1 / freq));
        }
        else
        {
            time = ((counter_data + 2) * (1 / freq));
        }
        //Console.WriteLine("counter" + i.ToString() + " data + 1 = " + (counter_data + 1).ToString());
        //Console.WriteLine("1/freq = " + (1 / freq).ToString());
        //Console.WriteLine("time0: " + time.ToString());

        // SI Prefixes
        if (time < 0.001) { time = time * 1000000; g.GreenPAK.cnt[i].timeSI = "µs"; }
        else if (time < 1) { time = time * 1000; g.GreenPAK.cnt[i].timeSI = "ms"; }
        else { g.GreenPAK.cnt[i].timeSI = "s"; }

        //Console.WriteLine("time1: " + time.ToString());

        g.GreenPAK.cnt[i].mode = mode;
        g.GreenPAK.cnt[i].mode_alt = mode_alt;
        g.GreenPAK.cnt[i].time.min = "--";        // ### Build in support for min/max values?
        g.GreenPAK.cnt[i].time.typ = Math.Round(time, 3).ToString();
        g.GreenPAK.cnt[i].time.max = "--";        // ### Build in support for min/max values?

        //Console.WriteLine("Counter" + i.ToString() + " mode: " + g.GreenPAK.cnt[i].mode);
        //Console.WriteLine("Counter" + i.ToString() + " mode_alt: " + g.GreenPAK.cnt[i].mode_alt);
        //Console.WriteLine();
    }

    private static void counter_config_PAK4(int i)
    {
        int control = g.GreenPAK.cnt[i].control;

        double freq = 0;
        double time = 0;
        string mode = null;
        string mode_alt = "dly";
        string bin = null;

        switch (g.nvmData[g.GreenPAK.cnt[i].selected].ToString())
        {
            case "0": mode = "Delay"; break;
            case "1": mode = "Counter"; mode_alt = "cnt"; break;
        }

        if (mode.Equals("Delay") || mode.Equals("Counter"))
        {
            switch (g.nvmData[control + 3].ToString() +
                    g.nvmData[control + 2].ToString() +
                    g.nvmData[control + 1].ToString() +
                    g.nvmData[control + 0].ToString())
            {
                case "0000": freq = g.GreenPAK.PAK4_RC_osc / 1; break;
                case "0001": freq = g.GreenPAK.PAK4_RC_osc / 4; break;
                case "0010": freq = g.GreenPAK.PAK4_RC_osc / 12; break;
                case "0011": freq = g.GreenPAK.PAK4_RC_osc / 24; break;
                case "0100": freq = g.GreenPAK.PAK4_RC_osc / 64; break;
                case "0101": break;     // DLY_out3
                case "0110": break;     // matrix_out67
                case "0111": break;     // matrix_out67 / 8
                case "1000": freq = g.GreenPAK.PAK4_RING_osc; break;
                case "1001": break;     // matrix_out80 (SPI_SCLK)
                case "1010": freq = g.GreenPAK.PAK4_LF_osc; break;
                case "1011": break;     // CKFSM_DIV256
                case "1100": break;     // CKPWM
            }
        }
        bin = Reverse(g.nvmData.Substring(g.GreenPAK.cnt[i].data, g.GreenPAK.cnt[i].length));

        //Console.WriteLine("bin = " + bin.ToString());

        int counter_data = Convert.ToInt32(bin, 2);
        //Console.WriteLine("counter_data = " + counter_data.ToString());

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

        //Console.WriteLine("time1: " + time.ToString());

        g.GreenPAK.cnt[i].mode = mode;
        g.GreenPAK.cnt[i].mode_alt = mode_alt;
        g.GreenPAK.cnt[i].time.min = "--";        // ### Build in support for min/max values?
        g.GreenPAK.cnt[i].time.typ = Math.Round(time, 3).ToString();
        g.GreenPAK.cnt[i].time.max = "--";        // ### Build in support for min/max values?

        //Console.WriteLine("Counter" + i.ToString() + " mode: " + g.GreenPAK.cnt[i].mode);
        //Console.WriteLine("Counter" + i.ToString() + " mode_alt: " + g.GreenPAK.cnt[i].mode_alt);
        //Console.WriteLine();
    }

    private static string HexStringToBinary(string hex)
    {
        string result = null;
        foreach (char c in hex)
        {
            result = result + hexCharacterToBinary[char.ToLower(c)];
        }
        return result.ToString();
    }

    private static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static void updateFields()
    {
        foreach (Section section in g.doc.Sections)
        {
            g.doc.Fields.Update();  // update each section

            HeadersFooters headers = section.Headers;  //Get all headers
            foreach (HeaderFooter header in headers)
            {
                Fields fields = header.Range.Fields;
                foreach (Field field in fields)
                {
                    field.Update();  // update all fields in headers
                }
            }

            HeadersFooters footers = section.Footers;  //Get all footers
            foreach (HeaderFooter footer in footers)
            {
                Fields fields = footer.Range.Fields;
                foreach (Field field in fields)
                {
                    field.Update();  //update all fields in footers
                }
            }
        }
    }

    private static void EC_clearSection(Table table, int row)
    {
        while (row < table.Rows.Count)
        {
            try
            {
                if (table.Cell(row + 1, 1).Range.Text != "")
                {
                    return;
                }
            }
            catch
            {
                table.Cell(row, 3).Merge(table.Cell(row + 1, 3));
                table.Cell(row, 4).Merge(table.Cell(row + 1, 4));
                table.Cell(row, 5).Merge(table.Cell(row + 1, 5));
                table.Cell(row, 6).Merge(table.Cell(row + 1, 6));
            }
        }
    }

    private static int EC_row_symbol(Table table, string symbol)
    {
        for (int i = 1; i <= table.Rows.Count; i++)
        {
            // if the cell doesn't exist skip it
            try
            {
                if (table.Cell(i, 1).Range.Text.Contains(symbol))
                {
                    return i;
                }
            }
            catch { }
        }
        int new_row;
        if (symbol.Equals("VDD2"))
        {
            table.Rows.Add(table.Rows[3]);
            new_row = 3;
        }
        else
        {
            table.Rows.Add();
            new_row = table.Rows.Count;
        }
        Range range = table.Cell(new_row, 1).Range;
        range.Text = symbol;
        range.SetRange(range.Start + 1, range.End);
        range.Select();
        range.Font.Subscript = 1;
        return new_row;
    }

    private static void EC_row_populate(Table table, int row, string param, string min, string typ, string max)
    {
        table.Cell(row, 3).Range.Text = param;
        table.Cell(row, 4).Range.Text = min;
        table.Cell(row, 5).Range.Text = typ;
        table.Cell(row, 6).Range.Text = max;
    }

    private static void EC_row_split(Table table, int row)
    {
        for (int i = 1; i <= table.Columns.Count; i++)
        {
            table.Cell(row, i).Split(2, 1);
        }
    }

    private static void EC_row_merge(Table table, int symbolRow, int row)
    {
        table.Cell(row, 1).Merge(table.Cell(symbolRow, 1));
        table.Cell(row, 2).Merge(table.Cell(symbolRow, 2));
        table.Cell(row, 3).Merge(table.Cell(row - 1, 3));
        table.Cell(row, 4).Merge(table.Cell(row - 1, 4));
        table.Cell(row, 5).Merge(table.Cell(row - 1, 5));
        table.Cell(row, 6).Merge(table.Cell(row - 1, 6));
        table.Cell(row, 7).Merge(table.Cell(symbolRow, 7));
    }

    private static void EC_IH_IL(Table table, string symbol, string VDD_DB, double VDD_typ, g.pin_IOs VICTOR)
    {
        string level = null;
        if (symbol.Contains("H")) { level = "HIGH"; }
        else if (symbol.Contains("L")) { level = "LOW"; }

        int symbolRow = EC_row_symbol(table, symbol);
        int row = symbolRow; ;

        EC_clearSection(table, row);
        table.Cell(row, 2).Range.Text = level + "-Level Input Voltage";
        if (g.GreenPAK.dual_supply && !symbol.Contains("2"))
        {
            table.Cell(row, 2).Range.Text += g.GreenPAK.dual_supply_vdd_pins;
        }
        else if (g.GreenPAK.dual_supply && symbol.Contains("2"))
        {
            table.Cell(row, 2).Range.Text += g.GreenPAK.dual_supply_vdd2_pins;
        }
        table.Cell(row, 7).Range.Text = "V";

        if (VICTOR.woSchmitt)
        {
            EC_row_populate(table, row, "Logic Input at VDD = " + VDD_typ.ToString() + "V",
                accessQuery(VDD_DB, symbol.Substring(0, 3), "woSchmitt", "min"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "woSchmitt", "typ"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "woSchmitt", "max"));
            EC_row_split(table, row);
            row++;
        }
        if (VICTOR.wSchmitt)
        {
            EC_row_populate(table, row, "Logic Input with Schmitt Trigger at VDD = " + VDD_typ.ToString() + "V",
                accessQuery(VDD_DB, symbol.Substring(0, 3), "wSchmitt", "min"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "wSchmitt", "typ"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "wSchmitt", "max"));
            EC_row_split(table, row);
            row++;
        }
        if (VICTOR.LVDI)
        {
            EC_row_populate(table, row, "LVDI Logic Input at VDD = " + VDD_typ.ToString() + "V",
                accessQuery(VDD_DB, symbol.Substring(0, 3), "LVDI", "min"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "LVDI", "typ"),
                accessQuery(VDD_DB, symbol.Substring(0, 3), "LVDI", "max"));
            EC_row_split(table, row);
            row++;
        }
        EC_row_merge(table, symbolRow, row);
    }

    private static void EC_OH_OL(Table table, string symbol, string VDD_DB, double VDD_typ, g.pin_IOs VICTOR)
    {
        string level = null;
        if (symbol.Contains("H")) { level = "HIGH"; }
        else if (symbol.Contains("L")) { level = "LOW"; }

        string VC = null;
        if (symbol.StartsWith("V")) { VC = "Voltage"; }
        else if (symbol.StartsWith("I")) { VC = "Current"; }

        int symbolRow = EC_row_symbol(table, symbol);
        int row = symbolRow;
        EC_clearSection(table, row);

        table.Cell(row, 2).Range.Text = level + "-Level Output " + VC;
        if (g.GreenPAK.dual_supply && !symbol.Contains("2"))
        {
            table.Cell(row, 2).Range.Text += g.GreenPAK.dual_supply_vdd_pins;
        }
        else if (g.GreenPAK.dual_supply && symbol.Contains("2"))
        {
            table.Cell(row, 2).Range.Text += g.GreenPAK.dual_supply_vdd2_pins;
        }
        table.Cell(row, 7).Range.Text = "V";

        if (level.Equals("HIGH"))
        {
            if (VICTOR.PP1x > 0 || VICTOR.ODP1x > 0)
            {
                EC_row_populate(table, row, "Push Pull & PMOS OD 1x Driver at VDD=" + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "max"));
                EC_row_split(table, row);
                row++;
            }
            if (VICTOR.PP2x > 0 || VICTOR.ODP2x > 0)
            {
                EC_row_populate(table, row, "Push Pull & PMOS OD 2x Driver at VDD=" + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "max"));
                EC_row_split(table, row);
                row++;
            }
        }
        else if (level.Equals("LOW"))
        {
            if (VICTOR.PP1x > 0)
            {
                EC_row_populate(table, row, "Push Pull 1x Driver at VDD = " + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP1x", "max"));
                EC_row_split(table, row);
                row++;
            }
            if (VICTOR.PP2x > 0)
            {
                EC_row_populate(table, row, "Push Pull 2x Driver at VDD = " + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "PP2x", "max"));
                EC_row_split(table, row);
                row++;
            }
            if (VICTOR.ODN1x > 0)
            {
                EC_row_populate(table, row, "Open Drain 1x Driver at VDD = " + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN1x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN1x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN1x", "max"));
                EC_row_split(table, row);
                row++;
            }
            if (VICTOR.ODN2x > 0)
            {
                EC_row_populate(table, row, "Open Drain 2x Driver at VDD = " + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN2x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN2x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN2x", "max"));
                EC_row_split(table, row);
                row++;
            }
            if (VICTOR.ODN4x > 0)
            {
                EC_row_populate(table, row, "Open Drain 4x Driver at VDD = " + g.GreenPAK.vdd.typ + "V",
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN4x", "min"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN4x", "typ"),
                    accessQuery(VDD_DB, symbol.Substring(0, 3), "ODN4x", "max"));
                EC_row_split(table, row);
                row++;
            }
        }
        EC_row_merge(table, symbolRow, row);
    }

    private static void saveFileAndOpen()
    {
        try
        {
            updateFields();
            string path = g.DataSheet_File.Substring(0, g.DataSheet_File.LastIndexOf('\\') + 1);
            g.doc.SaveAs2(path + g.part_number + "_DS_r" + g.DS_rev + "_" + DateTime.Now.ToString("MMddyyyy") + ".docx");

            g.doc.Close();
            g.app.Quit();

            Process.Start(path + g.part_number + "_DS_r" + g.DS_rev + "_" + DateTime.Now.ToString("MMddyyyy") + ".docx");

            return;
        }
        catch
        {
            MessageBox.Show("Error: Could not save DataSheet file!\nPlease close the DataSheet file then click \"OK\".");
            saveFileAndOpen();
        }
    }

    private static void shapeReplace(InlineShape shape, string title)
    {
        Range range = shape.Range;
        shape.Delete();

        Console.WriteLine(g.templatePath + "STQFN_" + (g.GreenPAK.pin.Length - 1).ToString() + "_" + title + ".png");

        InlineShape newShape = range.InlineShapes.AddPicture(g.templatePath + "STQFN_" +
            (g.GreenPAK.pin.Length - 1).ToString() + "_" + title + ".png");

        newShape.Title = "top_marking";
    }

    private static string accessQuery(string returnField, string param1, string param2 = "-1", string param3 = "-1")
    {
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
            MessageBox.Show("Error " + ex);
        }

        //g.connection.Close();
        return returnValue;
    }

    public static void theProgram(BackgroundWorker worker, DoWorkEventArgs e)
    {
        var form = Form.ActiveForm as SilegoForm.SilegoForm;
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Loading files...");

        object oMissing = System.Reflection.Missing.Value;

        // ### check if this still works
        try
        {
            g.ELEMENT = XElement.Load(g.GreenPAK_File);
        }
        catch
        {
            form.backgroundWorker.ReportProgress(0, "Error: Could not load GreenPAK file!");
            e.Cancel = true;
            return;
        }
        try
        {
            g.app = new Microsoft.Office.Interop.Word.Application();
            g.doc = g.app.Documents.Add(g.DataSheet_File, oMissing, oMissing, true);
        }
        catch
        {
            form.backgroundWorker.ReportProgress(0, "Error: Could not load DataSheet file!");
            e.Cancel = true;
            return;
        }

        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Getting Date / I_Q / DS_rev");
        //if (worker.CancellationPending) { e.Cancel = true; }

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
        form.backgroundWorker.ReportProgress(3, "Creating PAKs");

        PAK.createPAKs();

        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Selecting PAK");

        // GreenPAK5
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "05"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "1": g.GreenPAK = PAKs.SLG46531; break;
                case "2": g.GreenPAK = PAKs.SLG46532; break;
                case "3": g.GreenPAK = PAKs.SLG46533; break;
                case "4": g.GreenPAK = PAKs.SLG46534; break;
                case "5": g.GreenPAK = PAKs.SLG46535; break;
                case "6": g.GreenPAK = PAKs.SLG46536; break;
                default: break;
            }
        }
        // GreenPAK4
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "04"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "1": g.GreenPAK = PAKs.SLG46140; break;
                case "2": g.GreenPAK = PAKs.SLG46620; break;
                case "6": g.GreenPAK = PAKs.SLG46621; break;
                default: break;
            }
        }
        // GreenPAK3
        foreach (XElement chip in g.ELEMENT.Descendants("chip")
            .Where(xEle => (string)xEle.Attribute("type") == "03"))
        {
            switch (chip.Attribute("revision").Value)
            {
                case "2": g.GreenPAK = PAKs.SLG46721; break;
                case "3": g.GreenPAK = PAKs.SLG46722; break;
                case "4": g.GreenPAK = PAKs.SLG46110; break;
                case "5": g.GreenPAK = PAKs.SLG46120; break;
                case "6": g.GreenPAK = PAKs.SLG46116; break;
                case "7": g.GreenPAK = PAKs.SLG46117; break;
                case "11": g.GreenPAK = PAKs.SLG46121; break;
                case "12": g.GreenPAK = PAKs.SLG46108; break;
                default: break;
            }
        }

        g.doc.Variables["GreenPAK_Base_Die"].Value = g.GreenPAK.base_die;
        g.doc.Variables["GreenPAK_Package"].Value = g.GreenPAK.package;
        g.doc.Variables["GreenPAK_Package_size"].Value = g.GreenPAK.package_size;
        g.doc.Variables["GreenPAK_Family"].Value = g.GreenPAK.PAK_family.ToString();
        g.doc.Variables["GreenPAK_Pin_Count"].Value = (g.GreenPAK.pin.Length - 1).ToString();

        Console.WriteLine(g.GreenPAK.base_die);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Grab NVM, store in g.nvmData
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Loading NVM");

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
        form.backgroundWorker.ReportProgress(3, "Grabbing VDD / Temp specs");

        // VDD specs
        foreach (XElement xEle in g.ELEMENT.Descendants("vddSpecs"))
        {
            g.doc.Variables["vddMin"].Value = xEle.Attribute("vddMin").Value;
            g.doc.Variables["vddTyp"].Value = xEle.Attribute("vddTyp").Value;
            g.doc.Variables["vddMax"].Value = xEle.Attribute("vddMax").Value;

            g.GreenPAK.vdd = new PAK.mTM();
            g.GreenPAK.vdd.min = xEle.Attribute("vddMin").Value;
            g.GreenPAK.vdd.typ = xEle.Attribute("vddTyp").Value;
            g.GreenPAK.vdd.max = xEle.Attribute("vddMax").Value;
        }

        // VDD2 specs
        if (g.GreenPAK.dual_supply)
        {
            foreach (XElement xEle in g.ELEMENT.Descendants("vdd2Specs"))
            {
                g.doc.Variables["vdd2Min"].Value = xEle.Attribute("vdd2Min").Value;
                g.doc.Variables["vdd2Typ"].Value = xEle.Attribute("vdd2Typ").Value;
                g.doc.Variables["vdd2Max"].Value = xEle.Attribute("vdd2Max").Value;

                g.GreenPAK.vdd2 = new PAK.mTM();
                g.GreenPAK.vdd2.min = xEle.Attribute("vdd2Min").Value;
                g.GreenPAK.vdd2.typ = xEle.Attribute("vdd2Typ").Value;
                g.GreenPAK.vdd2.max = xEle.Attribute("vdd2Max").Value;
            }
        }

        // Temp specs
        foreach (XElement xEle in g.ELEMENT.Descendants("tempSpecs"))
        {
            g.doc.Variables["tempMin"].Value = xEle.Attribute("tempMin").Value;
            g.doc.Variables["tempTyp"].Value = xEle.Attribute("tempTyp").Value;
            g.doc.Variables["tempMax"].Value = xEle.Attribute("tempMax").Value;

            g.GreenPAK.temp = new PAK.mTM();
            g.GreenPAK.temp.min = xEle.Attribute("tempMin").Value;
            g.GreenPAK.temp.typ = xEle.Attribute("tempTyp").Value;
            g.GreenPAK.temp.max = xEle.Attribute("tempMax").Value;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Project Metadata
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Gathering MetaData");

        if (g.metadata_update)
        {
            // Customer Info (Name, project name, part number, version number)
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                     .Where(xEle => (string)xEle.Attribute("id") == "2"))
            {
                g.doc.Variables["Customer_Name"].Value = xEle.Attribute("text").Value;
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                     .Where(xEle => (string)xEle.Attribute("id") == "3"))
            {
                g.doc.Variables["Customer_Project_Name"].Value = xEle.Attribute("text").Value;
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                 .Where(xEle => (string)xEle.Attribute("id") == "4"))
            {
                g.doc.Variables["Customer_Part_Number"].Value = xEle.Attribute("text").Value;
                g.part_number = xEle.Attribute("text").Value;
            }
            foreach (XElement xEle in g.ELEMENT.Descendants("textLineDataField")
                     .Where(xEle => (string)xEle.Attribute("id") == "5"))
            {
                g.doc.Variables["Customer_Version_Number"].Value = xEle.Attribute("text").Value;
            }
        }

        // Pattern ID
        string pattern = Reverse(g.nvmData.Substring(g.GreenPAK.pattern_id_address, 8));
        g.doc.Variables["Pattern_ID"].Value = Convert.ToInt32(pattern, 2).ToString().PadLeft(3, '0');

        // Locked/Unlocked
        if (g.GreenPAK.PAK_family.Equals(5))
        {
            string nvm_lock = null;
            switch (g.nvmData[g.GreenPAK.PAK5_nvm_read_lock].ToString() +
                    g.nvmData[g.GreenPAK.PAK5_nvm_write_lock_bank0_2].ToString())
            {
                case "00": nvm_lock = ("NVM readable, NVM writable"); break;
                case "01": nvm_lock = ("NVM readable, NVM non-writable"); break;
                case "10": nvm_lock = ("NVM non-readable, NVM writable"); break;
                case "11": nvm_lock = ("NVM non-readable, NVM non-writable"); break;
                default: nvm_lock = ("NVM readable, NVM writable"); break;
            }
            g.doc.Variables["NVM_lock"].Value = nvm_lock;
        }
        //### GreenPAK4 Lock
        //### GreenPAK3 Lock

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Pin Labels and Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Creating Pins");

        if (g.pin_labels_update || g.pin_settings_update)
        {
            for (int i = 1; i < g.GreenPAK.pin.Length; i++)
            {
                if (g.GreenPAK.pin[i].pin_type.Equals("VDD"))
                {
                    g.doc.Variables["pin" + i.ToString() + "_label"].Value = "VDD";
                    g.GreenPAK.pin[i].name = "VDD";
                    g.GreenPAK.pin[i].resistor = "--";
                    g.GreenPAK.pin[i].type = "PWR";
                    g.GreenPAK.pin[i].description = "Supply Voltage";
                }
                else if (g.GreenPAK.pin[i].pin_type.Equals("GND"))
                {
                    g.doc.Variables["pin" + i.ToString() + "_label"].Value = "GND";
                    g.GreenPAK.pin[i].name = "GND";
                    g.GreenPAK.pin[i].resistor = "--";
                    g.GreenPAK.pin[i].type = "GND";
                    g.GreenPAK.pin[i].description = "Ground";
                }
                else if (g.GreenPAK.dual_supply.Equals(true) && g.GreenPAK.pin[i].pin_type.Equals("VDD2"))
                {
                    g.doc.Variables["pin" + i.ToString() + "_label"].Value = "VDD2";
                    g.GreenPAK.pin[i].name = "VDD2";
                    g.GreenPAK.pin[i].resistor = "--";
                    g.GreenPAK.pin[i].type = "PWR";
                    g.GreenPAK.pin[i].description = "Supply Voltage";
                }
                else
                {
                    // Look for xml elements called "item" with caption "PIN ?" and check if they have textLabel Element
                    foreach (XElement pin in g.ELEMENT.Descendants("item")
                        .Where(pin => (string)pin.Attribute("caption") == ("PIN " + (i).ToString())))
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
                            switch (g.GreenPAK.PAK_family)
                            {
                                case 5: pin_config_PAK5(i, g.GreenPAK.pin[i].address, g.GreenPAK.pin[i].pin_type); break;
                                case 4: pin_config_PAK4(i, g.GreenPAK.pin[i].address, g.GreenPAK.pin[i].pin_type); break;
                                case 3: break;  //###
                            }
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
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Output Summary
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Creating Output Summary");

        if (g.pin_settings_update)
        {
            string output_summary = ".";

            if (g.VDD.PP1x > 0) { output_summary += "\n\r" + (g.VDD.PP1x + g.VDD2.PP1x).ToString() + " Output \u2014 Push Pull 1x"; }
            if (g.VDD.PP2x > 0) { output_summary += "\n\r" + (g.VDD.PP2x + g.VDD2.PP2x).ToString() + " Output \u2014 Push Pull 2x"; }
            if (g.VDD.PP4x > 0) { output_summary += "\n\r" + (g.VDD.PP4x + g.VDD2.PP4x).ToString() + " Output \u2014 Push Pull 4x"; }
            if (g.VDD.ODN1x > 0) { output_summary += "\n\r" + (g.VDD.ODN1x + g.VDD2.ODN1x).ToString() + " Output \u2014 Open Drain NMOS 1x"; }
            if (g.VDD.ODN2x > 0) { output_summary += "\n\r" + (g.VDD.ODN2x + g.VDD2.ODN2x).ToString() + " Output \u2014 Open Drain NMOS 2x"; }
            if (g.VDD.ODN4x > 0) { output_summary += "\n\r" + (g.VDD.ODN4x + g.VDD2.ODN4x).ToString() + " Output \u2014 Open Drain NMOS 4x"; }
            if (g.VDD.ODP1x > 0) { output_summary += "\n\r" + (g.VDD.ODP1x + g.VDD2.ODP1x).ToString() + " Output \u2014 Open Drain PMOS 1x"; }
            if (g.VDD.ODP2x > 0) { output_summary += "\n\r" + (g.VDD.ODP2x + g.VDD2.ODP2x).ToString() + " Output \u2014 Open Drain PMOS 2x"; }

            g.doc.Variables["output_summary"].Value = output_summary.Substring(3);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Pin Configuration Table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Populating Pin Configuration Table");

        if (g.pin_labels_update || g.pin_settings_update)
        {
            foreach (Table table in g.doc.Tables)
            {
                if (table.Title.Equals("pin_configuration"))
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
                        table.Cell(i + 1, 3).Range.Text = g.GreenPAK.pin[i].description;
                        table.Cell(i + 1, 4).Range.Text = g.GreenPAK.pin[i].type;
                        table.Cell(i + 1, 5).Range.Text = g.GreenPAK.pin[i].resistor;
                    }
                    break;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  First Page Pinout Diagram
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Populating First Page Pinout Diagram");

        if (g.new_part_update)
        {
            switch (g.GreenPAK.pin.Length - 1)
            {
                case 20:
                    foreach (Shape shape in g.doc.Shapes)
                    {
                        if (shape.Title.Equals("pinout_diagram"))
                        {
                            int left = (int)shape.Left;
                            int top = (int)shape.Top;
                            int width = (int)shape.Width;
                            int height = (int)shape.Height;

                            shape.Delete();

                            Shape newShape = g.doc.Shapes.AddPicture(g.templatePath + "STQFN_20.png");
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

                case 14:
                    foreach (Shape shape in g.doc.Shapes)
                    {
                        if (shape.Title.Equals("pinout_diagram"))
                        {
                            int left = (int)shape.Left;
                            int top = (int)shape.Top;
                            int width = (int)shape.Width;
                            int height = (int)shape.Height;

                            shape.Delete();

                            Shape newShape = g.doc.Shapes.AddPicture(g.templatePath + "STQFN_14.png");
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

                case 12:                            //###
                    break;

                case 8:                             //###
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
                {
                    if (table.Title.Equals("pin" + i.ToString() + "_label"))
                    {
                        pin_cell_update(table, i);
                        i++;
                    }
                }
                catch { }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Oscillators
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Loading OSC Settings");

        //### is there a better way to do this?

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
                switch (g.nvmData[g.GreenPAK.PAK5_osc0_src].ToString())
                {
                    case "0": g.GreenPAK.PAK5_osc0 = 25000; break;
                    case "1": g.GreenPAK.PAK5_osc0 = 2000000; break;
                }
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.PAK5_osc0_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.PAK5_osc0_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.PAK5_osc0 = g.GreenPAK.PAK5_osc0 / 1; break;
                    case "01": g.GreenPAK.PAK5_osc0 = g.GreenPAK.PAK5_osc0 / 2; break;
                    case "10": g.GreenPAK.PAK5_osc0 = g.GreenPAK.PAK5_osc0 / 4; break;
                    case "11": g.GreenPAK.PAK5_osc0 = g.GreenPAK.PAK5_osc0 / 8; break;
                }

                switch (g.nvmData[g.GreenPAK.PAK5_osc0_force_on].ToString())
                {
                    case "0": break;        // ### still need this? Don't think so
                    case "1": break;
                }

                //////////////////////////////////////////////////
                //  OSC1
                //////////////////////////////////////////////////
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.PAK5_osc1_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.PAK5_osc1_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.PAK5_osc1 = g.GreenPAK.PAK5_osc1 / 1; break;
                    case "01": g.GreenPAK.PAK5_osc1 = g.GreenPAK.PAK5_osc1 / 2; break;
                    case "10": g.GreenPAK.PAK5_osc1 = g.GreenPAK.PAK5_osc1 / 4; break;
                    case "11": g.GreenPAK.PAK5_osc1 = g.GreenPAK.PAK5_osc1 / 8; break;
                }

                switch (g.nvmData[g.GreenPAK.PAK5_osc1_force_on].ToString())
                {
                    case "0": break;        // ### still need this?
                    case "1": break;
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
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.PAK4_LF_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.PAK4_LF_osc_pre_div + 1].ToString())
                {
                    case "00": g.GreenPAK.PAK4_LF_osc = g.GreenPAK.PAK4_LF_osc / 1; break;
                    case "01": g.GreenPAK.PAK4_LF_osc = g.GreenPAK.PAK4_LF_osc / 2; break;
                    case "10": g.GreenPAK.PAK4_LF_osc = g.GreenPAK.PAK4_LF_osc / 4; break;
                    case "11": g.GreenPAK.PAK4_LF_osc = g.GreenPAK.PAK4_LF_osc / 16; break;
                }

                //////////////////////////////////////////////////
                //  RC OSC
                //////////////////////////////////////////////////
                switch (g.nvmData[g.GreenPAK.PAK4_RC_osc_src].ToString())
                {
                    case "0": g.GreenPAK.PAK4_RC_osc = 25000; break;
                    case "1": g.GreenPAK.PAK4_RC_osc = 2000000; break;
                }
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.PAK4_RC_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.PAK4_RC_osc_pre_div + 0].ToString())
                {
                    case "00": g.GreenPAK.PAK4_RC_osc = g.GreenPAK.PAK4_RC_osc / 1; break;
                    case "01": g.GreenPAK.PAK4_RC_osc = g.GreenPAK.PAK4_RC_osc / 2; break;
                    case "10": g.GreenPAK.PAK4_RC_osc = g.GreenPAK.PAK4_RC_osc / 4; break;
                    case "11": g.GreenPAK.PAK4_RC_osc = g.GreenPAK.PAK4_RC_osc / 8; break;
                }

                //////////////////////////////////////////////////
                //  RING OSC
                //////////////////////////////////////////////////
                // Pre-divider
                switch (g.nvmData[g.GreenPAK.PAK4_RING_osc_pre_div + 1].ToString() +
                        g.nvmData[g.GreenPAK.PAK4_RING_osc_pre_div + 1].ToString())
                {
                    case "00": g.GreenPAK.PAK4_RING_osc = g.GreenPAK.PAK4_RING_osc / 1; break;
                    case "01": g.GreenPAK.PAK4_RING_osc = g.GreenPAK.PAK4_RING_osc / 4; break;
                    case "10": g.GreenPAK.PAK4_RING_osc = g.GreenPAK.PAK4_RING_osc / 8; break;
                    case "11": g.GreenPAK.PAK4_RING_osc = g.GreenPAK.PAK4_RING_osc / 16; break;
                }
            }

            //////////////////////////////////////////////////
            //  GreenPAK3
            //////////////////////////////////////////////////
            else if (g.GreenPAK.PAK_family.Equals(3))
            {

            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Counters
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Loading Counter settings");

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
                            g.nvmData[g.GreenPAK.cnt[i].selected].ToString().Equals("1"))
                        {
                            g.GreenPAK.cnt[i].used = true;
                            counter_config_PAK5(i);
                            break;
                        }
                        else if (g.GreenPAK.PAK_family.Equals(4))
                        {
                            switch (g.nvmData[g.GreenPAK.cnt[i].selected + 1].ToString() +
                                    g.nvmData[g.GreenPAK.cnt[i].selected + 0].ToString())
                            {
                                case "00":
                                case "01":
                                    g.GreenPAK.cnt[i].used = true;
                                    counter_config_PAK4(i);
                                    break;
                            }
                            break;
                        }
                        else if (g.GreenPAK.PAK_family.Equals(3))
                        {
                        }
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  ACMPs
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Loading ACMP settings");

        if (g.ACMPs_update)
        {
            for (int i = 0; i < g.GreenPAK.acmp.Length; i++)
            {
                foreach (XElement acmp in g.ELEMENT.Descendants("item")
                    .Where(acmp => acmp.Attribute("caption").Value.Equals("A CMP" + (i).ToString())))
                {
                    if (acmp.Element("graphics").Attribute("hidden").Value.Equals("0"))
                    {
                        g.GreenPAK.acmp[i].used = true;
                        switch (g.GreenPAK.PAK_family)
                        {
                            case 5: acmp_config_PAK5(i); break;
                            case 4: acmp_config_PAK4(i); break;
                            case 3: break; //###
                        }
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
        //  Electrical Characteristics Table
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(3, "Populating EC Table");

        int row = 0;
        int symbolRow = 0;

        string VDD_DB = null;
        double VDD_typ = Convert.ToDouble(g.GreenPAK.vdd.typ);

        if ((Math.Abs(VDD_typ - 1.8) < Math.Abs(VDD_typ - 3.3)) &&
            (Math.Abs(VDD_typ - 1.8) < Math.Abs(VDD_typ - 5.0)))
        {
            VDD_DB = "1_8";
            VDD_typ = 1.8;
        }
        else if ((Math.Abs(VDD_typ - 3.3) < Math.Abs(VDD_typ - 1.8)) &&
                 (Math.Abs(VDD_typ - 3.3) < Math.Abs(VDD_typ - 5.0)))
        {
            VDD_DB = "3_3";
            VDD_typ = 3.3;
        }
        else if ((Math.Abs(VDD_typ - 5.0) < Math.Abs(VDD_typ - 1.8)) &&
                 (Math.Abs(VDD_typ - 5.0) < Math.Abs(VDD_typ - 3.3)))
        {
            VDD_DB = "5_0";
            VDD_typ = 3.3;
        }

        string VDD2_DB = null;
        double VDD2_typ = Convert.ToDouble(g.GreenPAK.vdd2.typ);
        if (g.GreenPAK.dual_supply)
        {
            if ((Math.Abs(VDD2_typ - 1.8) < Math.Abs(VDD2_typ - 3.3)) &&
                (Math.Abs(VDD2_typ - 1.8) < Math.Abs(VDD2_typ - 5.0)))
            {
                VDD2_DB = "1_8";
                VDD2_typ = 1.8;
            }
            else if ((Math.Abs(VDD2_typ - 3.3) < Math.Abs(VDD2_typ - 1.8)) &&
                     (Math.Abs(VDD2_typ - 3.3) < Math.Abs(VDD2_typ - 5.0)))
            {
                VDD2_DB = "3_3";
                VDD2_typ = 3.3;
            }
            else if ((Math.Abs(VDD2_typ - 5.0) < Math.Abs(VDD2_typ - 1.8)) &&
                     (Math.Abs(VDD2_typ - 5.0) < Math.Abs(VDD2_typ - 3.3)))
            {
                VDD2_DB = "5_0";
                VDD2_typ = 3.3;
            }
        }

        g.connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + g.templatePath + "Base_Die_DB.accdb;";
        while (true)
        {
            try
            {
                g.connection.Open();
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(3, "Connection Successful");
                g.connection.Close();
                break;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                form.backgroundWorker.ReportProgress(0, "Error: Connection Unsuccessful. Close the Database!");
                e.Cancel = true;
                return;
            }
        }

        g.connection.Open();

        foreach (Table table in g.doc.Tables)
        {
            if (table.Title.Equals("ec_table"))
            {
                //table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitContent);

                if (g.pin_settings_update || g.temp_vdd_update)
                {
                    if (g.GreenPAK.dual_supply)
                    {
                        symbolRow = EC_row_symbol(table, "VDD2");
                        row = symbolRow;

                        table.Cell(row, 2).Range.Text = "Supply Voltage";
                        table.Cell(row, 7).Range.Text = "V";

                        EC_row_populate(table, row, "VDD2 ≤ VDD",
                            g.GreenPAK.vdd2.min,
                            g.GreenPAK.vdd2.typ,
                            g.GreenPAK.vdd2.max);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_IH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: V_IH");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI)
                    {
                        EC_IH_IL(table, "VIH", VDD_DB, VDD_typ, g.VDD);
                    }
                    else if (g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        EC_IH_IL(table, "VIH2", VDD2_DB, VDD2_typ, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_IL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: V_IL");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI)
                    {
                        EC_IH_IL(table, "VIL", VDD_DB, VDD_typ, g.VDD);
                    }
                    else if (g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        EC_IH_IL(table, "VIL2", VDD2_DB, VDD2_typ, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_IH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: I_IH");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI ||
                        g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        symbolRow = EC_row_symbol(table, "IIH");
                        row = symbolRow;

                        table.Cell(row, 2).Range.Text = "HIGH-Level Input Current";
                        table.Cell(row, 7).Range.Text = "µA";

                        EC_row_populate(table, row, "Logic Input PINs; VIN = VDD",
                            accessQuery(VDD_DB, "IIH", null, "min"),
                            accessQuery(VDD_DB, "IIH", null, "typ"),
                            accessQuery(VDD_DB, "IIH", null, "max"));
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_IL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: I_IL");

                    if (g.VDD.woSchmitt || g.VDD.wSchmitt || g.VDD.LVDI ||
                        g.VDD2.woSchmitt || g.VDD2.wSchmitt || g.VDD2.LVDI)
                    {
                        symbolRow = EC_row_symbol(table, "IIL");
                        row = symbolRow;

                        table.Cell(row, 2).Range.Text = "LOW-Level Input Current";
                        table.Cell(row, 7).Range.Text = "µA";

                        EC_row_populate(table, row, "Logic Input PINs; VIN = 0V",
                            accessQuery(VDD_DB, "IIL", null, "min"),
                            accessQuery(VDD_DB, "IIL", null, "typ"),
                            accessQuery(VDD_DB, "IIL", null, "max"));
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_HYS
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: V_HYS");
                    if (g.VHYS)
                    {
                        symbolRow = EC_row_symbol(table, "VHYS");
                        row = symbolRow;

                        table.Cell(row, 2).Range.Text = "Schmitt Trigger Hysteresis Voltage";
                        table.Cell(row, 7).Range.Text = "V";

                        EC_row_populate(table, row, "Logic Input with Schmitt Trigger at VDD = " + VDD_typ.ToString() + "V",
                            accessQuery(VDD_DB, "VHYS", null, "min"),
                            accessQuery(VDD_DB, "VHYS", null, "typ"),
                            accessQuery(VDD_DB, "VHYS", null, "max"));
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_OH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: V_OH");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODP1x > 0 || g.VDD.ODP2x > 0)
                    {
                        EC_OH_OL(table, "VOH", VDD_DB, VDD_typ, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODP1x > 0 || g.VDD2.ODP2x > 0)
                    {
                        EC_OH_OL(table, "VOH2", VDD2_DB, VDD2_typ, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  V_OL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: V_OL");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODN1x > 0 || g.VDD.ODN2x > 0 || g.VDD.ODN4x > 0)
                    {
                        EC_OH_OL(table, "VOL", VDD_DB, VDD_typ, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODN1x > 0 || g.VDD2.ODN2x > 0 || g.VDD2.ODN4x > 0)
                    {
                        EC_OH_OL(table, "VOL2", VDD2_DB, VDD2_typ, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_OH
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: I_OH");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODP1x > 0 || g.VDD.ODP2x > 0)
                    {
                        EC_OH_OL(table, "IOH", VDD_DB, VDD_typ, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODP1x > 0 || g.VDD2.ODP2x > 0)
                    {
                        EC_OH_OL(table, "IOH2", VDD2_DB, VDD2_typ, g.VDD2);
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    //  I_OL
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (worker.CancellationPending) { e.Cancel = true; return; }
                    form.backgroundWorker.ReportProgress(3, "Populating EC Table: I_OL");

                    if (g.VDD.PP1x > 0 || g.VDD.PP2x > 0 || g.VDD.ODN1x > 0 || g.VDD.ODN2x > 0 || g.VDD.ODN4x > 0)
                    {
                        EC_OH_OL(table, "IOL", VDD_DB, VDD_typ, g.VDD);
                    }
                    if (g.VDD2.PP1x > 0 || g.VDD2.PP2x > 0 || g.VDD2.ODN1x > 0 || g.VDD2.ODN2x > 0 || g.VDD2.ODN4x > 0)
                    {
                        EC_OH_OL(table, "IOL2", VDD2_DB, VDD2_typ, g.VDD2);
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  CNTs/DLYs
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // ### Update this with Access Data at some point

                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(3, "Populating EC Table: CNTs/DLYs");
                if (g.CNTs_DLYs_update)
                {
                    for (int i = 0; i < g.GreenPAK.cnt.Length; i++)
                    {
                        if (g.GreenPAK.cnt[i].used)
                        {
                            symbolRow = EC_row_symbol(table, "t" + g.GreenPAK.cnt[i].mode_alt + i.ToString());
                            row = symbolRow;
                            table.Cell(row, 2).Range.Text = g.GreenPAK.cnt[i].mode + " " + i.ToString() + " Time";
                            table.Cell(row, 7).Range.Text = g.GreenPAK.cnt[i].timeSI;

                            Console.WriteLine(g.GreenPAK.cnt[i].time.typ);
                            EC_row_populate(table, row, "At temperature " + g.GreenPAK.temp.typ + "\u00B0C",
                                "--",
                                g.GreenPAK.cnt[i].time.typ,
                                "--");
                            EC_row_split(table, row);
                            row++;
                            //EC_row_populate(table, row,
                            //    "At temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C",
                            //    "--",
                            //    g.GreenPAK.cnt[i].time.typ,
                            //    "--");
                            //EC_row_split(table, row);
                            //row++;

                            EC_row_merge(table, symbolRow, row);
                        }
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  ACMPs
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // ### Update this with Access Data at some point

                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(3, "Populating EC Table: ACMPs");
                if (g.ACMPs_update)
                {
                    for (int i = 0; i < g.GreenPAK.acmp.Length; i++)
                    {
                        if (g.GreenPAK.acmp[i].used)
                        {
                            symbolRow = EC_row_symbol(table, "VACMP" + i.ToString());
                            row = symbolRow;
                            EC_clearSection(table, row);
                            table.Cell(row, 2).Range.Text = "Analog Comparator" + i.ToString() + " Threshold Voltage";
                            table.Cell(row, 7).Range.Text = "mV";

                            EC_row_populate(table, row, "Low to High transition at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].acmpVIH, "--");
                            EC_row_split(table, row);
                            row++;
                            //EC_row_populate(table, row, "Low to High transition at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].threshold, "--");
                            //EC_row_split(table, row);
                            //row++;
                            EC_row_populate(table, row, "High to Low transition at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].acmpVIL, "--");
                            EC_row_split(table, row);
                            row++;
                            //EC_row_populate(table, row, "High to Low transition at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].threshold, "--");
                            //EC_row_split(table, row);
                            //row++;

                            EC_row_merge(table, symbolRow, row);

                            if (g.GreenPAK.acmp[i].hysteresis != "0")
                            {
                                symbolRow = EC_row_symbol(table, "VHYST" + i.ToString());
                                row = symbolRow;
                                EC_clearSection(table, row);
                                table.Cell(row, 2).Range.Text = "Analog Comparator" + i.ToString() + " Hysteresis Voltage (note 1)";
                                table.Cell(row, 7).Range.Text = "mV";

                                EC_row_populate(table, row, "ACMP" + i.ToString() + " at temperature 25\u00B0C", "--", g.GreenPAK.acmp[i].hysteresis, "--");
                                EC_row_split(table, row);
                                row++;
                                //EC_row_populate(table, row, "ACMP" + i.ToString() + " at temperature " + g.GreenPAK.temp.min + "\u00B0C" + " to " + g.GreenPAK.temp.max + "\u00B0C", "--", g.GreenPAK.acmp[i].hysteresis, "--");
                                //EC_row_split(table, row);
                                //row++;

                                EC_row_merge(table, symbolRow, row);
                            }
                        }
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  TSU / PONTHR / POFFTHR
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(3, "Populating EC Table: T_SU / P_ONTHR / P_OFFTHR");

                if (g.temp_vdd_update || g.new_part_update)
                {
                    symbolRow = EC_row_symbol(table, "TSU");
                    row = symbolRow;
                    table.Cell(row, 2).Range.Text = "Start up Time";
                    table.Cell(row, 7).Range.Text = "ms";

                    EC_row_populate(table, row, "From VDD rising past PONTHR",
                        accessQuery(VDD_DB, "TSU", null, "min"),
                        accessQuery(VDD_DB, "TSU", null, "typ"),
                        accessQuery(VDD_DB, "TSU", null, "max"));

                    symbolRow = EC_row_symbol(table, "PONTHR");
                    row = symbolRow;
                    table.Cell(row, 2).Range.Text = "Power On Threshold";
                    table.Cell(row, 7).Range.Text = "V";

                    EC_row_populate(table, row, "VDD Level Required to Start Up the Chip",
                        accessQuery(VDD_DB, "PONTHR", null, "min"),
                        accessQuery(VDD_DB, "PONTHR", null, "typ"),
                        accessQuery(VDD_DB, "PONTHR", null, "max"));

                    symbolRow = EC_row_symbol(table, "POFFTHR");
                    row = symbolRow;
                    table.Cell(row, 2).Range.Text = "Power OFF Threshold";
                    table.Cell(row, 7).Range.Text = "V";

                    EC_row_populate(table, row, "VDD Level Required to Switch Off the Chip",
                        accessQuery(VDD_DB, "POFFTHR", null, "min"),
                        accessQuery(VDD_DB, "POFFTHR", null, "typ"),
                        accessQuery(VDD_DB, "POFFTHR", null, "max"));
                }

                // ### insert PAK specific stuff like the PAK5 state machine

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //  ASM Asynchronous State Machine
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                if (worker.CancellationPending) { e.Cancel = true; return; }
                form.backgroundWorker.ReportProgress(3, "Populating EC Table: ASM");
                if (g.asm_used && g.new_part_update)
                {
                    row = EC_row_symbol(table, "tst_out_delay");
                    table.Cell(row, 2).Range.Text = "State Machine Output Delay";
                    EC_row_populate(table, row, "",
                        accessQuery(VDD_DB, "tst_out_delay", null, "min"),
                        accessQuery(VDD_DB, "tst_out_delay", null, "typ"),
                        accessQuery(VDD_DB, "tst_out_delay", null, "max"));
                    table.Cell(row, 7).Range.Text = "ns";

                    row = EC_row_symbol(table, "tst_out");
                    table.Cell(row, 2).Range.Text = "State Machine Output Transition Time";
                    EC_row_populate(table, row, "",
                        accessQuery(VDD_DB, "tst_out", null, "min"),
                        accessQuery(VDD_DB, "tst_out", null, "typ"),
                        accessQuery(VDD_DB, "tst_out", null, "max"));
                    table.Cell(row, 7).Range.Text = "ns";

                    row = EC_row_symbol(table, "tst_pulse");
                    table.Cell(row, 2).Range.Text = "State Machine Input Pulse Acceptance Time";
                    EC_row_populate(table, row, "",
                        accessQuery(VDD_DB, "tst_pulse", null, "min"),
                        accessQuery(VDD_DB, "tst_pulse", null, "typ"),
                        accessQuery(VDD_DB, "tst_pulse", null, "max"));
                    table.Cell(row, 7).Range.Text = "ns";

                    row = EC_row_symbol(table, "tst_comp");
                    table.Cell(row, 2).Range.Text = "State Machine Input Compete Time";
                    EC_row_populate(table, row, "",
                        accessQuery(VDD_DB, "tst_comp", null, "min"),
                        accessQuery(VDD_DB, "tst_comp", null, "typ"),
                        accessQuery(VDD_DB, "tst_comp", null, "max"));
                    table.Cell(row, 7).Range.Text = "ns";
                }

                g.connection.Close();
                table.Rows.SetHeight(1, WdRowHeightRule.wdRowHeightAuto);
                break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Top Marking, Tape & Reel
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (g.new_part_update)
        {
            g.doc.Variables["GreenPAK_Net_Weight"].Value = g.GreenPAK.package_weight;
            g.doc.Variables["TM_part_code"].Value = g.TM_part_code;
            g.doc.Variables["TM_revision"].Value = g.TM_revision;

            foreach (InlineShape shape in g.doc.InlineShapes)
            {
                if (shape.Title.Equals("TM")) { shapeReplace(shape, "TM"); }
                else if (shape.Title.Equals("size")) { shapeReplace(shape, "size"); }
                else if (shape.Title.Equals("TR_specs")) { shapeReplace(shape, "TR_specs"); }
                else if (shape.Title.Equals("TR")) { shapeReplace(shape, "TR"); }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  End of parsing, update fields, close the file, quit Word, open the file for user
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        if (worker.CancellationPending) { e.Cancel = true; return; }
        form.backgroundWorker.ReportProgress(100, "Saving and Opening DataSheet File");

        saveFileAndOpen();
        // BackgroundWorker stops, GUI closes, return to main
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