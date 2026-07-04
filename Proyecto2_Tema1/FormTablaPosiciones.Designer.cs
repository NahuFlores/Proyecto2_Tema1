using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Proyecto2_Tema1
{
    partial class FormTablaPosiciones
    {
        /// <summary>
        /// Variables del diseñador
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgv;
        private ComboBox cmbCategoria;

        /// <summary>
        /// Inicializa los controles del diseñador para que puedan editarse en Visual Studio.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTablaPosiciones));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Location = new System.Drawing.Point(0, 21);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(700, 320);
            this.dgv.TabIndex = 0;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Location = new System.Drawing.Point(0, 0);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(700, 21);
            this.cmbCategoria.TabIndex = 1;
            this.cmbCategoria.SelectedIndexChanged += new System.EventHandler(this.cmbCategoria_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 353);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 40);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "El equipo que más puntos logre (independientemente de la categoría)\r\nganará un vi" +
    "aje todo pago para ver el mundial.";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FormTablaPosiciones
            // 
            this.ClientSize = new System.Drawing.Size(700, 405);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.cmbCategoria);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTablaPosiciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tabla de Posiciones";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">True si se deben liberar recursos administrados; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox textBox1;
    }
}
