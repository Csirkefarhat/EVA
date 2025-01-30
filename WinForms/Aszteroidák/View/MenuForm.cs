using Aszteroidák.View;

namespace Aszteroidák
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            BackgroundImage = Resources.menuBC;
            BackgroundImageLayout = ImageLayout.Stretch;
            MainText.BackColor = Color.FromArgb(32, 25, 79);
        }
        private void NewGame_Click(object sender, EventArgs e)
        {
            MainGame mainGame = new MainGame();
            mainGame.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
