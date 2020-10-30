namespace projectGreatMaps
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
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.splitterLeftArea = new System.Windows.Forms.Splitter();
            this.textBoxUserID = new System.Windows.Forms.TextBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonDrawHull = new System.Windows.Forms.Button();
            this.buttonEnclosingCircle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Right;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(280, 0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 12;
            this.gmap.MinZoom = 15;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = true;
            this.gmap.Size = new System.Drawing.Size(520, 450);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 12D;
            this.gmap.Resize += new System.EventHandler(this.gmap_Resize);
            // 
            // splitterLeftArea
            // 
            this.splitterLeftArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterLeftArea.Location = new System.Drawing.Point(0, 0);
            this.splitterLeftArea.Name = "splitterLeftArea";
            this.splitterLeftArea.Size = new System.Drawing.Size(274, 450);
            this.splitterLeftArea.TabIndex = 2;
            this.splitterLeftArea.TabStop = false;
            // 
            // textBoxUserID
            // 
            this.textBoxUserID.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserID.Location = new System.Drawing.Point(12, 46);
            this.textBoxUserID.Name = "textBoxUserID";
            this.textBoxUserID.Size = new System.Drawing.Size(248, 28);
            this.textBoxUserID.TabIndex = 3;
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserID.Location = new System.Drawing.Point(12, 18);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(64, 20);
            this.labelUserID.TabIndex = 4;
            this.labelUserID.Text = "User ID";
            // 
            // buttonSend
            // 
            this.buttonSend.AutoSize = true;
            this.buttonSend.BackColor = System.Drawing.Color.Gold;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSend.Location = new System.Drawing.Point(12, 90);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(248, 32);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "Retrieve data";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonDrawHull
            // 
            this.buttonDrawHull.AutoSize = true;
            this.buttonDrawHull.BackColor = System.Drawing.Color.Gold;
            this.buttonDrawHull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDrawHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrawHull.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonDrawHull.Location = new System.Drawing.Point(12, 128);
            this.buttonDrawHull.Name = "buttonDrawHull";
            this.buttonDrawHull.Size = new System.Drawing.Size(248, 32);
            this.buttonDrawHull.TabIndex = 6;
            this.buttonDrawHull.Text = "Draw Hull";
            this.buttonDrawHull.UseVisualStyleBackColor = false;
            this.buttonDrawHull.Click += new System.EventHandler(this.buttonDrawHull_Click);
            // 
            // buttonEnclosingCircle
            // 
            this.buttonEnclosingCircle.AutoSize = true;
            this.buttonEnclosingCircle.BackColor = System.Drawing.Color.Gold;
            this.buttonEnclosingCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnclosingCircle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnclosingCircle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonEnclosingCircle.Location = new System.Drawing.Point(12, 166);
            this.buttonEnclosingCircle.Name = "buttonEnclosingCircle";
            this.buttonEnclosingCircle.Size = new System.Drawing.Size(248, 32);
            this.buttonEnclosingCircle.TabIndex = 7;
            this.buttonEnclosingCircle.Text = "Draw Enclosing Circle";
            this.buttonEnclosingCircle.UseVisualStyleBackColor = false;
            this.buttonEnclosingCircle.Click += new System.EventHandler(this.buttonEnclosingCircle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonEnclosingCircle);
            this.Controls.Add(this.buttonDrawHull);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.textBoxUserID);
            this.Controls.Add(this.splitterLeftArea);
            this.Controls.Add(this.gmap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Splitter splitterLeftArea;
        private System.Windows.Forms.TextBox textBoxUserID;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonDrawHull;
        private System.Windows.Forms.Button buttonEnclosingCircle;
    }
}

