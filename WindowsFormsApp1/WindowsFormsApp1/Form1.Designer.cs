namespace WindowsFormsApp1
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
            this.label1 = new System.Windows.Forms.Label();
            this.mtb_day = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mtb_freq = new System.Windows.Forms.MaskedTextBox();
            this.btn_button1 = new System.Windows.Forms.Button();
            this.label_ketqua = new System.Windows.Forms.Label();
            this.tb_ketqua = new System.Windows.Forms.TextBox();
            this.mtb_money = new System.Windows.Forms.MaskedTextBox();
            this.btn_chooseFile = new System.Windows.Forms.Button();
            this.tb_file = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(230, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dư đoán cụm khách hàng";
            // 
            // mtb_day
            // 
            this.mtb_day.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtb_day.Location = new System.Drawing.Point(517, 84);
            this.mtb_day.Mask = "00/00/0000";
            this.mtb_day.Name = "mtb_day";
            this.mtb_day.Size = new System.Drawing.Size(100, 26);
            this.mtb_day.TabIndex = 2;
            this.mtb_day.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nhập ngày đến gần nhất của khách hàng (MM/dd/YYYY)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhập số lần đến cửa hàng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nhập số tiền đã bỏ ra";
            // 
            // mtb_freq
            // 
            this.mtb_freq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtb_freq.Location = new System.Drawing.Point(517, 128);
            this.mtb_freq.Mask = "00000";
            this.mtb_freq.Name = "mtb_freq";
            this.mtb_freq.Size = new System.Drawing.Size(100, 26);
            this.mtb_freq.TabIndex = 8;
            this.mtb_freq.ValidatingType = typeof(int);
            // 
            // btn_button1
            // 
            this.btn_button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_button1.Location = new System.Drawing.Point(102, 273);
            this.btn_button1.Name = "btn_button1";
            this.btn_button1.Size = new System.Drawing.Size(101, 33);
            this.btn_button1.TabIndex = 10;
            this.btn_button1.Text = "Dự đoán";
            this.btn_button1.UseVisualStyleBackColor = true;
            this.btn_button1.Click += new System.EventHandler(this.btn_button1_Click);
            // 
            // label_ketqua
            // 
            this.label_ketqua.AutoSize = true;
            this.label_ketqua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ketqua.Location = new System.Drawing.Point(47, 350);
            this.label_ketqua.Name = "label_ketqua";
            this.label_ketqua.Size = new System.Drawing.Size(72, 20);
            this.label_ketqua.TabIndex = 11;
            this.label_ketqua.Text = "Kết quả: ";
            // 
            // tb_ketqua
            // 
            this.tb_ketqua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ketqua.Location = new System.Drawing.Point(125, 347);
            this.tb_ketqua.Name = "tb_ketqua";
            this.tb_ketqua.Size = new System.Drawing.Size(328, 26);
            this.tb_ketqua.TabIndex = 12;
            // 
            // mtb_money
            // 
            this.mtb_money.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtb_money.Location = new System.Drawing.Point(517, 184);
            this.mtb_money.Mask = "00000.00 $";
            this.mtb_money.Name = "mtb_money";
            this.mtb_money.Size = new System.Drawing.Size(100, 26);
            this.mtb_money.TabIndex = 9;
            // 
            // btn_chooseFile
            // 
            this.btn_chooseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_chooseFile.Location = new System.Drawing.Point(366, 234);
            this.btn_chooseFile.Name = "btn_chooseFile";
            this.btn_chooseFile.Size = new System.Drawing.Size(87, 33);
            this.btn_chooseFile.TabIndex = 16;
            this.btn_chooseFile.Text = "Chọn file";
            this.btn_chooseFile.UseVisualStyleBackColor = true;
            this.btn_chooseFile.Click += new System.EventHandler(this.btn_chooseFile_Click);
            // 
            // tb_file
            // 
            this.tb_file.Location = new System.Drawing.Point(366, 273);
            this.tb_file.Name = "tb_file";
            this.tb_file.Size = new System.Drawing.Size(346, 20);
            this.tb_file.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(613, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 33);
            this.button1.TabIndex = 18;
            this.button1.Text = "Dự đoán file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_file);
            this.Controls.Add(this.btn_chooseFile);
            this.Controls.Add(this.tb_ketqua);
            this.Controls.Add(this.label_ketqua);
            this.Controls.Add(this.btn_button1);
            this.Controls.Add(this.mtb_freq);
            this.Controls.Add(this.mtb_money);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtb_day);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtb_day;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtb_freq;
        private System.Windows.Forms.Button btn_button1;
        private System.Windows.Forms.Label label_ketqua;
        private System.Windows.Forms.TextBox tb_ketqua;
        private System.Windows.Forms.MaskedTextBox mtb_money;
        private System.Windows.Forms.Button btn_chooseFile;
        private System.Windows.Forms.TextBox tb_file;
        private System.Windows.Forms.Button button1;
    }
}

