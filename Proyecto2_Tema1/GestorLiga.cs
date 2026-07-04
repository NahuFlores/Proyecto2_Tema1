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

        // Datos de prueba precargados (con ayuda de una IA por tiempo)
        public static void CargarDatosPrueba()
        {
            // Datos manuales para demo con clubes de Bahía Blanca y jugadores explícitos
            Equipos.Clear();
            Jugadores.Clear();
            Partidos.Clear();
            proximoIdEquipo = 1;
            proximoIdPartido = 1;

            // --- PRIMERA: Olimpo vs Villa Mitre ---
            Equipo olimpo = AltaEquipo("Olimpo", Categoria.Primera);
            Equipo villa = AltaEquipo("Villa Mitre", Categoria.Primera);

            AltaJugador("30000001", "Martín", "López", 28, true, true);
            AsignarJugadorAEquipo("30000001", olimpo.Id);
            AltaJugador("30000002", "Diego", "Fernández", 26, true, true);
            AsignarJugadorAEquipo("30000002", olimpo.Id);
            AltaJugador("30000003", "Lucas", "Giménez", 24, true, true);
            AsignarJugadorAEquipo("30000003", olimpo.Id);
            AltaJugador("30000004", "Federico", "Romero", 23, true, true);
            AsignarJugadorAEquipo("30000004", olimpo.Id);
            AltaJugador("30000005", "Agustín", "Pérez", 21, true, true);
            AsignarJugadorAEquipo("30000005", olimpo.Id);
            AltaJugador("30000006", "Santiago", "González", 29, true, true);
            AsignarJugadorAEquipo("30000006", olimpo.Id);
            AltaJugador("30000007", "Tomás", "Martínez", 22, true, true);
            AsignarJugadorAEquipo("30000007", olimpo.Id);

            AltaJugador("30000011", "Javier", "Suárez", 27, true, true);
            AsignarJugadorAEquipo("30000011", villa.Id);
            AltaJugador("30000012", "Gonzalo", "Rojas", 25, true, true);
            AsignarJugadorAEquipo("30000012", villa.Id);
            AltaJugador("30000013", "Marcos", "Sosa", 24, true, true);
            AsignarJugadorAEquipo("30000013", villa.Id);
            AltaJugador("30000014", "Emiliano", "Cáceres", 26, true, true);
            AsignarJugadorAEquipo("30000014", villa.Id);
            AltaJugador("30000015", "Nicolás", "Díaz", 23, true, true);
            AsignarJugadorAEquipo("30000015", villa.Id);
            AltaJugador("30000016", "Facundo", "Alonso", 22, true, true);
            AsignarJugadorAEquipo("30000016", villa.Id);
            AltaJugador("30000017", "Pablo", "Torres", 30, true, true);
            AsignarJugadorAEquipo("30000017", villa.Id);

            // Partido Primera
            var p1 = AltaPartido(olimpo, villa, DateTime.Today.AddDays(-2), "16:00", "Estadio Municipal");
            if (p1 != null)
            {
                var tOL = ObtenerJugadoresPorEquipo(olimpo.Id);
                var tVI = ObtenerJugadoresPorEquipo(villa.Id);
                ConfigurarAlineacion(p1.Id, new List<Jugador>{tOL[0],tOL[1],tOL[2],tOL[3],tOL[4]}, new List<Jugador>{tOL[5],tOL[6]}, new List<Jugador>{tVI[0],tVI[1],tVI[2],tVI[3],tVI[4]}, new List<Jugador>{tVI[5],tVI[6]});
                RegistrarResultado(p1.Id, 2, 1);
            }

            // --- JUVENILES: Estudiantes vs Liniers ---
            Equipo estudiantes = AltaEquipo("Estudiantes", Categoria.Juveniles);
            Equipo liniers = AltaEquipo("Liniers", Categoria.Juveniles);

            AltaJugador("31000001", "Matías", "Vega", 17, true, true);
            AsignarJugadorAEquipo("31000001", estudiantes.Id);
            AltaJugador("31000002", "Marcelo", "Castro", 16, true, true);
            AsignarJugadorAEquipo("31000002", estudiantes.Id);
            AltaJugador("31000003", "Juan", "Iglesias", 16, true, true);
            AsignarJugadorAEquipo("31000003", estudiantes.Id);
            AltaJugador("31000004", "Leandro", "Aguirre", 17, true, true);
            AsignarJugadorAEquipo("31000004", estudiantes.Id);
            AltaJugador("31000005", "Rodrigo", "Ramos", 16, true, true);
            AsignarJugadorAEquipo("31000005", estudiantes.Id);
            AltaJugador("31000006", "Ezequiel", "Córdoba", 17, true, true);
            AsignarJugadorAEquipo("31000006", estudiantes.Id);
            AltaJugador("31000007", "Néstor", "Molina", 16, true, true);
            AsignarJugadorAEquipo("31000007", estudiantes.Id);

            AltaJugador("31000011", "Fabián", "Sánchez", 16, true, true);
            AsignarJugadorAEquipo("31000011", liniers.Id);
            AltaJugador("31000012", "Raúl", "Peralta", 17, true, true);
            AsignarJugadorAEquipo("31000012", liniers.Id);
            AltaJugador("31000013", "Hernán", "Villar", 16, true, true);
            AsignarJugadorAEquipo("31000013", liniers.Id);
            AltaJugador("31000014", "Germán", "Ojeda", 17, true, true);
            AsignarJugadorAEquipo("31000014", liniers.Id);
            AltaJugador("31000015", "Maximiliano", "Núñez", 16, true, true);
            AsignarJugadorAEquipo("31000015", liniers.Id);
            AltaJugador("31000016", "Álvaro", "Suárez", 17, true, true);
            AsignarJugadorAEquipo("31000016", liniers.Id);
            AltaJugador("31000017", "Iván", "Oliva", 16, true, true);
            AsignarJugadorAEquipo("31000017", liniers.Id);

            var p2 = AltaPartido(estudiantes, liniers, DateTime.Today.AddDays(-10), "15:00", "Cancha Juvenil");
            if (p2 != null)
            {
                var te = ObtenerJugadoresPorEquipo(estudiantes.Id);
                var tl = ObtenerJugadoresPorEquipo(liniers.Id);
                ConfigurarAlineacion(p2.Id, new List<Jugador>{te[0],te[1],te[2],te[3],te[4]}, new List<Jugador>{te[5],te[6]}, new List<Jugador>{tl[0],tl[1],tl[2],tl[3],tl[4]}, new List<Jugador>{tl[5],tl[6]});
                RegistrarResultado(p2.Id, 1, 1);
            }

            // --- CADETES: Libertad vs Dublin ---
            Equipo libertad = AltaEquipo("Libertad", Categoria.Cadetes);
            Equipo dublin = AltaEquipo("Dublin", Categoria.Cadetes);

            AltaJugador("32000001", "Emilio", "García", 14, true, true);
            AsignarJugadorAEquipo("32000001", libertad.Id);
            AltaJugador("32000002", "Tommy", "Herrera", 15, true, true);
            AsignarJugadorAEquipo("32000002", libertad.Id);
            AltaJugador("32000003", "Bautista", "Roldán", 15, true, true);
            AsignarJugadorAEquipo("32000003", libertad.Id);
            AltaJugador("32000004", "Sergio", "Cruz", 13, true, true);
            AsignarJugadorAEquipo("32000004", libertad.Id);
            AltaJugador("32000005", "Alan", "Suárez", 14, true, true);
            AsignarJugadorAEquipo("32000005", libertad.Id);
            AltaJugador("32000006", "León", "Perdi", 15, true, true);
            AsignarJugadorAEquipo("32000006", libertad.Id);
            AltaJugador("32000007", "Ítalo", "Funes", 14, true, true);
            AsignarJugadorAEquipo("32000007", libertad.Id);

            AltaJugador("32000011", "Matheo", "Rossi", 14, true, true);
            AsignarJugadorAEquipo("32000011", dublin.Id);
            AltaJugador("32000012", "Ciro", "Benítez", 13, true, true);
            AsignarJugadorAEquipo("32000012", dublin.Id);
            AltaJugador("32000013", "Brías", "Paz", 15, true, true);
            AsignarJugadorAEquipo("32000013", dublin.Id);
            AltaJugador("32000014", "Bautista", "Luna", 14, true, true);
            AsignarJugadorAEquipo("32000014", dublin.Id);
            AltaJugador("32000015", "Elías", "Campos", 15, true, true);
            AsignarJugadorAEquipo("32000015", dublin.Id);
            AltaJugador("32000016", "Facu", "Maldonado", 14, true, true);
            AsignarJugadorAEquipo("32000016", dublin.Id);
            AltaJugador("32000017", "Iván", "Pinto", 14, true, true);
            AsignarJugadorAEquipo("32000017", dublin.Id);

            var p3 = AltaPartido(libertad, dublin, DateTime.Today.AddDays(-5), "14:00", "Cancha Cadetes");
            if (p3 != null)
            {
                var tlb = ObtenerJugadoresPorEquipo(libertad.Id);
                var tdb = ObtenerJugadoresPorEquipo(dublin.Id);
                ConfigurarAlineacion(p3.Id, new List<Jugador>{tlb[0],tlb[1],tlb[2],tlb[3],tlb[4]}, new List<Jugador>{tlb[5],tlb[6]}, new List<Jugador>{tdb[0],tdb[1],tdb[2],tdb[3],tdb[4]}, new List<Jugador>{tdb[5],tdb[6]});
                RegistrarResultado(p3.Id, 0, 2);
            }

            // --- INFANTILES: Olimpo Infantiles vs Villa Mitre Infantiles ---
            Equipo olimpoInf = AltaEquipo("Olimpo", Categoria.Infantiles);
            Equipo villaInf = AltaEquipo("Villa Mitre", Categoria.Infantiles);

            AltaJugador("33000001", "Santino", "Navarro", 11, false, false);
            AsignarJugadorAEquipo("33000001", olimpoInf.Id);
            AltaJugador("33000002", "Dylan", "Leiva", 12, false, false);
            AsignarJugadorAEquipo("33000002", olimpoInf.Id);
            AltaJugador("33000003", "Tomás", "Ledesma", 10, false, false);
            AsignarJugadorAEquipo("33000003", olimpoInf.Id);
            AltaJugador("33000004", "Benjamín", "Correa", 12, false, false);
            AsignarJugadorAEquipo("33000004", olimpoInf.Id);
            AltaJugador("33000005", "Mateo", "Ayala", 11, false, false);
            AsignarJugadorAEquipo("33000005", olimpoInf.Id);
            AltaJugador("33000006", "Emiliano", "Cisneros", 12, false, false);
            AsignarJugadorAEquipo("33000006", olimpoInf.Id);
            AltaJugador("33000007", "Iker", "Agüero", 11, false, false);
            AsignarJugadorAEquipo("33000007", olimpoInf.Id);

            AltaJugador("33000011", "Lautaro", "Brito", 12, false, false);
            AsignarJugadorAEquipo("33000011", villaInf.Id);
            AltaJugador("33000012", "Ian", "Ferrer", 11, false, false);
            AsignarJugadorAEquipo("33000012", villaInf.Id);
            AltaJugador("33000013", "Gael", "Paredes", 10, false, false);
            AsignarJugadorAEquipo("33000013", villaInf.Id);
            AltaJugador("33000014", "Noah", "Riquelme", 12, false, false);
            AsignarJugadorAEquipo("33000014", villaInf.Id);
            AltaJugador("33000015", "Thiago", "Serrano", 11, false, false);
            AsignarJugadorAEquipo("33000015", villaInf.Id);
            AltaJugador("33000016", "Damián", "Vega", 12, false, false);
            AsignarJugadorAEquipo("33000016", villaInf.Id);
            AltaJugador("33000017", "Joel", "Morales", 10, false, false);
            AsignarJugadorAEquipo("33000017", villaInf.Id);

            var p4 = AltaPartido(olimpoInf, villaInf, DateTime.Today.AddDays(-1), "10:00", "Cancha Infantil");
            if (p4 != null)
            {
                var toi = ObtenerJugadoresPorEquipo(olimpoInf.Id);
                var tvi = ObtenerJugadoresPorEquipo(villaInf.Id);
                ConfigurarAlineacion(p4.Id, new List<Jugador>{toi[0],toi[1],toi[2],toi[3],toi[4]}, new List<Jugador>{toi[5],toi[6]}, new List<Jugador>{tvi[0],tvi[1],tvi[2],tvi[3],tvi[4]}, new List<Jugador>{tvi[5],tvi[6]});
                RegistrarResultado(p4.Id, 3, 0);
            }

            // --- VETERANOS: Estudiantes Veteranos vs Liniers Veteranos ---
            Equipo estVet = AltaEquipo("Estudiantes", Categoria.Veteranos);
            Equipo linVet = AltaEquipo("Liniers", Categoria.Veteranos);

            AltaJugador("34000001", "Roberto", "Arias", 36, true, true);
            AsignarJugadorAEquipo("34000001", estVet.Id);
            AltaJugador("34000002", "Hugo", "Márquez", 38, true, true);
            AsignarJugadorAEquipo("34000002", estVet.Id);
            AltaJugador("34000003", "Ricardo", "Santos", 40, true, true);
            AsignarJugadorAEquipo("34000003", estVet.Id);
            AltaJugador("34000004", "Carlos", "Herrero", 45, true, true);
            AsignarJugadorAEquipo("34000004", estVet.Id);
            AltaJugador("34000005", "Óscar", "Duarte", 37, true, true);
            AsignarJugadorAEquipo("34000005", estVet.Id);
            AltaJugador("34000006", "Alberto", "Rico", 42, true, true);
            AsignarJugadorAEquipo("34000006", estVet.Id);
            AltaJugador("34000007", "Norberto", "Vega", 39, true, true);
            AsignarJugadorAEquipo("34000007", estVet.Id);

            AltaJugador("34000011", "Víctor", "Ponce", 36, true, true);
            AsignarJugadorAEquipo("34000011", linVet.Id);
            AltaJugador("34000012", "Miguel", "Benítez", 41, true, true);
            AsignarJugadorAEquipo("34000012", linVet.Id);
            AltaJugador("34000013", "Eduardo", "Neri", 38, true, true);
            AsignarJugadorAEquipo("34000013", linVet.Id);
            AltaJugador("34000014", "Santos", "Pérez", 44, true, true);
            AsignarJugadorAEquipo("34000014", linVet.Id);
            AltaJugador("34000015", "Félix", "Córdoba", 35, true, true);
            AsignarJugadorAEquipo("34000015", linVet.Id);
            AltaJugador("34000016", "Joaquín", "Marín", 37, true, true);
            AsignarJugadorAEquipo("34000016", linVet.Id);
            AltaJugador("34000017", "Gustavo", "León", 39, true, true);
            AsignarJugadorAEquipo("34000017", linVet.Id);

            var p5 = AltaPartido(estVet, linVet, DateTime.Today.AddDays(-20), "11:00", "Cancha Veteranos");
            if (p5 != null)
            {
                var tev = ObtenerJugadoresPorEquipo(estVet.Id);
                var tlv = ObtenerJugadoresPorEquipo(linVet.Id);
                ConfigurarAlineacion(p5.Id, new List<Jugador>{tev[0],tev[1],tev[2],tev[3],tev[4]}, new List<Jugador>{tev[5],tev[6]}, new List<Jugador>{tlv[0],tlv[1],tlv[2],tlv[3],tlv[4]}, new List<Jugador>{tlv[5],tlv[6]});
                RegistrarResultado(p5.Id, 2, 2);
            }
        }
    }
}