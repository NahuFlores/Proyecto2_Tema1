using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_Tema1
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreClub { get; set; }
        public Categoria Categoria { get; set; }

        public Equipo(int id, string nombre, string nombreClub, Categoria categoria)
        {
            Id = id;
            Nombre = nombre;
            NombreClub = nombreClub;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return Nombre + " (" + Categoria + ")";
        }
    }
}
