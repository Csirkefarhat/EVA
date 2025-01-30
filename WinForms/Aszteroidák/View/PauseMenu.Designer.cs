namespace Aszteroidák.View
{
    partial class PauseMenu
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
            SaveButton = new Button();
            LoadGame = new Button();
            PauseText = new Label();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Bauhaus 93", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SaveButton.Location = new Point(298, 224);
            SaveButton.Margin = new Padding(3, 4, 3, 4);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(318, 71);
            SaveButton.TabIndex = 1;
            SaveButton.Text = "Save Game";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadGame
            // 
            LoadGame.Font = new Font("Bauhaus 93", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoadGame.Location = new Point(298, 303);
            LoadGame.Margin = new Padding(3, 4, 3, 4);
            LoadGame.Name = "LoadGame";
            LoadGame.Size = new Size(318, 71);
            LoadGame.TabIndex = 2;
            LoadGame.Text = "Load Game";
            LoadGame.UseVisualStyleBackColor = true;
            LoadGame.Click += LoadGame_Click;
            // 
            // PauseText
            // 
            PauseText.AutoSize = true;
            PauseText.Font = new Font("Bauhaus 93", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PauseText.Location = new Point(365, 146);
            PauseText.Name = "PauseText";
            PauseText.Size = new Size(178, 53);
            PauseText.TabIndex = 3;
            PauseText.Text = "Paused";
            // 
            // PauseMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(PauseText);
            Controls.Add(LoadGame);
            Controls.Add(SaveButton);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PauseMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PauseMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveButton;
        private Button LoadGame;
        private Label PauseText;
    }
}