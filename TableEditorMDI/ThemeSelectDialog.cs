using System;
using System.Windows.Forms;

namespace TableEditorMDI
{
    public partial class ThemeSelectDialog : Form
    {
        private ComboBox comboBox;
        private Button okButton;
        private Button cancelButton;

        public string SelectedTheme { get; private set; }

        public ThemeSelectDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Выберите тему таблицы";
            this.Width = 300;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            comboBox = new ComboBox
            {
                Left = 20,
                Top = 20,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBox.Items.AddRange(new string[] { "Студенты", "Книги", "Товары", "Пустая таблица" });
            comboBox.SelectedIndex = 0;

            okButton = new Button
            {
                Text = "ОК",
                Left = 70,
                Width = 80,
                Top = 60,
                DialogResult = DialogResult.OK
            };

            cancelButton = new Button
            {
                Text = "Отмена",
                Left = 160,
                Width = 80,
                Top = 60,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(comboBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;

            okButton.Click += OkButton_Click;
            cancelButton.Click += CancelButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelectedTheme = comboBox.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
