using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Tema1
{
    public class Jugador
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public bool TieneSeguro { get; set; }
        public bool EstaAfiliado { get; set; }

        // IDs de los equipos a los que pertenece el jugador
        public List<int> EquiposAsignados { get; set; }

        public Jugador(string dni, string nombre, string apellido, int edad, bool tieneSeguro, bool estaAfiliado)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            TieneSeguro = tieneSeguro;
            EstaAfiliado = estaAfiliado;
            EquiposAsignados = new List<int>();
        }

        public override string ToString()
        {
            return Apellido + ", " + Nombre + " (DNI: " + DNI + ")";
        }
    }
}
