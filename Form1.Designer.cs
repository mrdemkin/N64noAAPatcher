
namespace N64noAAPatcher
{
    partial class Form1
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
            this.addFileBtn = new System.Windows.Forms.Button();
            this.chooseOutputBtn = new System.Windows.Forms.Button();
            this.outputPath = new System.Windows.Forms.TextBox();
            this.filesListView = new System.Windows.Forms.ListView();
            this.Start = new System.Windows.Forms.Button();
            this.RemoveFromListBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addFileBtn
            // 
            this.addFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFileBtn.Location = new System.Drawing.Point(583, 12);
            this.addFileBtn.Name = "addFileBtn";
            this.addFileBtn.Size = new System.Drawing.Size(75, 23);
            this.addFileBtn.TabIndex = 0;
            this.addFileBtn.Text = "Add file...";
            this.addFileBtn.UseVisualStyleBackColor = true;
            this.addFileBtn.Click += new System.EventHandler(this.addFileBtn_Click);
            // 
            // chooseOutputBtn
            // 
            this.chooseOutputBtn.Location = new System.Drawing.Point(583, 41);
            this.chooseOutputBtn.Name = "chooseOutputBtn";
            this.chooseOutputBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseOutputBtn.TabIndex = 1;
            this.chooseOutputBtn.Text = "Choose output";
            this.chooseOutputBtn.UseVisualStyleBackColor = true;
            this.chooseOutputBtn.Click += new System.EventHandler(this.chooseOutputBtn_Click);
            // 
            // outputPath
            // 
            this.outputPath.Location = new System.Drawing.Point(42, 44);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(529, 20);
            this.outputPath.TabIndex = 2;
            // 
            // filesListView
            // 
            this.filesListView.HideSelection = false;
            this.filesListView.Location = new System.Drawing.Point(42, 98);
            this.filesListView.Name = "filesListView";
            this.filesListView.Size = new System.Drawing.Size(529, 199);
            this.filesListView.TabIndex = 3;
            this.filesListView.MultiSelect = false;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.List;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(583, 303);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // RemoveFromListBtn
            // 
            this.RemoveFromListBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveFromListBtn.Location = new System.Drawing.Point(583, 98);
            this.RemoveFromListBtn.Name = "RemoveFromListBtn";
            this.RemoveFromListBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveFromListBtn.TabIndex = 5;
            this.RemoveFromListBtn.Text = "Remove";
            this.RemoveFromListBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 334);
            this.Controls.Add(this.RemoveFromListBtn);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.filesListView);
            this.Controls.Add(this.outputPath);
            this.Controls.Add(this.chooseOutputBtn);
            this.Controls.Add(this.addFileBtn);
            this.Name = "Form1";
            this.Text = "N64noAAPatcher v0.1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addFileBtn;
        private System.Windows.Forms.Button chooseOutputBtn;
        private System.Windows.Forms.TextBox outputPath;
        private System.Windows.Forms.ListView filesListView;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button RemoveFromListBtn;
    }
}

