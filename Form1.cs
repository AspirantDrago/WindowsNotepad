using System.Windows.Forms;

namespace WindowsNotepad
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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
    }
}
