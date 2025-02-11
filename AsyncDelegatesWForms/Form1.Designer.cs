namespace AsyncDelegatesWForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            fromPath = new TextBox();
            toPath = new TextBox();
            progressBar1 = new ProgressBar();
            button1 = new Button();
            buttonStop = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 24);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 0;
            label1.Text = "Откуда";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 80);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 1;
            label2.Text = "Куда";
            // 
            // fromPath
            // 
            fromPath.Location = new Point(124, 21);
            fromPath.Name = "fromPath";
            fromPath.Size = new Size(201, 23);
            fromPath.TabIndex = 2;
            // 
            // toPath
            // 
            toPath.Location = new Point(124, 77);
            toPath.Name = "toPath";
            toPath.Size = new Size(201, 23);
            toPath.TabIndex = 3;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(42, 146);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(391, 31);
            progressBar1.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(235, 106);
            button1.Name = "button1";
            button1.Size = new Size(90, 23);
            button1.TabIndex = 5;
            button1.Text = "Копировать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(343, 106);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(90, 23);
            buttonStop.TabIndex = 6;
            buttonStop.Text = "Стоп";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 196);
            Controls.Add(buttonStop);
            Controls.Add(button1);
            Controls.Add(progressBar1);
            Controls.Add(toPath);
            Controls.Add(fromPath);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox fromPath;
        private TextBox toPath;
        private ProgressBar progressBar1;
        private Button button1;
        private Button buttonStop;
    }
}
