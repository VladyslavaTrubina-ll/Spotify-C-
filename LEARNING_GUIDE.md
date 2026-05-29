# Посібник вивчення переведеного коду

## 🎯 З чого почати?

### 1. **Порядок вивчення файлів**

Рекомендуємо вивчати в наступному порядку:

1. **Program.cs** - точка входу
   - 📌 Як запускається програма
   - 📌 Конфігурація Windows Forms

2. **Models/Audio.cs** - базовий клас
   - 📌 Абстрактність в C#
   - 📌 Властивості (Properties)

3. **Models/Cancion.cs** - спадковість
   - 📌 Успадкування від Audio
   - 📌 Перевизначення конструкторів

4. **Models/Cliente.cs** - управління даними
   - 📌 Список (List<T>)
   - 📌 Типи даних у C#

5. **Controllers/ControladorDB.cs** - робота з БД
   - 📌 ADO.NET
   - 📌 Параметризовані запити
   - 📌 Підключення до MySQL

6. **Controllers/ControladorFicheros.cs** - файлові операції
   - 📌 File I/O
   - 📌 Directory операції

7. **Views/FormLogin.cs** - динамічне UI
   - 📌 **ГОЛОВНА ФОРМА** для розуміння динамічного розташування
   - 📌 Метод `CriarElementosProgramaticamente()`
   - 📌 Point для координат
   - 📌 Size для розмірів
   - 📌 Controls.Add() для додавання

8. **Views/FormCalcularFuncion.cs** - обчислення функцій
   - 📌 Цикл `for` з обчисленнями
   - 📌 Приклад з інструкції
   - 📌 ListBox.Items.Add()

9. **Views/FormMusica.cs** - музичний програвач
   - 📌 Динамічне наповнення ListBox
   - 📌 Обробники подій (EventHandlers)
   - 📌 ProgressBar

---

## 🔍 Ключові концепції для вивчення

### A. Динамічне розташування елементів (НАЙВАЖЛИВІШЕ!)

**Файл**: `FormLogin.cs`, метод `CriarElementosProgramaticamente()`

```csharp
// 1. Створити елемент
PictureBox pictureBox = new PictureBox();

// 2. Встановити координати (X, Y) верхнього лівого кута
pictureBox.Location = new Point(10, 10);

// 3. Встановити розміри (Ширина, Висота)
pictureBox.Size = new Size(200, 100);

// 4. Додати до форми
this.Controls.Add(pictureBox);
```

**Чому це важливо?**
- Це демонструє розташування програмно, НЕ через ToolBox
- Елементи створюються під час роботи програми
- Легко додавати/видаляти елементи динамічно

### B. Основні типи елементів

| Елемент | Використання | Приклад |
|---------|--------------|---------|
| **Label** | Статичний текст | `Label lbl = new Label(); lbl.Text = "Привіт";` |
| **TextBox** | Введення тексту | `TextBox txt = new TextBox();` |
| **Button** | Кнопка для дії | `Button btn = new Button(); btn.Click += (s,e) => {...};` |
| **ListBox** | Список елементів | `ListBox lst = new ListBox(); lst.Items.Add("елемент");` |
| **ComboBox** | Розпадаюче меню | `ComboBox cb = new ComboBox(); cb.Items.AddRange(new[]{"А","Б"});` |
| **PictureBox** | Зображення | `PictureBox pb = new PictureBox(); pb.Image = Image.FromFile(...);` |
| **ProgressBar** | Прогрес операції | `ProgressBar pb = new ProgressBar(); pb.Value = 50;` |

### C. Обробники подій

**Приклад**: FormLogin.cs

```csharp
// Створити кнопку
Button btnLogin = new Button();
btnLogin.Text = "Вхід";

// Додати обробник події Click
btnLogin.Click += BtnLogin_Click;

// Метод-обробник
private void BtnLogin_Click(object sender, EventArgs e)
{
    string usuario = txtUsuario.Text;
    // ... логіка входу
}
```

### D. Списки та цикли

**Приклад**: FormCalcularFuncion.cs

```csharp
ListBox listBox = new ListBox();

// Цикл обчислення функції
for (double x = a; x <= b; x += c)
{
    double w = Math.Tan(x);
    // Додати результат до списку
    listBox.Items.Add($"x = {x:F4}  =>  w = {w:F6}");
}
```

### E. Робота з БД

**Файл**: `ControladorDB.cs`

```csharp
// Підключення
MySqlConnection conexion = new MySqlConnection(connectionString);
conexion.Open();

// SQL запит
string sql = "SELECT * FROM cliente WHERE usuario = @usuario";

// Команда з параметрами (БЕЗПЕЧНО від SQL-ін'єкцій)
MySqlCommand cmd = new MySqlCommand(sql, conexion);
cmd.Parameters.AddWithValue("@usuario", usuario);

// Читання результатів
using (MySqlDataReader reader = cmd.ExecuteReader())
{
    while (reader.Read())
    {
        int id = reader.GetInt32("idCliente");
        string nome = reader.GetString("nombre");
    }
}

conexion.Close();
```

