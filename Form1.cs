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

        private void cutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            textBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            textBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            textBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int n = textBox.SelectionLength;
            int pos = textBox.SelectionStart;
            if (n == 0)
            {
                return;
            }
            textBox.Text = textBox.Text.Remove(pos, n);
            textBox.SelectionStart = pos;
            textBox.ScrollToCaret();
        }

        private void cancelToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            textBox.Undo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            textBox.SelectAll();
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private void saveAsКакToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveAs();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseDocument())
            {
                e.Cancel = true;
            }
        }
    }
}
