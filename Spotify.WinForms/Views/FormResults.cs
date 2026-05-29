using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Spotify.WinForms.Views
{
    public class FormResults : Form
    {
        private ListBox listBox;
        private Label lblCount;

        public FormResults(string title, List<string> items, int count)
        {
            this.Text = title;
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            listBox = new ListBox() { Location = new Point(10, 10), Size = new Size(560, 380) };
            lblCount = new Label() { Location = new Point(10, 400), Size = new Size(560, 30) };

            if (items != null)
            {
                foreach (var it in items) listBox.Items.Add(it);
            }

            lblCount.Text = $"Кількість обчислень: {count}";

            this.Controls.Add(listBox);
            this.Controls.Add(lblCount);
        }
    }
}
