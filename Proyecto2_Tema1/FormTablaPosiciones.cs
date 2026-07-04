using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    public class FormTablaPosiciones : Form
    {
        private DataGridView dgv;
        private Button btnCerrar;

        public FormTablaPosiciones()
        {
            Text = "Tabla de Posiciones";
            Width = 700;
            Height = 400;
            StartPosition = FormStartPosition.CenterParent;

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 320, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            btnCerrar = new Button { Text = "Cerrar", Dock = DockStyle.Bottom, Height = 30 };

            btnCerrar.Click += (s, e) => Close();

            Controls.Add(dgv);
            Controls.Add(btnCerrar);

            Load += FormTablaPosiciones_Load;
        }

        private void FormTablaPosiciones_Load(object sender, EventArgs e)
        {
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

            foreach (var equipo in GestorLiga.Equipos)
            {
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
                table.Rows.Add(equipo.Nombre, pj, pg, pe, pp, gf, gc, dg, pts);
            }

            var dv = table.DefaultView;
            dv.Sort = "Pts DESC, DG DESC, GF DESC";
            dgv.DataSource = dv.ToTable();
        }
    }
}
