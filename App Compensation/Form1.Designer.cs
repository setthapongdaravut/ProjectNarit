namespace App_Compensation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.Graph_panel = new System.Windows.Forms.Panel();
            this.graphmenu = new Bunifu.Framework.UI.BunifuFlatButton();
            this.slider = new System.Windows.Forms.PictureBox();
            this.datamenu = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contain = new System.Windows.Forms.Panel();
            this.graphForm1 = new App_Compensation.GraphForm();
            this.dataform1 = new App_Compensation.dataform();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.panel2.SuspendLayout();
            this.contain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel1;
            this.bunifuDragControl1.Vertical = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.panel1.Controls.Add(this.Graph_panel);
            this.panel1.Controls.Add(this.graphmenu);
            this.panel1.Controls.Add(this.slider);
            this.panel1.Controls.Add(this.datamenu);
            this.panel1.Controls.Add(this.bunifuCustomLabel1);
            this.panel1.Controls.Add(this.bunifuImageButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 939);
            this.panel1.TabIndex = 0;
            // 
            // Graph_panel
            // 
            this.Graph_panel.Location = new System.Drawing.Point(164, 3);
            this.Graph_panel.Name = "Graph_panel";
            this.Graph_panel.Size = new System.Drawing.Size(1132, 466);
            this.Graph_panel.TabIndex = 1;
            // 
            // graphmenu
            // 
            this.graphmenu.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.graphmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.graphmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graphmenu.BorderRadius = 0;
            this.graphmenu.ButtonText = "Data Analize";
            this.graphmenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.graphmenu.DisabledColor = System.Drawing.Color.Gray;
            this.graphmenu.Iconcolor = System.Drawing.Color.Transparent;
            this.graphmenu.Iconimage = null;
            this.graphmenu.Iconimage_right = null;
            this.graphmenu.Iconimage_right_Selected = null;
            this.graphmenu.Iconimage_Selected = null;
            this.graphmenu.IconMarginLeft = 0;
            this.graphmenu.IconMarginRight = 0;
            this.graphmenu.IconRightVisible = true;
            this.graphmenu.IconRightZoom = 0D;
            this.graphmenu.IconVisible = true;
            this.graphmenu.IconZoom = 90D;
            this.graphmenu.IsTab = false;
            this.graphmenu.Location = new System.Drawing.Point(21, 197);
            this.graphmenu.Name = "graphmenu";
            this.graphmenu.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.graphmenu.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.graphmenu.OnHoverTextColor = System.Drawing.Color.WhiteSmoke;
            this.graphmenu.selected = false;
            this.graphmenu.Size = new System.Drawing.Size(101, 37);
            this.graphmenu.TabIndex = 4;
            this.graphmenu.Text = "Data Analize";
            this.graphmenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.graphmenu.Textcolor = System.Drawing.Color.White;
            this.graphmenu.TextFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphmenu.Click += new System.EventHandler(this.Graphmenu_Click);
            // 
            // slider
            // 
            this.slider.BackColor = System.Drawing.Color.WhiteSmoke;
            this.slider.Location = new System.Drawing.Point(-4, 112);
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(10, 38);
            this.slider.TabIndex = 3;
            this.slider.TabStop = false;
            // 
            // datamenu
            // 
            this.datamenu.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.datamenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.datamenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.datamenu.BorderRadius = 0;
            this.datamenu.ButtonText = "Sensor Monitor";
            this.datamenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.datamenu.DisabledColor = System.Drawing.Color.Gray;
            this.datamenu.Iconcolor = System.Drawing.Color.Transparent;
            this.datamenu.Iconimage = null;
            this.datamenu.Iconimage_right = null;
            this.datamenu.Iconimage_right_Selected = null;
            this.datamenu.Iconimage_Selected = null;
            this.datamenu.IconMarginLeft = 0;
            this.datamenu.IconMarginRight = 0;
            this.datamenu.IconRightVisible = true;
            this.datamenu.IconRightZoom = 0D;
            this.datamenu.IconVisible = true;
            this.datamenu.IconZoom = 90D;
            this.datamenu.IsTab = false;
            this.datamenu.Location = new System.Drawing.Point(21, 113);
            this.datamenu.Name = "datamenu";
            this.datamenu.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.datamenu.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.datamenu.OnHoverTextColor = System.Drawing.Color.WhiteSmoke;
            this.datamenu.selected = false;
            this.datamenu.Size = new System.Drawing.Size(110, 37);
            this.datamenu.TabIndex = 2;
            this.datamenu.Text = "Sensor Monitor";
            this.datamenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.datamenu.Textcolor = System.Drawing.Color.White;
            this.datamenu.TextFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datamenu.Click += new System.EventHandler(this.Datamenu_Click);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(52, 25);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(61, 25);
            this.bunifuCustomLabel1.TabIndex = 1;
            this.bunifuCustomLabel1.Text = "Menu";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(183)))));
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(21, 21);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(33, 34);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 0;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.contain);
            this.panel2.Controls.Add(this.graphForm1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(138, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1657, 939);
            this.panel2.TabIndex = 1;
            // 
            // contain
            // 
            this.contain.Controls.Add(this.dataform1);
            this.contain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contain.Location = new System.Drawing.Point(0, 0);
            this.contain.Name = "contain";
            this.contain.Size = new System.Drawing.Size(1657, 939);
            this.contain.TabIndex = 1;
            // 
            // graphForm1
            // 
            this.graphForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphForm1.Location = new System.Drawing.Point(0, 0);
            this.graphForm1.Name = "graphForm1";
            this.graphForm1.Size = new System.Drawing.Size(1657, 939);
            this.graphForm1.TabIndex = 0;
            // 
            // dataform1
            // 
            this.dataform1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataform1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataform1.Location = new System.Drawing.Point(0, 0);
            this.dataform1.Name = "dataform1";
            this.dataform1.Size = new System.Drawing.Size(1657, 939);
            this.dataform1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1795, 939);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compensation App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.contain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private Bunifu.Framework.UI.BunifuFlatButton datamenu;
        private System.Windows.Forms.PictureBox slider;
        private Bunifu.Framework.UI.BunifuFlatButton graphmenu;
        private System.Windows.Forms.Panel Graph_panel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel contain;
        private dataform dataform1;
        private GraphForm graphForm1;
    }
}

