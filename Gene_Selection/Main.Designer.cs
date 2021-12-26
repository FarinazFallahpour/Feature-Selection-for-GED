namespace Gene_Selection
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Calc_Sim = new System.Windows.Forms.GroupBox();
            this.Max_Combo = new System.Windows.Forms.CheckBox();
            this.Jac_Combo = new System.Windows.Forms.CheckBox();
            this.Cos_Combo = new System.Windows.Forms.CheckBox();
            this.Cor_Combo = new System.Windows.Forms.CheckBox();
            this.Normalized_Check = new System.Windows.Forms.CheckBox();
            this.Sim_Calc_Btn = new System.Windows.Forms.Button();
            this.Read_Dataset_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DataSet_Cmb = new System.Windows.Forms.ComboBox();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Criteria_Cmb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Ant_Number_Txt = new System.Windows.Forms.NumericUpDown();
            this.Selected_Feature_Num_Txt = new System.Windows.Forms.NumericUpDown();
            this.Cycle_Lenght_Txt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Iteration_Txt = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Chart_Davis = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart_2 = new System.Windows.Forms.GroupBox();
            this.Chart_Accuracy = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Chart_Purity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Calc_Sim.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ant_Number_Txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Selected_Feature_Num_Txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cycle_Lenght_Txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Iteration_Txt)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Davis)).BeginInit();
            this.Chart_2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Accuracy)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Purity)).BeginInit();
            this.SuspendLayout();
            // 
            // Calc_Sim
            // 
            this.Calc_Sim.Controls.Add(this.Max_Combo);
            this.Calc_Sim.Controls.Add(this.Jac_Combo);
            this.Calc_Sim.Controls.Add(this.Cos_Combo);
            this.Calc_Sim.Controls.Add(this.Cor_Combo);
            this.Calc_Sim.Controls.Add(this.Normalized_Check);
            this.Calc_Sim.Controls.Add(this.Sim_Calc_Btn);
            this.Calc_Sim.Controls.Add(this.Read_Dataset_Btn);
            this.Calc_Sim.Controls.Add(this.label1);
            this.Calc_Sim.Controls.Add(this.DataSet_Cmb);
            this.Calc_Sim.Location = new System.Drawing.Point(26, 4);
            this.Calc_Sim.Name = "Calc_Sim";
            this.Calc_Sim.Size = new System.Drawing.Size(246, 253);
            this.Calc_Sim.TabIndex = 8;
            this.Calc_Sim.TabStop = false;
            this.Calc_Sim.Text = "Loading";
            // 
            // Max_Combo
            // 
            this.Max_Combo.AutoSize = true;
            this.Max_Combo.Location = new System.Drawing.Point(117, 174);
            this.Max_Combo.Name = "Max_Combo";
            this.Max_Combo.Size = new System.Drawing.Size(119, 17);
            this.Max_Combo.TabIndex = 26;
            this.Max_Combo.Text = "Maximal Information";
            this.Max_Combo.UseVisualStyleBackColor = true;
            // 
            // Jac_Combo
            // 
            this.Jac_Combo.AutoSize = true;
            this.Jac_Combo.Location = new System.Drawing.Point(24, 174);
            this.Jac_Combo.Name = "Jac_Combo";
            this.Jac_Combo.Size = new System.Drawing.Size(64, 17);
            this.Jac_Combo.TabIndex = 25;
            this.Jac_Combo.Text = "Jaccard";
            this.Jac_Combo.UseVisualStyleBackColor = true;
            // 
            // Cos_Combo
            // 
            this.Cos_Combo.AutoSize = true;
            this.Cos_Combo.Location = new System.Drawing.Point(117, 140);
            this.Cos_Combo.Name = "Cos_Combo";
            this.Cos_Combo.Size = new System.Drawing.Size(58, 17);
            this.Cos_Combo.TabIndex = 24;
            this.Cos_Combo.Text = "Cosine";
            this.Cos_Combo.UseVisualStyleBackColor = true;
            // 
            // Cor_Combo
            // 
            this.Cor_Combo.AutoSize = true;
            this.Cor_Combo.Location = new System.Drawing.Point(24, 140);
            this.Cor_Combo.Name = "Cor_Combo";
            this.Cor_Combo.Size = new System.Drawing.Size(76, 17);
            this.Cor_Combo.TabIndex = 23;
            this.Cor_Combo.Text = "Correlation";
            this.Cor_Combo.UseVisualStyleBackColor = true;
            // 
            // Normalized_Check
            // 
            this.Normalized_Check.AutoSize = true;
            this.Normalized_Check.Checked = true;
            this.Normalized_Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Normalized_Check.Location = new System.Drawing.Point(47, 61);
            this.Normalized_Check.Name = "Normalized_Check";
            this.Normalized_Check.Size = new System.Drawing.Size(129, 17);
            this.Normalized_Check.TabIndex = 20;
            this.Normalized_Check.Text = "Dataset Normalization";
            this.Normalized_Check.UseVisualStyleBackColor = true;
            // 
            // Sim_Calc_Btn
            // 
            this.Sim_Calc_Btn.Enabled = false;
            this.Sim_Calc_Btn.Location = new System.Drawing.Point(47, 207);
            this.Sim_Calc_Btn.Name = "Sim_Calc_Btn";
            this.Sim_Calc_Btn.Size = new System.Drawing.Size(137, 30);
            this.Sim_Calc_Btn.TabIndex = 18;
            this.Sim_Calc_Btn.Text = "Similarirty Computation";
            this.Sim_Calc_Btn.UseVisualStyleBackColor = true;
            this.Sim_Calc_Btn.Click += new System.EventHandler(this.Sim_Calc_Btn_Click);
            // 
            // Read_Dataset_Btn
            // 
            this.Read_Dataset_Btn.Location = new System.Drawing.Point(47, 92);
            this.Read_Dataset_Btn.Name = "Read_Dataset_Btn";
            this.Read_Dataset_Btn.Size = new System.Drawing.Size(137, 30);
            this.Read_Dataset_Btn.TabIndex = 7;
            this.Read_Dataset_Btn.Text = "Read_Dataset";
            this.Read_Dataset_Btn.UseVisualStyleBackColor = true;
            this.Read_Dataset_Btn.Click += new System.EventHandler(this.Read_Dataset_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dataset Name:";
            // 
            // DataSet_Cmb
            // 
            this.DataSet_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataSet_Cmb.FormattingEnabled = true;
            this.DataSet_Cmb.Items.AddRange(new object[] {
            "Sonar",
            "Colon",
            "Leukemia",
            "Lung",
            "CNS"});
            this.DataSet_Cmb.Location = new System.Drawing.Point(97, 24);
            this.DataSet_Cmb.Margin = new System.Windows.Forms.Padding(4);
            this.DataSet_Cmb.Name = "DataSet_Cmb";
            this.DataSet_Cmb.Size = new System.Drawing.Size(114, 21);
            this.DataSet_Cmb.TabIndex = 5;
            // 
            // Start_Btn
            // 
            this.Start_Btn.Enabled = false;
            this.Start_Btn.Location = new System.Drawing.Point(47, 222);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(138, 30);
            this.Start_Btn.TabIndex = 43;
            this.Start_Btn.Text = "Start";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Criteria_Cmb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Start_Btn);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Ant_Number_Txt);
            this.groupBox1.Controls.Add(this.Selected_Feature_Num_Txt);
            this.groupBox1.Controls.Add(this.Cycle_Lenght_Txt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Iteration_Txt);
            this.groupBox1.Location = new System.Drawing.Point(26, 283);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 271);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // Criteria_Cmb
            // 
            this.Criteria_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Criteria_Cmb.FormattingEnabled = true;
            this.Criteria_Cmb.Items.AddRange(new object[] {
            "Davis",
            "Accuracy",
            "Purity"});
            this.Criteria_Cmb.Location = new System.Drawing.Point(126, 185);
            this.Criteria_Cmb.Name = "Criteria_Cmb";
            this.Criteria_Cmb.Size = new System.Drawing.Size(79, 21);
            this.Criteria_Cmb.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Iteration:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Ant Number:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(54, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Cycle Lenght:";
            // 
            // Ant_Number_Txt
            // 
            this.Ant_Number_Txt.Location = new System.Drawing.Point(132, 59);
            this.Ant_Number_Txt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Ant_Number_Txt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Ant_Number_Txt.Name = "Ant_Number_Txt";
            this.Ant_Number_Txt.Size = new System.Drawing.Size(65, 20);
            this.Ant_Number_Txt.TabIndex = 30;
            this.Ant_Number_Txt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Selected_Feature_Num_Txt
            // 
            this.Selected_Feature_Num_Txt.Location = new System.Drawing.Point(132, 142);
            this.Selected_Feature_Num_Txt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Selected_Feature_Num_Txt.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Selected_Feature_Num_Txt.Name = "Selected_Feature_Num_Txt";
            this.Selected_Feature_Num_Txt.Size = new System.Drawing.Size(65, 20);
            this.Selected_Feature_Num_Txt.TabIndex = 32;
            this.Selected_Feature_Num_Txt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Cycle_Lenght_Txt
            // 
            this.Cycle_Lenght_Txt.Location = new System.Drawing.Point(132, 99);
            this.Cycle_Lenght_Txt.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Cycle_Lenght_Txt.Name = "Cycle_Lenght_Txt";
            this.Cycle_Lenght_Txt.Size = new System.Drawing.Size(65, 20);
            this.Cycle_Lenght_Txt.TabIndex = 38;
            this.Cycle_Lenght_Txt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Criteria:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Max Gene:";
            // 
            // Iteration_Txt
            // 
            this.Iteration_Txt.Location = new System.Drawing.Point(132, 19);
            this.Iteration_Txt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Iteration_Txt.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Iteration_Txt.Name = "Iteration_Txt";
            this.Iteration_Txt.Size = new System.Drawing.Size(65, 20);
            this.Iteration_Txt.TabIndex = 28;
            this.Iteration_Txt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Chart_Davis);
            this.groupBox2.Location = new System.Drawing.Point(297, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 216);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chart Davis";
            // 
            // Chart_Davis
            // 
            chartArea10.Name = "ChartArea1";
            this.Chart_Davis.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.Chart_Davis.Legends.Add(legend10);
            this.Chart_Davis.Location = new System.Drawing.Point(15, 15);
            this.Chart_Davis.Name = "Chart_Davis";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Legend1";
            series10.Name = "Series1";
            this.Chart_Davis.Series.Add(series10);
            this.Chart_Davis.Size = new System.Drawing.Size(520, 192);
            this.Chart_Davis.TabIndex = 0;
            this.Chart_Davis.Text = "chart1";
            // 
            // Chart_2
            // 
            this.Chart_2.Controls.Add(this.Chart_Accuracy);
            this.Chart_2.Location = new System.Drawing.Point(297, 226);
            this.Chart_2.Name = "Chart_2";
            this.Chart_2.Size = new System.Drawing.Size(544, 214);
            this.Chart_2.TabIndex = 23;
            this.Chart_2.TabStop = false;
            this.Chart_2.Text = "Chart Accuracy";
            // 
            // Chart_Accuracy
            // 
            chartArea11.Name = "ChartArea1";
            this.Chart_Accuracy.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.Chart_Accuracy.Legends.Add(legend11);
            this.Chart_Accuracy.Location = new System.Drawing.Point(15, 16);
            this.Chart_Accuracy.Name = "Chart_Accuracy";
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            this.Chart_Accuracy.Series.Add(series11);
            this.Chart_Accuracy.Size = new System.Drawing.Size(520, 192);
            this.Chart_Accuracy.TabIndex = 0;
            this.Chart_Accuracy.Text = "chart1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Chart_Purity);
            this.groupBox4.Location = new System.Drawing.Point(297, 446);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(544, 219);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Chart Purity";
            // 
            // Chart_Purity
            // 
            chartArea12.Name = "ChartArea1";
            this.Chart_Purity.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.Chart_Purity.Legends.Add(legend12);
            this.Chart_Purity.Location = new System.Drawing.Point(15, 16);
            this.Chart_Purity.Name = "Chart_Purity";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Legend1";
            series12.Name = "Series1";
            this.Chart_Purity.Series.Add(series12);
            this.Chart_Purity.Size = new System.Drawing.Size(520, 192);
            this.Chart_Purity.TabIndex = 0;
            this.Chart_Purity.Text = "chart1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 677);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Chart_2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Calc_Sim);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Calc_Sim.ResumeLayout(false);
            this.Calc_Sim.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ant_Number_Txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Selected_Feature_Num_Txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cycle_Lenght_Txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Iteration_Txt)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Davis)).EndInit();
            this.Chart_2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Accuracy)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Purity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Calc_Sim;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.CheckBox Normalized_Check;
        private System.Windows.Forms.Button Sim_Calc_Btn;
        private System.Windows.Forms.Button Read_Dataset_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DataSet_Cmb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown Ant_Number_Txt;
        private System.Windows.Forms.NumericUpDown Selected_Feature_Num_Txt;
        private System.Windows.Forms.NumericUpDown Cycle_Lenght_Txt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown Iteration_Txt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Davis;
        private System.Windows.Forms.CheckBox Max_Combo;
        private System.Windows.Forms.CheckBox Jac_Combo;
        private System.Windows.Forms.CheckBox Cos_Combo;
        private System.Windows.Forms.CheckBox Cor_Combo;
        private System.Windows.Forms.GroupBox Chart_2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Accuracy;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Purity;
        private System.Windows.Forms.ComboBox Criteria_Cmb;
        private System.Windows.Forms.Label label2;
    }
}

