namespace VisualRealty
{
  partial class MainForm
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
			this.MainMap = new GMap.NET.WindowsForms.GMapControl();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// MainMap
			// 
			this.MainMap.Bearing = 0F;
			this.MainMap.CanDragMap = true;
			this.MainMap.EmptyTileColor = System.Drawing.Color.Navy;
			this.MainMap.GrayScaleMode = false;
			this.MainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.MainMap.LevelsKeepInMemmory = 5;
			this.MainMap.Location = new System.Drawing.Point(12, 12);
			this.MainMap.MarkersEnabled = true;
			this.MainMap.MaxZoom = 2;
			this.MainMap.MinZoom = 2;
			this.MainMap.MouseWheelZoomEnabled = true;
			this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.MainMap.Name = "MainMap";
			this.MainMap.NegativeMode = false;
			this.MainMap.PolygonsEnabled = true;
			this.MainMap.RetryLoadTile = 0;
			this.MainMap.RoutesEnabled = true;
			this.MainMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.MainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.MainMap.ShowTileGridLines = false;
			this.MainMap.Size = new System.Drawing.Size(956, 636);
			this.MainMap.TabIndex = 0;
			this.MainMap.Zoom = 0D;
			this.MainMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.MainMap_OnMarkerClick);
			this.MainMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.MainMap_OnMapZoomChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(974, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(129, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Сброс";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(974, 41);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(129, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Переделать карту";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1115, 660);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.MainMap);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

    }

    #endregion

    private GMap.NET.WindowsForms.GMapControl MainMap;
    private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}

