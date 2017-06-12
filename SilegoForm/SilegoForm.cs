using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SilegoForm
{
    public partial class SilegoForm : Form
    {
        public myPinBox[] Pin_boxes = new myPinBox[33];

        public SilegoForm()
        {
            InitializeComponent();
            //MaximumSize = new System.Drawing.Size(400, 420);
            Location = new System.Drawing.Point(800, 0);
            new_part_checkbox.Checked = true;           //### Erase this later?

            initialize_pin_boxes();                     //### move this later?
        }

        private void ACMPs_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ACMPs_checkbox.Checked) MainProgram.g.ACMPs_update = true;
            else MainProgram.g.ACMPs_update = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            MainProgram.theProgram(worker, e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar.Increment(e.ProgressPercentage);

            string message = (string)e.UserState;

            if (message.StartsWith("Error:"))
            {
                error_label.Visible = true;
                error_label.Text = message;
            }
            else status_label.Text = message;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                status_label.Text = "Operation cancelled.";
                MainProgram.closeDontSave();

                new_part_checkbox.Enabled = true;

                GP_button.Enabled = true;
                DS_button.Enabled = true;

                part_number_textbox.ReadOnly = false;
                customer_name_textbox.ReadOnly = false;
                project_name_textbox.ReadOnly = false;

                I_Q_checkbox.Enabled = true;
                I_Q_condition_checkbox.Enabled = true;

                DS_rev_combobox.Enabled = true;
                DRH_textbox.ReadOnly = false;

                pin_labels_checkbox.Enabled = true;
                pin_settings_checkbox.Enabled = true;
                CNTs_DLYs_checkbox.Enabled = true;
                ACMPs_checkbox.Enabled = true;
                TM_Part_Code_checkbox.Enabled = true;
                TM_Part_Code_textbox.ReadOnly = false;
                TM_Revision_checkbox.Enabled = true;
                TM_Revision_textbox.ReadOnly = false;

                start_button.Enabled = true;
                start_button.Visible = true;
                cancel_button.Enabled = false;
                cancel_button.Visible = false;
                error_label.Visible = true;
                status_label.Visible = true;
                progressBar.Visible = true;
                progressBar.Value = 0;
            }
            else
            {
                error_label.Text = status_label.Text;
                status_label.Text = "Done!";
                MainProgram.closeDontSave();

                new_part_checkbox.Enabled = true;

                GP_button.Enabled = true;
                DS_button.Enabled = true;

                part_number_textbox.ReadOnly = false;
                customer_name_textbox.ReadOnly = false;
                project_name_textbox.ReadOnly = false;

                I_Q_checkbox.Enabled = true;
                I_Q_condition_checkbox.Enabled = true;

                DS_rev_combobox.Enabled = true;
                DRH_textbox.ReadOnly = false;

                pin_labels_checkbox.Enabled = true;
                pin_settings_checkbox.Enabled = true;
                CNTs_DLYs_checkbox.Enabled = true;
                ACMPs_checkbox.Enabled = true;
                TM_Part_Code_checkbox.Enabled = true;
                TM_Part_Code_textbox.ReadOnly = false;
                TM_Revision_checkbox.Enabled = true;
                TM_Revision_textbox.ReadOnly = false;

                start_button.Enabled = true;
                start_button.Visible = true;
                cancel_button.Enabled = false;
                cancel_button.Visible = false;
                error_label.Visible = true;
                status_label.Visible = true;
                progressBar.Visible = true;
                progressBar.Value = 100;
            }
        }

        public void progressIncrement(int value)
        {
            progressBar.Increment(value);
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void CNTs_DLYs_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (CNTs_DLYs_checkbox.Checked) MainProgram.g.CNTs_DLYs_update = true;
            else MainProgram.g.CNTs_DLYs_update = false;
        }

        private void customer_name_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.customer_name = customer_name_textbox.Text;
        }

        private void DRH_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.DRH_text = DRH_textbox.Text;
        }

        private void DS_file_textbox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(MainProgram.g.DataSheet_File);
            MainProgram.g.DataSheet_File = DS_file_textbox.Text;
        }

        private void DS_rev_CheckedChanged(object sender, EventArgs e)
        {
            //if (DS_rev_checkbox.Checked)
            //{
            //    DS_rev_combobox.Enabled = true;
            //}
            //else
            //{
            //    //DS_rev_combobox.Enabled = false;
            //    DS_rev_checkbox.Checked = true;
            //}
        }

        private void DS_rev_combobox_MouseHover(object sender, EventArgs e)
        {
            DS_rev_combobox.Focus();
        }

        private void DS_rev_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainProgram.g.DS_rev = DS_rev_combobox.Text;
            Console.WriteLine("DS Rev: " + MainProgram.g.DS_rev);
        }

        private void GP_file_textbox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(MainProgram.g.GreenPAK_File);
            MainProgram.g.GreenPAK_File = GP_file_textbox.Text;
            string[] returnedArray = MainProgram.load_GP();

            base_die_textbox.Text = returnedArray[0];
            part_number_textbox.Text = returnedArray[1];
            customer_name_textbox.Text = returnedArray[2];
            project_name_textbox.Text = returnedArray[3];
            vdd_min_textbox.Text = returnedArray[4];
            vdd_typ_textbox.Text = returnedArray[5];
            vdd_max_textbox.Text = returnedArray[6];
            vdd2_min_textbox.Text = returnedArray[7];
            vdd2_typ_textbox.Text = returnedArray[8];
            vdd2_max_textbox.Text = returnedArray[9];
            temp_min_textbox.Text = returnedArray[10];
            temp_typ_textbox.Text = returnedArray[11];
            temp_max_textbox.Text = returnedArray[12];
            lock_status_textbox.Text = returnedArray[13];
            pattern_id_textbox.Text = returnedArray[14];

            for (int i = 1; i < 33; i++)
            {
                PinTableLayoutPanel.Controls.Remove(Pin_boxes[i]);
            }

            for (int i = 1; i < MainProgram.g.GreenPAK.pin.Length; i++)
            {
                configure_pin_boxes(i);
            }

            if (new_part_checkbox.Checked)
            {
                DRH_textbox.Text = "New design for " + base_die_textbox.Text + ".";
            }
            else
            {
                DRH_textbox.Text = "";
            }
        }

        public void initialize_pin_boxes()
        {
            for (int i = 0; i < 32; i++)
            {
                Pin_boxes[i] = new myPinBox();
            }
        }

        public void configure_pin_boxes(int i)
        {
            Pin_boxes[i].AutoSize = true;
            Pin_boxes[i].AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Pin_boxes[i].Pin_box.Text = "Pin " + i.ToString().PadLeft(2, '0');

            Pin_boxes[i].Pin_label_textbox.Text = MainProgram.g.GreenPAK.pin[i].label;
            Pin_boxes[i].Pin_type_textbox.Text = MainProgram.g.GreenPAK.pin[i].type;
            Pin_boxes[i].Pin_resistor_textbox.Text = MainProgram.g.GreenPAK.pin[i].resistor;
            Pin_boxes[i].Pin_description_textbox.Text = MainProgram.g.GreenPAK.pin[i].description;

            PinTableLayoutPanel.Controls.Add(Pin_boxes[i], 0, i - 1);
        }

        private void help_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (help_checkbox.Checked)
            {
                Size = new System.Drawing.Size(800, 750);
                MaximumSize = new System.Drawing.Size(800, 750);
                MinimumSize = new System.Drawing.Size(800, 750);
                help_textbox.Visible = true;
            }
            else if (!help_checkbox.Checked)
            {
                Size = new System.Drawing.Size(540, 750);
                MaximumSize = new System.Drawing.Size(540, 750);
                MinimumSize = new System.Drawing.Size(540, 750);
                help_textbox.Visible = false;
            }
        }

        private void I_Q_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (I_Q_checkbox.Checked)
            {
                MainProgram.g.I_Q_update = true;
                I_Q_textbox.ReadOnly = false;
                I_Q_textbox.Focus();
            }
            else
            {
                MainProgram.g.I_Q_update = false;
                I_Q_textbox.ReadOnly = true;
            }
        }

        private void I_Q_condition_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (I_Q_condition_checkbox.Checked)
            {
                MainProgram.g.I_Q_condition_update = true;
                I_Q_condition_textbox.ReadOnly = false;
                I_Q_condition_textbox.Focus();
            }
            else
            {
                MainProgram.g.I_Q_update = false;
                I_Q_condition_textbox.ReadOnly = true;
            }
        }

        private void I_Q_condition_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.I_Q_condition = I_Q_condition_textbox.Text;
        }

        private void I_Q_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.I_Q = I_Q_textbox.Text;
        }

        private void new_part_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (new_part_checkbox.Checked)
            {
                Console.WriteLine("new part checked");

                MainProgram.g.new_part_update = true;
                pin_labels_checkbox.Checked = true;
                pin_settings_checkbox.Checked = true;
                CNTs_DLYs_checkbox.Checked = true;
                ACMPs_checkbox.Checked = true;

                if (GP_file_textbox.Text != "Drop GP File")
                {
                    DRH_textbox.Text = "New Design for " + MainProgram.g.GreenPAK.base_die + ".";
                }

                status_label.Visible = true;
                status_label.Text = "New Part. Loaded New_DS_Template.docx";
                //DS_file_textbox.Text = @"P:\Apps_Tools\New_DS_Template\New_DS_Template.docx";
                DS_file_textbox.Text = MainProgram.g.templatePath + @"\New_DS_Template.docx";
                DS_button.Enabled = false;
            }
            else
            {
                Console.WriteLine("new part not checked");

                MainProgram.g.new_part_update = false;
                pin_labels_checkbox.Checked = false;
                pin_settings_checkbox.Checked = false;
                CNTs_DLYs_checkbox.Checked = false;
                ACMPs_checkbox.Checked = false;
                DRH_textbox.Text = "";

                status_label.Visible = false;
                status_label.Text = "";
                DS_file_textbox.Text = "Drop DS file";
                DS_button.Enabled = true;
            }
        }

        private void part_number_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.part_number = part_number_textbox.Text;
        }

        private void pin_labels_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (pin_labels_checkbox.Checked) MainProgram.g.pin_labels_update = true;
            else MainProgram.g.pin_labels_update = false;
        }

        private void pin_settings_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (pin_settings_checkbox.Checked) MainProgram.g.pin_settings_update = true;
            else MainProgram.g.pin_settings_update = false;
        }

        private void project_name_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.project_name = project_name_textbox.Text;
        }

        private void Select_DS_Click(object sender, EventArgs e)
        {
            if (select_DS_file.ShowDialog() == DialogResult.OK && (
                select_DS_file.FileName.EndsWith(".docx") ||
                select_DS_file.FileName.EndsWith(".doc")))
            {
                //DS_file_textbox.Text = select_DS_file.FileName.Substring(select_DS_file.FileName.LastIndexOf("\\"));
                DS_file_textbox.Text = select_DS_file.FileName;
                MainProgram.g.DataSheet_File = select_DS_file.FileName;
                error_label.Visible = false;
            }
            else
            {
                Console.WriteLine("Wrong fileType for DS!");
                error_label.Visible = true;
                error_label.Text = "Wrong fileType for DS!";
            }
        }

        private void Select_GP_Click(object sender, EventArgs e)
        {
            if (select_GP_file.ShowDialog() == DialogResult.OK && (
                select_GP_file.FileName.EndsWith(".gp6") ||
                select_GP_file.FileName.EndsWith(".gp5") ||
                select_GP_file.FileName.EndsWith(".gp4") ||
                select_GP_file.FileName.EndsWith(".gp3")))
            {
                //GP_file_textbox.Text = select_GP_file.FileName.Substring(select_GP_file.FileName.LastIndexOf("\\"));
                GP_file_textbox.Text = select_GP_file.FileName;
                MainProgram.g.GreenPAK_File = select_GP_file.FileName;
                error_label.Visible = false;
            }
            else
            {
                Console.WriteLine("Wrong fileType for GP!");
                error_label.Visible = true;
                error_label.Text = "Wrong fileType for GP!";
            }
        }

        private void SilegoForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < files.Length; i++)
            {
                if ((files[i].EndsWith(".docx") || files[i].EndsWith(".doc")) && new_part_checkbox.Checked)
                {
                    error_label.Visible = true;
                    error_label.Text = "Warning: New Part is selected.";
                }
                else if (files[i].EndsWith(".docx") || files[i].EndsWith(".doc"))
                {
                    error_label.Visible = false;
                    DS_file_textbox.Text = files[i];
                }
                else if (files[i].EndsWith(".gp6") || files[i].EndsWith(".gp5") || files[i].EndsWith(".gp4") || files[i].EndsWith(".gp3"))
                {
                    error_label.Visible = false;

                    if (GP_file_textbox.Text == files[i])
                    {
                        GP_file_textbox_TextChanged(this, EventArgs.Empty);
                    }
                    else
                    {
                        GP_file_textbox.Text = files[i];
                    }
                }
                else
                {
                    error_label.Visible = true;
                    error_label.Text = "Wrong file type!";
                }
            }
        }

        private void SilegoForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void SilegoForm_Load(object sender, EventArgs e)
        {
            // Start the BackgroundWorker.
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(MainProgram.g.GreenPAK_File);
            //Console.WriteLine(MainProgram.g.DataSheet_File);

            if (DS_rev_combobox.Text == "")
            {
                error_label.Show();
                error_label.Text = "Select DS Rev";
                return;
            }
            if (DRH_textbox.Text == "")
            {
                error_label.Show();
                error_label.Text = "Add DS Rev History";
                return;
            }
            if (part_number_textbox.Text == "")
            {
                error_label.Show();
                error_label.Text = "Add Part Number";
                return;
            }
            if (customer_name_textbox.Text == "")
            {
                error_label.Show();
                error_label.Text = "Add Customer Name";
                return;
            }
            if (project_name_textbox.Text == "")
            {
                error_label.Show();
                error_label.Text = "Add Project Name";
                return;
            }

            new_part_checkbox.Enabled = false;

            GP_button.Enabled = false;
            DS_button.Enabled = false;

            part_number_textbox.ReadOnly = true;
            customer_name_textbox.ReadOnly = true;
            project_name_textbox.ReadOnly = true;

            I_Q_checkbox.Enabled = false;
            I_Q_textbox.ReadOnly = true;
            I_Q_condition_checkbox.Enabled = false;
            I_Q_condition_textbox.ReadOnly = true;

            DS_rev_combobox.Enabled = false;
            DRH_textbox.ReadOnly = true;

            pin_labels_checkbox.Enabled = false;
            pin_settings_checkbox.Enabled = false;
            CNTs_DLYs_checkbox.Enabled = false;
            ACMPs_checkbox.Enabled = false;
            TM_Part_Code_checkbox.Enabled = false;
            TM_Part_Code_textbox.ReadOnly = true;
            TM_Revision_checkbox.Enabled = false;
            TM_Revision_textbox.ReadOnly = true;

            start_button.Enabled = false;
            start_button.Visible = false;
            cancel_button.Enabled = true;
            cancel_button.Visible = true;
            error_label.Visible = false;
            error_label.Text = "";
            status_label.Visible = true;
            status_label.Text = "";
            progressBar.Visible = true;
            progressBar.Value = 0;

            backgroundWorker.RunWorkerAsync();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (misc_tab.SelectedTab == Pins_tab)
            {
                Pin_boxes[1].Pin_label_textbox.Focus();
            }
            if (misc_tab.SelectedTab == Project_Info_tab)
            {
                part_number_textbox.Focus();
            }
        }

        private void TM_Part_Code_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.TM_part_code = TM_Part_Code_textbox.Text;
        }

        private void TM_Revision_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.TM_revision = TM_Revision_textbox.Text;
        }

        private void TM_Part_Code_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (TM_Part_Code_checkbox.Checked)
            {
                MainProgram.g.TM_part_code_update = true;
                TM_Part_Code_textbox.ReadOnly = false;
                TM_Part_Code_textbox.Focus();
            }
            else
            {
                MainProgram.g.TM_part_code_update = false;
                TM_Part_Code_textbox.ReadOnly = true;
                TM_Part_Code_textbox.Text = "";
            }
        }

        private void TM_Revision_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (TM_Revision_checkbox.Checked)
            {
                MainProgram.g.TM_revision_update = true;
                TM_Revision_textbox.ReadOnly = false;
                TM_Revision_textbox.Focus();
            }
            else
            {
                MainProgram.g.TM_revision_update = false;
                TM_Revision_textbox.ReadOnly = true;
                TM_Revision_textbox.Text = "";
            }
        }
    }
}