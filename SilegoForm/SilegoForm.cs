﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SilegoForm
{
    public partial class SilegoForm : Form
    {
        public SilegoForm()
        {
            InitializeComponent();
            MaximumSize = new System.Drawing.Size(400, 420);
            new_part_checkbox.Checked = true;           //### Erase this later?
        }

        public void progressIncrement(int value)
        {
            progressBar.Increment(value);
        }

        private void ACMPs_CheckedChanged(object sender, EventArgs e)
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

                GP_button.Enabled = true;
                DS_button.Enabled = true;
                I_Q_textBox.ReadOnly = false;
                TM_Part_Code_textbox.ReadOnly = false;
                TM_Revision_textbox.ReadOnly = false;
                DS_rev_combobox.Enabled = true;
                start_button.Enabled = true;
                start_button.Visible = true;
                cancel_button.Visible = false;
                cancel_button.Enabled = false;

                new_part_checkbox.Enabled = true;
                metadata_checkbox.Enabled = true;
                pin_labels_checkbox.Enabled = true;
                temp_vdd_checkbox.Enabled = true;
                CNTs_DLYs_checkbox.Enabled = true;
                ACMPs_checkbox.Enabled = true;
                pin_settings_checkbox.Enabled = true;
                DS_rev_checkbox.Enabled = true;
                I_Q_checkbox.Enabled = true;
                TM_Part_Code_checkbox.Enabled = true;
                TM_Revision_checkbox.Enabled = true;
                lock_status_checkbox.Enabled = true;
                DS_rev_change_textbox.ReadOnly = false;

                progressBar.Visible = false;
                progressBar.Value = 0;
            }
            else
            {
                Close();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void CNTs_DLYs_CheckedChanged(object sender, EventArgs e)
        {
            if (CNTs_DLYs_checkbox.Checked) MainProgram.g.CNTs_DLYs_update = true;
            else MainProgram.g.CNTs_DLYs_update = false;
        }

        private void DS_rev_change_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.DS_rev_change = DS_rev_change_textbox.Text;
        }

        private void DS_rev_CheckedChanged(object sender, EventArgs e)
        {
            if (DS_rev_checkbox.Checked)
            {
                DS_rev_combobox.Enabled = true;
            }
            else
            {
                DS_rev_combobox.Enabled = false;
            }
        }

        private void DS_rev_combobox_MouseHover(object sender, EventArgs e)
        {
            DS_rev_combobox.Focus();
        }

        private void DS_rev_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainProgram.g.DS_rev = DS_rev_combobox.Text;
        }

        private void help_CheckedChanged(object sender, EventArgs e)
        {
            if (help_checkbox.Checked)
            {
                Size = new System.Drawing.Size(700, 500);
                MaximumSize = new System.Drawing.Size(700, 420);
                MinimumSize = new System.Drawing.Size(700, 420);
                help_textbox.Visible = true;
            }
            else if (!help_checkbox.Checked)
            {
                Size = new System.Drawing.Size(400, 500);
                MaximumSize = new System.Drawing.Size(400, 420);
                MinimumSize = new System.Drawing.Size(400, 420);
                help_textbox.Visible = false;
            }
        }

        private void I_Q_CheckedChanged(object sender, EventArgs e)
        {
            if (I_Q_checkbox.Checked)
            {
                MainProgram.g.I_Q_update = true;
                I_Q_textBox.Enabled = true;
            }
            else
            {
                MainProgram.g.I_Q_update = false;
                I_Q_textBox.Enabled = false;
            }
        }

        private void I_Q_TextBox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.I_Q = I_Q_textBox.Text;
        }

        private void lock_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (lock_status_checkbox.Checked) MainProgram.g.lock_status_update = true;
            else MainProgram.g.lock_status_update = false;
        }

        private void metadata_CheckedChanged(object sender, EventArgs e)
        {
            if (metadata_checkbox.Checked) MainProgram.g.metadata_update = true;
            else MainProgram.g.metadata_update = false;
        }

        private void new_part_CheckedChanged(object sender, EventArgs e)
        {
            if (new_part_checkbox.Checked)
            {
                MainProgram.g.new_part_update = true;
                metadata_checkbox.Checked = true;
                pin_labels_checkbox.Checked = true;
                temp_vdd_checkbox.Checked = true;
                CNTs_DLYs_checkbox.Checked = true;
                ACMPs_checkbox.Checked = true;
                pin_settings_checkbox.Checked = true;
                DS_rev_checkbox.Checked = true;
                I_Q_checkbox.Checked = true;
                lock_status_checkbox.Checked = true;
            }
            else
            {
                MainProgram.g.new_part_update = false;
                metadata_checkbox.Checked = false;
                pin_labels_checkbox.Checked = false;
                temp_vdd_checkbox.Checked = false;
                CNTs_DLYs_checkbox.Checked = false;
                ACMPs_checkbox.Checked = false;
                pin_settings_checkbox.Checked = false;
                DS_rev_checkbox.Checked = false;
                I_Q_checkbox.Checked = false;
                lock_status_checkbox.Checked = false;
            }
        }

        private void pin_labels_CheckedChanged(object sender, EventArgs e)
        {
            if (pin_labels_checkbox.Checked) MainProgram.g.pin_labels_update = true;
            else MainProgram.g.pin_labels_update = false;
        }

        private void pin_settings_CheckedChanged(object sender, EventArgs e)
        {
            if (pin_settings_checkbox.Checked) MainProgram.g.pin_settings_update = true;
            else MainProgram.g.pin_settings_update = false;
        }

        private void Select_DS_Click(object sender, EventArgs e)
        {
            if (select_DS_file.ShowDialog() == DialogResult.OK && (
                select_DS_file.FileName.EndsWith(".docx") ||
                select_DS_file.FileName.EndsWith(".doc")))
            {
                DS_file_textbox.Text = select_DS_file.FileName.Substring(select_DS_file.FileName.LastIndexOf("\\"));
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
                GP_file_textbox.Text = select_GP_file.FileName.Substring(select_GP_file.FileName.LastIndexOf("\\"));
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
                if (files[i].EndsWith(".docx") || files[i].EndsWith(".doc"))
                {
                    error_label.Visible = false;
                    MainProgram.g.DataSheet_File = files[i];
                    DS_file_textbox.Text = files[i].Substring(files[i].LastIndexOf("\\"));
                }
                else if (files[i].EndsWith(".gp6") || files[i].EndsWith(".gp5") || files[i].EndsWith(".gp4") || files[i].EndsWith(".gp3"))
                {
                    error_label.Visible = false;
                    MainProgram.g.GreenPAK_File = files[i];
                    GP_file_textbox.Text = files[i].Substring(files[i].LastIndexOf("\\"));
                }
                else
                {
                    error_label.Visible = true;
                    error_label.Text = "Wrong fileType!";
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

            GP_button.Enabled = false;
            DS_button.Enabled = false;
            I_Q_textBox.ReadOnly = true;
            TM_Part_Code_textbox.ReadOnly = true;
            TM_Revision_textbox.ReadOnly = true;
            DS_rev_combobox.Enabled = false;
            start_button.Enabled = false;
            start_button.Visible = false;
            cancel_button.Visible = true;
            cancel_button.Enabled = true;
            error_label.Visible = false;
            error_label.Text = "";

            new_part_checkbox.Enabled = false;
            metadata_checkbox.Enabled = false;
            pin_labels_checkbox.Enabled = false;
            temp_vdd_checkbox.Enabled = false;
            CNTs_DLYs_checkbox.Enabled = false;
            ACMPs_checkbox.Enabled = false;
            pin_settings_checkbox.Enabled = false;
            DS_rev_checkbox.Enabled = false;
            I_Q_checkbox.Enabled = false;
            TM_Part_Code_checkbox.Enabled = false;
            TM_Revision_checkbox.Enabled = false;
            lock_status_checkbox.Enabled = false;
            DS_rev_change_textbox.ReadOnly = true;

            progressBar.Visible = true;
            status_label.Visible = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void temp_vdd_CheckedChanged(object sender, EventArgs e)
        {
            if (temp_vdd_checkbox.Checked) MainProgram.g.temp_vdd_update = true;
            else MainProgram.g.temp_vdd_update = false;
        }

        private void TM_Part_Code_CheckedChanged(object sender, EventArgs e)
        {
            if (TM_Part_Code_checkbox.Checked)
            {
                MainProgram.g.TM_part_code_update = true;
                TM_Part_Code_textbox.Enabled = true;
            }
            else
            {
                MainProgram.g.TM_part_code_update = false;
                TM_Part_Code_textbox.Enabled = false;
                TM_Part_Code_textbox.Text = "";
            }
        }

        private void TM_Part_Code_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.TM_part_code = TM_Part_Code_textbox.Text;
        }

        private void TM_Revision_CheckedChanged(object sender, EventArgs e)
        {
            if (TM_Revision_checkbox.Checked)
            {
                MainProgram.g.TM_revision_update = true;
                TM_Revision_textbox.Enabled = true;
            }
            else
            {
                MainProgram.g.TM_revision_update = false;
                TM_Revision_textbox.Enabled = false;
                TM_Revision_textbox.Text = "";
            }
        }

        private void TM_Revision_textbox_TextChanged(object sender, EventArgs e)
        {
            MainProgram.g.TM_revision = TM_Revision_textbox.Text;
        }

        private void GP_file_textbox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(MainProgram.g.GreenPAK_File);
        }

        private void DS_file_textbox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(MainProgram.g.DataSheet_File);
        }
    }
}