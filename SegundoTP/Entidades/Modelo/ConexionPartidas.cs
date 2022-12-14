using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Modelo
{
    public class ConexionPartidas
    {
        string stringConnection;
        SqlConnection connection;
        SqlCommand command;

        public ConexionPartidas()
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
        /// obtiene las partidas de la base de datos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Partida> ObtenerPartidas()
        {
            try
            {
                List<Partida> partidas = new List<Partida>();
                connection.Open();

                Comando("select * from Partidas order by Id asc");

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string ganador = dataReader.GetString(1);
                    string perdedor = dataReader.GetString(2);
                    DateTime fecha = dataReader.GetDateTime(3);

                    Partida partida = new Partida(id, ganador, fecha, perdedor);
                    partidas.Add(partida);
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return partidas;
            }
            catch (Exception)
            {
                throw new Exception("ocurrio un error en la obtención de partidas");
            }
        }

        public List<Partida> ObtenerPartidas(string ganadorPartida)
        {
            try
            {
                List<Partida> partidas = new List<Partida>();
                connection.Open();

                Comando("select * from Partidas where Ganador = @ganador");

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Ganador", ganadorPartida);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32(0);
                    string ganador = dataReader.GetString(1);
                    string perdedor = dataReader.GetString(2);
                    DateTime fecha = dataReader.GetDateTime(3);

                    Partida partida = new Partida(id, ganador, fecha, perdedor);
                    partidas.Add(partida);
                }

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return partidas;
            }
            catch (Exception)
            {
                throw new Exception("ocurrio un error en la obtención de las partidas de " + ganadorPartida);
            }
        }


        /// <summary>
        /// agrega una partida a la base de datos
        /// </summary>
        /// <param name="partida"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarPartida(Partida partida)
        {
            try
            {
                connection.Open();

                Comando("insert into Partidas (Ganador, Perdedor, Fecha) values(@Ganador, @Perdedor, @Fecha)");

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Ganador", partida.Ganador);
                command.Parameters.AddWithValue("@Perdedor", partida.Perdedor);
                command.Parameters.AddWithValue("@Fecha", partida.Fecha);

                command.ExecuteNonQuery();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al agregar una partida");
            }

        }
    }
}
