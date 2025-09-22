using System.IO;
using System.Windows.Forms;

namespace WindowsNotepad
{
    public partial class FormMain
    {
        private string _filename;
        private bool _isModified = false;
        private bool _isSaved = false;
        private int _scale = 100;

        private const int MIN_SCALE = 10;
        private const int MAX_SCALE = 1000;
        private const int STEP_SCALE = 10;
        private const int DEFAULT_FONT = 9;

        public string filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value.Trim();
                _filename = (_filename.Length > 0) ? _filename : "Новый текстовый документ.txt";
                UpdateTitle();
            }
        }

        public bool isModified
        {
            get
            {
                return _isModified;
            }
            set
            {
                _isModified = value;
                UpdateTitle();
            }
        }

        public int scale
        {
            get
            {
                return _scale;
            }
            set
            {
                if (value < MIN_SCALE || value > MAX_SCALE)
                {
                    return;
                }
                _scale = value;
                float fontSize = DEFAULT_FONT * scale / 100f;
                textBox.Font = new System.Drawing.Font(
                    textBox.Font.Name,
                    fontSize
                );
                toolStripStatusLabelScale.Text = $"{value}%";
            }
        }

        public void UpdateTitle()
        {
            if (_isModified)
            {
                this.Text = $"*{_filename} - Блокнот";
            } else
            {
                this.Text = $"{_filename} - Блокнот";
            }
        }

        public void NewDocument()
        {
            if (!CloseDocument())
            {
                return;
            }
            filename = "";
            textBox.Clear();
            isModified = false;
            _isSaved = false;
        }

        /// <summary>
        /// Закрытие документа
        /// </summary>
        /// <returns>
        /// true если получилось успешно закрыть
        /// иначе, false
        /// </returns>
        public bool CloseDocument()
        {
            if (!isModified)
            {
                return true;
            }
            DialogResult r = MessageBox.Show(
                $"Вы действительно хотите сохранить изменения в файле \"{filename}\"?",
                "Блокнот",
                MessageBoxButtons.YesNoCancel
            );
            switch (r)
            {
                case DialogResult.Yes:
                    return Save();
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }

        public void CloseWindow()
        {
            if (CloseDocument())
            {
                this.Close();
            }
        }

        public bool Save(bool checkIsSaved = true)
        {
            if (!_isSaved && checkIsSaved)
            {
                return SaveAs();
            }

            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filename);
                writer.Write(textBox.Text);
                _isSaved = true;
                isModified = false;
                return true;
            }
            catch (System.UnauthorizedAccessException)
            {
                ShowError($"Ошибка доступа при сохранении файла \"{filename}\"");
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                ShowError($"Путь \"{filename}\" не существует");
                return false;
            }
            catch (PathTooLongException)
            {
                ShowError($"Слишком длинное имя файла \"{filename}\"");
                return false;
            }
            catch (IOException)
            {
                ShowError($"Ошибка сохранения файла \"{filename}\"");
                return false;
            }
            catch (System.Security.SecurityException)
            {
                ShowError($"Ошибка безопасности при сохранении файла \"{filename}\"");
                return false;
            }
            finally
            {
                writer?.Close();
            }
        }

        public bool SaveAs()
        {
            if (_isSaved)
            {
                saveFileDialog.FileName = filename;
            } else
            {
                saveFileDialog.FileName = "";
            }
            DialogResult r = saveFileDialog.ShowDialog();
            if (r != DialogResult.OK)
            {
                return false;
            }
            filename = saveFileDialog.FileName;
            return Save(checkIsSaved: false);
        }

        public void TryOpen()
        {
            if (!CloseDocument())
            {
                return;
            }
            DialogResult r = openFileDialog.ShowDialog(this);
            if (r != DialogResult.OK)
            {
                return;
            }
            Open(openFileDialog.FileName);
        }

        public bool Open(string name)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(name);
                textBox.Text = reader.ReadToEnd();
                filename = name;
                isModified = false;
                _isSaved = true;
            }
            catch (FileNotFoundException)
            {
                ShowError($"Файл \"{filename}\" не существует");
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                ShowError($"Путь \"{filename}\" не существует");
                return false;
            }
            catch (IOException)
            {
                ShowError($"Ошибка чтения файла \"{filename}\"");
                return false;
            }
            finally
            {
                reader?.Close();
            }
            return true;
        }
    }
}
