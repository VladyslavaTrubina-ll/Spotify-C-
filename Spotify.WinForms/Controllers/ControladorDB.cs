using MySql.Data.MySqlClient;
using System;

namespace Spotify.WinForms.Controllers
{
    public class ControladorDB
    {
        private MySqlConnection _conexion;
        private string _nombreDB;
        private int _idClienteActual = -1;
        private const string SERVER = "127.0.0.1";
        private const uint PORT = 3306;
        private const string USER = "root";
        private const string PASSWORD = "";

        public ControladorDB(string nombreDB)
        {
            _nombreDB = nombreDB;
        }

        public ControladorDB()
        {
        }

        public bool StartConnection()
        {
            bool conexionEstablecida = false;
            try
            {
                string connectionString = $"Server={SERVER};Port={PORT};Database={_nombreDB};User Id={USER};Password={PASSWORD};SslMode=None;Charset=utf8mb4;";
                _conexion = new MySqlConnection(connectionString);
                _conexion.Open();
                conexionEstablecida = true;
                Console.WriteLine($"Успішно підключено до БД: {_nombreDB}");
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Не можна підключитися до БД {_nombreDB}: {e.Message}");
            }
            return conexionEstablecida;
        }

        public void CloseConnection()
        {
            try
            {
                if (_conexion != null && _conexion.State == System.Data.ConnectionState.Open)
                {
                    _conexion.Close();
                    Console.WriteLine("З'єднання з БД закрито.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка при закритті з'єднання: {e.Message}");
            }
        }

        /// <summary>
        /// Перевіряє, чи встановлено з'єднання.
        /// </summary>
        private bool HayConexion()
        {
            return _conexion != null && _conexion.State == System.Data.ConnectionState.Open;
        }

        /// <summary>
        /// Пробує увійти в систему, перевіряючи облікові дані користувача через SQL-запит.
        /// </summary>
        public bool IniciarSesion(string alias, string contrasenaUsuario)
        {
            return SqlLogin(alias, contrasenaUsuario);
        }

        /// <summary>
        /// Запит таблиці `cliente` для перевірки облікових даних.
        /// </summary>
        public bool SqlLogin(string nombreUsuario, string contrasenaUsuario)
        {
            if (!HayConexion())
            {
                return false;
            }

            string sql = "SELECT idCliente FROM cliente WHERE usuario = @usuario AND contrasena = @contrasena";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasenaUsuario);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _idClienteActual = reader.GetInt32(0);
                            return true;
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Помилка входу: {e.Message}");
            }

            return false;
        }

        public bool RegistrarCliente(string usuario, string contrasena)
        {
            if (!HayConexion())
            {
                return false;
            }

            const string sql = "INSERT INTO cliente (usuario, contrasena, tipo) VALUES (@usuario, @contrasena, 'Free')";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        _idClienteActual = (int)cmd.LastInsertedId;
                        return true;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Помилка реєстрації: {e.Message}");
            }

            return false;
        }

        /// <summary>
        /// Отримує ID поточного авторизованого клієнта.
        /// </summary>
        public int GetIdClienteActual()
        {
            return _idClienteActual;
        }

        /// <summary>
        /// Встановлює ID поточного клієнта.
        /// </summary>
        public void SetIdClienteActual(int id)
        {
            _idClienteActual = id;
        }
    }
}
