using System.Windows.Forms;

namespace WindowsNotepad
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            scale = 100;
            textBox.MouseWheel += new MouseEventHandler(textBox_MouseWheel);
        }

        private void textBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    scale += STEP_SCALE;
                }
                else if (e.Delta < 0)
                {
                    scale -= STEP_SCALE;
                }
            }
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            NewDocument();
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NewDocument();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CloseWindow();
        }

        private void textBox_TextChanged(object sender, System.EventArgs e)
        {
            isModified = true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            TryOpen();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add && e.Modifiers == Keys.Control)
            {
                scale += STEP_SCALE;
            }
            else if (e.KeyCode == Keys.Subtract && e.Modifiers == Keys.Control)
            {
                scale -= STEP_SCALE;
            }
        }
    }
}
