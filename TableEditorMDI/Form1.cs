using System;
using System.IO;
using System.Windows.Forms;

namespace TableEditorMDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Text = "Редактор таблиц";
            this.WindowState = FormWindowState.Maximized;

            CreateMenu();
        }

        private void CreateMenu()
        {
            MenuStrip menuStrip = new MenuStrip();

            // --- Меню "Файл" ---
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem newTableItem = new ToolStripMenuItem("Создать таблицу", null, NewTable_Click);
            ToolStripMenuItem openTableItem = new ToolStripMenuItem("Открыть таблицу", null, OpenTable_Click);
            ToolStripMenuItem saveTableItem = new ToolStripMenuItem("Сохранить таблицу", null, SaveTable_Click);
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход", null, (s, e) => this.Close());
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] {
                newTableItem, openTableItem, saveTableItem, new ToolStripSeparator(), exitItem
            });

            // --- Меню "Редактировать" ---
            ToolStripMenuItem editMenu = new ToolStripMenuItem("Редактировать");
            ToolStripMenuItem addColumnItem = new ToolStripMenuItem("Добавить столбец", null, AddColumn_Click);
            ToolStripMenuItem removeColumnItem = new ToolStripMenuItem("Удалить столбец", null, RemoveColumn_Click);
            ToolStripMenuItem clearTableItem = new ToolStripMenuItem("Очистить таблицу", null, ClearTable_Click);
            editMenu.DropDownItems.AddRange(new ToolStripItem[] {
                addColumnItem, removeColumnItem, clearTableItem
            });

            // --- Добавление в главное меню ---
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void NewTable_Click(object sender, EventArgs e)
        {
            using (var dialog = new ThemeSelectDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var table = new TableForm();
                    table.MdiParent = this;

                    if (dialog.SelectedTheme == "Пустая таблица")
                    {
                        // Просто не добавляем столбцы, создаём пустую таблицу
                        table.Text = "Пустая таблица";
                    }
                    else
                    {
                        table.InitializeWithTheme(dialog.SelectedTheme);
                        table.Text = $"Тема: {dialog.SelectedTheme}";
                    }

                    table.Show();
                }
            }
        }

        private void OpenTable_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV файлы (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TableForm tableForm = new TableForm();
                tableForm.MdiParent = this;
                tableForm.LoadFromCSV(openFileDialog.FileName);
                tableForm.Text = Path.GetFileName(openFileDialog.FileName);
                tableForm.Show();
            }
        }

        private void SaveTable_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is TableForm activeTable)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    activeTable.SaveToCSV(saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("Нет активной таблицы для сохранения.");
            }
        }

        private void AddColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is TableForm activeTable)
            {
                string columnName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите название столбца:", "Добавить столбец", "НовыйСтолбец");

                if (!string.IsNullOrWhiteSpace(columnName))
                {
                    activeTable.AddColumn(columnName);
                }
            }
        }

        private void RemoveColumn_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is TableForm activeTable)
            {
                activeTable.ShowRemoveColumnDialog();
            }
        }

        private void ClearTable_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is TableForm activeTable)
            {
                activeTable.ClearTable();
            }
        }
    }
}

