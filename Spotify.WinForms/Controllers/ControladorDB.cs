using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Spotify.WinForms.Models;

namespace Spotify.WinForms.Controllers
{
    /// <summary>
    /// Контролер БД (DAO) для операцій JDBC з базою даних `spoty`.
    /// Забезпечує методи для автентифікації, управління клієнтами, 
    /// художниками, плейлистами та операціями, пов'язаними з аудіо.
    /// </summary>
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

        /// <summary>
        /// Запускає JDBC з'єднання з налаштованою БД.
        /// </summary>
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

        /// <summary>
        /// Закриває з'єднання з БД.
        /// </summary>
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
        /// Спроба входу з делегуванням на `SqlLogin`.
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

        /// <summary>
        /// Реєструє нового клієнта в таблиці `cliente`.
        /// </summary>
        public bool RegistrarCliente(string nombre, string usuario, string contrasena, int idIdioma = 1)
        {
            if (!HayConexion())
            {
                return false;
            }

            const string sql = @"
INSERT INTO cliente (nombre, apellidos, usuario, contrasena, fechaNacimiento, tipo, idIdioma)
VALUES (@nombre, @apellidos, @usuario, @contrasena, NULL, 'Free', @idIdioma)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellidos", string.Empty);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@idIdioma", idIdioma);

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

        /// <summary>
        /// Отримує інформацію про клієнта за ID.
        /// </summary>
        public Cliente ObtenerCliente(int idCliente)
        {
            if (!HayConexion())
            {
                return null;
            }

            string sql = "SELECT * FROM cliente WHERE idCliente = @id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idClienteOrdinal = reader.GetOrdinal("idCliente");
                            int nombreOrdinal = reader.GetOrdinal("nombre");
                            int apellidosOrdinal = reader.GetOrdinal("apellidos");
                            int usuarioOrdinal = reader.GetOrdinal("usuario");
                            int contrasenaOrdinal = reader.GetOrdinal("contrasena");
                            int fechaNacimientoOrdinal = reader.GetOrdinal("fechaNacimiento");
                            int fechaRegistroOrdinal = reader.GetOrdinal("fechaRegistro");
                            int tipoOrdinal = reader.GetOrdinal("tipo");

                            Cliente cliente = new Cliente
                            {
                                Id = reader.GetInt32(idClienteOrdinal),
                                Nome = reader.GetString(nombreOrdinal),
                                Apellido = reader.IsDBNull(apellidosOrdinal) ? "" : reader.GetString(apellidosOrdinal),
                                Usuario = reader.GetString(usuarioOrdinal),
                                Contrasena = reader.GetString(contrasenaOrdinal),
                                FecNac = reader.IsDBNull(fechaNacimientoOrdinal) ? "" : reader.GetString(fechaNacimientoOrdinal),
                                FecReg = reader.IsDBNull(fechaRegistroOrdinal) ? "" : reader.GetString(fechaRegistroOrdinal),
                                EsPremium = reader.GetString(tipoOrdinal) == "Premium"
                            };
                            return cliente;
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Помилка при отриманні клієнта: {e.Message}");
            }

            return null;
        }

        /// <summary>
        /// Отримує всі плейлисти клієнта.
        /// </summary>
        public List<Playlist> ObtenerPlaylistsCliente(int idCliente)
        {
            List<Playlist> playlists = new List<Playlist>();

            if (!HayConexion())
            {
                return playlists;
            }

            string sql = "SELECT * FROM playlist WHERE idCliente = @idCliente";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, _conexion))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPlaylistOrdinal = reader.GetOrdinal("idPlaylist");
                            int tituloOrdinal = reader.GetOrdinal("titulo");
                            int fechaCreacionOrdinal = reader.GetOrdinal("fechaCreacion");

                            Playlist p = new Playlist
                            {
                                Id = reader.GetInt32(idPlaylistOrdinal),
                                Titulo = reader.GetString(tituloOrdinal),
                                FechaCreacion = reader.GetString(fechaCreacionOrdinal),
                                IdCliente = idCliente
                            };
                            playlists.Add(p);
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Помилка при отриманні плейлистів: {e.Message}");
            }

            return playlists;
        }
    }
}
