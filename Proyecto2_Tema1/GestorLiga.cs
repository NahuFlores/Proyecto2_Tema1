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
            // Validaciones básicas de entrada
            if (string.IsNullOrWhiteSpace(dni)) throw new ArgumentException("DNI vacío.");
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre vacío.");
            if (string.IsNullOrWhiteSpace(apellido)) throw new ArgumentException("Apellido vacío.");
            if (edad <= 0) throw new ArgumentException("Edad inválida.");

            // Verificar duplicado de DNI
            if (DNIExiste(dni)) throw new InvalidOperationException("Ya existe un jugador con ese DNI.");

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
                // Remover asignaciones a equipos
                aEliminar.EquiposAsignados.Clear();
                Jugadores.Remove(aEliminar);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Asigna un jugador a un equipo verificando existencia, no duplicados y compatibilidad de edad.
        /// </summary>
        /// <param name="dni">DNI del jugador.</param>
        /// <param name="idEquipo">ID del equipo.</param>
        /// <returns>True si se asignó correctamente, false en caso contrario.</returns>
        public static bool AsignarJugadorAEquipo(string dni, int idEquipo)
        {
            Jugador jugador = null;
            foreach (Jugador j in Jugadores)
                if (j.DNI == dni) { jugador = j; break; }
            if (jugador == null) return false;

            Equipo equipo = null;
            foreach (Equipo e in Equipos)
                if (e.Id == idEquipo) { equipo = e; break; }
            if (equipo == null) return false;

            // Verificar edad acorde a la categoría del equipo
            if (!EdadEsValida(jugador.Edad, equipo.Categoria)) return false;

            if (!jugador.EquiposAsignados.Contains(idEquipo))
            {
                jugador.EquiposAsignados.Add(idEquipo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Quita la asignación de un jugador a un equipo.
        /// </summary>
        /// <param name="dni">DNI del jugador.</param>
        /// <param name="idEquipo">ID del equipo.</param>
        /// <returns>True si se removió, false si no estaba asignado o no existe.</returns>
        public static bool QuitarJugadorDeEquipo(string dni, int idEquipo)
        {
            Jugador jugador = null;
            foreach (Jugador j in Jugadores)
                if (j.DNI == dni) { jugador = j; break; }
            if (jugador == null) return false;

            if (jugador.EquiposAsignados.Contains(idEquipo))
            {
                jugador.EquiposAsignados.Remove(idEquipo);
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
            // Verificar que ambos equipos pertenezcan a la misma categoría
            if (local == null || visitante == null) return null;
            if (local.Categoria != visitante.Categoria) return null;

            Partido nuevo = new Partido(proximoIdPartido++, local, visitante, fecha, horario, lugar);
            Partidos.Add(nuevo);
            return nuevo;
        }

        /// <summary>
        /// Configura la alineación de un partido: titulares (exactamente 5) y suplentes (0-3) por equipo.
        /// Verifica pertenencia de los jugadores a sus equipos y evita duplicados.
        /// </summary>
        public static bool ConfigurarAlineacion(int idPartido, List<Jugador> titularesLocal, List<Jugador> suplentesLocal, List<Jugador> titularesVisitante, List<Jugador> suplentesVisitante)
        {
            Partido partido = null;
            foreach (Partido p in Partidos)
                if (p.Id == idPartido) { partido = p; break; }
            if (partido == null) return false;

            // Validaciones de cantidad
            if (titularesLocal == null || titularesVisitante == null) return false;
            if (titularesLocal.Count != 5 || titularesVisitante.Count != 5) return false;
            if (suplentesLocal != null && suplentesLocal.Count > 3) return false;
            if (suplentesVisitante != null && suplentesVisitante.Count > 3) return false;

            // Verificar pertenencia de jugadores a sus equipos y no duplicados
            var idsLocal = new HashSet<string>();
            foreach (Jugador j in titularesLocal)
            {
                if (!j.EquiposAsignados.Contains(partido.EquipoLocal.Id)) return false;
                if (!idsLocal.Add(j.DNI)) return false; // duplicado
            }
            if (suplentesLocal != null)
            {
                foreach (Jugador j in suplentesLocal)
                {
                    if (!j.EquiposAsignados.Contains(partido.EquipoLocal.Id)) return false;
                    if (!idsLocal.Add(j.DNI)) return false;
                }
            }

            var idsVisitante = new HashSet<string>();
            foreach (Jugador j in titularesVisitante)
            {
                if (!j.EquiposAsignados.Contains(partido.EquipoVisitante.Id)) return false;
                if (!idsVisitante.Add(j.DNI)) return false;
            }
            if (suplentesVisitante != null)
            {
                foreach (Jugador j in suplentesVisitante)
                {
                    if (!j.EquiposAsignados.Contains(partido.EquipoVisitante.Id)) return false;
                    if (!idsVisitante.Add(j.DNI)) return false;
                }
            }

            // Alineación válida: asignar a la entidad Partido
            partido.TitularesLocal = new List<Jugador>(titularesLocal);
            partido.SuplentesLocal = suplentesLocal != null ? new List<Jugador>(suplentesLocal) : new List<Jugador>();
            partido.TitularesVisitante = new List<Jugador>(titularesVisitante);
            partido.SuplentesVisitante = suplentesVisitante != null ? new List<Jugador>(suplentesVisitante) : new List<Jugador>();

            return true;
        }

        /// <summary>
        /// Registra el resultado final del partido.
        /// </summary>
        public static bool RegistrarResultado(int idPartido, int golesLocal, int golesVisitante)
        {
            Partido partido = null;
            foreach (Partido p in Partidos)
                if (p.Id == idPartido) { partido = p; break; }
            if (partido == null) return false;

            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;
            return true;
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
                {
                    sw.WriteLine(p.EquipoLocal.Nombre + " " + p.GolesLocal + " - " + p.GolesVisitante + " " + p.EquipoVisitante.Nombre + " | " + p.Fecha.ToShortDateString());
                    // Formaciones
                    sw.WriteLine("  Titulares " + p.EquipoLocal.Nombre + ": " + string.Join(", ", p.TitularesLocal.Select(t => t.Apellido + " " + t.Nombre)));
                    sw.WriteLine("  Suplentes " + p.EquipoLocal.Nombre + ": " + (p.SuplentesLocal.Count > 0 ? string.Join(", ", p.SuplentesLocal.Select(s => s.Apellido + " " + s.Nombre)) : "-") );
                    sw.WriteLine("  Titulares " + p.EquipoVisitante.Nombre + ": " + string.Join(", ", p.TitularesVisitante.Select(t => t.Apellido + " " + t.Nombre)));
                    sw.WriteLine("  Suplentes " + p.EquipoVisitante.Nombre + ": " + (p.SuplentesVisitante.Count > 0 ? string.Join(", ", p.SuplentesVisitante.Select(s => s.Apellido + " " + s.Nombre)) : "-") );
                }

                sw.WriteLine("\n=== TABLA DE POSICIONES ===");
                sw.WriteLine("Equipo | PJ | PG | PE | PP | Pts");
                foreach (Equipo e in Equipos)
                {
                    int pj = ObtenerPartidosJugados(e);
                    int pg = ObtenerVictorias(e);
                    int pe = ObtenerEmpates(e);
                    int pp = ObtenerDerrotas(e);
                    int pts = pg * 3 + pe;
                    sw.WriteLine(e.Nombre + " | " + pj + " | " + pg + " | " + pe + " | " + pp + " | " + pts);
                }

                sw.WriteLine("\n=== RESUMEN ESTADÍSTICO POR EQUIPO ===");
                foreach (Equipo e in Equipos)
                {
                    int golesFavor = 0;
                    int golesContra = 0;
                    foreach (Partido p in Partidos)
                    {
                        if (p.EquipoLocal == e)
                        {
                            golesFavor += p.GolesLocal;
                            golesContra += p.GolesVisitante;
                        }
                        if (p.EquipoVisitante == e)
                        {
                            golesFavor += p.GolesVisitante;
                            golesContra += p.GolesLocal;
                        }
                    }
                    sw.WriteLine(e.Nombre + " | GF: " + golesFavor + " | GC: " + golesContra + " | Victorias: " + ObtenerVictorias(e) + " | Derrotas: " + ObtenerDerrotas(e));
                }
            }
        }

        // Datos de prueba precargados para facilitar el testeo sin carga manual.
        // Suposición: se incluyen equipos y jugadores de ejemplo por requerimiento del enunciado.
        public static void CargarDatosPrueba()
        {
            // Equipos
            Equipo e1 = AltaEquipo("Club Norte", Categoria.Primera);
            Equipo e2 = AltaEquipo("Club Sur", Categoria.Primera);

            // Jugadores para equipo 1 (5 titulares + 2 suplentes)
            AltaJugador("11111111", "Juan", "Perez", 22, true, true);
            AltaJugador("11111112", "Pedro", "Martinez", 24, true, true);
            AltaJugador("11111113", "Luis", "Sanchez", 21, true, true);
            AltaJugador("11111114", "Diego", "Diaz", 23, true, true);
            AltaJugador("11111115", "Mateo", "Rios", 22, true, true);
            AltaJugador("11111116", "Andres", "Ruiz", 26, true, true);
            AltaJugador("11111117", "Hector", "Morales", 20, true, true);

            // Jugadores para equipo 2 (5 titulares + 1 suplente)
            AltaJugador("22222221", "Carlos", "Lopez", 25, true, false);
            AltaJugador("22222222", "Lucas", "Gomez", 19, false, true);
            AltaJugador("22222223", "Sergio", "Vega", 27, true, true);
            AltaJugador("22222224", "Martin", "Diaz", 23, true, true);
            AltaJugador("22222225", "Federico", "Torres", 29, true, true);

            // Asignar jugadores a equipos
            AsignarJugadorAEquipo("11111111", e1.Id);
            AsignarJugadorAEquipo("11111112", e1.Id);
            AsignarJugadorAEquipo("11111113", e1.Id);
            AsignarJugadorAEquipo("11111114", e1.Id);
            AsignarJugadorAEquipo("11111115", e1.Id);
            AsignarJugadorAEquipo("11111116", e1.Id);
            AsignarJugadorAEquipo("11111117", e1.Id);

            AsignarJugadorAEquipo("22222221", e2.Id);
            AsignarJugadorAEquipo("22222222", e2.Id);
            AsignarJugadorAEquipo("22222223", e2.Id);
            AsignarJugadorAEquipo("22222224", e2.Id);
            AsignarJugadorAEquipo("22222225", e2.Id);

            // Crear un partido de ejemplo entre e1 y e2
            Partido p = AltaPartido(e1, e2, DateTime.Today, "16:00", "Cancha Central");
            if (p != null)
            {
                // Preparar alineaciones (primeros 5 como titulares, resto suplentes)
                var titularesLocal = new List<Jugador>() { ObtenerJugadoresPorEquipo(e1.Id)[0], ObtenerJugadoresPorEquipo(e1.Id)[1], ObtenerJugadoresPorEquipo(e1.Id)[2], ObtenerJugadoresPorEquipo(e1.Id)[3], ObtenerJugadoresPorEquipo(e1.Id)[4] };
                var suplentesLocal = new List<Jugador>() { ObtenerJugadoresPorEquipo(e1.Id)[5], ObtenerJugadoresPorEquipo(e1.Id)[6] };
                var titularesVisitante = new List<Jugador>() { ObtenerJugadoresPorEquipo(e2.Id)[0], ObtenerJugadoresPorEquipo(e2.Id)[1], ObtenerJugadoresPorEquipo(e2.Id)[2], ObtenerJugadoresPorEquipo(e2.Id)[3], ObtenerJugadoresPorEquipo(e2.Id)[4] };
                var suplentesVisitante = new List<Jugador>();

                ConfigurarAlineacion(p.Id, titularesLocal, suplentesLocal, titularesVisitante, suplentesVisitante);
                RegistrarResultado(p.Id, 2, 1);
            }
        }
    }
}