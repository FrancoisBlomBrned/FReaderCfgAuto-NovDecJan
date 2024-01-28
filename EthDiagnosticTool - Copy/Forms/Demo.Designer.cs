
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.Forms
{
    using System.Windows.Forms;
    using static EthDiagnosticTool.Mapping.ToString;
    using static EthDiagnosticTool.Global.Helper.ConfigHelper;
    using System.Text.RegularExpressions;

    partial class Demo
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label1 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button7 = new Button();
            tableLayoutPanel7 = new TableLayoutPanel();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel12 = new TableLayoutPanel();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            tabPage8 = new TabPage();
            tabPage2 = new TabPage();
            tableLayoutPanel22 = new TableLayoutPanel();
            button20 = new Button();
            button21 = new Button();
            button22 = new Button();
            button23 = new Button();
            textBox8 = new TextBox();
            textBox9 = new TextBox();
            comboBox3 = new ComboBox();
            tableLayoutPanel23 = new TableLayoutPanel();
            comboBox4 = new ComboBox();
            label10 = new Label();
            tabPage3 = new TabPage();
            tableLayoutPanel13 = new TableLayoutPanel();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            button14 = new Button();
            tableLayoutPanel15 = new TableLayoutPanel();
            label5 = new Label();
            textBox3 = new TextBox();
            tableLayoutPanel14 = new TableLayoutPanel();
            label4 = new Label();
            textBox2 = new TextBox();
            button15 = new Button();
            button16 = new Button();
            button17 = new Button();
            button18 = new Button();
            tableLayoutPanel16 = new TableLayoutPanel();
            label6 = new Label();
            textBox4 = new TextBox();
            tabPage4 = new TabPage();
            tableLayoutPanel17 = new TableLayoutPanel();
            tabPage5 = new TabPage();
            tableLayoutPanel21 = new TableLayoutPanel();
            tabPage6 = new TabPage();
            tabPage7 = new TabPage();
            tableLayoutPanel18 = new TableLayoutPanel();
            tableLayoutPanel19 = new TableLayoutPanel();
            textBox5 = new TextBox();
            button19 = new Button();
            tableLayoutPanel20 = new TableLayoutPanel();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            comboBox2 = new ComboBox();
            groupBox3 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            richTextBox1 = new RichTextBox();
            tableLayoutPanel10 = new TableLayoutPanel();
            button4 = new Button();
            button5 = new Button();
            groupBox4 = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            textBox1 = new TextBox();
            tableLayoutPanel11 = new TableLayoutPanel();
            button6 = new Button();
            menuStrip1 = new MenuStrip();
            文件ToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            groupBox2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel22.SuspendLayout();
            tableLayoutPanel23.SuspendLayout();
            tabPage3.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel16.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage7.SuspendLayout();
            tableLayoutPanel18.SuspendLayout();
            tableLayoutPanel19.SuspendLayout();
            tableLayoutPanel20.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            groupBox4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox4, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 30);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 500F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1264, 706);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 800F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox3, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1258, 494);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel3.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(794, 488);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel9);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(788, 114);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "配置";
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel8, 0, 0);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(3, 19);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 2;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(782, 92);
            tableLayoutPanel9.TabIndex = 3;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 1, 0);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(776, 40);
            tableLayoutPanel8.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 4;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel6.Controls.Add(label1, 0, 0);
            tableLayoutPanel6.Controls.Add(comboBox1, 1, 0);
            tableLayoutPanel6.Controls.Add(button1, 2, 0);
            tableLayoutPanel6.Controls.Add(button7, 3, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(382, 34);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(13, 8);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "产品：";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(63, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(196, 25);
            comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(265, 5);
            button1.Name = "button1";
            button1.Size = new Size(54, 23);
            button1.TabIndex = 2;
            button1.Text = "选择";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button7.Location = new Point(325, 5);
            button7.Name = "button7";
            button7.Size = new Size(54, 23);
            button7.TabIndex = 3;
            button7.Text = "重置";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 4;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(button2, 0, 0);
            tableLayoutPanel7.Controls.Add(button3, 1, 0);
            tableLayoutPanel7.Controls.Add(label2, 2, 0);
            tableLayoutPanel7.Controls.Add(label3, 3, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(391, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(382, 34);
            tableLayoutPanel7.TabIndex = 1;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(3, 5);
            button2.Name = "button2";
            button2.Size = new Size(54, 23);
            button2.TabIndex = 0;
            button2.Text = "连接";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            button3.Location = new Point(63, 5);
            button3.Name = "button3";
            button3.Size = new Size(54, 23);
            button3.TabIndex = 1;
            button3.Text = "断开";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(133, 8);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 2;
            label2.Text = "状态：";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(183, 8);
            label3.Name = "label3";
            label3.Size = new Size(0, 17);
            label3.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tabControl1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 123);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(788, 362);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "诊断";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 19);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(782, 340);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel12);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(774, 310);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "0x10 会话模式";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.ColumnCount = 5;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel12.Controls.Add(button8, 0, 0);
            tableLayoutPanel12.Controls.Add(button9, 1, 0);
            tableLayoutPanel12.Controls.Add(button10, 2, 0);
            tableLayoutPanel12.Dock = DockStyle.Fill;
            tableLayoutPanel12.Location = new Point(3, 3);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 2;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle());
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel12.Size = new Size(768, 304);
            tableLayoutPanel12.TabIndex = 0;
            // 
            // button8
            // 
            button8.Dock = DockStyle.Fill;
            button8.Location = new Point(3, 3);
            button8.Name = "button8";
            button8.Size = new Size(147, 44);
            button8.TabIndex = 0;
            button8.Text = "标准会话";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Dock = DockStyle.Fill;
            button9.Location = new Point(156, 3);
            button9.Name = "button9";
            button9.Size = new Size(147, 44);
            button9.TabIndex = 1;
            button9.Text = "编程会话";
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Dock = DockStyle.Fill;
            button10.Location = new Point(309, 3);
            button10.Name = "button10";
            button10.Size = new Size(147, 44);
            button10.TabIndex = 2;
            button10.Text = "扩展会话";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // tabPage8
            // 
            tabPage8.Location = new Point(4, 26);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(774, 310);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "0x3E 会话保持";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel22);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(774, 310);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "0x27 安全访问";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel22
            // 
            tableLayoutPanel22.ColumnCount = 2;
            tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.20424F));
            tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.79576F));
            tableLayoutPanel22.Controls.Add(button20, 0, 1);
            tableLayoutPanel22.Controls.Add(button21, 0, 2);
            tableLayoutPanel22.Controls.Add(button22, 0, 3);
            tableLayoutPanel22.Controls.Add(button23, 0, 0);
            tableLayoutPanel22.Controls.Add(textBox8, 1, 3);
            tableLayoutPanel22.Controls.Add(textBox9, 1, 1);
            tableLayoutPanel22.Controls.Add(comboBox3, 1, 2);
            tableLayoutPanel22.Controls.Add(tableLayoutPanel23, 1, 0);
            tableLayoutPanel22.Dock = DockStyle.Fill;
            tableLayoutPanel22.Location = new Point(3, 3);
            tableLayoutPanel22.Name = "tableLayoutPanel22";
            tableLayoutPanel22.RowCount = 4;
            tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel22.Size = new Size(768, 304);
            tableLayoutPanel22.TabIndex = 0;
            // 
            // button20
            // 
            button20.Dock = DockStyle.Fill;
            button20.Location = new Point(3, 53);
            button20.Name = "button20";
            button20.Size = new Size(195, 94);
            button20.TabIndex = 0;
            button20.Text = "获取 Seed";
            button20.UseVisualStyleBackColor = true;
            // 
            // button21
            // 
            button21.Dock = DockStyle.Fill;
            button21.Location = new Point(3, 153);
            button21.Name = "button21";
            button21.Size = new Size(195, 44);
            button21.TabIndex = 1;
            button21.Text = "计算 Key";
            button21.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            button22.Dock = DockStyle.Fill;
            button22.Location = new Point(3, 203);
            button22.Name = "button22";
            button22.Size = new Size(195, 98);
            button22.TabIndex = 2;
            button22.Text = "发送 Key";
            button22.UseVisualStyleBackColor = true;
            // 
            // button23
            // 
            button23.Dock = DockStyle.Fill;
            button23.Location = new Point(3, 3);
            button23.Name = "button23";
            button23.Size = new Size(195, 44);
            button23.TabIndex = 3;
            button23.Text = "一键解锁";
            button23.UseVisualStyleBackColor = true;
            // 
            // textBox8
            // 
            textBox8.Dock = DockStyle.Fill;
            textBox8.Location = new Point(204, 203);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(561, 98);
            textBox8.TabIndex = 4;
            // 
            // textBox9
            // 
            textBox9.Dock = DockStyle.Fill;
            textBox9.Location = new Point(204, 53);
            textBox9.Multiline = true;
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(561, 94);
            textBox9.TabIndex = 5;
            // 
            // comboBox3
            // 
            comboBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(204, 162);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(561, 25);
            comboBox3.TabIndex = 6;
            // 
            // tableLayoutPanel23
            // 
            tableLayoutPanel23.ColumnCount = 2;
            tableLayoutPanel23.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel23.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel23.Controls.Add(comboBox4, 1, 0);
            tableLayoutPanel23.Controls.Add(label10, 0, 0);
            tableLayoutPanel23.Dock = DockStyle.Fill;
            tableLayoutPanel23.Location = new Point(204, 3);
            tableLayoutPanel23.Name = "tableLayoutPanel23";
            tableLayoutPanel23.RowCount = 1;
            tableLayoutPanel23.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel23.Size = new Size(561, 44);
            tableLayoutPanel23.TabIndex = 7;
            // 
            // comboBox4
            // 
            comboBox4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(83, 9);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(475, 25);
            comboBox4.TabIndex = 7;
            comboBox4.Text = "0x61";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(40, 13);
            label10.Name = "label10";
            label10.Size = new Size(37, 17);
            label10.TabIndex = 8;
            label10.Text = "Level";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel13);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(774, 310);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "0x19 DTC 信息";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.ColumnCount = 5;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel13.Controls.Add(button11, 0, 0);
            tableLayoutPanel13.Controls.Add(button12, 0, 1);
            tableLayoutPanel13.Controls.Add(button13, 1, 1);
            tableLayoutPanel13.Controls.Add(button14, 2, 1);
            tableLayoutPanel13.Controls.Add(tableLayoutPanel15, 3, 1);
            tableLayoutPanel13.Controls.Add(tableLayoutPanel14, 2, 0);
            tableLayoutPanel13.Controls.Add(button15, 1, 0);
            tableLayoutPanel13.Controls.Add(button16, 0, 2);
            tableLayoutPanel13.Controls.Add(button17, 1, 2);
            tableLayoutPanel13.Controls.Add(button18, 2, 2);
            tableLayoutPanel13.Controls.Add(tableLayoutPanel16, 3, 2);
            tableLayoutPanel13.Dock = DockStyle.Top;
            tableLayoutPanel13.Location = new Point(3, 3);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 4;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle());
            tableLayoutPanel13.Size = new Size(768, 251);
            tableLayoutPanel13.TabIndex = 0;
            // 
            // button11
            // 
            button11.Dock = DockStyle.Fill;
            button11.Location = new Point(3, 3);
            button11.Name = "button11";
            button11.Size = new Size(147, 44);
            button11.TabIndex = 0;
            button11.Text = "清除所有 DTC";
            button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            button12.Dock = DockStyle.Fill;
            button12.Location = new Point(3, 53);
            button12.Name = "button12";
            button12.Size = new Size(147, 44);
            button12.TabIndex = 2;
            button12.Text = "当前 DTC 数量";
            button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Dock = DockStyle.Fill;
            button13.Location = new Point(156, 53);
            button13.Name = "button13";
            button13.Size = new Size(147, 44);
            button13.TabIndex = 3;
            button13.Text = "历史 DTC 数量";
            button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Dock = DockStyle.Fill;
            button14.Location = new Point(309, 53);
            button14.Name = "button14";
            button14.Size = new Size(147, 44);
            button14.TabIndex = 4;
            button14.Text = "指定条件 DTC 数量";
            button14.UseVisualStyleBackColor = true;
            button14.Click += button14_Click;
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.ColumnCount = 2;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel15.Controls.Add(label5, 0, 0);
            tableLayoutPanel15.Controls.Add(textBox3, 1, 0);
            tableLayoutPanel15.Location = new Point(462, 53);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 1;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel15.Size = new Size(147, 44);
            tableLayoutPanel15.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(10, 13);
            label5.Name = "label5";
            label5.Size = new Size(60, 17);
            label5.TabIndex = 0;
            label5.Text = "DTC 掩码";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(76, 10);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(68, 23);
            textBox3.TabIndex = 1;
            textBox3.Text = "FF";
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.ColumnCount = 2;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.Controls.Add(label4, 0, 0);
            tableLayoutPanel14.Controls.Add(textBox2, 1, 0);
            tableLayoutPanel14.Location = new Point(309, 3);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 1;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.Size = new Size(147, 44);
            tableLayoutPanel14.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(10, 13);
            label4.Name = "label4";
            label4.Size = new Size(60, 17);
            label4.TabIndex = 0;
            label4.Text = "DTC 掩码";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(76, 10);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(68, 23);
            textBox2.TabIndex = 1;
            textBox2.Text = "FFFFFF";
            // 
            // button15
            // 
            button15.Dock = DockStyle.Fill;
            button15.Location = new Point(156, 3);
            button15.Name = "button15";
            button15.Size = new Size(147, 44);
            button15.TabIndex = 6;
            button15.Text = "清除指定条件 DTC";
            button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            button16.Dock = DockStyle.Fill;
            button16.Location = new Point(3, 103);
            button16.Name = "button16";
            button16.Size = new Size(147, 44);
            button16.TabIndex = 7;
            button16.Text = "当前 DTC 信息";
            button16.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            button17.Dock = DockStyle.Fill;
            button17.Location = new Point(156, 103);
            button17.Name = "button17";
            button17.Size = new Size(147, 44);
            button17.TabIndex = 8;
            button17.Text = "历史 DTC 信息";
            button17.UseVisualStyleBackColor = true;
            // 
            // button18
            // 
            button18.Dock = DockStyle.Fill;
            button18.Location = new Point(309, 103);
            button18.Name = "button18";
            button18.Size = new Size(147, 44);
            button18.TabIndex = 9;
            button18.Text = "指定条件 DTC 信息";
            button18.UseVisualStyleBackColor = true;
            button18.Click += button18_Click;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.ColumnCount = 2;
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Controls.Add(label6, 0, 0);
            tableLayoutPanel16.Controls.Add(textBox4, 1, 0);
            tableLayoutPanel16.Location = new Point(462, 103);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 1;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Size = new Size(147, 44);
            tableLayoutPanel16.TabIndex = 10;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(10, 13);
            label6.Name = "label6";
            label6.Size = new Size(60, 17);
            label6.TabIndex = 0;
            label6.Text = "DTC 掩码";
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox4.Location = new Point(76, 10);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(68, 23);
            textBox4.TabIndex = 1;
            textBox4.Text = "FF";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(tableLayoutPanel17);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(774, 310);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "0x22 读 DID";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel17
            // 
            tableLayoutPanel17.ColumnCount = 5;
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel17.Dock = DockStyle.Fill;
            tableLayoutPanel17.Location = new Point(3, 3);
            tableLayoutPanel17.Name = "tableLayoutPanel17";
            tableLayoutPanel17.RowCount = 2;
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel17.RowStyles.Add(new RowStyle());
            tableLayoutPanel17.Size = new Size(768, 304);
            tableLayoutPanel17.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(tableLayoutPanel21);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(774, 310);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "0x2E 写 DID";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel21
            // 
            tableLayoutPanel21.ColumnCount = 5;
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel21.Dock = DockStyle.Fill;
            tableLayoutPanel21.Location = new Point(3, 3);
            tableLayoutPanel21.Name = "tableLayoutPanel21";
            tableLayoutPanel21.RowCount = 2;
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel21.RowStyles.Add(new RowStyle());
            tableLayoutPanel21.Size = new Size(768, 304);
            tableLayoutPanel21.TabIndex = 0;
            // 
            // tabPage6
            // 
            tabPage6.Location = new Point(4, 26);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(774, 310);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "其他";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            tabPage7.Controls.Add(tableLayoutPanel18);
            tabPage7.Location = new Point(4, 26);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(774, 310);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "自定义";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.ColumnCount = 1;
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.Controls.Add(tableLayoutPanel19, 0, 1);
            tableLayoutPanel18.Controls.Add(tableLayoutPanel20, 0, 0);
            tableLayoutPanel18.Location = new Point(65, 39);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.RowCount = 2;
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.Size = new Size(625, 235);
            tableLayoutPanel18.TabIndex = 0;
            // 
            // tableLayoutPanel19
            // 
            tableLayoutPanel19.ColumnCount = 2;
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.5283051F));
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.4716978F));
            tableLayoutPanel19.Controls.Add(textBox5, 0, 0);
            tableLayoutPanel19.Controls.Add(button19, 1, 0);
            tableLayoutPanel19.Dock = DockStyle.Fill;
            tableLayoutPanel19.Location = new Point(3, 53);
            tableLayoutPanel19.Name = "tableLayoutPanel19";
            tableLayoutPanel19.RowCount = 1;
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.Size = new Size(619, 179);
            tableLayoutPanel19.TabIndex = 0;
            // 
            // textBox5
            // 
            textBox5.Dock = DockStyle.Fill;
            textBox5.Location = new Point(3, 3);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(517, 173);
            textBox5.TabIndex = 0;
            // 
            // button19
            // 
            button19.Dock = DockStyle.Fill;
            button19.Location = new Point(526, 3);
            button19.Name = "button19";
            button19.Size = new Size(90, 173);
            button19.TabIndex = 1;
            button19.Text = "发送";
            button19.UseVisualStyleBackColor = true;
            button19.Click += button19_Click;
            // 
            // tableLayoutPanel20
            // 
            tableLayoutPanel20.ColumnCount = 7;
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.9999981F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.0000038F));
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel20.Controls.Add(label7, 0, 0);
            tableLayoutPanel20.Controls.Add(label8, 2, 0);
            tableLayoutPanel20.Controls.Add(label9, 4, 0);
            tableLayoutPanel20.Controls.Add(textBox6, 3, 0);
            tableLayoutPanel20.Controls.Add(textBox7, 5, 0);
            tableLayoutPanel20.Controls.Add(comboBox2, 1, 0);
            tableLayoutPanel20.Dock = DockStyle.Fill;
            tableLayoutPanel20.Location = new Point(3, 3);
            tableLayoutPanel20.Name = "tableLayoutPanel20";
            tableLayoutPanel20.RowCount = 1;
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel20.Size = new Size(619, 44);
            tableLayoutPanel20.TabIndex = 1;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(14, 13);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 0;
            label7.Text = "解析算法";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(330, 13);
            label8.Name = "label8";
            label8.Size = new Size(63, 17);
            label8.TabIndex = 1;
            label8.Text = "附加参数1";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(476, 13);
            label9.Name = "label9";
            label9.Size = new Size(63, 17);
            label9.TabIndex = 2;
            label9.Text = "附加参数2";
            // 
            // textBox6
            // 
            textBox6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox6.Location = new Point(399, 10);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(67, 23);
            textBox6.TabIndex = 3;
            // 
            // textBox7
            // 
            textBox7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox7.Location = new Point(545, 10);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(67, 23);
            textBox7.TabIndex = 4;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(76, 9);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(244, 25);
            comboBox2.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel4);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(803, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(452, 488);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "结果";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(richTextBox1, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel10, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.Size = new Size(446, 466);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(440, 420);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.Right;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel10.Controls.Add(button4, 0, 0);
            tableLayoutPanel10.Controls.Add(button5, 1, 0);
            tableLayoutPanel10.Location = new Point(283, 429);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(160, 34);
            tableLayoutPanel10.TabIndex = 1;
            // 
            // button4
            // 
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(3, 3);
            button4.Name = "button4";
            button4.Size = new Size(74, 28);
            button4.TabIndex = 0;
            button4.Text = "保存";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Fill;
            button5.Location = new Point(83, 3);
            button5.Name = "button5";
            button5.Size = new Size(74, 28);
            button5.TabIndex = 1;
            button5.Text = "清除";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tableLayoutPanel5);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(3, 503);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1258, 200);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "日志";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel11, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 19);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel5.Size = new Size(1252, 178);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(1246, 132);
            textBox1.TabIndex = 0;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Right;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel11.Controls.Add(button6, 0, 0);
            tableLayoutPanel11.Location = new Point(1169, 141);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(80, 34);
            tableLayoutPanel11.TabIndex = 1;
            // 
            // button6
            // 
            button6.Dock = DockStyle.Fill;
            button6.Location = new Point(3, 3);
            button6.Name = "button6";
            button6.Size = new Size(74, 28);
            button6.TabIndex = 0;
            button6.Text = "清除";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1264, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            文件ToolStripMenuItem.Size = new Size(44, 21);
            文件ToolStripMenuItem.Text = "文件";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 739);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1264, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // Demo
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 761);
            Controls.Add(statusStrip1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MaximumSize = new Size(1440, 900);
            MinimumSize = new Size(1280, 800);
            Name = "Demo";
            Text = "UDS 诊断工具（Demo）";
            FormClosing += Demo_FormClosing;
            Load += Demo_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            groupBox2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel22.ResumeLayout(false);
            tableLayoutPanel22.PerformLayout();
            tableLayoutPanel23.ResumeLayout(false);
            tableLayoutPanel23.PerformLayout();
            tabPage3.ResumeLayout(false);
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel14.PerformLayout();
            tableLayoutPanel16.ResumeLayout(false);
            tableLayoutPanel16.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage7.ResumeLayout(false);
            tableLayoutPanel18.ResumeLayout(false);
            tableLayoutPanel19.ResumeLayout(false);
            tableLayoutPanel19.PerformLayout();
            tableLayoutPanel20.ResumeLayout(false);
            tableLayoutPanel20.PerformLayout();
            groupBox3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel10.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TableLayoutPanel tableLayoutPanel4;
        private RichTextBox richTextBox1;
        private TabPage tabPage8;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件ToolStripMenuItem;
        private StatusStrip statusStrip1;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox textBox1;
        private TableLayoutPanel tableLayoutPanel7;
        private Button button2;
        private Button button3;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private Button button4;
        private Button button5;
        private TableLayoutPanel tableLayoutPanel11;
        private Button button6;
        private Button button7;
        private TableLayoutPanel tableLayoutPanel12;
        private Button button8;
        private Button button9;
        private Button button10;
        private TableLayoutPanel tableLayoutPanel13;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private TableLayoutPanel tableLayoutPanel15;
        private Label label5;
        private TextBox textBox3;
        private TableLayoutPanel tableLayoutPanel14;
        private Label label4;
        private TextBox textBox2;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private TableLayoutPanel tableLayoutPanel16;
        private Label label6;
        private TextBox textBox4;
        private TableLayoutPanel tableLayoutPanel17;
        private TableLayoutPanel tableLayoutPanel18;
        private TableLayoutPanel tableLayoutPanel19;
        private TextBox textBox5;
        private Button button19;
        private TableLayoutPanel tableLayoutPanel20;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox6;
        private TextBox textBox7;
        private ComboBox comboBox2;
        private TableLayoutPanel tableLayoutPanel21;
        private TableLayoutPanel tableLayoutPanel22;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private TextBox textBox8;
        private TextBox textBox9;
        private ComboBox comboBox3;
        private TableLayoutPanel tableLayoutPanel23;
        private ComboBox comboBox4;
        private Label label10;
    }
}