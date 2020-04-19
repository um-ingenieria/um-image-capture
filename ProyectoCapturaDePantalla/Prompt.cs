using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCapturaDePantalla
{
    public class Prompt
    {
        Form propmtForm;
        Label label;
        TextBox textBox;
        Button confirmation;

        public Prompt(string caption, string text)
        {
            this.propmtForm = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            this.label = new Label() { Left = 50, Top = 20, Text = text };
            this.textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            this.confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { propmtForm.Close(); };
            propmtForm.Controls.Add(textBox);
            propmtForm.Controls.Add(confirmation);
            propmtForm.Controls.Add(label);
            propmtForm.AcceptButton = confirmation;
        }

        public string show()
        {
            return propmtForm.ShowDialog() == DialogResult.OK ? this.textBox.Text : "";
        }
    }
}
