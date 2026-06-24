using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Tema1
{
    // Clase estática que centraliza toda la lógica de negocio del sistema.
    // Al ser estática no necesita instanciarse y actúa como gestor global.
    public static class GestorLiga
    {
        public static List<Equipo> Equipos { get; private set; } = new List<Equipo>();
        public static List<Jugador> Jugadores { get; private set; } = new List<Jugador>();
        public static List<Partido> Partidos { get; private set; } = new List<Partido>();

        private static int proximoIdEquipo = 1;
        private static int proximoIdPartido = 1;

        // VALIDACIONES

        /// <summary>
        /// Verifica si ya existe un jugador registrado con el DNI ingresado.
        /// </summary>
        /// <param name="dni">DNI a buscar.</param>
        /// <returns>True si el DNI ya existe, false si no.</returns>
        public static bool DNIExiste(string dni)
        {
            foreach (Jugador j in Jugadores)
                if (j.DNI == dni) return true;
            return false;
        }

        /// <summary>
        /// Verifica si la edad es coherente con el rango etario de la categoría.
        /// </summary>
        /// <param name="edad">Edad del jugador.</param>
        /// <param name="categoria">Categoría del equipo.</param>
        /// <returns>True si la edad es válida para la categoría, false si no.</returns>
        public static bool EdadEsValida(int edad, Categoria categoria)
        {
            switch (categoria)
            {
                case Categoria.Infantiles: return edad <= 12;
                case Categoria.Cadetes: return edad >= 13 && edad <= 15;
                case Categoria.Juveniles: return edad >= 16 && edad <= 17;
                case Categoria.Primera: return edad >= 18 && edad <= 34;
                case Categoria.Veteranos: return edad >= 35;
                default: return false;
            }
        }

        /// <summary>
        /// Genera el nombre automático del equipo según el club y la categoría.
        /// Ejemplo: primer equipo de "Club Norte" en Primera → "Club Norte A".
        /// </summary>
        /// <param name="nombreClub">Nombre del club.</param>
        /// <param name="categoria">Categoría del equipo.</param>
        /// <returns>Nombre generado para el equipo.</returns>
        public static string GenerarNombreEquipo(string nombreClub, Categoria categoria)
        {
            int contador = 0;
            foreach (Equipo e in Equipos)
                if (e.NombreClub == nombreClub && e.Categoria == categoria)
                    contador++;
            char letra = (char)('A' + contador);
            return nombreClub + " " + letra;
        }

        /// <summary>
        /// Verifica si un equipo tiene al menos un jugador asignado.
        /// Se usa antes de eliminar o modificar un equipo.
        /// </summary>
        /// <param name="idEquipo">ID del equipo a verificar.</param>
        /// <returns>True si el equipo tiene jugadores, false si no.</returns>
        public static bool EquipoTieneJugadores(int idEquipo)
        {
            foreach (Jugador j in Jugadores)
                if (j.EquiposAsignados.Contains(idEquipo))
                    return true;
            return false;
        }

        // ABM EQUIPOS

        /// <summary>
        /// Registra un nuevo equipo con nombre generado automáticamente.
        /// </summary>
        /// <param name="nombreClub">Nombre del club.</param>
        /// <param name="categoria">Categoría del equipo.</param>
        /// <returns>El equipo creado.</returns>
        public static Equipo AltaEquipo(string nombreClub, Categoria categoria)
        {
            string nombre = GenerarNombreEquipo(nombreClub, categoria);
            Equipo nuevoEquipo = new Equipo(proximoIdEquipo++, nombre, nombreClub, categoria);
            Equipos.Add(nuevoEquipo);
            return nuevoEquipo;
        }

        /// <summary>
        /// Elimina un equipo por ID. No permite eliminar si tiene jugadores asignados.
        /// </summary>
        /// <param name="id">ID del equipo a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si tiene jugadores asignados.</returns>
        public static bool BajaEquipo(int id)
        {
            if (EquipoTieneJugadores(id)) return false;
            Equipo aEliminar = null;
            foreach (Equipo e in Equipos)
                if (e.Id == id) { aEliminar = e; break; }
            if (aEliminar != null)
            {
                Equipos.Remove(aEliminar);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Modifica el nombre del club de un equipo. No permite modificar si tiene jugadores asignados.
        /// </summary>
        /// <param name="id">ID del equipo a modificar.</param>
        /// <param name="nuevoNombreClub">Nuevo nombre del club.</param>
        /// <returns>True si se modificó correctamente, false si tiene jugadores asignados.</returns>
        public static bool ModificarEquipo(int id, string nuevoNombreClub)
        {
            if (EquipoTieneJugadores(id)) return false;
            foreach (Equipo e in Equipos)
            {
                if (e.Id == id)
                {
                    e.NombreClub = nuevoNombreClub;
                    e.Nombre = GenerarNombreEquipo(nuevoNombreClub, e.Categoria);
                    return true;
                }
            }
            return false;
        }

        // ABM JUGADORES

        /// <summary>
        /// Registra un nuevo jugador en el sistema.
        /// </summary>
        /// <returns>El jugador creado.</returns>
        public static Jugador AltaJugador(string dni, string nombre, string apellido, int edad, bool tieneSeguro, bool estaAfiliado)
        {
            Jugador nuevoJugador = new Jugador(dni, nombre, apellido, edad, tieneSeguro, estaAfiliado);
            Jugadores.Add(nuevoJugador);
            return nuevoJugador;
        }

        /// <summary>
        /// Elimina un jugador por DNI.
        /// </summary>
        /// <param name="dni">DNI del jugador a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si no existe.</returns>
        public static bool BajaJugador(string dni)
        {
            Jugador aEliminar = null;
            foreach (Jugador j in Jugadores)
                if (j.DNI == dni) { aEliminar = j; break; }
            if (aEliminar != null)
            {
                Jugadores.Remove(aEliminar);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Devuelve la lista de jugadores asignados a un equipo específico.
        /// </summary>
        /// <param name="idEquipo">ID del equipo a consultar.</param>
        /// <returns>Lista de jugadores del equipo.</returns>
        public static List<Jugador> ObtenerJugadoresPorEquipo(int idEquipo)
        {
            List<Jugador> resultado = new List<Jugador>();
            foreach (Jugador j in Jugadores)
                if (j.EquiposAsignados.Contains(idEquipo))
                    resultado.Add(j);
            return resultado;
        }

        // PARTIDOS

        /// <summary>
        /// Registra un nuevo partido en el sistema.
        /// </summary>
        /// <returns>El partido creado.</returns>
        public static Partido AltaPartido(Equipo local, Equipo visitante, DateTime fecha, string horario, string lugar)
        {
            Partido nuevo = new Partido(proximoIdPartido++, local, visitante, fecha, horario, lugar);
            Partidos.Add(nuevo);
            return nuevo;
        }

        // ESTADÍSTICAS

        public static int ObtenerPartidosJugados(Equipo equipo)
        {
            int count = 0;
            foreach (Partido p in Partidos)
            {
                if (p.EquipoLocal == equipo || p.EquipoVisitante == equipo)
                    count++;
            }
            return count;
        }

        public static int ObtenerVictorias(Equipo equipo)
        {
            int count = 0;
            foreach (Partido p in Partidos)
            {
                if (p.EquipoLocal == equipo && p.GolesLocal > p.GolesVisitante)
                    count++;
                if (p.EquipoVisitante == equipo && p.GolesVisitante > p.GolesLocal)
                    count++;
            }
            return count;
        }

        public static int ObtenerEmpates(Equipo equipo)
        {
            int count = 0;
            foreach (Partido p in Partidos)
            {
                if (p.EquipoLocal == equipo || p.EquipoVisitante == equipo)
                {
                    if (p.GolesLocal == p.GolesVisitante)
                        count++;
                }
            }
            return count;
        }

        public static int ObtenerDerrotas(Equipo equipo)
        {
            int count = 0;
            foreach (Partido p in Partidos)
            {
                if (p.EquipoLocal == equipo && p.GolesLocal < p.GolesVisitante)
                    count++;
                if (p.EquipoVisitante == equipo && p.GolesVisitante < p.GolesLocal)
                    count++;
            }
            return count;
        }

        public static Equipo ObtenerEquipoConMasVictorias()
        {
            Equipo mejor = null;
            int max = -1;
            foreach (Equipo e in Equipos)
            {
                int v = ObtenerVictorias(e);
                if (v > max)
                {
                    max = v;
                    mejor = e;
                }
            }
            return mejor;
        }

        public static Equipo ObtenerEquipoConMasDerrotas()
        {
            Equipo peor = null;
            int max = -1;
            foreach (Equipo e in Equipos)
            {
                int d = ObtenerDerrotas(e);
                if (d > max)
                {
                    max = d;
                    peor = e;
                }
            }
            return peor;
        }

        // REPORTE

        /// <summary>
        /// Genera un archivo de texto con equipos, jugadores, historial de partidos y tabla de posiciones.
        /// StreamWriter se usa para escribir línea por línea. El bloque using garantiza que el archivo se cierre correctamente.
        /// </summary>
        /// <param name="rutaArchivo">Ruta donde se guardará el archivo.</param>
        public static void GenerarReporte(string rutaArchivo)
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                sw.WriteLine("=== EQUIPOS REGISTRADOS ===");
                foreach (Equipo e in Equipos)
                    sw.WriteLine(e.Nombre + " | Club: " + e.NombreClub + " | Categoría: " + e.Categoria);

                sw.WriteLine("\n=== JUGADORES REGISTRADOS ===");
                foreach (Jugador j in Jugadores)
                    sw.WriteLine(j.Apellido + ", " + j.Nombre + " | DNI: " + j.DNI + " | Edad: " + j.Edad);

                sw.WriteLine("\n=== HISTORIAL DE PARTIDOS ===");
                foreach (Partido p in Partidos)
                    sw.WriteLine(p.EquipoLocal.Nombre + " " + p.GolesLocal + " - " + p.GolesVisitante + " " + p.EquipoVisitante.Nombre + " | " + p.Fecha.ToShortDateString());

                sw.WriteLine("\n=== TABLA DE POSICIONES ===");
                foreach (Equipo e in Equipos)
                    sw.WriteLine(e.Nombre + " | PJ: " + ObtenerPartidosJugados(e) + " | PG: " + ObtenerVictorias(e) + " | PE: " + ObtenerEmpates(e) + " | PP: " + ObtenerDerrotas(e));
            }
        }

        // Datos de prueba precargados para facilitar el testeo sin carga manual.
        // Suposición: se incluyen equipos y jugadores de ejemplo por requerimiento del enunciado.
        public static void CargarDatosPrueba()
        {
            AltaEquipo("Club Norte", Categoria.Primera);
            AltaEquipo("Club Norte", Categoria.Primera);
            AltaEquipo("Club Sur", Categoria.Primera);
            AltaEquipo("Club Sur", Categoria.Cadetes);

            AltaJugador("11111111", "Juan", "Perez", 22, true, true);
            AltaJugador("22222222", "Carlos", "Lopez", 25, true, false);
            AltaJugador("33333333", "Lucas", "Gomez", 19, false, true);
        }
    }
}