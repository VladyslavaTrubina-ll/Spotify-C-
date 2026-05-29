# Інструкції по запуску Spotify C# .NET

## Системні вимоги

- **ОС**: Windows 10/11
- **.NET Framework**: .NET 6.0 або вище (з підтримкою Windows Forms)
- **Visual Studio**: 2022 або новіше (або VS Code з розширеннями C#)
- **MySQL Server**: 5.7 або вище
- **Пам'ять**: 2 ГБ мінімум
- **Місце на диску**: 500 МБ для установки .NET + проекту

## Крок 1: Установка .NET 6.0

1. Завантажте **[.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)**
2. Виконайте встановлювач
3. Перевірте встановлення відкритием терміналу та виконанням:
   ```bash
   dotnet --version
   ```

## Крок 2: Налаштування MySQL БД

### Варіант A: Використання існуючого SQL скрипту

1. Відкрийте MySQL командний рядок:
   ```bash
   mysql -u root -p
   ```
2. Введіть пароль (за замовчуванням порожний - просто натисніть Enter)
3. Виконайте скрипт із оригінального проекту:
   ```sql
   source spoty.sql
   ```

### Варіант B: Ручне створення БД

1. Відкрийте MySQL:
   ```bash
   mysql -u root
   ```
2. Створіть БД:
   ```sql
   CREATE DATABASE spoty CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   USE spoty;
   ```
3. Скопіюйте tavиці з `scriptDB/spoty.sql` та виконайте

## Крок 3: Налаштування проекту

### Перевірка параметрів підключення

Відкрийте `Controllers/ControladorDB.cs` і переконайтеся:

```csharp
private const string SERVER = "localhost";    // Ваш хост БД
private const string USER = "root";            // Користувач БД
private const string PASSWORD = "";            // Пароль БД
```

Якщо ваші параметри інші, змініть їх.

### Створення необхідних папок

В корені проекту створіть папки:

```
Spotify.WinForms/
├── canciones/       (для .mp3, .wav файлів)
├── imagenes/        (для .jpg, .png файлів)
└── podcasts/        (для подкастів)
```

## Крок 4: Запуск програми

### Варіант A: Visual Studio

1. Відкрийте файл `Spotify.WinForms.sln`
2. В меню виберіть **Build > Build Solution** (побудова)
3. Натисніть **F5** або **Debug > Start Debugging**

### Варіант B: Команда доступу

1. ВідкрийтеCmd/PowerShell в папці проекту
2. Виконайте:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## Крок 5: Тестування логіну

При запуску програми:

1. Введіть тестові облікові дані:
   - **Користувач**: (будь-яке існуюче в БД)
   - **Пароль**: (відповідний пароль)

2. Якщо БД не готова, спочатку додайте тестового користувача:
   ```sql
   INSERT INTO cliente (nombre, usuario, contrasena, tipo)
   VALUES ('Тестовий користувач', 'test', 'password', 'Free');
   ```

## Розв'язання проблем

### Проблема: "Не можна підключитися до БД"

**Рішення:**

1. Перевірте, чи запущен MySQL сервер
2. Переконайтеся в правильності параметрів в `ControladorDB.cs`
3. Спробуйте підключитися безпосередньо:
   ```bash
   mysql -u root
   ```

### Проблема: "Не знайдено файлів музики"

**Рішення:**

1. Переконайтеся, що папка `canciones` створена
2. Додайте .mp3 або .wav файли в цю папку
3. Перестартуйте програму

### Проблема: "MySql.Data не знайден"

**Рішення:**

1. Забезпечте наявність пакету в `.csproj`:
   ```xml
   <PackageReference Include="MySql.Data" Version="8.0.33" />
   ```
2. Виконайте:
   ```bash
   dotnet restore
   ```

## Особливості динамічного розташування

Програма демонструє **програмне створення елементів управління** під час роботи:

```csharp
// Приклад з FormLogin.cs
PictureBox pictureBox = new PictureBox();
pictureBox.Location = new Point(150, 10);     // X=150, Y=10
pictureBox.Size = new Size(200, 100);          // Ширина=200, Висота=100
this.Controls.Add(pictureBox);

ListBox listBox = new ListBox();
listBox.Location = new Point(50, 280);
this.Controls.Add(listBox);

ComboBox comboBox = new ComboBox();
comboBox.Items.AddRange(new[] { "Опція 1", "Опція 2" });
this.Controls.Add(comboBox);
```

Це відповідає вимогам практичної роботи про динамічне розташування елементів!

## Приклади кодів

### Цикл обчисленнь функції (FormCalcularFuncion.cs)

```csharp
for (double x = a; x <= b; x += c)
{
    double w = Math.Tan(x);
    listBox.Items.Add($"x = {x:F4}  =>  w = {w:F6}");
}
```

### Роботе з БД

```csharp
ControladorDB db = new ControladorDB("spoty");
db.StartConnection();
bool loggedIn = db.IniciarSesion("usuario", "password");
Cliente cliente = db.ObtenerCliente(1);
db.CloseConnection();
```

## Корисні посилання

- [.NET 6.0 Документація](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
- [Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [MySQL в .NET](https://dev.mysql.com/doc/connector-net/en/)
- [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/)

---

**Остання оновлення:** 2024  
**Версія:** 1.0
