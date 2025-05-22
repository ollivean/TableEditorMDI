using System;
using System.IO;
using System.Windows.Forms;

namespace TableEditorMDI
{
    public partial class TableForm : Form
    {
        private DataGridView dataGrid;

        public TableForm()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            dataGrid = new DataGridView();
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.AllowUserToAddRows = true;
            dataGrid.AllowUserToDeleteRows = true;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.ColumnHeadersVisible = true; // Обязательно показывать заголовки
            this.Controls.Add(dataGrid);
        }

        // Метод для добавления столбца
        public void AddColumn(string name) => dataGrid.Columns.Add(name, name);

        // Заполнение таблицы темой
        public void InitializeWithTheme(string theme)
        {
            dataGrid.Columns.Clear();

            if (theme == "Студенты")
            {
                dataGrid.Columns.Add("FIO", "ФИО");
                dataGrid.Columns.Add("Group", "Группа");
                dataGrid.Columns.Add("Age", "Возраст");
                dataGrid.Columns.Add("Grade", "Оценка");
            }
            else if (theme == "Книги")
            {
                dataGrid.Columns.Add("Title", "Название");
                dataGrid.Columns.Add("Author", "Автор");
                dataGrid.Columns.Add("Genre", "Жанр");
                dataGrid.Columns.Add("Year", "Год");
            }
            else if (theme == "Товары")
            {
                dataGrid.Columns.Add("ProductName", "Наименование");
                dataGrid.Columns.Add("Price", "Цена");
                dataGrid.Columns.Add("Quantity", "Количество");
                dataGrid.Columns.Add("Supplier", "Поставщик");
            }
        }

        // Очистка всех строк
        public void ClearTable() => dataGrid.Rows.Clear();

        // Сохранение в CSV
        public void SaveToCSV(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    sw.Write(dataGrid.Columns[i].HeaderText);
                    if (i < dataGrid.Columns.Count - 1) sw.Write(";");
                }
                sw.WriteLine();

                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            sw.Write(row.Cells[i].Value?.ToString() ?? "");
                            if (i < dataGrid.Columns.Count - 1) sw.Write(";");
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

        // Загрузка из CSV
        public void LoadFromCSV(string path)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();

            string[] lines = File.ReadAllLines(path);
            if (lines.Length == 0) return;

            string[] headers = lines[0].Split(';');
            foreach (string header in headers)
                dataGrid.Columns.Add(header, header);

            for (int i = 1; i < lines.Length; i++)
                dataGrid.Rows.Add(lines[i].Split(';'));
        }

        // Диалог для удаления столбца
        public void ShowRemoveColumnDialog()
        {
            if (dataGrid.Columns.Count == 0)
            {
                MessageBox.Show("В таблице нет столбцов для удаления.");
                return;
            }

            Form selectColumnForm = new Form()
            {
                Width = 300,
                Height = 150,
                Text = "Выберите столбец для удаления",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ComboBox comboBox = new ComboBox() { Left = 20, Top = 20, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (DataGridViewColumn col in dataGrid.Columns)
                comboBox.Items.Add(col.HeaderText);

            comboBox.SelectedIndex = 0;

            Button okButton = new Button() { Text = "Удалить", Left = 70, Width = 80, Top = 60, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Отмена", Left = 160, Width = 80, Top = 60, DialogResult = DialogResult.Cancel };

            selectColumnForm.Controls.Add(comboBox);
            selectColumnForm.Controls.Add(okButton);
            selectColumnForm.Controls.Add(cancelButton);

            selectColumnForm.AcceptButton = okButton;
            selectColumnForm.CancelButton = cancelButton;

            if (selectColumnForm.ShowDialog(this) == DialogResult.OK)
            {
                int indexToRemove = comboBox.SelectedIndex;
                if (indexToRemove >= 0 && indexToRemove < dataGrid.Columns.Count)
                {
                    dataGrid.Columns.RemoveAt(indexToRemove);
                }
            }
        }
    }
}

