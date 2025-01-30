using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aszteroidák.View
{
    public partial class PauseMenu : Form
    {
        private MainGame game;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        public PauseMenu(MainGame game)
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Esc_KeyDown);
            this.game = game;
            BackgroundImage = Resources.pauseBC;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Esc_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
        }


        private void SaveButton_Click(object? sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    game.SaveGame(saveFileDialog.FileName);
                }
                catch (Aszteroidák.Persistence.DataException)
                {
                    MessageBox.Show("Hiba keletkezett a mentés során.", "Aszteriodák", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    game.LoadGame(openFileDialog.FileName);
                }
                catch (Aszteroidák.Persistence.DataException)
                {
                    MessageBox.Show("Hiba keletkezett a betöltés során.", "Tic-Tac-Toe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
