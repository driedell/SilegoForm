using System.Windows.Forms;
using System.ComponentModel;

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
            this.I_Q_textBox = new System.Windows.Forms.TextBox();
            this.DS_file_textbox = new System.Windows.Forms.TextBox();
            this.GP_file_textbox = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Button();
            this.select_GP_file = new System.Windows.Forms.OpenFileDialog();
            this.select_DS_file = new System.Windows.Forms.OpenFileDialog();
            this.GP_button = new System.Windows.Forms.Button();
            this.DS_button = new System.Windows.Forms.Button();
            this.DS_rev_combobox = new System.Windows.Forms.ComboBox();
            this.I_Q_checkbox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.status_label = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.error_label = new System.Windows.Forms.Label();
            this.pin_labels_checkbox = new System.Windows.Forms.CheckBox();
            this.pin_settings_checkbox = new System.Windows.Forms.CheckBox();
            this.project_info_checkbox = new System.Windows.Forms.CheckBox();
            this.temp_vdd_checkbox = new System.Windows.Forms.CheckBox();
            this.CNTs_DLYs_checkbox = new System.Windows.Forms.CheckBox();
            this.ACMPs_checkbox = new System.Windows.Forms.CheckBox();
            this.version_label = new System.Windows.Forms.Label();
            this.TM_Part_Code_textbox = new System.Windows.Forms.TextBox();
            this.TM_Part_Code_checkbox = new System.Windows.Forms.CheckBox();
            this.TM_Revision_checkbox = new System.Windows.Forms.CheckBox();
            this.TM_Revision_textbox = new System.Windows.Forms.TextBox();
            this.DS_rev_checkbox = new System.Windows.Forms.CheckBox();
            this.new_part_checkbox = new System.Windows.Forms.CheckBox();
            this.help_checkbox = new System.Windows.Forms.CheckBox();
            this.help_textbox = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.DS_rev_change_textbox = new System.Windows.Forms.TextBox();
            this.ds_rev_change_label = new System.Windows.Forms.Label();
            this.lock_status_checkbox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.select_DS_save_location = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.SilegoLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            // I_Q_textBox
            // 
            this.I_Q_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.I_Q_textBox.Enabled = false;
            this.I_Q_textBox.Location = new System.Drawing.Point(282, 30);
            this.I_Q_textBox.Name = "I_Q_textBox";
            this.I_Q_textBox.Size = new System.Drawing.Size(78, 20);
            this.I_Q_textBox.TabIndex = 17;
            this.I_Q_textBox.TextChanged += new System.EventHandler(this.I_Q_TextBox_TextChanged);
            // 
            // DS_file_textbox
            // 
            this.DS_file_textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DS_file_textbox.Location = new System.Drawing.Point(12, 266);
            this.DS_file_textbox.Multiline = true;
            this.DS_file_textbox.Name = "DS_file_textbox";
            this.DS_file_textbox.ReadOnly = true;
            this.DS_file_textbox.Size = new System.Drawing.Size(275, 40);
            this.DS_file_textbox.TabIndex = 26;
            this.DS_file_textbox.Text = "P:\\Apps_Tools\\New_DS_Template\\New_DS_Template.docx";
            this.DS_file_textbox.TextChanged += new System.EventHandler(this.DS_file_textbox_TextChanged);
            // 
            // GP_file_textbox
            // 
            this.GP_file_textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GP_file_textbox.Location = new System.Drawing.Point(12, 219);
            this.GP_file_textbox.Multiline = true;
            this.GP_file_textbox.Name = "GP_file_textbox";
            this.GP_file_textbox.ReadOnly = true;
            this.GP_file_textbox.Size = new System.Drawing.Size(275, 41);
            this.GP_file_textbox.TabIndex = 25;
            this.GP_file_textbox.Text = "Drop GP File";
            this.GP_file_textbox.TextChanged += new System.EventHandler(this.GP_file_textbox_TextChanged);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(297, 346);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 1;
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
            this.GP_button.Location = new System.Drawing.Point(297, 219);
            this.GP_button.Name = "GP_button";
            this.GP_button.Size = new System.Drawing.Size(75, 40);
            this.GP_button.TabIndex = 27;
            this.GP_button.Text = "Select GP File";
            this.GP_button.UseVisualStyleBackColor = true;
            this.GP_button.Click += new System.EventHandler(this.Select_GP_Click);
            // 
            // DS_button
            // 
            this.DS_button.Location = new System.Drawing.Point(297, 266);
            this.DS_button.Name = "DS_button";
            this.DS_button.Size = new System.Drawing.Size(75, 40);
            this.DS_button.TabIndex = 28;
            this.DS_button.Text = "Select DS File";
            this.DS_button.UseVisualStyleBackColor = true;
            this.DS_button.Click += new System.EventHandler(this.Select_DS_Click);
            // 
            // DS_rev_combobox
            // 
            this.DS_rev_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DS_rev_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DS_rev_combobox.Enabled = false;
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
            this.DS_rev_combobox.Location = new System.Drawing.Point(282, 3);
            this.DS_rev_combobox.Name = "DS_rev_combobox";
            this.DS_rev_combobox.Size = new System.Drawing.Size(78, 21);
            this.DS_rev_combobox.TabIndex = 29;
            this.DS_rev_combobox.SelectedIndexChanged += new System.EventHandler(this.DS_rev_combobox_SelectedIndexChanged);
            this.DS_rev_combobox.MouseHover += new System.EventHandler(this.DS_rev_combobox_MouseHover);
            // 
            // I_Q_checkbox
            // 
            this.I_Q_checkbox.AutoSize = true;
            this.I_Q_checkbox.Location = new System.Drawing.Point(181, 30);
            this.I_Q_checkbox.Name = "I_Q_checkbox";
            this.I_Q_checkbox.Size = new System.Drawing.Size(68, 17);
            this.I_Q_checkbox.TabIndex = 30;
            this.I_Q_checkbox.Text = "I_Q (µA):";
            this.I_Q_checkbox.UseVisualStyleBackColor = true;
            this.I_Q_checkbox.CheckedChanged += new System.EventHandler(this.I_Q_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.Location = new System.Drawing.Point(12, 346);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(275, 23);
            this.progressBar.TabIndex = 31;
            this.progressBar.Visible = false;
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label.Location = new System.Drawing.Point(12, 326);
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
            this.error_label.Location = new System.Drawing.Point(12, 309);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(39, 17);
            this.error_label.TabIndex = 33;
            this.error_label.Text = "error";
            this.error_label.Visible = false;
            // 
            // pin_labels_checkbox
            // 
            this.pin_labels_checkbox.AutoSize = true;
            this.pin_labels_checkbox.Location = new System.Drawing.Point(3, 56);
            this.pin_labels_checkbox.Name = "pin_labels_checkbox";
            this.pin_labels_checkbox.Size = new System.Drawing.Size(75, 17);
            this.pin_labels_checkbox.TabIndex = 34;
            this.pin_labels_checkbox.Text = "Pin Labels";
            this.pin_labels_checkbox.UseVisualStyleBackColor = true;
            this.pin_labels_checkbox.CheckedChanged += new System.EventHandler(this.pin_labels_CheckedChanged);
            // 
            // pin_settings_checkbox
            // 
            this.pin_settings_checkbox.AutoSize = true;
            this.pin_settings_checkbox.Location = new System.Drawing.Point(3, 82);
            this.pin_settings_checkbox.Name = "pin_settings_checkbox";
            this.pin_settings_checkbox.Size = new System.Drawing.Size(82, 17);
            this.pin_settings_checkbox.TabIndex = 35;
            this.pin_settings_checkbox.Text = "Pin Settings";
            this.pin_settings_checkbox.UseVisualStyleBackColor = true;
            this.pin_settings_checkbox.CheckedChanged += new System.EventHandler(this.pin_settings_CheckedChanged);
            // 
            // project_info_checkbox
            // 
            this.project_info_checkbox.AutoSize = true;
            this.project_info_checkbox.Location = new System.Drawing.Point(3, 30);
            this.project_info_checkbox.Name = "project_info_checkbox";
            this.project_info_checkbox.Size = new System.Drawing.Size(80, 17);
            this.project_info_checkbox.TabIndex = 37;
            this.project_info_checkbox.Text = "Project Info";
            this.project_info_checkbox.UseVisualStyleBackColor = true;
            this.project_info_checkbox.CheckedChanged += new System.EventHandler(this.project_info_CheckedChanged);
            // 
            // temp_vdd_checkbox
            // 
            this.temp_vdd_checkbox.AutoSize = true;
            this.temp_vdd_checkbox.Location = new System.Drawing.Point(91, 3);
            this.temp_vdd_checkbox.Name = "temp_vdd_checkbox";
            this.temp_vdd_checkbox.Size = new System.Drawing.Size(81, 17);
            this.temp_vdd_checkbox.TabIndex = 36;
            this.temp_vdd_checkbox.Text = "Temp/VDD";
            this.temp_vdd_checkbox.UseVisualStyleBackColor = true;
            this.temp_vdd_checkbox.CheckedChanged += new System.EventHandler(this.temp_vdd_CheckedChanged);
            // 
            // CNTs_DLYs_checkbox
            // 
            this.CNTs_DLYs_checkbox.AutoSize = true;
            this.CNTs_DLYs_checkbox.Location = new System.Drawing.Point(91, 30);
            this.CNTs_DLYs_checkbox.Name = "CNTs_DLYs_checkbox";
            this.CNTs_DLYs_checkbox.Size = new System.Drawing.Size(84, 17);
            this.CNTs_DLYs_checkbox.TabIndex = 39;
            this.CNTs_DLYs_checkbox.Text = "CNTs/DLYs";
            this.CNTs_DLYs_checkbox.UseVisualStyleBackColor = true;
            this.CNTs_DLYs_checkbox.CheckedChanged += new System.EventHandler(this.CNTs_DLYs_CheckedChanged);
            // 
            // ACMPs_checkbox
            // 
            this.ACMPs_checkbox.AutoSize = true;
            this.ACMPs_checkbox.Location = new System.Drawing.Point(91, 56);
            this.ACMPs_checkbox.Name = "ACMPs_checkbox";
            this.ACMPs_checkbox.Size = new System.Drawing.Size(61, 17);
            this.ACMPs_checkbox.TabIndex = 38;
            this.ACMPs_checkbox.Text = "ACMPs";
            this.ACMPs_checkbox.UseVisualStyleBackColor = true;
            this.ACMPs_checkbox.CheckedChanged += new System.EventHandler(this.ACMPs_CheckedChanged);
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Location = new System.Drawing.Point(294, 17);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(43, 13);
            this.version_label.TabIndex = 43;
            this.version_label.Text = "v0.0.12";
            this.version_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TM_Part_Code_textbox
            // 
            this.TM_Part_Code_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TM_Part_Code_textbox.Enabled = false;
            this.TM_Part_Code_textbox.Location = new System.Drawing.Point(282, 56);
            this.TM_Part_Code_textbox.Name = "TM_Part_Code_textbox";
            this.TM_Part_Code_textbox.Size = new System.Drawing.Size(78, 20);
            this.TM_Part_Code_textbox.TabIndex = 44;
            this.TM_Part_Code_textbox.TextChanged += new System.EventHandler(this.TM_Part_Code_textbox_TextChanged);
            // 
            // TM_Part_Code_checkbox
            // 
            this.TM_Part_Code_checkbox.AutoSize = true;
            this.TM_Part_Code_checkbox.Location = new System.Drawing.Point(181, 56);
            this.TM_Part_Code_checkbox.Name = "TM_Part_Code_checkbox";
            this.TM_Part_Code_checkbox.Size = new System.Drawing.Size(95, 17);
            this.TM_Part_Code_checkbox.TabIndex = 45;
            this.TM_Part_Code_checkbox.Text = "TM Part Code:";
            this.TM_Part_Code_checkbox.UseVisualStyleBackColor = true;
            this.TM_Part_Code_checkbox.CheckedChanged += new System.EventHandler(this.TM_Part_Code_CheckedChanged);
            // 
            // TM_Revision_checkbox
            // 
            this.TM_Revision_checkbox.AutoSize = true;
            this.TM_Revision_checkbox.Location = new System.Drawing.Point(181, 82);
            this.TM_Revision_checkbox.Name = "TM_Revision_checkbox";
            this.TM_Revision_checkbox.Size = new System.Drawing.Size(89, 17);
            this.TM_Revision_checkbox.TabIndex = 47;
            this.TM_Revision_checkbox.Text = "TM Revision:";
            this.TM_Revision_checkbox.UseVisualStyleBackColor = true;
            this.TM_Revision_checkbox.CheckedChanged += new System.EventHandler(this.TM_Revision_CheckedChanged);
            // 
            // TM_Revision_textbox
            // 
            this.TM_Revision_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TM_Revision_textbox.Enabled = false;
            this.TM_Revision_textbox.Location = new System.Drawing.Point(282, 82);
            this.TM_Revision_textbox.Name = "TM_Revision_textbox";
            this.TM_Revision_textbox.Size = new System.Drawing.Size(78, 20);
            this.TM_Revision_textbox.TabIndex = 46;
            this.TM_Revision_textbox.TextChanged += new System.EventHandler(this.TM_Revision_textbox_TextChanged);
            // 
            // DS_rev_checkbox
            // 
            this.DS_rev_checkbox.AutoSize = true;
            this.DS_rev_checkbox.Location = new System.Drawing.Point(181, 3);
            this.DS_rev_checkbox.Name = "DS_rev_checkbox";
            this.DS_rev_checkbox.Size = new System.Drawing.Size(67, 17);
            this.DS_rev_checkbox.TabIndex = 48;
            this.DS_rev_checkbox.Text = "DS Rev:";
            this.DS_rev_checkbox.UseVisualStyleBackColor = true;
            this.DS_rev_checkbox.CheckedChanged += new System.EventHandler(this.DS_rev_CheckedChanged);
            // 
            // new_part_checkbox
            // 
            this.new_part_checkbox.AutoSize = true;
            this.new_part_checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_part_checkbox.Location = new System.Drawing.Point(3, 3);
            this.new_part_checkbox.Name = "new_part_checkbox";
            this.new_part_checkbox.Size = new System.Drawing.Size(78, 17);
            this.new_part_checkbox.TabIndex = 42;
            this.new_part_checkbox.Text = "New Part";
            this.new_part_checkbox.UseVisualStyleBackColor = true;
            this.new_part_checkbox.CheckedChanged += new System.EventHandler(this.new_part_CheckedChanged);
            // 
            // help_checkbox
            // 
            this.help_checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.help_checkbox.AutoSize = true;
            this.help_checkbox.Location = new System.Drawing.Point(333, 12);
            this.help_checkbox.Name = "help_checkbox";
            this.help_checkbox.Size = new System.Drawing.Size(39, 23);
            this.help_checkbox.TabIndex = 49;
            this.help_checkbox.Text = "Help";
            this.help_checkbox.UseVisualStyleBackColor = true;
            this.help_checkbox.CheckedChanged += new System.EventHandler(this.help_CheckedChanged);
            // 
            // help_textbox
            // 
            this.help_textbox.Location = new System.Drawing.Point(379, 25);
            this.help_textbox.Multiline = true;
            this.help_textbox.Name = "help_textbox";
            this.help_textbox.ReadOnly = true;
            this.help_textbox.Size = new System.Drawing.Size(293, 345);
            this.help_textbox.TabIndex = 50;
            this.help_textbox.Text = resources.GetString("help_textbox.Text");
            this.help_textbox.Visible = false;
            // 
            // cancel_button
            // 
            this.cancel_button.ForeColor = System.Drawing.Color.Red;
            this.cancel_button.Location = new System.Drawing.Point(297, 346);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 51;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Visible = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // DS_rev_change_textbox
            // 
            this.DS_rev_change_textbox.Location = new System.Drawing.Point(100, 173);
            this.DS_rev_change_textbox.Multiline = true;
            this.DS_rev_change_textbox.Name = "DS_rev_change_textbox";
            this.DS_rev_change_textbox.Size = new System.Drawing.Size(272, 40);
            this.DS_rev_change_textbox.TabIndex = 52;
            this.DS_rev_change_textbox.Text = "New Design for SLG";
            this.DS_rev_change_textbox.TextChanged += new System.EventHandler(this.DS_rev_change_textbox_TextChanged);
            // 
            // ds_rev_change_label
            // 
            this.ds_rev_change_label.AutoSize = true;
            this.ds_rev_change_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds_rev_change_label.Location = new System.Drawing.Point(9, 173);
            this.ds_rev_change_label.Name = "ds_rev_change_label";
            this.ds_rev_change_label.Size = new System.Drawing.Size(75, 26);
            this.ds_rev_change_label.TabIndex = 53;
            this.ds_rev_change_label.Text = "Datasheet\r\nRev Changes:";
            this.ds_rev_change_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lock_status_checkbox
            // 
            this.lock_status_checkbox.AutoSize = true;
            this.lock_status_checkbox.Location = new System.Drawing.Point(91, 82);
            this.lock_status_checkbox.Name = "lock_status_checkbox";
            this.lock_status_checkbox.Size = new System.Drawing.Size(83, 17);
            this.lock_status_checkbox.TabIndex = 54;
            this.lock_status_checkbox.Text = "Lock Status";
            this.lock_status_checkbox.UseVisualStyleBackColor = true;
            this.lock_status_checkbox.CheckedChanged += new System.EventHandler(this.lock_checkbox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.new_part_checkbox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lock_status_checkbox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.project_info_checkbox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pin_labels_checkbox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pin_settings_checkbox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.temp_vdd_checkbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TM_Revision_textbox, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.TM_Revision_checkbox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.TM_Part_Code_textbox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.CNTs_DLYs_checkbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TM_Part_Code_checkbox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.ACMPs_checkbox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.I_Q_checkbox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.I_Q_textBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.DS_rev_checkbox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.DS_rev_combobox, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 105);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 17);
            this.label1.TabIndex = 56;
            this.label1.Text = "Select Applicable Changes:";
            // 
            // SilegoForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 382);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ds_rev_change_label);
            this.Controls.Add(this.DS_rev_change_textbox);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.help_textbox);
            this.Controls.Add(this.help_checkbox);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.error_label);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.DS_button);
            this.Controls.Add(this.GP_button);
            this.Controls.Add(this.DS_file_textbox);
            this.Controls.Add(this.GP_file_textbox);
            this.Controls.Add(this.SilegoLogo);
            this.Controls.Add(this.cancel_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 420);
            this.MinimumSize = new System.Drawing.Size(400, 420);
            this.Name = "SilegoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Silego DataSheet Automation Tool";
            this.Load += new System.EventHandler(this.SilegoForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SilegoForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SilegoForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.SilegoLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox SilegoLogo;
        private TextBox I_Q_textBox;
        private TextBox DS_file_textbox;
        private TextBox GP_file_textbox;
        private Button start_button;
        private OpenFileDialog select_GP_file;
        private OpenFileDialog select_DS_file;
        private Button GP_button;
        private Button DS_button;
        private ComboBox DS_rev_combobox;
        private CheckBox I_Q_checkbox;
        private ProgressBar progressBar;
        private Label status_label;
        public BackgroundWorker backgroundWorker;
        private Label error_label;
        private CheckBox pin_labels_checkbox;
        private CheckBox pin_settings_checkbox;
        private CheckBox project_info_checkbox;
        private CheckBox temp_vdd_checkbox;
        private CheckBox CNTs_DLYs_checkbox;
        private CheckBox ACMPs_checkbox;
        private Label version_label;
        private TextBox TM_Part_Code_textbox;
        private CheckBox TM_Part_Code_checkbox;
        private CheckBox TM_Revision_checkbox;
        private TextBox TM_Revision_textbox;
        private CheckBox DS_rev_checkbox;
        private CheckBox new_part_checkbox;
        private CheckBox help_checkbox;
        private TextBox help_textbox;
        public Button cancel_button;
        private TextBox DS_rev_change_textbox;
        private Label ds_rev_change_label;
        private CheckBox lock_status_checkbox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private FolderBrowserDialog select_DS_save_location;
    }
}