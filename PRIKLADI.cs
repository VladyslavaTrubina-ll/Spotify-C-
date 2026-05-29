// ============================================================================
// ПРИКЛАДИ ПРОГРАМНОГО РОЗТАШУВАННЯ ЕЛЕМЕНТІВ В C# .NET WINDOWS FORMS
// ============================================================================
// Цей файл демонструє, як програмно створювати й розташовувати елементи 
// управління при роботі програми (не через ToolBox)
// ============================================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spotify.WinForms.Examples
{
    // ========================================================================
    // ПРИКЛАД 1: Базовий приклад з PictureBox і Button (як в інструкції)
    // ========================================================================
    
    public class ExemploBasico : Form
    {
        public ExemploBasico()
        {
            this.Text = "Приклад 1: Базовий";
            this.Size = new Size(500, 400);
            
            // PictureBox розташований програмно
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(10, 10);        // Координати (X, Y)
            pictureBox.Size = new Size(200, 100);           // Розміри (Ширина, Висота)
            pictureBox.BackColor = Color.LightGray;
            this.Controls.Add(pictureBox);
            
            // Button розташований програмно
            Button button = new Button();
            button.Location = new Point(10, 200);
            button.Text = "Натисніть мене";
            button.Click += (s, e) => MessageBox.Show("Кнопку натиснуто!");
            this.Controls.Add(button);
        }
    }

    // ========================================================================
    // ПРИКЛАД 2: ListBox з динамічним заповненням
    // ========================================================================
    
    public class ExemploListBox : Form
    {
        private ListBox listBox;
        
        public ExemploListBox()
        {
            this.Text = "Приклад 2: ListBox";
            this.Size = new Size(400, 300);
            
            // Створення ListBox програмно
            listBox = new ListBox();
            listBox.Location = new Point(20, 20);
            listBox.Size = new Size(350, 200);
            listBox.BackColor = Color.FromArgb(40, 40, 40);
            listBox.ForeColor = Color.White;
            this.Controls.Add(listBox);
            
            // Кнопка для додавання елементів
            Button btnDodadir = new Button();
            btnDodadir.Text = "Додати";
            btnDodadir.Location = new Point(20, 230);
            btnDodadir.Click += (s, e) => 
            {
                listBox.Items.Add($"Елемент {listBox.Items.Count + 1}");
            };
            this.Controls.Add(btnDodadir);
            
            // Кнопка для очищення
            Button btnOchistiti = new Button();
            btnOchistiti.Text = "Очистити";
            btnOchistiti.Location = new Point(150, 230);
            btnOchistiti.Click += (s, e) => listBox.Items.Clear();
            this.Controls.Add(btnOchistiti);
        }
    }

    // ========================================================================
    // ПРИКЛАД 3: ComboBox для вибору
    // ========================================================================
    
    public class ExemploComboBox : Form
    {
        public ExemploComboBox()
        {
            this.Text = "Приклад 3: ComboBox";
            this.Size = new Size(400, 250);
            
            Label lbl = new Label();
            lbl.Text = "Виберіть опцію:";
            lbl.Location = new Point(20, 20);
            lbl.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lbl);
            
            // ComboBox програмно
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(20, 50);
            comboBox.Size = new Size(300, 25);
            comboBox.Items.AddRange(new[] 
            { 
                "Українська",
                "English",
                "Español",
                "Português"
            });
            comboBox.SelectedIndex = 0;
            this.Controls.Add(comboBox);
            
            // TextBox для вивода обраної опції
            TextBox txt = new TextBox();
            txt.Location = new Point(20, 100);
            txt.Size = new Size(300, 25);
            txt.ReadOnly = true;
            this.Controls.Add(txt);
            
            // Обробник вибору
            comboBox.SelectedValueChanged += (s, e) =>
            {
                txt.Text = $"Ви обрали: {comboBox.SelectedItem}";
            };
        }
    }

    // ========================================================================
    // ПРИКЛАД 4: Цикл з обчисленням функцій (як в інструкції)
    // ========================================================================
    
    public class ExemploFuncija : Form
    {
        private ListBox listBox;
        
        public ExemploFuncija()
        {
            this.Text = "Приклад 4: Обчислення функцій";
            this.Size = new Size(500, 450);
            
            // Поля введення
            Label lblA = new Label();
            lblA.Text = "a = ";
            lblA.Location = new Point(20, 20);
            this.Controls.Add(lblA);
            
            TextBox txtA = new TextBox();
            txtA.Text = "0";
            txtA.Location = new Point(50, 20);
            txtA.Size = new Size(80, 25);
            this.Controls.Add(txtA);
            
            Label lblB = new Label();
            lblB.Text = "b = ";
            lblB.Location = new Point(20, 55);
            this.Controls.Add(lblB);
            
            TextBox txtB = new TextBox();
            txtB.Text = "3.14159";
            txtB.Location = new Point(50, 55);
            txtB.Size = new Size(80, 25);
            this.Controls.Add(txtB);
            
            Label lblC = new Label();
            lblC.Text = "крок = ";
            lblC.Location = new Point(20, 90);
            this.Controls.Add(lblC);
            
            TextBox txtC = new TextBox();
            txtC.Text = "0.5";
            txtC.Location = new Point(50, 90);
            txtC.Size = new Size(80, 25);
            this.Controls.Add(txtC);
            
            // Кнопка обчислення
            Button btnCalcular = new Button();
            btnCalcular.Text = "Обчислити w=tg(x)";
            btnCalcular.Location = new Point(20, 130);
            btnCalcular.Size = new Size(200, 30);
            btnCalcular.BackColor = Color.FromArgb(30, 215, 96);
            btnCalcular.ForeColor = Color.White;
            this.Controls.Add(btnCalcular);
            
            // ListBox для результатів
            listBox = new ListBox();
            listBox.Location = new Point(20, 170);
            listBox.Size = new Size(440, 220);
            listBox.BackColor = Color.FromArgb(40, 40, 40);
            listBox.ForeColor = Color.FromArgb(0, 200, 100);
            listBox.Font = new Font("Courier New", 9);
            this.Controls.Add(listBox);
            
            // Обробник кнопки
            btnCalcular.Click += (s, e) =>
            {
                try
                {
                    double a = double.Parse(txtA.Text);
                    double b = double.Parse(txtB.Text);
                    double c = double.Parse(txtC.Text);
                    
                    listBox.Items.Clear();
                    
                    // ЦИКЛ ОБЧИСЛЕННЯ (як в інструкції)
                    for (double x = a; x <= b; x += c)
                    {
                        double w = Math.Tan(x);
                        listBox.Items.Add($"x={x:F4}  =>  w={w:F6}");
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка введення!");
                }
            };
        }
    }

    // ========================================================================
    // ПРИКЛАД 5: ProgressBar і Label з оновленням
    // ========================================================================
    
    public class ExemploProgress : Form
    {
        private ProgressBar progressBar;
        private Label label;
        
        public ExemploProgress()
        {
            this.Text = "Приклад 5: ProgressBar";
            this.Size = new Size(400, 250);
            
            label = new Label();
            label.Text = "Прогрес: 0%";
            label.Location = new Point(20, 20);
            label.Font = new Font("Arial", 11, FontStyle.Bold);
            this.Controls.Add(label);
            
            // ProgressBar програмно
            progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 60);
            progressBar.Size = new Size(350, 30);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            this.Controls.Add(progressBar);
            
            // Кнопка для запуску
            Button btn = new Button();
            btn.Text = "Почати процес";
            btn.Location = new Point(20, 120);
            btn.Size = new Size(200, 40);
            btn.Click += (s, e) => SimuliuvatiProcess();
            this.Controls.Add(btn);
        }
        
        private void SimuliuvatiProcess()
        {
            for (int i = 0; i <= 100; i += 10)
            {
                progressBar.Value = i;
                label.Text = $"Прогрес: {i}%";
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
            }
            MessageBox.Show("Готово!");
        }
    }

    // ========================================================================
    // ПРИКЛАД 6: GroupBox з контролами (як в інструкції)
    // ========================================================================
    
    public class ExemploGroupBox : Form
    {
        public ExemploGroupBox()
        {
            this.Text = "Приклад 6: GroupBox";
            this.Size = new Size(450, 350);
            
            // GroupBox 1
            GroupBox grp1 = new GroupBox();
            grp1.Text = "Особисті дані";
            grp1.Location = new Point(20, 20);
            grp1.Size = new Size(400, 130);
            this.Controls.Add(grp1);
            
            Label lbl1 = new Label();
            lbl1.Text = "Ім'я:";
            lbl1.Location = new Point(20, 30);
            grp1.Controls.Add(lbl1);
            
            TextBox txt1 = new TextBox();
            txt1.Location = new Point(100, 30);
            txt1.Size = new Size(250, 25);
            grp1.Controls.Add(txt1);
            
            Label lbl2 = new Label();
            lbl2.Text = "Email:";
            lbl2.Location = new Point(20, 70);
            grp1.Controls.Add(lbl2);
            
            TextBox txt2 = new TextBox();
            txt2.Location = new Point(100, 70);
            txt2.Size = new Size(250, 25);
            grp1.Controls.Add(txt2);
            
            // GroupBox 2
            GroupBox grp2 = new GroupBox();
            grp2.Text = "Вибір";
            grp2.Location = new Point(20, 170);
            grp2.Size = new Size(400, 100);
            this.Controls.Add(grp2);
            
            RadioButton rad1 = new RadioButton();
            rad1.Text = "Чоловіча";
            rad1.Location = new Point(30, 30);
            grp2.Controls.Add(rad1);
            
            RadioButton rad2 = new RadioButton();
            rad2.Text = "Жіноча";
            rad2.Location = new Point(30, 60);
            grp2.Controls.Add(rad2);
        }
    }

    // ========================================================================
    // ПРИКЛАД 7: Формула обчислення (за прикладом із інструкції)
    // ========================================================================
    
    public class ExemploFormula : Form
    {
        public ExemploFormula()
        {
            this.Text = "Приклад 7: Формули";
            this.Size = new Size(500, 400);
            
            // Демонстрація розрахунків
            Label lbl = new Label();
            lbl.Text = "Приклади розрахунків";
            lbl.Location = new Point(20, 20);
            lbl.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(lbl);
            
            ListBox listBox = new ListBox();
            listBox.Location = new Point(20, 60);
            listBox.Size = new Size(440, 300);
            listBox.Items.Add("=== Перетворення часу ===");
            
            // Приклад 1: Обчислення мінут та секунд
            listBox.Items.Add("60 секунд = 1 хвилина");
            listBox.Items.Add("3600 секунд = 1 година");
            
            // Приклад 2: Обчислення площі
            double radius = 5;
            double area = Math.PI * radius * radius;
            listBox.Items.Add($"Площа кола (r={radius}): {area:F2}");
            
            // Приклад 3: Дистанційні розрахунки (v = d/t)
            double distance = 100;
            double time = 5;
            double velocity = distance / time;
            listBox.Items.Add($"Швидкість: v={distance}/{time}={velocity} м/с");
            
            this.Controls.Add(listBox);
        }
    }

    // ========================================================================
    //點 ЗАПУСК ПРИКЛАДІВ
    // ========================================================================
    
    public static class PrimeriLancher
    {
        public static void RunExample1() => new ExemploBasico().ShowDialog();
        public static void RunExample2() => new ExemploListBox().ShowDialog();
        public static void RunExample3() => new ExemploComboBox().ShowDialog();
        public static void RunExample4() => new ExemploFuncija().ShowDialog();
        public static void RunExample5() => new ExemploProgress().ShowDialog();
        public static void RunExample6() => new ExemploGroupBox().ShowDialog();
        public static void RunExample7() => new ExemploFormula().ShowDialog();
    }
}

// ============================================================================
// ПРИМІТКИ:
// ============================================================================
// 1. Location = new Point(X, Y) - координати верхнього лівого кута
// 2. Size = new Size(Width, Height) - розміри елемента
// 3. Controls.Add() - додає елемент до форми
// 4. Всі ці операції виконуються програмно, БЕЗ використання ToolBox
// 5. Це дозволяє створювати динамічні інтерфейси під час роботи програми
// ============================================================================
