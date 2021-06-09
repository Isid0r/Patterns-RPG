namespace rpg
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
            this.createArmies = new System.Windows.Forms.Button();
            this.MakeMove = new System.Windows.Forms.Button();
            this.ToEnd = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblCash1 = new System.Windows.Forms.Label();
            this.lblCash2 = new System.Windows.Forms.Label();
            this.textBoxCash1 = new System.Windows.Forms.TextBox();
            this.textBoxCash2 = new System.Windows.Forms.TextBox();
            this.lblInfoExceptions = new System.Windows.Forms.Label();
            this.lblArmy1 = new System.Windows.Forms.Label();
            this.lblArmy2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxBeep = new System.Windows.Forms.CheckBox();
            this.button1x1 = new System.Windows.Forms.Button();
            this.button3x3 = new System.Windows.Forms.Button();
            this.buttonAxA = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRedo = new System.Windows.Forms.Button();
            this.lblArmy12 = new System.Windows.Forms.Label();
            this.lblArmy13 = new System.Windows.Forms.Label();
            this.lblArmy22 = new System.Windows.Forms.Label();
            this.lblArmy23 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // createArmies
            // 
            this.createArmies.Location = new System.Drawing.Point(632, 70);
            this.createArmies.Name = "createArmies";
            this.createArmies.Size = new System.Drawing.Size(103, 23);
            this.createArmies.TabIndex = 0;
            this.createArmies.Text = "Создать армии";
            this.createArmies.UseVisualStyleBackColor = true;
            this.createArmies.Click += new System.EventHandler(this.createArmies_Click);
            // 
            // MakeMove
            // 
            this.MakeMove.Location = new System.Drawing.Point(632, 292);
            this.MakeMove.Name = "MakeMove";
            this.MakeMove.Size = new System.Drawing.Size(103, 23);
            this.MakeMove.TabIndex = 2;
            this.MakeMove.Text = "Сделать ход";
            this.MakeMove.UseVisualStyleBackColor = true;
            this.MakeMove.Click += new System.EventHandler(this.MakeMove_Click);
            // 
            // ToEnd
            // 
            this.ToEnd.Location = new System.Drawing.Point(632, 321);
            this.ToEnd.Name = "ToEnd";
            this.ToEnd.Size = new System.Drawing.Size(103, 23);
            this.ToEnd.TabIndex = 3;
            this.ToEnd.Text = "Играть до конца";
            this.ToEnd.UseVisualStyleBackColor = true;
            this.ToEnd.Click += new System.EventHandler(this.ToEnd_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(122, 70);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 13);
            this.lblInfo.TabIndex = 4;
            // 
            // lblCash1
            // 
            this.lblCash1.AutoSize = true;
            this.lblCash1.Location = new System.Drawing.Point(568, 117);
            this.lblCash1.Name = "lblCash1";
            this.lblCash1.Size = new System.Drawing.Size(104, 13);
            this.lblCash1.TabIndex = 5;
            this.lblCash1.Text = "Деньги 1-ой армии";
            // 
            // lblCash2
            // 
            this.lblCash2.AutoSize = true;
            this.lblCash2.Location = new System.Drawing.Point(693, 117);
            this.lblCash2.Name = "lblCash2";
            this.lblCash2.Size = new System.Drawing.Size(104, 13);
            this.lblCash2.TabIndex = 6;
            this.lblCash2.Text = "Деньги 2-ой армии";
            // 
            // textBoxCash1
            // 
            this.textBoxCash1.Location = new System.Drawing.Point(571, 145);
            this.textBoxCash1.Name = "textBoxCash1";
            this.textBoxCash1.Size = new System.Drawing.Size(100, 20);
            this.textBoxCash1.TabIndex = 7;
            // 
            // textBoxCash2
            // 
            this.textBoxCash2.Location = new System.Drawing.Point(696, 145);
            this.textBoxCash2.Name = "textBoxCash2";
            this.textBoxCash2.Size = new System.Drawing.Size(100, 20);
            this.textBoxCash2.TabIndex = 8;
            // 
            // lblInfoExceptions
            // 
            this.lblInfoExceptions.AutoSize = true;
            this.lblInfoExceptions.Location = new System.Drawing.Point(122, 212);
            this.lblInfoExceptions.Name = "lblInfoExceptions";
            this.lblInfoExceptions.Size = new System.Drawing.Size(0, 13);
            this.lblInfoExceptions.TabIndex = 9;
            // 
            // lblArmy1
            // 
            this.lblArmy1.AutoSize = true;
            this.lblArmy1.Location = new System.Drawing.Point(51, 302);
            this.lblArmy1.Name = "lblArmy1";
            this.lblArmy1.Size = new System.Drawing.Size(52, 13);
            this.lblArmy1.TabIndex = 10;
            this.lblArmy1.Text = "Армия 1:";
            // 
            // lblArmy2
            // 
            this.lblArmy2.AutoSize = true;
            this.lblArmy2.Location = new System.Drawing.Point(51, 384);
            this.lblArmy2.Name = "lblArmy2";
            this.lblArmy2.Size = new System.Drawing.Size(52, 13);
            this.lblArmy2.TabIndex = 11;
            this.lblArmy2.Text = "Армия 2:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::rpg.Properties.Resources._58942;
            this.pictureBox1.Location = new System.Drawing.Point(101, 353);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // checkBoxBeep
            // 
            this.checkBoxBeep.AutoSize = true;
            this.checkBoxBeep.Location = new System.Drawing.Point(683, 197);
            this.checkBoxBeep.Name = "checkBoxBeep";
            this.checkBoxBeep.Size = new System.Drawing.Size(70, 17);
            this.checkBoxBeep.TabIndex = 13;
            this.checkBoxBeep.Text = "Пикалка";
            this.checkBoxBeep.UseVisualStyleBackColor = true;
            this.checkBoxBeep.CheckedChanged += new System.EventHandler(this.checkBoxBeep_CheckedChanged);
            // 
            // button1x1
            // 
            this.button1x1.Location = new System.Drawing.Point(597, 350);
            this.button1x1.Name = "button1x1";
            this.button1x1.Size = new System.Drawing.Size(54, 23);
            this.button1x1.TabIndex = 14;
            this.button1x1.Text = "1x1";
            this.button1x1.UseVisualStyleBackColor = true;
            this.button1x1.Click += new System.EventHandler(this.button1x1_Click);
            // 
            // button3x3
            // 
            this.button3x3.Location = new System.Drawing.Point(657, 350);
            this.button3x3.Name = "button3x3";
            this.button3x3.Size = new System.Drawing.Size(54, 23);
            this.button3x3.TabIndex = 15;
            this.button3x3.Text = "3x3";
            this.button3x3.UseVisualStyleBackColor = true;
            this.button3x3.Click += new System.EventHandler(this.button3x3_Click);
            // 
            // buttonAxA
            // 
            this.buttonAxA.Location = new System.Drawing.Point(717, 350);
            this.buttonAxA.Name = "buttonAxA";
            this.buttonAxA.Size = new System.Drawing.Size(54, 23);
            this.buttonAxA.TabIndex = 16;
            this.buttonAxA.Text = "AxA";
            this.buttonAxA.UseVisualStyleBackColor = true;
            this.buttonAxA.Click += new System.EventHandler(this.buttonAxA_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(613, 379);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(75, 23);
            this.buttonUndo.TabIndex = 17;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.Location = new System.Drawing.Point(683, 379);
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.Size = new System.Drawing.Size(75, 23);
            this.buttonRedo.TabIndex = 18;
            this.buttonRedo.Text = "Redo";
            this.buttonRedo.UseVisualStyleBackColor = true;
            this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
            // 
            // lblArmy12
            // 
            this.lblArmy12.AutoSize = true;
            this.lblArmy12.Location = new System.Drawing.Point(51, 315);
            this.lblArmy12.Name = "lblArmy12";
            this.lblArmy12.Size = new System.Drawing.Size(52, 13);
            this.lblArmy12.TabIndex = 19;
            this.lblArmy12.Text = "Армия 1:";
            // 
            // lblArmy13
            // 
            this.lblArmy13.AutoSize = true;
            this.lblArmy13.Location = new System.Drawing.Point(51, 328);
            this.lblArmy13.Name = "lblArmy13";
            this.lblArmy13.Size = new System.Drawing.Size(52, 13);
            this.lblArmy13.TabIndex = 20;
            this.lblArmy13.Text = "Армия 1:";
            // 
            // lblArmy22
            // 
            this.lblArmy22.AutoSize = true;
            this.lblArmy22.Location = new System.Drawing.Point(51, 397);
            this.lblArmy22.Name = "lblArmy22";
            this.lblArmy22.Size = new System.Drawing.Size(52, 13);
            this.lblArmy22.TabIndex = 21;
            this.lblArmy22.Text = "Армия 2:";
            // 
            // lblArmy23
            // 
            this.lblArmy23.AutoSize = true;
            this.lblArmy23.Location = new System.Drawing.Point(51, 410);
            this.lblArmy23.Name = "lblArmy23";
            this.lblArmy23.Size = new System.Drawing.Size(52, 13);
            this.lblArmy23.TabIndex = 22;
            this.lblArmy23.Text = "Армия 2:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblArmy23);
            this.Controls.Add(this.lblArmy22);
            this.Controls.Add(this.lblArmy13);
            this.Controls.Add(this.lblArmy12);
            this.Controls.Add(this.buttonRedo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonAxA);
            this.Controls.Add(this.button3x3);
            this.Controls.Add(this.button1x1);
            this.Controls.Add(this.checkBoxBeep);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblArmy2);
            this.Controls.Add(this.lblArmy1);
            this.Controls.Add(this.lblInfoExceptions);
            this.Controls.Add(this.textBoxCash2);
            this.Controls.Add(this.textBoxCash1);
            this.Controls.Add(this.lblCash2);
            this.Controls.Add(this.lblCash1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.ToEnd);
            this.Controls.Add(this.MakeMove);
            this.Controls.Add(this.createArmies);
            this.Name = "Form1";
            this.Text = "Кровь и кишки";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createArmies;
        private System.Windows.Forms.Button MakeMove;
        private System.Windows.Forms.Button ToEnd;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblCash1;
        private System.Windows.Forms.Label lblCash2;
        private System.Windows.Forms.TextBox textBoxCash1;
        private System.Windows.Forms.TextBox textBoxCash2;
        private System.Windows.Forms.Label lblInfoExceptions;
        private System.Windows.Forms.Label lblArmy1;
        private System.Windows.Forms.Label lblArmy2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxBeep;
        private System.Windows.Forms.Button button1x1;
        private System.Windows.Forms.Button button3x3;
        private System.Windows.Forms.Button buttonAxA;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRedo;
        private System.Windows.Forms.Label lblArmy12;
        private System.Windows.Forms.Label lblArmy13;
        private System.Windows.Forms.Label lblArmy22;
        private System.Windows.Forms.Label lblArmy23;
    }
}