---

## 🔧 Практичні вправи для вивчення

### Вправа 1: Додати новий елемент на форму входу

**Завдання**: Додати CheckBox "Запам'ятати мене" на FormLogin

**Рішення**:

```csharp
// В методі CriarElementosProgramaticamente() додайте:

CheckBox chkMemorizar = new CheckBox();
chkMemorizar.Text = "Запам'ятати мене";
chkMemorizar.Location = new Point(150, 190);
chkMemorizar.ForeColor = Color.White;
this.Controls.Add(chkMemorizar);
```

### Вправа 2: Обчислити іншу функцію

**Завдання**: Додати обчислення функції `w = log(x)` в FormCalcularFuncion

**Рішення**:

```csharp
case "w = log(x)":
    if (x > 0) {
        w = Math.Log(x);
        resultado = $"x = {x:F4}  =>  w = {w:F6}";
    } else {
        resultado = $"x = {x:F4}  =>  w = невизначена (x <= 0)";
    }
    break;
```

### Вправа 3: Завантажити більше музичних файлів

**Завдання**: Розширити папку `canciones` музичними файлами та перевірити завантаження

**Рішення**: В папці проекту створіть папку `canciones` та добавте .mp3 файли

### Вправа 4: Додати новий SQL запит

**Завдання**: Додати метод для отримання кількості плейлистів клієнта

**Рішення**:

```csharp
public int ObtenerCantidadPlaylists(int idCliente)
{
    string sql = "SELECT COUNT(*) FROM playlist WHERE idCliente = @idCliente";
    
    using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
    {
        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        object result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : 0;
    }
}
```

---

## 📚 Важливі концепції C# для вивчення

### 1. Властивості (Properties)

```csharp
// Java стиль (СТАРИЙ)
public int getId() { return id; }
public void setId(int id) { this.id = id; }

// C# стиль (НОВИЙ) - властивості
public int Id { get; set; }

// Використання однакова
cliente.Id = 5;
int id = cliente.Id;
```

### 2. Список (List<T>)

```csharp
// Java
ArrayList<Cancion> canciones = new ArrayList<>();
canciones.add(new Cancion(...));

// C#
List<Cancion> canciones = new List<Cancion>();
canciones.Add(new Cancion(...));
```

### 3. String форматування

```csharp
// Старий стиль
string msg = "x = " + x.ToString() + " w = " + w.ToString();

// Новий стиль (String.Format)
string msg = String.Format("x = {0} w = {1}", x, w);

// Найкращий стиль (Interpolation)
string msg = $"x = {x} w = {w}";

// З форматуванням
string msg = $"x = {x:F4} w = {w:F6}"; // F4 = 4 знаки після коми
```

### 4. Using для управління ресурсами

```csharp
// Java
try (MySqlConnection con = new MySqlConnection(...)) {
    // використання
}

// C#
using (MySqlDataReader reader = cmd.ExecuteReader())
{
    // використання
} // автоматично закривається
```

### 5. Делегати та Lambda вирази

```csharp
// Старий стиль
button.Click += new EventHandler(Button_Click);
void Button_Click(object sender, EventArgs e) { }

// Новий стиль (Lambda)
button.Click += (s, e) => { /* код */ };

// Ще коротше для однієї команди
button.Click += (s, e) => MessageBox.Show("Клікнуто!");
```

---

## 🧪 Тестування та налагодження

### Як додати точки зупину (Breakpoints)

1. Клацніть на номер рядка в редакторі
2. Там з'явиться красна точка
3. Запустіть програму (F5)
4. Програма призупиниться на цьому рядку
5. Використовуйте Debug > Step Over (F10) або Debug > Step Into (F11)

### Перевірка значень змінних

У вікні Debug можна бачити:
- Локальні змінні (Locals)
- Список спостереження (Watch)
- Вивід консолі (Output)

---

## 📖 Рекомендовані посилання для подальшого вивчення

- [C# Beginner Handbook](https://learn.microsoft.com/en-us/training/modules/csharp-language-basics/)
- [Windows Forms Tutorial](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [ADO.NET Tutorial](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/)
- [MySQL Connector/NET](https://dev.mysql.com/doc/connector-net/en/)

---

## ✅ Контрольний список розуміння

Після вивчення повинні розуміти:

- [ ] Як створюється новий C# проект .NET
- [ ] Различие між Java Properties та C# Properties
- [ ] Як програмно розташовуються елементи на формі
- [ ] Як обробляються клієнтські події (Button Click, etc.)
- [ ] Як працює цикл `for` для обчисленнь
- [ ] Як підключатися до MySQL БД з ADO.NET
- [ ] Як параметризуються SQL запити
- [ ] Як читаються результати з MySqlDataReader
- [ ] Як динамічно додаються елементи до ListBox
- [ ] Що таке Lambda вирази та делегати

---

**Успіхів у вивченні! 🚀**

Якщо маєте питання - звертайтеся до коментарів у коді або документації.
