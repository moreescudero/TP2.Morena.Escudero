using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Modelo;

namespace Entidades.Presentador
{
    public class PresentadorSala 
    {
        ISala sala;
        Partida? partida;
        List<Usuario> jugadores = new List<Usuario>();
        public Action delTerminarVuelta;
        public Action delTerminarPartida;
        string chatJug1 = String.Empty;
        string chatJug2 = String.Empty;
        string ganadorAux = String.Empty;
        bool gano;
        bool contesto;
        int puntos;

        int envidoJug1;
        int envidoJug2;
        bool primeraMano = true;
        bool seCantoTruco = false;
        bool decirEnvido = false;
        bool seContestoTruco = false;

        public PresentadorSala (ISala sala, Object partida)
        {
            this.sala = sala;
            this.partida = partida as Partida;
            if(this.partida is not null)
            {
                try
                {
                    Asignar();
                }
                catch(Exception ex)
                {
                    throw new Exception();
                }
            }
        }

        private void Asignar()
        {
            try
            {
                jugadores = this.partida.Jugadores;
                jugadores[0].EsMano = true;
                delTerminarVuelta = sala.LimpiarVuelta;
                delTerminarVuelta += LimpiarVuelta;
                delTerminarVuelta += sala.LimpiarMesa;
                delTerminarVuelta += this.partida.FinalizarVuelta;
                delTerminarVuelta += this.partida.ActivarEventoMazo;
                delTerminarPartida = sala.FrenarTimer;
                delTerminarPartida += this.partida.ActivarEventoFinalizarPartida;
                delTerminarPartida += AgregarPartida;
                sala.UsuarioJugador1 = jugadores[0].NombreUsuario;
                sala.UsuarioJugador2 = jugadores[1].NombreUsuario;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// los jugadores cantan envido si es que pueden hacerlo, si es así se responden si quieren o no
        /// </summary>
        public void JugarEnvido()
        {
            if ((!jugadores[1].CantoEnvido && !jugadores[0].CantoEnvido) && (!jugadores[0].CantoFaltaEnvido && !jugadores[1].CantoFaltaEnvido))
            {
                if (jugadores[0].EsMano)
                {
                    VerificarEnvido(0, 1);
                }
                else if (jugadores[1].EsMano)
                {
                    VerificarEnvido(1, 0);
                }
                if (sala.HayEnvido)
                {
                    if (jugadores[0].CantoEnvido && !jugadores[1].CantoEnvido)
                    {
                        sala.Chat += jugadores[0].NombreUsuario + ": Envido\n";
                        chatJug1 = "Envido";
                    }
                    else if (jugadores[1].CantoEnvido && !jugadores[0].CantoEnvido)
                    {
                        sala.Chat += jugadores[1].NombreUsuario + ": Envido\n";
                        chatJug2 = "Envido";
                    }
                    else if (jugadores[0].CantoFaltaEnvido && !jugadores[1].CantoFaltaEnvido)
                    {
                        sala.Chat += jugadores[0].NombreUsuario + ": Falta envido\n";
                        chatJug1 = "Falta envido";
                    }
                    else if (!jugadores[0].CantoFaltaEnvido && jugadores[1].CantoFaltaEnvido)
                    {
                        sala.Chat += jugadores[1].NombreUsuario + ": Falta envido\n";
                        chatJug2 = "Falta envido";
                    }
                }
            }
            else if (((jugadores[0].CantoFaltaEnvido && !jugadores[1].CantoFaltaEnvido) || (jugadores[0].CantoEnvido && !jugadores[1].CantoEnvido)) && chatJug2 == String.Empty)
            {
                if (partida.CantarEnvido(jugadores[1]) && (jugadores[0].CantoEnvido || (jugadores[0].CantoFaltaEnvido && jugadores[1].DecirEnvido() >= 28)))
                {
                    sala.Chat += jugadores[1].NombreUsuario + ": Quiero \n";
                    chatJug2 = "Quiero";
                }
                else
                {
                    sala.Chat += jugadores[1].NombreUsuario + ": No quiero \n";
                    chatJug2 = "No quiero";
                    jugadores[0].PuntosPartida++;
                    puntos = jugadores[0].PuntosPartida;
                    sala.PuntosJug1 = puntos.ToString();
                }
            }
            else if (((jugadores[1].CantoFaltaEnvido && !jugadores[0].CantoFaltaEnvido) || (!jugadores[0].CantoEnvido && jugadores[1].CantoEnvido)) && chatJug1 == String.Empty)
            {
                if (partida.CantarEnvido(jugadores[0]) && (jugadores[1].CantoEnvido || (jugadores[1].CantoFaltaEnvido && jugadores[0].DecirEnvido() >= 28)))
                {
                    sala.Chat += jugadores[0].NombreUsuario + ": Quiero \n";
                    chatJug1 = "Quiero";
                }
                else
                {
                    sala.Chat += jugadores[0].NombreUsuario + ": No quiero \n";
                    chatJug1 = "No quiero";
                    jugadores[1].PuntosPartida++;
                    puntos = jugadores[1].PuntosPartida;
                    sala.PuntosJug2 = puntos.ToString();
                }
            }
            if (decirEnvido)
            {
                DecirCantEnvido();
            }
            else
            {
                if (chatJug1 == "No quiero" || chatJug2 == "No quiero")
                {
                    sala.HayEnvido = false;
                }
                else if (chatJug1 == "Quiero" || chatJug2 == "Quiero")
                {
                    decirEnvido = true;
                }
            }
        }

        /// <summary>
        /// si del método partida.CantarEnvido vuelve un true el jugador indicado canta envido, sino dejarán pasar
        /// el turno para que el otro jugador decida si cantarlo o no
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="indiceOtroJug"></param>
        private void VerificarEnvido(int indice, int indiceOtroJug)
        {
            bool hayFlorJugIndice = jugadores[indice].BuscarFlor();
            bool hayFlorJugOtroIndice = jugadores[indiceOtroJug].BuscarFlor();
            if (!hayFlorJugIndice && !hayFlorJugOtroIndice)
            {
                if (partida.CantarEnvido(jugadores[indice]))
                {
                    if (jugadores[indice].DecirEnvido() >= 28)
                    {
                        jugadores[indice].CantoFaltaEnvido = true;
                        jugadores[indice].CantSacoFaltaEnvido++;
                    }
                    else
                    {
                        jugadores[indice].CantoEnvido = true;
                    }
                }
                else if (partida.CantarEnvido(jugadores[indiceOtroJug]))
                {
                    if (jugadores[indiceOtroJug].DecirEnvido() >= 28)
                    {
                        jugadores[indiceOtroJug].CantoFaltaEnvido = true;
                        jugadores[indiceOtroJug].CantSacoFaltaEnvido++;
                    }
                    else
                    {
                        jugadores[indiceOtroJug].CantoEnvido = true;
                    }
                }
                else
                {
                    sala.HayEnvido = false;
                }
            }
            else if (hayFlorJugIndice)
            {
                jugadores[indice].PuntosPartida += 3;
            }
            else if (hayFlorJugOtroIndice)
            {
                jugadores[indiceOtroJug].PuntosPartida += 3;
            }
        }

        /// <summary>
        /// se llama al método que suma la cantidad de envido de cada jugador y luego al que los compara, 
        /// se muestran los puntos sumados 
        /// </summary>
        private void DecirCantEnvido()
        {
            envidoJug1 = jugadores[0].DecirEnvido();
            sala.Chat += jugadores[0].NombreUsuario + ": " + envidoJug1.ToString() + "\n";
            envidoJug2 = jugadores[1].DecirEnvido();
            sala.Chat += jugadores[1].NombreUsuario + ": " + envidoJug2.ToString() + "\n";
            ganadorAux = partida.DeterminarGanadorEnvido(envidoJug1, envidoJug2);
            sala.Chat += ganadorAux + "\n";

            if (ganadorAux == jugadores[0].NombreUsuario + " ganó envido" || ganadorAux == jugadores[0].NombreUsuario + " ganó falta envido")
            {
                jugadores[0].CantoEnvido = true;
                puntos = jugadores[0].PuntosPartida;
                sala.PuntosJug1 = puntos.ToString();
            }
            else
            {
                jugadores[1].CantoEnvido = true;
                puntos = jugadores[1].PuntosPartida;
                sala.PuntosJug2 = puntos.ToString();
            }

            if (jugadores[0].PuntosPartida >= 15 || jugadores[1].PuntosPartida >= 15)
            {
                if (jugadores[0].PuntosPartida >= 15)
                {
                    Ganar(0, 1);
                }
                else
                {
                    Ganar(1, 0);
                }
            }
            else
            {
                sala.HayEnvido = false;
            }    

        }

        /// <summary>
        /// los jugadores deciden si cantar truco o no (de cantarlo el otro jugador debe contestarle), tiran
        /// cartas en la mesa y se suman los puntos en cada mano para determinar cuándo un jugador gana, luego 
        /// de cada vuelta se invoca un delegado que setee los atributos necesarios para continuar jugando 
        /// </summary>
        public void Jugar()
        {
            try
            {
                contesto = false;
                if (!gano)
                {
                    if (!seCantoTruco && !seContestoTruco || seCantoTruco && seContestoTruco)
                    {
                        if ((jugadores[0].ManosGanadas == 2 || jugadores[1].ManosGanadas == 2) && jugadores[0].Cartas.Count == jugadores[1].Cartas.Count)
                        {
                            if (jugadores[0].ManosGanadas == 2)
                            {
                                Ganar(0, 1);
                            }
                            else
                            {
                                Ganar(1, 0);
                            }
                            return;
                        }
                        else if (((jugadores[0].EsMano && !jugadores[1].EsMano && primeraMano) || !partida.AsignarTurno() && jugadores[0].Cartas.Count == jugadores[1].Cartas.Count || jugadores[0].Cartas.Count > jugadores[1].Cartas.Count) && jugadores[0].Cartas.Count > 0)
                        {
                            if (!seCantoTruco)
                            {
                                chatJug1 = CantarTruco(jugadores[0]);
                                if (chatJug1 != String.Empty)
                                {
                                    sala.Chat += jugadores[0].NombreUsuario + ": " + chatJug1 + "\n";
                                }
                            }
                            Carta cartaJugada = partida.Jugar(jugadores[0], jugadores[1].CartaJugada);
                            sala.TirarCartaEnMesa(cartaJugada.Numero + " " + cartaJugada.Palo, 1);
                        }
                        else if ((jugadores[1].EsMano && !jugadores[0].EsMano && primeraMano) || jugadores[1].Cartas.Count > 0 || jugadores[0].CantoTruco && !seContestoTruco)
                        {
                            if (!seCantoTruco)
                            {
                                chatJug2 = CantarTruco(jugadores[1]);
                                if (chatJug2 != String.Empty)
                                {
                                    sala.Chat += jugadores[1].NombreUsuario + ": " + chatJug2 + "\n";
                                }
                            }
                            Carta cartaJugada = partida.Jugar(jugadores[1], jugadores[0].CartaJugada);
                            sala.TirarCartaEnMesa(cartaJugada.Numero + " " + cartaJugada.Palo, 2);
                        }
                        primeraMano = false;
                    }
                    else if (seCantoTruco && !seContestoTruco)
                    {
                        contesto = true;
                        ContestarTruco();
                    }
                    if (jugadores[0].Cartas.Count == jugadores[1].Cartas.Count && !contesto)
                    {
                        partida.SumarMano();
                    }
                }
                else
                {
                    delTerminarVuelta();
                    gano = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// determina aleatoriamente si el jugador va a cantar truco o no
        /// </summary>
        /// <param name="jugador"></param>
        /// <returns></returns>
        private string? CantarTruco(Usuario jugador)
        {
            string? retorno = String.Empty;
            Random rnd = new Random();
            int decision = rnd.Next(1, 3);
            if (decision == 1)
            {
                jugador.CantoTruco = true;
                retorno = "Truco";
                seCantoTruco = true;
            }
            return retorno;
        }

        /// <summary>
        /// llama un método para determinar si quiere jugar truco o no, si no quiere se finaliza la partida
        /// </summary>
        private void ContestarTruco()
        {
            seContestoTruco = true;
            if (jugadores[0].CantoTruco && !jugadores[1].CantoTruco)
            {
                chatJug2 = jugadores[1].ContestarTruco();
                sala.Chat += jugadores[1].NombreUsuario + ": " + chatJug2  + "\n";
                if (chatJug2 == "No quiero" && chatJug1 == "Truco")
                {
                    Ganar(0, 1);
                    return;
                }
            }
            else if(jugadores[1].CantoTruco && !jugadores[0].CantoTruco)
            {
                chatJug1 = jugadores[0].ContestarTruco();
                sala.Chat += jugadores[0].NombreUsuario + ": " + chatJug1 + "\n";
                if (chatJug1 == "No quiero" && chatJug2 == "Truco")
                {
                    Ganar(1, 0);
                    return;
                }
            }
        }

        /// <summary>
        /// se suman los puntos de la partida (si quisieron envido muestra todas las cartas en la mesa) y se
        /// invierten los bool de qué jugador es mano 
        /// si los puntos son superiores o iguales a 15 se finaliza la partida mostrando el ganador e invocando 
        /// un delegado que setee todo nuevamente para volver a jugar otra partida correctamente
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="indiceOtroJug"></param>
        /// <param name="mensajeGanador"></param>
        /// <param name="mensajeGanadorPartida"></param>
        private void Ganar(int indice, int indiceOtroJug)
        {
            if (jugadores[indice].CantoTruco && jugadores[indiceOtroJug].CantoTruco)
            {
                jugadores[indice].PuntosPartida += 2;
            }
            else
            {
                jugadores[indice].PuntosPartida++;
            }
            puntos = jugadores[indice].PuntosPartida;


            if (decirEnvido)
            {
                MostrarCartas();
            }
            if (indice == 0)
            {
                sala.PuntosJug1 = puntos.ToString();
            }
            else
            {
                sala.PuntosJug2 = puntos.ToString();
            }
            if (jugadores[indice].PuntosPartida > 14 || jugadores[indiceOtroJug].PuntosPartida > 14)
            {
                string? ganador;
                if (jugadores[indice].PuntosPartida > 14)
                {
                    ganador = jugadores[indice].NombreUsuario;
                }
                else 
                {
                    ganador = jugadores[indiceOtroJug].NombreUsuario;

                }
                sala.Chat += "Felicidades " + ganador + ", ganaste!";
                delTerminarPartida();
                ConexionUsuarios conexion = new ConexionUsuarios();
                conexion.ModificarUsuarios(jugadores);
                sala.GuardarPartida(ganador);
            }
            else
            {
                if (jugadores[indice].EsMano && !jugadores[indiceOtroJug].EsMano)
                {
                    jugadores[indice].EsMano = false;
                    jugadores[indiceOtroJug].EsMano = true;
                }
                else
                {
                    jugadores[indice].EsMano = true;
                    jugadores[indiceOtroJug].EsMano = false;
                }
                chatJug1 = String.Empty;
                chatJug2 = String.Empty;
                string? mensaje = "Ganó " + jugadores[indice].NombreUsuario;
                sala.Chat += mensaje;
                sala.Chat += "\n\n";
                gano = true;
            }
        }


        /// <summary>
        /// muestra las cartas que no fueron jugadas en mesa para demostrar el envido
        /// </summary>
        private void MostrarCartas()
        {
            if (jugadores[0].Cartas.Count > 0)
            {
                jugadores[0].Cartas.ForEach( (x) =>
                {
                    if (!jugadores[0].CartasJugadas.Contains(x))
                    {
                        sala.TirarCartaEnMesa(x.Numero + " " + x.Palo, 1);
                    }
                });
            }
            if (jugadores[1].Cartas.Count > 0)
            {
                jugadores[1].Cartas.ForEach((x) =>
                {
                    if (!jugadores[1].CartasJugadas.Contains(x))
                    {
                        sala.TirarCartaEnMesa(x.Numero + " " + x.Palo, 2);
                    }
                });
            }
            sala.Chat += "Envido en mesa \n";
        }

        /// <summary>
        /// agrega la partida a la base de datos
        /// </summary>
        private void AgregarPartida()
        {
            try
            {
                ConexionPartidas conexionPartidas = new ConexionPartidas();
                conexionPartidas.AgregarPartida(partida);
            }
            catch (Exception ex)
            {
                sala.MostrarError(ex.Message);
            }
        }

        private void LimpiarVuelta()
        {
            envidoJug1 = 0;
            envidoJug2 = 0;
            primeraMano = true;
            seCantoTruco = false;
            decirEnvido = false;
            seContestoTruco = false;
        }
    }
}
