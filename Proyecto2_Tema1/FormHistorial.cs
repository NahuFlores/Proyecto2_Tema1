using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    public partial class FormHistorial : Form
    {
        public FormHistorial()
        {
            InitializeComponent();
            CargarHistorial();
        }

        // Carga el historial de partidos en el DataGridView
        private void CargarHistorial()
        {
            dgvHistorial.DataSource = null;
            dgvHistorial.Rows.Clear();
            dgvHistorial.Columns.Clear();

            dgvHistorial.Columns.Add("Local", "Equipo Local");
            dgvHistorial.Columns.Add("Resultado", "Resultado");
            dgvHistorial.Columns.Add("Visitante", "Equipo Visitante");
            dgvHistorial.Columns.Add("Fecha", "Fecha");
            dgvHistorial.Columns.Add("Horario", "Horario");
            dgvHistorial.Columns.Add("Lugar", "Lugar");

            foreach (Partido p in GestorLiga.Partidos)
            {
                dgvHistorial.Rows.Add(
                    p.EquipoLocal.Nombre,
                    p.GolesLocal + " - " + p.GolesVisitante,
                    p.EquipoVisitante.Nombre,
                    p.Fecha.ToShortDateString(),
                    p.Horario,
                    p.Lugar
                );
            }
        }

        private void btnVerFormacion_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un partido de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int indice = dgvHistorial.SelectedRows[0].Index;
            Partido partido = GestorLiga.Partidos[indice];

            string formacion = "FORMACIÓN\n\n";

            formacion += "--- " + partido.EquipoLocal.Nombre + " ---\n";
            formacion += "Titulares:\n";
            foreach (Jugador j in partido.TitularesLocal)
                formacion += "  " + j.ToString() + "\n";

            formacion += "Suplentes:\n";
            foreach (Jugador j in partido.SuplentesLocal)
                formacion += "  " + j.ToString() + "\n";

            formacion += "\n--- " + partido.EquipoVisitante.Nombre + " ---\n";
            formacion += "Titulares:\n";
            foreach (Jugador j in partido.TitularesVisitante)
                formacion += "  " + j.ToString() + "\n";

            formacion += "Suplentes:\n";
            foreach (Jugador j in partido.SuplentesVisitante)
                formacion += "  " + j.ToString() + "\n";

            MessageBox.Show(formacion, "Formación del Partido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
