using System;
using System.Windows.Forms;

namespace TableEditorMDI
{
    partial class ThemeSelectDialog
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxThemes;
        private Button buttonOK;
        private Button buttonCancel;
        private Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxThemes = new ComboBox();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            this.label1 = new Label();

            // Добавляем пункт "Пустая таблица"
            comboBoxThemes.Items.AddRange(new object[] {
            "Пустая таблица", "Студенты", "Книги", "Товары"
        });
            comboBoxThemes.Location = new System.Drawing.Point(20, 40);
            comboBoxThemes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxThemes.SelectedIndex = 0; // чтобы сразу был выбран первый элемент

            label1.Text = "Выберите тему:";
            label1.Location = new System.Drawing.Point(20, 15);

            buttonOK.Text = "OK";
            buttonOK.Location = new System.Drawing.Point(100, 80);
            buttonOK.Click += new EventHandler(this.OkButton_Click);

            buttonCancel.Text = "Отмена";
            buttonCancel.Location = new System.Drawing.Point(180, 80);
            buttonCancel.Click += new EventHandler(this.CancelButton_Click);

            this.ClientSize = new System.Drawing.Size(300, 130);
            this.Controls.Add(comboBoxThemes);
            this.Controls.Add(buttonOK);
            this.Controls.Add(buttonCancel);
            this.Controls.Add(label1);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Выбор темы";
        }
    }
}
