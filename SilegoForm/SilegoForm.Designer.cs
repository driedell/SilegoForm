using System.Windows.Forms;
using System.ComponentModel;
using System;

namespace SilegoForm
{
    partial class SilegoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SilegoForm));
            this.SilegoLogo = new System.Windows.Forms.PictureBox();
            this.DS_file_textbox = new System.Windows.Forms.TextBox();
            this.GP_file_textbox = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Button();
            this.select_GP_file = new System.Windows.Forms.OpenFileDialog();
            this.select_DS_file = new System.Windows.Forms.OpenFileDialog();
            this.GP_button = new System.Windows.Forms.Button();
            this.DS_button = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.status_label = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.error_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.new_part_checkbox = new System.Windows.Forms.CheckBox();
            this.help_checkbox = new System.Windows.Forms.CheckBox();
            this.help_textbox = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.select_DS_save_location = new System.Windows.Forms.FolderBrowserDialog();
            this.misc_tab = new System.Windows.Forms.TabControl();
            this.Project_Info_tab = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Files_box = new System.Windows.Forms.GroupBox();
            this.VDD_Temp_box = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.vdd_label = new System.Windows.Forms.Label();
            this.vdd2_label = new System.Windows.Forms.Label();
            this.temp_label = new System.Windows.Forms.Label();
            this.vdd_min_textbox = new System.Windows.Forms.TextBox();
            this.vdd2_min_textbox = new System.Windows.Forms.TextBox();
            this.temp_min_textbox = new System.Windows.Forms.TextBox();
            this.vdd_typ_textbox = new System.Windows.Forms.TextBox();
            this.temp_typ_textbox = new System.Windows.Forms.TextBox();
            this.temp_max_textbox = new System.Windows.Forms.TextBox();
            this.vdd2_typ_textbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.vdd2_max_textbox = new System.Windows.Forms.TextBox();
            this.vdd_max_textbox = new System.Windows.Forms.TextBox();
            this.Part_Info_box = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.part_number_label = new System.Windows.Forms.Label();
            this.customer_name_textbox = new System.Windows.Forms.TextBox();
            this.part_number_textbox = new System.Windows.Forms.TextBox();
            this.customer_name_label = new System.Windows.Forms.Label();
            this.project_name_label = new System.Windows.Forms.Label();
            this.project_name_textbox = new System.Windows.Forms.TextBox();
            this.lock_status_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pattern_id_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.base_die_textbox = new System.Windows.Forms.TextBox();
            this.I_Q_box = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.I_Q_condition_textbox = new System.Windows.Forms.TextBox();
            this.I_Q_checkbox = new System.Windows.Forms.CheckBox();
            this.I_Q_condition_checkbox = new System.Windows.Forms.CheckBox();
            this.I_Q_textbox = new System.Windows.Forms.TextBox();
            this.DS_Rev_box = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.DS_Rev_label = new System.Windows.Forms.Label();
            this.DS_rev_combobox = new System.Windows.Forms.ComboBox();
            this.DRH_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CNTs_DLYs_checkbox = new System.Windows.Forms.CheckBox();
            this.ACMPs_checkbox = new System.Windows.Forms.CheckBox();
            this.pin_labels_checkbox = new System.Windows.Forms.CheckBox();
            this.pin_settings_checkbox = new System.Windows.Forms.CheckBox();
            this.TM_Part_Code_checkbox = new System.Windows.Forms.CheckBox();
            this.TM_Revision_checkbox = new System.Windows.Forms.CheckBox();
            this.TM_Part_Code_textbox = new System.Windows.Forms.TextBox();
            this.TM_Revision_textbox = new System.Windows.Forms.TextBox();
            this.Pins_tab = new System.Windows.Forms.TabPage();
            this.PinTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Counters_tab = new System.Windows.Forms.TabPage();
            this.ACMPs_tab = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.SilegoLogo)).BeginInit();
            this.misc_tab.SuspendLayout();
            this.Project_Info_tab.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.Files_box.SuspendLayout();
            this.VDD_Temp_box.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.Part_Info_box.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.I_Q_box.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.DS_Rev_box.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.Pins_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // SilegoLogo
            // 
            this.SilegoLogo.Image = ((System.Drawing.Image)(resources.GetObject("SilegoLogo.Image")));
            this.SilegoLogo.Location = new System.Drawing.Point(12, 12);
            this.SilegoLogo.Name = "SilegoLogo";
            this.SilegoLogo.Size = new System.Drawing.Size(100, 30);
            this.SilegoLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SilegoLogo.TabIndex = 0;
            this.SilegoLogo.TabStop = false;
            // 
            // DS_file_textbox
            // 
            this.DS_file_textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DS_file_textbox.Location = new System.Drawing.Point(6, 75);
            this.DS_file_textbox.Multiline = true;
            this.DS_file_textbox.Name = "DS_file_textbox";
            this.DS_file_textbox.ReadOnly = true;
            this.DS_file_textbox.Size = new System.Drawing.Size(225, 50);
            this.DS_file_textbox.TabIndex = 26;
            this.DS_file_textbox.Text = "DS File";
            this.DS_file_textbox.TextChanged += new System.EventHandler(this.DS_file_textbox_TextChanged);
            // 
            // GP_file_textbox
            // 
            this.GP_file_textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GP_file_textbox.Location = new System.Drawing.Point(6, 19);
            this.GP_file_textbox.Multiline = true;
            this.GP_file_textbox.Name = "GP_file_textbox";
            this.GP_file_textbox.ReadOnly = true;
            this.GP_file_textbox.Size = new System.Drawing.Size(225, 50);
            this.GP_file_textbox.TabIndex = 25;
            this.GP_file_textbox.Text = "Drop GP File";
            this.GP_file_textbox.TextChanged += new System.EventHandler(this.GP_file_textbox_TextChanged);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(437, 677);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 13;
            this.start_button.Text = "START";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // select_GP_file
            // 
            this.select_GP_file.FileName = "none";
            // 
            // select_DS_file
            // 
            this.select_DS_file.FileName = "none";
            // 
            // GP_button
            // 
            this.GP_button.Location = new System.Drawing.Point(237, 18);
            this.GP_button.Name = "GP_button";
            this.GP_button.Size = new System.Drawing.Size(50, 40);
            this.GP_button.TabIndex = 27;
            this.GP_button.Text = "Select \r\nGP";
            this.GP_button.UseVisualStyleBackColor = true;
            this.GP_button.Click += new System.EventHandler(this.Select_GP_Click);
            // 
            // DS_button
            // 
            this.DS_button.Location = new System.Drawing.Point(237, 75);
            this.DS_button.Name = "DS_button";
            this.DS_button.Size = new System.Drawing.Size(50, 40);
            this.DS_button.TabIndex = 28;
            this.DS_button.Text = "Select\r\nDS";
            this.DS_button.UseVisualStyleBackColor = true;
            this.DS_button.Click += new System.EventHandler(this.Select_DS_Click);
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.Location = new System.Drawing.Point(12, 677);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(419, 23);
            this.progressBar.TabIndex = 31;
            this.progressBar.Visible = false;
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label.Location = new System.Drawing.Point(9, 657);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(46, 17);
            this.status_label.TabIndex = 32;
            this.status_label.Text = "status";
            this.status_label.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_label.ForeColor = System.Drawing.Color.Red;
            this.error_label.Location = new System.Drawing.Point(9, 640);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(39, 17);
            this.error_label.TabIndex = 33;
            this.error_label.Text = "error";
            this.error_label.Visible = false;
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Location = new System.Drawing.Point(469, 32);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(43, 13);
            this.version_label.TabIndex = 43;
            this.version_label.Text = "v0.0.16";
            this.version_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // new_part_checkbox
            // 
            this.new_part_checkbox.AutoSize = true;
            this.new_part_checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_part_checkbox.Location = new System.Drawing.Point(389, 10);
            this.new_part_checkbox.Name = "new_part_checkbox";
            this.new_part_checkbox.Size = new System.Drawing.Size(78, 17);
            this.new_part_checkbox.TabIndex = 42;
            this.new_part_checkbox.Text = "New Part";
            this.new_part_checkbox.UseVisualStyleBackColor = true;
            this.new_part_checkbox.CheckedChanged += new System.EventHandler(this.new_part_checkbox_CheckedChanged);
            // 
            // help_checkbox
            // 
            this.help_checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.help_checkbox.AutoSize = true;
            this.help_checkbox.Location = new System.Drawing.Point(473, 6);
            this.help_checkbox.Name = "help_checkbox";
            this.help_checkbox.Size = new System.Drawing.Size(39, 23);
            this.help_checkbox.TabIndex = 49;
            this.help_checkbox.Text = "Help";
            this.help_checkbox.UseVisualStyleBackColor = true;
            this.help_checkbox.CheckedChanged += new System.EventHandler(this.help_checkbox_CheckedChanged);
            // 
            // help_textbox
            // 
            this.help_textbox.Location = new System.Drawing.Point(518, 6);
            this.help_textbox.Multiline = true;
            this.help_textbox.Name = "help_textbox";
            this.help_textbox.ReadOnly = true;
            this.help_textbox.Size = new System.Drawing.Size(250, 622);
            this.help_textbox.TabIndex = 50;
            this.help_textbox.Text = resources.GetString("help_textbox.Text");
            this.help_textbox.Visible = false;
            // 
            // cancel_button
            // 
            this.cancel_button.ForeColor = System.Drawing.Color.Red;
            this.cancel_button.Location = new System.Drawing.Point(437, 677);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 51;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Visible = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // misc_tab
            // 
            this.misc_tab.Controls.Add(this.Project_Info_tab);
            this.misc_tab.Controls.Add(this.Pins_tab);
            this.misc_tab.Controls.Add(this.Counters_tab);
            this.misc_tab.Controls.Add(this.ACMPs_tab);
            this.misc_tab.Controls.Add(this.tabPage1);
            this.misc_tab.Location = new System.Drawing.Point(12, 48);
            this.misc_tab.Name = "misc_tab";
            this.misc_tab.SelectedIndex = 0;
            this.misc_tab.Size = new System.Drawing.Size(500, 580);
            this.misc_tab.TabIndex = 0;
            this.misc_tab.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Project_Info_tab
            // 
            this.Project_Info_tab.AutoScroll = true;
            this.Project_Info_tab.Controls.Add(this.flowLayoutPanel1);
            this.Project_Info_tab.Location = new System.Drawing.Point(4, 22);
            this.Project_Info_tab.Name = "Project_Info_tab";
            this.Project_Info_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Project_Info_tab.Size = new System.Drawing.Size(492, 554);
            this.Project_Info_tab.TabIndex = 0;
            this.Project_Info_tab.Text = "Project Info";
            this.Project_Info_tab.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Files_box);
            this.flowLayoutPanel1.Controls.Add(this.VDD_Temp_box);
            this.flowLayoutPanel1.Controls.Add(this.Part_Info_box);
            this.flowLayoutPanel1.Controls.Add(this.I_Q_box);
            this.flowLayoutPanel1.Controls.Add(this.DS_Rev_box);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(482, 545);
            this.flowLayoutPanel1.TabIndex = 87;
            // 
            // Files_box
            // 
            this.Files_box.AutoSize = true;
            this.Files_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Files_box.Controls.Add(this.GP_file_textbox);
            this.Files_box.Controls.Add(this.DS_file_textbox);
            this.Files_box.Controls.Add(this.DS_button);
            this.Files_box.Controls.Add(this.GP_button);
            this.Files_box.Location = new System.Drawing.Point(3, 3);
            this.Files_box.Name = "Files_box";
            this.Files_box.Padding = new System.Windows.Forms.Padding(0);
            this.Files_box.Size = new System.Drawing.Size(290, 141);
            this.Files_box.TabIndex = 85;
            this.Files_box.TabStop = false;
            this.Files_box.Text = "Files";
            // 
            // VDD_Temp_box
            // 
            this.VDD_Temp_box.AutoSize = true;
            this.VDD_Temp_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.VDD_Temp_box.Controls.Add(this.tableLayoutPanel3);
            this.VDD_Temp_box.Location = new System.Drawing.Point(299, 3);
            this.VDD_Temp_box.Name = "VDD_Temp_box";
            this.VDD_Temp_box.Padding = new System.Windows.Forms.Padding(0);
            this.VDD_Temp_box.Size = new System.Drawing.Size(160, 123);
            this.VDD_Temp_box.TabIndex = 82;
            this.VDD_Temp_box.TabStop = false;
            this.VDD_Temp_box.Text = "VDD/Temp Specs";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.vdd_label, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.vdd2_label, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.temp_label, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.vdd_min_textbox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.vdd2_min_textbox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.temp_min_textbox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.vdd_typ_textbox, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.temp_typ_textbox, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.temp_max_textbox, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.vdd2_typ_textbox, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.vdd2_max_textbox, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.vdd_max_textbox, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(154, 91);
            this.tableLayoutPanel3.TabIndex = 78;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "min:";
            // 
            // vdd_label
            // 
            this.vdd_label.AutoSize = true;
            this.vdd_label.Location = new System.Drawing.Point(3, 13);
            this.vdd_label.Name = "vdd_label";
            this.vdd_label.Size = new System.Drawing.Size(33, 13);
            this.vdd_label.TabIndex = 55;
            this.vdd_label.Text = "VDD:";
            // 
            // vdd2_label
            // 
            this.vdd2_label.AutoSize = true;
            this.vdd2_label.Location = new System.Drawing.Point(3, 39);
            this.vdd2_label.Name = "vdd2_label";
            this.vdd2_label.Size = new System.Drawing.Size(39, 13);
            this.vdd2_label.TabIndex = 62;
            this.vdd2_label.Text = "VDD2:";
            // 
            // temp_label
            // 
            this.temp_label.AutoSize = true;
            this.temp_label.Location = new System.Drawing.Point(3, 65);
            this.temp_label.Name = "temp_label";
            this.temp_label.Size = new System.Drawing.Size(40, 13);
            this.temp_label.TabIndex = 66;
            this.temp_label.Text = "TEMP:";
            // 
            // vdd_min_textbox
            // 
            this.vdd_min_textbox.Location = new System.Drawing.Point(49, 16);
            this.vdd_min_textbox.Name = "vdd_min_textbox";
            this.vdd_min_textbox.ReadOnly = true;
            this.vdd_min_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd_min_textbox.TabIndex = 56;
            // 
            // vdd2_min_textbox
            // 
            this.vdd2_min_textbox.Location = new System.Drawing.Point(49, 42);
            this.vdd2_min_textbox.Name = "vdd2_min_textbox";
            this.vdd2_min_textbox.ReadOnly = true;
            this.vdd2_min_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd2_min_textbox.TabIndex = 63;
            // 
            // temp_min_textbox
            // 
            this.temp_min_textbox.Location = new System.Drawing.Point(49, 68);
            this.temp_min_textbox.Name = "temp_min_textbox";
            this.temp_min_textbox.ReadOnly = true;
            this.temp_min_textbox.Size = new System.Drawing.Size(30, 20);
            this.temp_min_textbox.TabIndex = 67;
            // 
            // vdd_typ_textbox
            // 
            this.vdd_typ_textbox.Location = new System.Drawing.Point(85, 16);
            this.vdd_typ_textbox.Name = "vdd_typ_textbox";
            this.vdd_typ_textbox.ReadOnly = true;
            this.vdd_typ_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd_typ_textbox.TabIndex = 57;
            // 
            // temp_typ_textbox
            // 
            this.temp_typ_textbox.Location = new System.Drawing.Point(85, 68);
            this.temp_typ_textbox.Name = "temp_typ_textbox";
            this.temp_typ_textbox.ReadOnly = true;
            this.temp_typ_textbox.Size = new System.Drawing.Size(30, 20);
            this.temp_typ_textbox.TabIndex = 68;
            // 
            // temp_max_textbox
            // 
            this.temp_max_textbox.Location = new System.Drawing.Point(121, 68);
            this.temp_max_textbox.Name = "temp_max_textbox";
            this.temp_max_textbox.ReadOnly = true;
            this.temp_max_textbox.Size = new System.Drawing.Size(30, 20);
            this.temp_max_textbox.TabIndex = 69;
            // 
            // vdd2_typ_textbox
            // 
            this.vdd2_typ_textbox.Location = new System.Drawing.Point(85, 42);
            this.vdd2_typ_textbox.Name = "vdd2_typ_textbox";
            this.vdd2_typ_textbox.ReadOnly = true;
            this.vdd2_typ_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd2_typ_textbox.TabIndex = 64;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(85, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "typ:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(121, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "max:";
            // 
            // vdd2_max_textbox
            // 
            this.vdd2_max_textbox.Location = new System.Drawing.Point(121, 42);
            this.vdd2_max_textbox.Name = "vdd2_max_textbox";
            this.vdd2_max_textbox.ReadOnly = true;
            this.vdd2_max_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd2_max_textbox.TabIndex = 65;
            // 
            // vdd_max_textbox
            // 
            this.vdd_max_textbox.Location = new System.Drawing.Point(121, 16);
            this.vdd_max_textbox.Name = "vdd_max_textbox";
            this.vdd_max_textbox.ReadOnly = true;
            this.vdd_max_textbox.Size = new System.Drawing.Size(30, 20);
            this.vdd_max_textbox.TabIndex = 58;
            // 
            // Part_Info_box
            // 
            this.Part_Info_box.AutoSize = true;
            this.Part_Info_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Part_Info_box.Controls.Add(this.tableLayoutPanel2);
            this.Part_Info_box.Location = new System.Drawing.Point(3, 150);
            this.Part_Info_box.Name = "Part_Info_box";
            this.Part_Info_box.Padding = new System.Windows.Forms.Padding(0);
            this.Part_Info_box.Size = new System.Drawing.Size(437, 130);
            this.Part_Info_box.TabIndex = 81;
            this.Part_Info_box.TabStop = false;
            this.Part_Info_box.Text = "Part Info";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.part_number_label, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.customer_name_textbox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.part_number_textbox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.customer_name_label, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.project_name_label, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.project_name_textbox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lock_status_textbox, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.pattern_id_textbox, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.base_die_textbox, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(431, 98);
            this.tableLayoutPanel2.TabIndex = 77;
            // 
            // part_number_label
            // 
            this.part_number_label.AutoSize = true;
            this.part_number_label.Location = new System.Drawing.Point(3, 0);
            this.part_number_label.Name = "part_number_label";
            this.part_number_label.Size = new System.Drawing.Size(76, 13);
            this.part_number_label.TabIndex = 1;
            this.part_number_label.Text = "Part Number: *";
            // 
            // customer_name_textbox
            // 
            this.customer_name_textbox.Location = new System.Drawing.Point(101, 29);
            this.customer_name_textbox.Name = "customer_name_textbox";
            this.customer_name_textbox.Size = new System.Drawing.Size(150, 20);
            this.customer_name_textbox.TabIndex = 1;
            this.customer_name_textbox.TextChanged += new System.EventHandler(this.customer_name_textbox_TextChanged);
            // 
            // part_number_textbox
            // 
            this.part_number_textbox.Location = new System.Drawing.Point(101, 3);
            this.part_number_textbox.Name = "part_number_textbox";
            this.part_number_textbox.Size = new System.Drawing.Size(150, 20);
            this.part_number_textbox.TabIndex = 0;
            this.part_number_textbox.TextChanged += new System.EventHandler(this.part_number_textbox_TextChanged);
            // 
            // customer_name_label
            // 
            this.customer_name_label.AutoSize = true;
            this.customer_name_label.Location = new System.Drawing.Point(3, 26);
            this.customer_name_label.Name = "customer_name_label";
            this.customer_name_label.Size = new System.Drawing.Size(92, 13);
            this.customer_name_label.TabIndex = 3;
            this.customer_name_label.Text = "Customer Name: *";
            // 
            // project_name_label
            // 
            this.project_name_label.AutoSize = true;
            this.project_name_label.Location = new System.Drawing.Point(3, 52);
            this.project_name_label.Name = "project_name_label";
            this.project_name_label.Size = new System.Drawing.Size(81, 13);
            this.project_name_label.TabIndex = 5;
            this.project_name_label.Text = "Project Name: *";
            // 
            // project_name_textbox
            // 
            this.project_name_textbox.Location = new System.Drawing.Point(101, 55);
            this.project_name_textbox.Multiline = true;
            this.project_name_textbox.Name = "project_name_textbox";
            this.project_name_textbox.Size = new System.Drawing.Size(150, 40);
            this.project_name_textbox.TabIndex = 2;
            this.project_name_textbox.TextChanged += new System.EventHandler(this.project_name_textbox_TextChanged);
            // 
            // lock_status_textbox
            // 
            this.lock_status_textbox.Location = new System.Drawing.Point(328, 29);
            this.lock_status_textbox.Name = "lock_status_textbox";
            this.lock_status_textbox.ReadOnly = true;
            this.lock_status_textbox.Size = new System.Drawing.Size(100, 20);
            this.lock_status_textbox.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Lock status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Pattern ID:";
            // 
            // pattern_id_textbox
            // 
            this.pattern_id_textbox.Location = new System.Drawing.Point(328, 55);
            this.pattern_id_textbox.Name = "pattern_id_textbox";
            this.pattern_id_textbox.ReadOnly = true;
            this.pattern_id_textbox.Size = new System.Drawing.Size(100, 20);
            this.pattern_id_textbox.TabIndex = 84;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 89;
            this.label3.Text = "Base Die:";
            // 
            // base_die_textbox
            // 
            this.base_die_textbox.Location = new System.Drawing.Point(328, 3);
            this.base_die_textbox.Name = "base_die_textbox";
            this.base_die_textbox.ReadOnly = true;
            this.base_die_textbox.Size = new System.Drawing.Size(100, 20);
            this.base_die_textbox.TabIndex = 90;
            // 
            // I_Q_box
            // 
            this.I_Q_box.AutoSize = true;
            this.I_Q_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.I_Q_box.Controls.Add(this.tableLayoutPanel4);
            this.I_Q_box.Location = new System.Drawing.Point(3, 286);
            this.I_Q_box.Name = "I_Q_box";
            this.I_Q_box.Padding = new System.Windows.Forms.Padding(0);
            this.I_Q_box.Size = new System.Drawing.Size(236, 114);
            this.I_Q_box.TabIndex = 83;
            this.I_Q_box.TabStop = false;
            this.I_Q_box.Text = "I_Q";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.I_Q_condition_textbox, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.I_Q_checkbox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.I_Q_condition_checkbox, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.I_Q_textbox, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(230, 82);
            this.tableLayoutPanel4.TabIndex = 79;
            // 
            // I_Q_condition_textbox
            // 
            this.I_Q_condition_textbox.Location = new System.Drawing.Point(102, 29);
            this.I_Q_condition_textbox.Multiline = true;
            this.I_Q_condition_textbox.Name = "I_Q_condition_textbox";
            this.I_Q_condition_textbox.ReadOnly = true;
            this.I_Q_condition_textbox.Size = new System.Drawing.Size(125, 50);
            this.I_Q_condition_textbox.TabIndex = 6;
            this.I_Q_condition_textbox.TextChanged += new System.EventHandler(this.I_Q_condition_textbox_TextChanged);
            // 
            // I_Q_checkbox
            // 
            this.I_Q_checkbox.AutoSize = true;
            this.I_Q_checkbox.Location = new System.Drawing.Point(3, 3);
            this.I_Q_checkbox.Name = "I_Q_checkbox";
            this.I_Q_checkbox.Size = new System.Drawing.Size(68, 17);
            this.I_Q_checkbox.TabIndex = 3;
            this.I_Q_checkbox.Text = "I_Q (µA):";
            this.I_Q_checkbox.UseVisualStyleBackColor = true;
            this.I_Q_checkbox.CheckedChanged += new System.EventHandler(this.I_Q_checkbox_CheckedChanged);
            // 
            // I_Q_condition_checkbox
            // 
            this.I_Q_condition_checkbox.AutoSize = true;
            this.I_Q_condition_checkbox.Location = new System.Drawing.Point(3, 29);
            this.I_Q_condition_checkbox.Name = "I_Q_condition_checkbox";
            this.I_Q_condition_checkbox.Size = new System.Drawing.Size(93, 17);
            this.I_Q_condition_checkbox.TabIndex = 5;
            this.I_Q_condition_checkbox.Text = "I_Q Condition:";
            this.I_Q_condition_checkbox.UseVisualStyleBackColor = true;
            this.I_Q_condition_checkbox.CheckedChanged += new System.EventHandler(this.I_Q_condition_checkbox_CheckedChanged);
            // 
            // I_Q_textbox
            // 
            this.I_Q_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.I_Q_textbox.Location = new System.Drawing.Point(102, 3);
            this.I_Q_textbox.Name = "I_Q_textbox";
            this.I_Q_textbox.ReadOnly = true;
            this.I_Q_textbox.Size = new System.Drawing.Size(125, 20);
            this.I_Q_textbox.TabIndex = 4;
            this.I_Q_textbox.TextChanged += new System.EventHandler(this.I_Q_textbox_TextChanged);

            // 
            // DS_Rev_box
            // 
            this.DS_Rev_box.AutoSize = true;
            this.DS_Rev_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DS_Rev_box.Controls.Add(this.tableLayoutPanel5);
            this.DS_Rev_box.Location = new System.Drawing.Point(245, 286);
            this.DS_Rev_box.Name = "DS_Rev_box";
            this.DS_Rev_box.Padding = new System.Windows.Forms.Padding(0);
            this.DS_Rev_box.Size = new System.Drawing.Size(216, 113);
            this.DS_Rev_box.TabIndex = 84;
            this.DS_Rev_box.TabStop = false;
            this.DS_Rev_box.Text = "DS_Rev";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.DS_Rev_label, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.DS_rev_combobox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.DRH_textbox, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(210, 73);
            this.tableLayoutPanel5.TabIndex = 80;
            // 
            // DS_Rev_label
            // 
            this.DS_Rev_label.AutoSize = true;
            this.DS_Rev_label.Location = new System.Drawing.Point(3, 0);
            this.DS_Rev_label.Name = "DS_Rev_label";
            this.DS_Rev_label.Size = new System.Drawing.Size(48, 13);
            this.DS_Rev_label.TabIndex = 76;
            this.DS_Rev_label.Text = "DS Rev:";
            // 
            // DS_rev_combobox
            // 
            this.DS_rev_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DS_rev_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DS_rev_combobox.FormattingEnabled = true;
            this.DS_rev_combobox.Items.AddRange(new object[] {
            "010",
            "011",
            "012",
            "013",
            "014",
            "015",
            "016",
            "017",
            "018",
            "019",
            "020",
            "021",
            "022",
            "023",
            "024",
            "025",
            "026",
            "027",
            "028",
            "029",
            "030",
            "031",
            "032",
            "033",
            "034",
            "035",
            "036",
            "037",
            "038",
            "039",
            "040"});
            this.DS_rev_combobox.Location = new System.Drawing.Point(57, 3);
            this.DS_rev_combobox.Name = "DS_rev_combobox";
            this.DS_rev_combobox.Size = new System.Drawing.Size(150, 21);
            this.DS_rev_combobox.TabIndex = 7;
            this.DS_rev_combobox.SelectedIndexChanged += new System.EventHandler(this.DS_rev_combobox_SelectedIndexChanged);
            // 
            // DRH_textbox
            // 
            this.DRH_textbox.Location = new System.Drawing.Point(57, 30);
            this.DRH_textbox.Multiline = true;
            this.DRH_textbox.Name = "DRH_textbox";
            this.DRH_textbox.Size = new System.Drawing.Size(150, 40);
            this.DRH_textbox.TabIndex = 8;
            this.DRH_textbox.TextChanged += new System.EventHandler(this.DRH_textbox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 26);
            this.label4.TabIndex = 86;
            this.label4.Text = "DS Rev\r\nHistory:";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 406);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 90);
            this.groupBox1.TabIndex = 86;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Updates:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.CNTs_DLYs_checkbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ACMPs_checkbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pin_labels_checkbox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pin_settings_checkbox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TM_Part_Code_checkbox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.TM_Revision_checkbox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.TM_Part_Code_textbox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.TM_Revision_textbox, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 52);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // CNTs_DLYs_checkbox
            // 
            this.CNTs_DLYs_checkbox.AutoSize = true;
            this.CNTs_DLYs_checkbox.Location = new System.Drawing.Point(91, 3);
            this.CNTs_DLYs_checkbox.Name = "CNTs_DLYs_checkbox";
            this.CNTs_DLYs_checkbox.Size = new System.Drawing.Size(84, 17);
            this.CNTs_DLYs_checkbox.TabIndex = 11;
            this.CNTs_DLYs_checkbox.Text = "CNTs/DLYs";
            this.CNTs_DLYs_checkbox.UseVisualStyleBackColor = true;
            this.CNTs_DLYs_checkbox.CheckedChanged += new System.EventHandler(this.CNTs_DLYs_checkbox_CheckedChanged);
            // 
            // ACMPs_checkbox
            // 
            this.ACMPs_checkbox.AutoSize = true;
            this.ACMPs_checkbox.Location = new System.Drawing.Point(91, 29);
            this.ACMPs_checkbox.Name = "ACMPs_checkbox";
            this.ACMPs_checkbox.Size = new System.Drawing.Size(61, 17);
            this.ACMPs_checkbox.TabIndex = 12;
            this.ACMPs_checkbox.Text = "ACMPs";
            this.ACMPs_checkbox.UseVisualStyleBackColor = true;
            this.ACMPs_checkbox.CheckedChanged += new System.EventHandler(this.ACMPs_checkbox_CheckedChanged);
            // 
            // pin_labels_checkbox
            // 
            this.pin_labels_checkbox.AutoSize = true;
            this.pin_labels_checkbox.Location = new System.Drawing.Point(3, 3);
            this.pin_labels_checkbox.Name = "pin_labels_checkbox";
            this.pin_labels_checkbox.Size = new System.Drawing.Size(75, 17);
            this.pin_labels_checkbox.TabIndex = 9;
            this.pin_labels_checkbox.Text = "Pin Labels";
            this.pin_labels_checkbox.UseVisualStyleBackColor = true;
            this.pin_labels_checkbox.CheckedChanged += new System.EventHandler(this.pin_labels_checkbox_CheckedChanged);
            // 
            // pin_settings_checkbox
            // 
            this.pin_settings_checkbox.AutoSize = true;
            this.pin_settings_checkbox.Location = new System.Drawing.Point(3, 29);
            this.pin_settings_checkbox.Name = "pin_settings_checkbox";
            this.pin_settings_checkbox.Size = new System.Drawing.Size(82, 17);
            this.pin_settings_checkbox.TabIndex = 10;
            this.pin_settings_checkbox.Text = "Pin Settings";
            this.pin_settings_checkbox.UseVisualStyleBackColor = true;
            this.pin_settings_checkbox.CheckedChanged += new System.EventHandler(this.pin_settings_checkbox_CheckedChanged);
            // 
            // TM_Part_Code_checkbox
            // 
            this.TM_Part_Code_checkbox.AutoSize = true;
            this.TM_Part_Code_checkbox.Location = new System.Drawing.Point(181, 3);
            this.TM_Part_Code_checkbox.Name = "TM_Part_Code_checkbox";
            this.TM_Part_Code_checkbox.Size = new System.Drawing.Size(95, 17);
            this.TM_Part_Code_checkbox.TabIndex = 45;
            this.TM_Part_Code_checkbox.Text = "TM Part Code:";
            this.TM_Part_Code_checkbox.UseVisualStyleBackColor = true;
            this.TM_Part_Code_checkbox.CheckedChanged += new System.EventHandler(this.TM_Part_Code_checkbox_CheckedChanged);
            // 
            // TM_Revision_checkbox
            // 
            this.TM_Revision_checkbox.AutoSize = true;
            this.TM_Revision_checkbox.Location = new System.Drawing.Point(181, 29);
            this.TM_Revision_checkbox.Name = "TM_Revision_checkbox";
            this.TM_Revision_checkbox.Size = new System.Drawing.Size(89, 17);
            this.TM_Revision_checkbox.TabIndex = 47;
            this.TM_Revision_checkbox.Text = "TM Revision:";
            this.TM_Revision_checkbox.UseVisualStyleBackColor = true;
            this.TM_Revision_checkbox.CheckedChanged += new System.EventHandler(this.TM_Revision_checkbox_CheckedChanged);
            // 
            // TM_Part_Code_textbox
            // 
            this.TM_Part_Code_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TM_Part_Code_textbox.Location = new System.Drawing.Point(282, 3);
            this.TM_Part_Code_textbox.Name = "TM_Part_Code_textbox";
            this.TM_Part_Code_textbox.ReadOnly = true;
            this.TM_Part_Code_textbox.Size = new System.Drawing.Size(78, 20);
            this.TM_Part_Code_textbox.TabIndex = 44;
            this.TM_Part_Code_textbox.TextChanged += new System.EventHandler(this.TM_Part_Code_textbox_TextChanged);
            // 
            // TM_Revision_textbox
            // 
            this.TM_Revision_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TM_Revision_textbox.Location = new System.Drawing.Point(282, 29);
            this.TM_Revision_textbox.Name = "TM_Revision_textbox";
            this.TM_Revision_textbox.ReadOnly = true;
            this.TM_Revision_textbox.Size = new System.Drawing.Size(78, 20);
            this.TM_Revision_textbox.TabIndex = 46;
            this.TM_Revision_textbox.TextChanged += new System.EventHandler(this.TM_Revision_textbox_TextChanged);
            // 
            // Pins_tab
            // 
            this.Pins_tab.AutoScroll = true;
            this.Pins_tab.Controls.Add(this.PinTableLayoutPanel);
            this.Pins_tab.Location = new System.Drawing.Point(4, 22);
            this.Pins_tab.Name = "Pins_tab";
            this.Pins_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Pins_tab.Size = new System.Drawing.Size(492, 554);
            this.Pins_tab.TabIndex = 1;
            this.Pins_tab.Text = "Pins";
            this.Pins_tab.UseVisualStyleBackColor = true;
            // 
            // PinTableLayoutPanel
            // 
            this.PinTableLayoutPanel.AutoSize = true;
            this.PinTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PinTableLayoutPanel.ColumnCount = 1;
            this.PinTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PinTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.PinTableLayoutPanel.Name = "PinTableLayoutPanel";
            this.PinTableLayoutPanel.RowCount = 32;
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PinTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PinTableLayoutPanel.Size = new System.Drawing.Size(0, 20);
            this.PinTableLayoutPanel.TabIndex = 1;
            // 
            // Counters_tab
            // 
            this.Counters_tab.Location = new System.Drawing.Point(4, 22);
            this.Counters_tab.Name = "Counters_tab";
            this.Counters_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Counters_tab.Size = new System.Drawing.Size(492, 554);
            this.Counters_tab.TabIndex = 2;
            this.Counters_tab.Text = "Counters";
            this.Counters_tab.UseVisualStyleBackColor = true;
            // 
            // ACMPs_tab
            // 
            this.ACMPs_tab.Location = new System.Drawing.Point(4, 22);
            this.ACMPs_tab.Name = "ACMPs_tab";
            this.ACMPs_tab.Padding = new System.Windows.Forms.Padding(3);
            this.ACMPs_tab.Size = new System.Drawing.Size(492, 554);
            this.ACMPs_tab.TabIndex = 3;
            this.ACMPs_tab.Text = "ACMPs";
            this.ACMPs_tab.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(492, 554);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SilegoForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 712);
            this.Controls.Add(this.new_part_checkbox);
            this.Controls.Add(this.misc_tab);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.help_textbox);
            this.Controls.Add(this.help_checkbox);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.error_label);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.SilegoLogo);
            this.Controls.Add(this.cancel_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(400, 420);
            this.Name = "SilegoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Silego DataSheet Automation Tool";
            this.Load += new System.EventHandler(this.SilegoForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SilegoForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SilegoForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.SilegoLogo)).EndInit();
            this.misc_tab.ResumeLayout(false);
            this.Project_Info_tab.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.Files_box.ResumeLayout(false);
            this.Files_box.PerformLayout();
            this.VDD_Temp_box.ResumeLayout(false);
            this.VDD_Temp_box.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.Part_Info_box.ResumeLayout(false);
            this.Part_Info_box.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.I_Q_box.ResumeLayout(false);
            this.I_Q_box.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.DS_Rev_box.ResumeLayout(false);
            this.DS_Rev_box.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.Pins_tab.ResumeLayout(false);
            this.Pins_tab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox SilegoLogo;
        private TextBox DS_file_textbox;
        private TextBox GP_file_textbox;
        private Button start_button;
        private OpenFileDialog select_GP_file;
        private OpenFileDialog select_DS_file;
        private Button GP_button;
        private Button DS_button;
        private ProgressBar progressBar;
        private Label status_label;
        public BackgroundWorker backgroundWorker;
        private Label error_label;
        private Label version_label;
        private CheckBox new_part_checkbox;
        private CheckBox help_checkbox;
        private TextBox help_textbox;
        public Button cancel_button;
        private FolderBrowserDialog select_DS_save_location;
        private TabControl misc_tab;
        private TabPage Project_Info_tab;
        private TabPage Pins_tab;
        private TabPage Counters_tab;
        private TabPage ACMPs_tab;
        private Label label10;
        private Label label9;
        private Label label8;
        private TextBox vdd_max_textbox;
        private TextBox vdd_typ_textbox;
        private TextBox vdd_min_textbox;
        private Label vdd_label;
        private TextBox DRH_textbox;
        private ComboBox DS_rev_combobox;
        private Label project_name_label;
        private Label customer_name_label;
        public TextBox customer_name_textbox;
        private Label part_number_label;
        public TextBox part_number_textbox;
        private TextBox I_Q_textbox;
        private TextBox temp_max_textbox;
        private TextBox temp_typ_textbox;
        private TextBox temp_min_textbox;
        private Label temp_label;
        private TextBox vdd2_max_textbox;
        private TextBox vdd2_typ_textbox;
        private TextBox vdd2_min_textbox;
        private Label vdd2_label;
        private TableLayoutPanel tableLayoutPanel2;
        private Label DS_Rev_label;
        private CheckBox I_Q_condition_checkbox;
        private CheckBox I_Q_checkbox;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private GroupBox Part_Info_box;
        private GroupBox DS_Rev_box;
        private GroupBox I_Q_box;
        private GroupBox VDD_Temp_box;
        private TextBox I_Q_condition_textbox;
        private TableLayoutPanel PinTableLayoutPanel;
        private GroupBox Files_box;
        private TextBox project_name_textbox;
        private TabPage tabPage1;
        public TextBox pattern_id_textbox;
        public TextBox lock_status_textbox;
        private Label label1;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label3;
        public TextBox base_die_textbox;
        private Label label4;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox CNTs_DLYs_checkbox;
        private CheckBox ACMPs_checkbox;
        private CheckBox pin_labels_checkbox;
        private CheckBox pin_settings_checkbox;
        private CheckBox TM_Part_Code_checkbox;
        private CheckBox TM_Revision_checkbox;
        private TextBox TM_Part_Code_textbox;
        private TextBox TM_Revision_textbox;
    }

    public partial class myPinBox : UserControl
    {
        public myPinBox()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            this.Pin_box = new System.Windows.Forms.GroupBox();
            this.Pin_table = new System.Windows.Forms.TableLayoutPanel();
            this.Pin_type_textbox = new System.Windows.Forms.TextBox();
            this.Pin_label_label = new System.Windows.Forms.Label();
            this.Pin_label_textbox = new System.Windows.Forms.TextBox();
            this.Pin_type_label = new System.Windows.Forms.Label();
            this.Pin_description_label = new System.Windows.Forms.Label();
            this.Pin_resistor_label = new System.Windows.Forms.Label();
            this.Pin_description_textbox = new System.Windows.Forms.TextBox();
            this.Pin_resistor_textbox = new System.Windows.Forms.TextBox();

            this.Pin_box.SuspendLayout();
            this.Pin_table.SuspendLayout();
            this.SuspendLayout();

            //
            // Pin_box
            //
            this.Pin_box.AutoSize = true;
            this.Pin_box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Pin_box.Controls.Add(this.Pin_table);
            this.Pin_box.Location = new System.Drawing.Point(3, 3);
            this.Pin_box.Name = "Pin_box";
            this.Pin_box.Size = new System.Drawing.Size(387, 130);
            this.Pin_box.TabIndex = 0;
            this.Pin_box.TabStop = false;
            this.Pin_box.Text = "Pin";
            //
            // Pin_table
            //
            this.Pin_table.AutoSize = true;
            this.Pin_table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Pin_table.ColumnCount = 2;
            this.Pin_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Pin_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Pin_table.Controls.Add(this.Pin_type_textbox, 1, 1);
            this.Pin_table.Controls.Add(this.Pin_label_label, 0, 0);
            this.Pin_table.Controls.Add(this.Pin_label_textbox, 1, 0);
            this.Pin_table.Controls.Add(this.Pin_type_label, 0, 1);
            this.Pin_table.Controls.Add(this.Pin_description_label, 0, 2);
            this.Pin_table.Controls.Add(this.Pin_resistor_label, 0, 3);
            this.Pin_table.Controls.Add(this.Pin_description_textbox, 1, 2);
            this.Pin_table.Controls.Add(this.Pin_resistor_textbox, 1, 3);
            this.Pin_table.Location = new System.Drawing.Point(6, 19);
            this.Pin_table.Name = "Pin_table";
            this.Pin_table.RowCount = 4;
            this.Pin_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Pin_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Pin_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Pin_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Pin_table.Size = new System.Drawing.Size(375, 92);
            this.Pin_table.TabIndex = 2;
            //
            // Pin_type_textbox
            //
            this.Pin_type_textbox.Location = new System.Drawing.Point(72, 29);
            this.Pin_type_textbox.Name = "Pin_type_textbox";
            this.Pin_type_textbox.Size = new System.Drawing.Size(300, 20);
            this.Pin_type_textbox.TabIndex = 3;
            this.Pin_type_textbox.TextChanged += new System.EventHandler(this.Pin_type_textbox_textChanged);
            //
            // Pin_label_label
            //
            this.Pin_label_label.AutoSize = true;
            this.Pin_label_label.Location = new System.Drawing.Point(3, 0);
            this.Pin_label_label.Name = "Pin_label_label";
            this.Pin_label_label.Size = new System.Drawing.Size(36, 13);
            this.Pin_label_label.TabIndex = 0;
            this.Pin_label_label.Text = "Label:";
            //
            // Pin_label_textbox
            //
            this.Pin_label_textbox.Location = new System.Drawing.Point(72, 3);
            this.Pin_label_textbox.Name = "Pin_label_textbox";
            this.Pin_label_textbox.Size = new System.Drawing.Size(300, 20);
            this.Pin_label_textbox.TabIndex = 1;
            this.Pin_label_textbox.TextChanged += new System.EventHandler(this.Pin_label_textbox_textChanged);
            //
            // Pin_type_label
            //
            this.Pin_type_label.AutoSize = true;
            this.Pin_type_label.Location = new System.Drawing.Point(3, 26);
            this.Pin_type_label.Name = "Pin_type_label";
            this.Pin_type_label.Size = new System.Drawing.Size(34, 13);
            this.Pin_type_label.TabIndex = 2;
            this.Pin_type_label.Text = "Type:";
            //
            // Pin_description_label
            //
            this.Pin_description_label.AutoSize = true;
            this.Pin_description_label.Location = new System.Drawing.Point(3, 52);
            this.Pin_description_label.Name = "Pin_description_label";
            this.Pin_description_label.Size = new System.Drawing.Size(63, 13);
            this.Pin_description_label.TabIndex = 3;
            this.Pin_description_label.Text = "Description:";
            //
            // Pin_resistor_label
            //
            this.Pin_resistor_label.AutoSize = true;
            this.Pin_resistor_label.Location = new System.Drawing.Point(3, 72);
            this.Pin_resistor_label.Name = "Pin_resistor_label";
            this.Pin_resistor_label.Size = new System.Drawing.Size(48, 13);
            this.Pin_resistor_label.TabIndex = 4;
            this.Pin_resistor_label.Text = "Resistor:";
            //
            // Pin_description_textbox
            //
            this.Pin_description_textbox.Location = new System.Drawing.Point(72, 55);
            this.Pin_description_textbox.Name = "Pin_description_textbox";
            this.Pin_description_textbox.Size = new System.Drawing.Size(300, 20);
            this.Pin_description_textbox.TabIndex = 5;
            this.Pin_description_textbox.TextChanged += new System.EventHandler(this.Pin_description_textbox_textChanged);
            //
            // Pin_resistor_textbox
            //
            this.Pin_resistor_textbox.Location = new System.Drawing.Point(72, 75);
            this.Pin_resistor_textbox.Name = "Pin_resistor_textbox";
            this.Pin_resistor_textbox.Size = new System.Drawing.Size(300, 20);
            this.Pin_resistor_textbox.TabIndex = 6;
            this.Pin_resistor_textbox.TextChanged += new System.EventHandler(this.Pin_resistor_textbox_textChanged);


            this.Controls.Add(this.Pin_box);

            this.Load += new System.EventHandler(this.myGroupBox_Load);

            this.Pin_box.ResumeLayout(false);
            this.Pin_box.PerformLayout();
            this.Pin_table.ResumeLayout(false);
            this.Pin_table.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Pin_label_textbox_textChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < MainProgram.g.GreenPAK.pin.Length; i++)
            {
                if (Pin_label_textbox.Parent.Parent.ToString().EndsWith(i.ToString().PadLeft(2, '0')))
                {
                    Console.WriteLine("Pin " + i.ToString() + "'s label was changed");

                    MainProgram.g.GreenPAK.pin[i].label = Pin_label_textbox.Text;
                    break;
                }
            }
        }

        private void Pin_type_textbox_textChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < MainProgram.g.GreenPAK.pin.Length; i++)
            {
                if (Pin_type_textbox.Parent.Parent.ToString().EndsWith(i.ToString().PadLeft(2, '0')))
                {
                    Console.WriteLine("Pin " + i.ToString() + "'s type was changed");

                    MainProgram.g.GreenPAK.pin[i].type = Pin_type_textbox.Text;
                    break;
                }
            }
        }

        private void Pin_description_textbox_textChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < MainProgram.g.GreenPAK.pin.Length; i++)
            {
                if (Pin_description_textbox.Parent.Parent.ToString().EndsWith(i.ToString().PadLeft(2, '0')))
                {
                    Console.WriteLine("Pin " + i.ToString() + "'s description was changed");

                    MainProgram.g.GreenPAK.pin[i].description = Pin_description_textbox.Text;
                    break;
                }
            }
        }

        private void Pin_resistor_textbox_textChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < MainProgram.g.GreenPAK.pin.Length; i++)
            {
                if (Pin_resistor_textbox.Parent.Parent.ToString().EndsWith(i.ToString().PadLeft(2, '0')))
                {
                    Console.WriteLine("Pin " + i.ToString() + "'s resistor was changed");

                    MainProgram.g.GreenPAK.pin[i].resistor = Pin_resistor_textbox.Text;
                    break;
                }
            }
        }



        public void myGroupBox_Load(object sender, System.EventArgs e)
        {
        }

        public GroupBox Pin_box;
        public Label Pin_description_label;
        public Label Pin_label_label;
        public Label Pin_resistor_label;
        public Label Pin_type_label;
        public TableLayoutPanel Pin_table;
        public TextBox Pin_description_textbox;
        public TextBox Pin_label_textbox;
        public TextBox Pin_resistor_textbox;
        public TextBox Pin_type_textbox;
    }
}