using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    partial class FormTablaPosiciones
    {
        /// <summary>
        /// Designer variables
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgv;
        private ComboBox cmbCategoria;
        private Button btnCerrar;

        /// <summary>
        /// Initialize designer controls so they are editable in Visual Studio.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.dgv = new DataGridView();
            this.cmbCategoria = new ComboBox();
            this.btnCerrar = new Button();

            // Form
            this.Text = "Tabla de Posiciones";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(700, 400);

            // cmbCategoria
            this.cmbCategoria.Dock = DockStyle.Top;
            this.cmbCategoria.Height = 30;
            this.cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            // dgv
            this.dgv.Dock = DockStyle.Top;
            this.dgv.Height = 320;
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnCerrar
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Dock = DockStyle.Bottom;
            this.btnCerrar.Height = 30;

            // Add controls in desired order (Combo arriba, luego tabla, luego botón)
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.btnCerrar);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
