namespace ImgContainer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labImage = new System.Windows.Forms.Label();
            this.labLog = new System.Windows.Forms.Label();
            this.btnChoose = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.labInfo1 = new System.Windows.Forms.Label();
            this.btnCombine = new System.Windows.Forms.Button();
            this.btnSeparate = new System.Windows.Forms.Button();
            this.labInfo2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labImage
            // 
            this.labImage.BackColor = System.Drawing.SystemColors.HighlightText;
            this.labImage.Location = new System.Drawing.Point(13, 13);
            this.labImage.Name = "labImage";
            this.labImage.Size = new System.Drawing.Size(480, 270);
            this.labImage.TabIndex = 0;
            this.labImage.Text = "Click to choose a image";
            this.labImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labLog
            // 
            this.labLog.BackColor = System.Drawing.SystemColors.Control;
            this.labLog.Location = new System.Drawing.Point(12, 290);
            this.labLog.Name = "labLog";
            this.labLog.Size = new System.Drawing.Size(640, 12);
            this.labLog.TabIndex = 1;
            this.labLog.Text = "Resolution: 0 x 0    Max volume: 0 B";
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(624, 27);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(32, 23);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = "...";
            this.btnChoose.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtInput.Location = new System.Drawing.Point(499, 28);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(120, 21);
            this.txtInput.TabIndex = 3;
            // 
            // labInfo1
            // 
            this.labInfo1.AutoSize = true;
            this.labInfo1.Location = new System.Drawing.Point(499, 13);
            this.labInfo1.Name = "labInfo1";
            this.labInfo1.Size = new System.Drawing.Size(83, 12);
            this.labInfo1.TabIndex = 4;
            this.labInfo1.Text = "Choose a file";
            // 
            // btnCombine
            // 
            this.btnCombine.BackColor = System.Drawing.Color.LightCoral;
            this.btnCombine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCombine.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCombine.Location = new System.Drawing.Point(499, 100);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(157, 75);
            this.btnCombine.TabIndex = 5;
            this.btnCombine.Text = "Combine!";
            this.btnCombine.UseVisualStyleBackColor = false;
            // 
            // btnSeparate
            // 
            this.btnSeparate.BackColor = System.Drawing.Color.LightCyan;
            this.btnSeparate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSeparate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSeparate.Location = new System.Drawing.Point(499, 208);
            this.btnSeparate.Name = "btnSeparate";
            this.btnSeparate.Size = new System.Drawing.Size(157, 75);
            this.btnSeparate.TabIndex = 6;
            this.btnSeparate.Text = "Separate!";
            this.btnSeparate.UseVisualStyleBackColor = false;
            // 
            // labInfo2
            // 
            this.labInfo2.AutoSize = true;
            this.labInfo2.Location = new System.Drawing.Point(499, 52);
            this.labInfo2.Name = "labInfo2";
            this.labInfo2.Size = new System.Drawing.Size(59, 12);
            this.labInfo2.TabIndex = 8;
            this.labInfo2.Text = "Size: 0 B";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 308);
            this.Controls.Add(this.labInfo2);
            this.Controls.Add(this.btnSeparate);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.labInfo1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.labLog);
            this.Controls.Add(this.labImage);
            this.Name = "Form1";
            this.Text = "ImageContainer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labImage;
        private System.Windows.Forms.Label labLog;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label labInfo1;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.Button btnSeparate;
        private System.Windows.Forms.Label labInfo2;
    }
}

