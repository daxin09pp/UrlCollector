namespace URLCollector
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
            this.StartButton = new System.Windows.Forms.Button();
            this.SearchKeyWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.baiduCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TimeInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StartPage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PageNumber = new System.Windows.Forms.TextBox();
            this.sogouCheckBox = new System.Windows.Forms.CheckBox();
            this.ThreadNumberLabel = new System.Windows.Forms.Label();
            this.ThreadNumber = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CollectIPButton = new System.Windows.Forms.Button();
            this.UsableProxyCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.proxyCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(21, 303);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "开始扫描";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SearchKeyWord
            // 
            this.SearchKeyWord.Location = new System.Drawing.Point(115, 16);
            this.SearchKeyWord.Name = "SearchKeyWord";
            this.SearchKeyWord.Size = new System.Drawing.Size(167, 20);
            this.SearchKeyWord.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "关键词:";
            // 
            // baiduCheckBox
            // 
            this.baiduCheckBox.AutoSize = true;
            this.baiduCheckBox.Location = new System.Drawing.Point(115, 197);
            this.baiduCheckBox.Name = "baiduCheckBox";
            this.baiduCheckBox.Size = new System.Drawing.Size(53, 17);
            this.baiduCheckBox.TabIndex = 3;
            this.baiduCheckBox.Text = "Baidu";
            this.baiduCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "搜索引擎:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "间隔时间（秒）:";
            // 
            // TimeInterval
            // 
            this.TimeInterval.Location = new System.Drawing.Point(115, 51);
            this.TimeInterval.Name = "TimeInterval";
            this.TimeInterval.Size = new System.Drawing.Size(167, 20);
            this.TimeInterval.TabIndex = 7;
            this.TimeInterval.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "起始页码:";
            // 
            // StartPage
            // 
            this.StartPage.Location = new System.Drawing.Point(115, 91);
            this.StartPage.Name = "StartPage";
            this.StartPage.Size = new System.Drawing.Size(100, 20);
            this.StartPage.TabIndex = 9;
            this.StartPage.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "搜索页数:";
            // 
            // PageNumber
            // 
            this.PageNumber.Location = new System.Drawing.Point(115, 131);
            this.PageNumber.Name = "PageNumber";
            this.PageNumber.Size = new System.Drawing.Size(100, 20);
            this.PageNumber.TabIndex = 11;
            this.PageNumber.Text = "100";
            // 
            // sogouCheckBox
            // 
            this.sogouCheckBox.AutoSize = true;
            this.sogouCheckBox.Location = new System.Drawing.Point(115, 220);
            this.sogouCheckBox.Name = "sogouCheckBox";
            this.sogouCheckBox.Size = new System.Drawing.Size(57, 17);
            this.sogouCheckBox.TabIndex = 12;
            this.sogouCheckBox.Text = "Sogou";
            this.sogouCheckBox.UseVisualStyleBackColor = true;
            // 
            // ThreadNumberLabel
            // 
            this.ThreadNumberLabel.AutoSize = true;
            this.ThreadNumberLabel.Location = new System.Drawing.Point(19, 163);
            this.ThreadNumberLabel.Name = "ThreadNumberLabel";
            this.ThreadNumberLabel.Size = new System.Drawing.Size(58, 13);
            this.ThreadNumberLabel.TabIndex = 14;
            this.ThreadNumberLabel.Text = "并发线程:";
            // 
            // ThreadNumber
            // 
            this.ThreadNumber.Location = new System.Drawing.Point(115, 160);
            this.ThreadNumber.Name = "ThreadNumber";
            this.ThreadNumber.Size = new System.Drawing.Size(100, 20);
            this.ThreadNumber.TabIndex = 15;
            this.ThreadNumber.Text = "5";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 484);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.proxyCheckBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.sogouCheckBox);
            this.tabPage1.Controls.Add(this.baiduCheckBox);
            this.tabPage1.Controls.Add(this.StartButton);
            this.tabPage1.Controls.Add(this.PageNumber);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.ThreadNumberLabel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.StartPage);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.SearchKeyWord);
            this.tabPage1.Controls.Add(this.ThreadNumber);
            this.tabPage1.Controls.Add(this.TimeInterval);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(625, 458);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "收集网址";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RefreshButton);
            this.tabPage2.Controls.Add(this.CollectIPButton);
            this.tabPage2.Controls.Add(this.UsableProxyCount);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(625, 458);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代理获取";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CollectIPButton
            // 
            this.CollectIPButton.Location = new System.Drawing.Point(9, 159);
            this.CollectIPButton.Name = "CollectIPButton";
            this.CollectIPButton.Size = new System.Drawing.Size(102, 23);
            this.CollectIPButton.TabIndex = 3;
            this.CollectIPButton.Text = "收集代理";
            this.CollectIPButton.UseVisualStyleBackColor = true;
            this.CollectIPButton.Click += new System.EventHandler(this.CollectIPButton_Click);
            // 
            // UsableProxyCount
            // 
            this.UsableProxyCount.AutoSize = true;
            this.UsableProxyCount.Location = new System.Drawing.Point(98, 30);
            this.UsableProxyCount.Name = "UsableProxyCount";
            this.UsableProxyCount.Size = new System.Drawing.Size(13, 13);
            this.UsableProxyCount.TabIndex = 2;
            this.UsableProxyCount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "可用代理数量：";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(70, 494);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(19, 13);
            this.StatusLabel.TabIndex = 5;
            this.StatusLabel.Text = "无";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 494);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "当前状态：";
            // 
            // proxyCheckBox
            // 
            this.proxyCheckBox.AutoSize = true;
            this.proxyCheckBox.Location = new System.Drawing.Point(22, 270);
            this.proxyCheckBox.Name = "proxyCheckBox";
            this.proxyCheckBox.Size = new System.Drawing.Size(74, 17);
            this.proxyCheckBox.TabIndex = 16;
            this.proxyCheckBox.Text = "使用代理";
            this.proxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(9, 130);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(102, 23);
            this.RefreshButton.TabIndex = 4;
            this.RefreshButton.Text = "刷新本地代理";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "（每个搜索引擎）";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 516);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label9);
            this.Name = "Form1";
            this.Text = "URLCollector v0.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox SearchKeyWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox baiduCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TimeInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StartPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PageNumber;
        private System.Windows.Forms.CheckBox sogouCheckBox;
        private System.Windows.Forms.Label ThreadNumberLabel;
        private System.Windows.Forms.TextBox ThreadNumber;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label UsableProxyCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button CollectIPButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.CheckBox proxyCheckBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label label6;
    }
}

