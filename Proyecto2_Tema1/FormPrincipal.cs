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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            GestorLiga.CargarDatosPrueba();
        }

        private void btnEquipos_Click(object sender, EventArgs e)
        {
            FormEquipos formEquipos = new FormEquipos();
            formEquipos.ShowDialog();
        }

        private void btnJugadores_Click(object sender, EventArgs e)
        {
            FormJugadores formJugadores = new FormJugadores();
            formJugadores.ShowDialog();
        }

        private void btnPartidos_Click(object sender, EventArgs e)
        {
            FormPartidos formpartidos = new FormPartidos();
            formpartidos.ShowDialog();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            FormHistorial formHistorial = new FormHistorial();
            formHistorial.ShowDialog();
        }

        private void btnTabla_Click(object sender, EventArgs e)
        {
            FormTablaPosiciones formTabla = new FormTablaPosiciones();
            formTabla.ShowDialog();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            // SaveFileDialog permite al usuario elegir dónde guardar el archivo de reporte.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de texto|*.txt";
            sfd.FileName = "ReporteLiga.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GestorLiga.GenerarReporte(sfd.FileName);
                MessageBox.Show("Reporte generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}