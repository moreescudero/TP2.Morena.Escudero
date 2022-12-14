using System.Data.SqlClient;
using System.Data;

namespace Entidades.Modelo
{
    public class ConexionUsuarios
    {
        string stringConnection;
        SqlConnection connection;
        SqlCommand command;

        public ConexionUsuarios()
        {
            stringConnection = @"Data Source = 192.168.0.163 ; Initial Catalog = truco ; User ID = more ; Password = segtp ;";
            connection = new SqlConnection(stringConnection);
            command = new SqlCommand();
        }

        private void Comando(string mensaje)
        {
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = mensaje;
        }

        /// <summary>
        /// Obtiene los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                connection.Open();

                Comando("select * from Usuarios order by Id asc");

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string nombreUsuario = dataReader.GetString(1);
                    string contraseña = dataReader.GetString(2);
                    int partidasGanadas = dataReader.GetInt32(3);
                    int partidasPerdidas = dataReader.GetInt32(4);
                    int cantAnchosDeEspada = dataReader.GetInt32(5);
                    int cantSacoFaltaEnvido = dataReader.GetInt32(6);

                    Usuario usuario = new Usuario(id, nombreUsuario, contraseña, partidasGanadas, partidasPerdidas, cantAnchosDeEspada, cantSacoFaltaEnvido);
                    usuarios.Add(usuario);
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return usuarios;
            }
            catch (Exception )
            {
                throw new Exception("Ocurrio un error en la obtención de usuarios");
            }
        }

        public List<Usuario> ObtenerTop5Usuarios()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                connection.Open();

                Comando("select top(5) * from Usuarios order by PartidasGanadas desc");

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string nombreUsuario = dataReader.GetString(1);
                    string contraseña = dataReader.GetString(2);
                    int partidasGanadas = dataReader.GetInt32(3);
                    int partidasPerdidas = dataReader.GetInt32(4);
                    int cantAnchosDeEspada = dataReader.GetInt32(5);
                    int cantSacoFaltaEnvido = dataReader.GetInt32(6);

                    Usuario usuario = new Usuario(id, nombreUsuario, contraseña, partidasGanadas, partidasPerdidas, cantAnchosDeEspada, cantSacoFaltaEnvido);
                    usuarios.Add(usuario);
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return usuarios;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error en la obtención de top 5 usuarios");
            }

        }

        /// <summary>
        /// agrega un usuario a la base de datos
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contraseña"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarUsuario(Usuario usuario, string contraseña)
        {
            try
            {
                connection.Open();

                Comando("insert into Usuarios (Usuario, Contraseña, PartidasGanadas, PartidasPerdidas, AnchosEspadaObtenidos, CantFaltaEnvidoJugados) values(@Usuario, @Contraseña, @PartidasGanadas, @PartidasPerdidas, @AnchosEspadaObtenidos, @CantFaltaEnvidoJugados)");

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", contraseña);
                command.Parameters.AddWithValue("@PartidasGanadas", usuario.PartidasGanadas);
                command.Parameters.AddWithValue("@PartidasPerdidas", usuario.PartidasPerdidas);
                command.Parameters.AddWithValue("@AnchosEspadaObtenidos", usuario.CantAnchosDeEspada);
                command.Parameters.AddWithValue("@CantFaltaEnvidoJugados", usuario.CantSacoFaltaEnvido);

                command.ExecuteNonQuery();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al agregar usuario");
            }

        }

        /// <summary>
        /// modifica los usuarios de la base de datos
        /// </summary>
        /// <param name="usuarios"></param>
        /// <exception cref="Exception"></exception>
        public void ModificarUsuarios(List<Usuario> usuarios)
        {
            try
            {
                connection.Open();

                foreach (Usuario usuario in usuarios)
                {
                    Comando("update Usuarios set PartidasGanadas = @PartidasGanadas, PartidasPerdidas = @PartidasPerdidas, AnchosEspadaObtenidos = @AnchosEspadaObtenidos, CantFaltaEnvidoJugados = @CantFaltaEnvidoJugados where Usuario = @Usuario");

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@PartidasGanadas", usuario.PartidasGanadas);
                    command.Parameters.AddWithValue("@PartidasPerdidas", usuario.PartidasPerdidas);
                    command.Parameters.AddWithValue("@AnchosEspadaObtenidos", usuario.CantAnchosDeEspada);
                    command.Parameters.AddWithValue("@CantFaltaEnvidoJugados", usuario.CantSacoFaltaEnvido);
                    command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);

                    command.ExecuteNonQuery();
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al guardar usuarios");
            }
        }
    }
}