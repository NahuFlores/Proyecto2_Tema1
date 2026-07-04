using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    public partial class FormTablaPosiciones : Form
    {
        public FormTablaPosiciones()
        {
            InitializeComponent();
            Load += FormTablaPosiciones_Load;
            cmbCategoria.SelectedIndexChanged += (s, e) => CargarTablaSegunSeleccion();
        }

        private void FormTablaPosiciones_Load(object sender, EventArgs e)
        {
            // Categorías (incluye opción "Todas")
            cmbCategoria.Items.Add("Todas");
            foreach (var c in Enum.GetValues(typeof(Categoria)))
                cmbCategoria.Items.Add(c.ToString());
            cmbCategoria.SelectedIndex = 0; // Por defecto muestra todas las categorías

            var table = new DataTable();
            table.Columns.Add("Equipo", typeof(string));
            table.Columns.Add("PJ", typeof(int));
            table.Columns.Add("PG", typeof(int));
            table.Columns.Add("PE", typeof(int));
            table.Columns.Add("PP", typeof(int));
            table.Columns.Add("GF", typeof(int));
            table.Columns.Add("GC", typeof(int));
            table.Columns.Add("DG", typeof(int));
            table.Columns.Add("Pts", typeof(int));

            // Rellenar la tabla
            CargarTablaSegunSeleccion();
        }

        private void CargarTablaSegunSeleccion()
        {
            var tableLocal = new DataTable();
            tableLocal.Columns.Add("Equipo", typeof(string));
            tableLocal.Columns.Add("PJ", typeof(int));
            tableLocal.Columns.Add("PG", typeof(int));
            tableLocal.Columns.Add("PE", typeof(int));
            tableLocal.Columns.Add("PP", typeof(int));
            tableLocal.Columns.Add("GF", typeof(int));
            tableLocal.Columns.Add("GC", typeof(int));
            tableLocal.Columns.Add("DG", typeof(int));
            tableLocal.Columns.Add("Pts", typeof(int));

            Categoria? filtro = null;
            if (cmbCategoria.SelectedIndex > 0)
            {
                var texto = cmbCategoria.SelectedItem.ToString();
                if (Enum.TryParse<Categoria>(texto, out var cat)) filtro = cat;
            }

            foreach (var equipo in GestorLiga.Equipos)
            {
                if (filtro != null && equipo.Categoria != filtro.Value) continue;

                int pj = GestorLiga.ObtenerPartidosJugados(equipo);
                int pg = GestorLiga.ObtenerVictorias(equipo);
                int pe = GestorLiga.ObtenerEmpates(equipo);
                int pp = GestorLiga.ObtenerDerrotas(equipo);
                int gf = 0;
                int gc = 0;
                foreach (var p in GestorLiga.Partidos)
                {
                    if (p.EquipoLocal == equipo)
                    {
                        gf += p.GolesLocal;
                        gc += p.GolesVisitante;
                    }
                    if (p.EquipoVisitante == equipo)
                    {
                        gf += p.GolesVisitante;
                        gc += p.GolesLocal;
                    }
                }
                int pts = pg * 3 + pe;
                int dg = gf - gc;
                tableLocal.Rows.Add(equipo.Nombre, pj, pg, pe, pp, gf, gc, dg, pts);
            }

            var dv = tableLocal.DefaultView;
            // Ordenar por puntos, diferencia de goles y goles a favor
            dv.Sort = "Pts DESC, DG DESC, GF DESC";
            dgv.DataSource = dv.ToTable();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
