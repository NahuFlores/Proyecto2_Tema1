using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Tema1
{
    public class Partido
    {
        public int Id { get; set; }
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        public List<Jugador> TitularesLocal { get; set; }
        public List<Jugador> TitularesVisitante { get; set; }
        public List<Jugador> SuplentesLocal { get; set; }
        public List<Jugador> SuplentesVisitante { get; set; }
        public DateTime Fecha { get; set; }
        public string Horario { get; set; }
        public string Lugar { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }

        public Partido(int id, Equipo equipoLocal, Equipo equipoVisitante, DateTime fecha, string horario, string lugar)
        {
            Id = id;
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
            Fecha = fecha;
            Horario = horario;
            Lugar = lugar;
            TitularesLocal = new List<Jugador>();
            TitularesVisitante = new List<Jugador>();
            SuplentesLocal = new List<Jugador>();
            SuplentesVisitante = new List<Jugador>();
            GolesLocal = 0;
            GolesVisitante = 0;
        }

        public override string ToString()
        {
            return EquipoLocal.Nombre + " vs " + EquipoVisitante.Nombre + " - " + Fecha.ToShortDateString();
        }
    }
}
