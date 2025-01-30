namespace Aszteroidák
{
    partial class MenuForm
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
            NewGame = new Button();
            ExitButton = new Button();
            MainText = new Label();
            SuspendLayout();
            // 
            // NewGame
            // 
            NewGame.Font = new Font("Bauhaus 93", 16.2F);
            NewGame.Location = new Point(306, 224);
            NewGame.Margin = new Padding(3, 4, 3, 4);
            NewGame.Name = "NewGame";
            NewGame.Size = new Size(318, 71);
            NewGame.TabIndex = 0;
            NewGame.Text = "New Game";
            NewGame.UseVisualStyleBackColor = true;
            NewGame.Click += NewGame_Click;
            // 
            // ExitButton
            // 
            ExitButton.Font = new Font("Bauhaus 93", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ExitButton.Location = new Point(306, 302);
            ExitButton.Margin = new Padding(3, 4, 3, 4);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(318, 71);
            ExitButton.TabIndex = 1;
            ExitButton.Text = "Exit Game";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // MainText
            // 
            MainText.AutoSize = true;
            MainText.BackColor = Color.MidnightBlue;
            MainText.Font = new Font("Bauhaus 93", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MainText.ForeColor = SystemColors.Control;
            MainText.Location = new Point(324, 134);
            MainText.Name = "MainText";
            MainText.Size = new Size(279, 53);
            MainText.TabIndex = 2;
            MainText.Text = "Aszteroidák";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(MainText);
            Controls.Add(ExitButton);
            Controls.Add(NewGame);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button NewGame;
        private Button ExitButton;
        private Label MainText;
    }
}
