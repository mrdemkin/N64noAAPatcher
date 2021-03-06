
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
            this.AddDirBtn = new System.Windows.Forms.Button();
            this.outputPathLabel = new System.Windows.Forms.Label();
            this.FilesTableLable = new System.Windows.Forms.Label();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // addFileBtn
            // 
            this.addFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFileBtn.Location = new System.Drawing.Point(545, 6);
            this.addFileBtn.Name = "addFileBtn";
            this.addFileBtn.Size = new System.Drawing.Size(75, 23);
            this.addFileBtn.TabIndex = 0;
            this.addFileBtn.Text = "Add file";
            this.addFileBtn.UseVisualStyleBackColor = true;
            this.addFileBtn.Click += new System.EventHandler(this.addFileBtn_Click);
            // 
            // chooseOutputBtn
            // 
            this.chooseOutputBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseOutputBtn.Location = new System.Drawing.Point(545, 35);
            this.chooseOutputBtn.Name = "chooseOutputBtn";
            this.chooseOutputBtn.Size = new System.Drawing.Size(75, 23);
            this.chooseOutputBtn.TabIndex = 1;
            this.chooseOutputBtn.Text = "Choose output";
            this.chooseOutputBtn.UseVisualStyleBackColor = true;
            this.chooseOutputBtn.Click += new System.EventHandler(this.chooseOutputBtn_Click);
            // 
            // outputPath
            // 
            this.outputPath.Location = new System.Drawing.Point(8, 38);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(529, 20);
            this.outputPath.TabIndex = 2;
            // 
            // filesListView
            // 
            this.filesListView.HideSelection = false;
            this.filesListView.Location = new System.Drawing.Point(8, 92);
            this.filesListView.MultiSelect = false;
            this.filesListView.Name = "filesListView";
            this.filesListView.Size = new System.Drawing.Size(529, 199);
            this.filesListView.TabIndex = 3;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.List;
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Location = new System.Drawing.Point(545, 297);
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
            this.RemoveFromListBtn.Location = new System.Drawing.Point(545, 92);
            this.RemoveFromListBtn.Name = "RemoveFromListBtn";
            this.RemoveFromListBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveFromListBtn.TabIndex = 5;
            this.RemoveFromListBtn.Text = "Remove";
            this.RemoveFromListBtn.UseVisualStyleBackColor = true;
            this.RemoveFromListBtn.Click += new System.EventHandler(this.RemoveFromList);
            // 
            // AddDirBtn
            // 
            this.AddDirBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddDirBtn.Location = new System.Drawing.Point(454, 6);
            this.AddDirBtn.Name = "AddDirBtn";
            this.AddDirBtn.Size = new System.Drawing.Size(79, 23);
            this.AddDirBtn.TabIndex = 6;
            this.AddDirBtn.Text = "Add directory";
            this.AddDirBtn.UseVisualStyleBackColor = true;
            this.AddDirBtn.Click += new System.EventHandler(this.AddDirBtn_Click);
            // 
            // outputPathLabel
            // 
            this.outputPathLabel.AutoSize = true;
            this.outputPathLabel.Location = new System.Drawing.Point(6, 19);
            this.outputPathLabel.Name = "outputPathLabel";
            this.outputPathLabel.Size = new System.Drawing.Size(66, 13);
            this.outputPathLabel.TabIndex = 7;
            this.outputPathLabel.Text = "Output path:";
            // 
            // FilesTableLable
            // 
            this.FilesTableLable.AutoSize = true;
            this.FilesTableLable.Location = new System.Drawing.Point(6, 73);
            this.FilesTableLable.Name = "FilesTableLable";
            this.FilesTableLable.Size = new System.Drawing.Size(83, 13);
            this.FilesTableLable.TabIndex = 8;
            this.FilesTableLable.Text = "Files to process:";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mainProgressBar.Location = new System.Drawing.Point(8, 297);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(529, 23);
            this.mainProgressBar.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 334);
            this.Controls.Add(this.mainProgressBar);
            this.Controls.Add(this.FilesTableLable);
            this.Controls.Add(this.outputPathLabel);
            this.Controls.Add(this.AddDirBtn);
            this.Controls.Add(this.RemoveFromListBtn);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.filesListView);
            this.Controls.Add(this.outputPath);
            this.Controls.Add(this.chooseOutputBtn);
            this.Controls.Add(this.addFileBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "N64noAAPatcher v0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button AddDirBtn;
        private System.Windows.Forms.Label outputPathLabel;
        private System.Windows.Forms.Label FilesTableLable;
        private System.Windows.Forms.ProgressBar mainProgressBar;
    }
}

