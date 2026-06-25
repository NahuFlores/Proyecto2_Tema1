namespace Proyecto2_Tema1
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnEquipos = new System.Windows.Forms.Button();
            this.btnJugadores = new System.Windows.Forms.Button();
            this.btnPartidos = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.btnTabla = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(339, 69);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(126, 19);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Liga Deportiva ";
            // 
            // btnEquipos
            // 
            this.btnEquipos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEquipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEquipos.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEquipos.Location = new System.Drawing.Point(260, 106);
            this.btnEquipos.Name = "btnEquipos";
            this.btnEquipos.Size = new System.Drawing.Size(277, 36);
            this.btnEquipos.TabIndex = 1;
            this.btnEquipos.Text = "Gestión de Equipos";
            this.btnEquipos.UseVisualStyleBackColor = true;
            this.btnEquipos.Click += new System.EventHandler(this.btnEquipos_Click);
            // 
            // btnJugadores
            // 
            this.btnJugadores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnJugadores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJugadores.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJugadores.Location = new System.Drawing.Point(260, 148);
            this.btnJugadores.Name = "btnJugadores";
            this.btnJugadores.Size = new System.Drawing.Size(277, 36);
            this.btnJugadores.TabIndex = 2;
            this.btnJugadores.Text = "Gestión de Jugadores";
            this.btnJugadores.UseVisualStyleBackColor = true;
            this.btnJugadores.Click += new System.EventHandler(this.btnJugadores_Click);
            // 
            // btnPartidos
            // 
            this.btnPartidos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPartidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartidos.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPartidos.Location = new System.Drawing.Point(260, 190);
            this.btnPartidos.Name = "btnPartidos";
            this.btnPartidos.Size = new System.Drawing.Size(277, 36);
            this.btnPartidos.TabIndex = 3;
            this.btnPartidos.Text = "Registrar Partido";
            this.btnPartidos.UseVisualStyleBackColor = true;
            this.btnPartidos.Click += new System.EventHandler(this.btnPartidos_Click);
            // 
            // btnHistorial
            // 
            this.btnHistorial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorial.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorial.Location = new System.Drawing.Point(260, 232);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(277, 36);
            this.btnHistorial.TabIndex = 4;
            this.btnHistorial.Text = "Historial de Partidos";
            this.btnHistorial.UseVisualStyleBackColor = true;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // btnTabla
            // 
            this.btnTabla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTabla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabla.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTabla.Location = new System.Drawing.Point(260, 274);
            this.btnTabla.Name = "btnTabla";
            this.btnTabla.Size = new System.Drawing.Size(277, 36);
            this.btnTabla.TabIndex = 5;
            this.btnTabla.Text = "Tabla de Posiciones";
            this.btnTabla.UseVisualStyleBackColor = true;
            this.btnTabla.Click += new System.EventHandler(this.btnTabla_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporte.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.Location = new System.Drawing.Point(260, 316);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(277, 36);
            this.btnReporte.TabIndex = 6;
            this.btnReporte.Text = "Generar Reporte";
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 542);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.btnTabla);
            this.Controls.Add(this.btnHistorial);
            this.Controls.Add(this.btnPartidos);
            this.Controls.Add(this.btnJugadores);
            this.Controls.Add(this.btnEquipos);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FormPrincipal";
            this.Text = "Liga Deportiva - Fútbol 5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEquipos;
        private System.Windows.Forms.Button btnJugadores;
        private System.Windows.Forms.Button btnPartidos;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnTabla;
        private System.Windows.Forms.Button btnReporte;
    }
}