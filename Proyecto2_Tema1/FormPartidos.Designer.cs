namespace Proyecto2_Tema1
{
    partial class FormPartidos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPartidos));
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.lblLocal = new System.Windows.Forms.Label();
            this.cboLocal = new System.Windows.Forms.ComboBox();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.cboVisitante = new System.Windows.Forms.ComboBox();
            this.lblTitularesLocal = new System.Windows.Forms.Label();
            this.clbTitularesLocal = new System.Windows.Forms.CheckedListBox();
            this.lblSuplentesLocal = new System.Windows.Forms.Label();
            this.clbSuplentesLocal = new System.Windows.Forms.CheckedListBox();
            this.clbTitularesVisitante = new System.Windows.Forms.CheckedListBox();
            this.lblTitularesVisitante = new System.Windows.Forms.Label();
            this.clbSuplentesVisitante = new System.Windows.Forms.CheckedListBox();
            this.lblSuplentesVisitante = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblHorario = new System.Windows.Forms.Label();
            this.txtHorario = new System.Windows.Forms.TextBox();
            this.lblLugar = new System.Windows.Forms.Label();
            this.txtLugar = new System.Windows.Forms.TextBox();
            this.lblGolesLocal = new System.Windows.Forms.Label();
            this.txtGolesLocal = new System.Windows.Forms.TextBox();
            this.txtGolesVisitante = new System.Windows.Forms.TextBox();
            this.lblGolesVisitante = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(29, 15);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(57, 13);
            this.lblCategoria.TabIndex = 0;
            this.lblCategoria.Text = "Categoría:";
            // 
            // cboCategoria
            // 
            this.cboCategoria.FormattingEnabled = true;
            this.cboCategoria.Location = new System.Drawing.Point(92, 12);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Size = new System.Drawing.Size(121, 21);
            this.cboCategoria.TabIndex = 1;
            this.cboCategoria.SelectedIndexChanged += new System.EventHandler(this.cboCategoria_SelectedIndexChanged);
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(14, 59);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(72, 13);
            this.lblLocal.TabIndex = 2;
            this.lblLocal.Text = "Equipo Local:";
            // 
            // cboLocal
            // 
            this.cboLocal.FormattingEnabled = true;
            this.cboLocal.Location = new System.Drawing.Point(92, 56);
            this.cboLocal.Name = "cboLocal";
            this.cboLocal.Size = new System.Drawing.Size(121, 21);
            this.cboLocal.TabIndex = 3;
            this.cboLocal.SelectedIndexChanged += new System.EventHandler(this.cboLocal_SelectedIndexChanged);
            // 
            // lblVisitante
            // 
            this.lblVisitante.AutoSize = true;
            this.lblVisitante.Location = new System.Drawing.Point(257, 59);
            this.lblVisitante.Name = "lblVisitante";
            this.lblVisitante.Size = new System.Drawing.Size(86, 13);
            this.lblVisitante.TabIndex = 4;
            this.lblVisitante.Text = "Equipo Visitante:";
            // 
            // cboVisitante
            // 
            this.cboVisitante.FormattingEnabled = true;
            this.cboVisitante.Location = new System.Drawing.Point(349, 56);
            this.cboVisitante.Name = "cboVisitante";
            this.cboVisitante.Size = new System.Drawing.Size(121, 21);
            this.cboVisitante.TabIndex = 5;
            this.cboVisitante.SelectedIndexChanged += new System.EventHandler(this.cboVisitante_SelectedIndexChanged);
            // 
            // lblTitularesLocal
            // 
            this.lblTitularesLocal.AutoSize = true;
            this.lblTitularesLocal.Location = new System.Drawing.Point(90, 100);
            this.lblTitularesLocal.Name = "lblTitularesLocal";
            this.lblTitularesLocal.Size = new System.Drawing.Size(122, 13);
            this.lblTitularesLocal.TabIndex = 6;
            this.lblTitularesLocal.Text = "Titulares Local (elegir 5):";
            // 
            // clbTitularesLocal
            // 
            this.clbTitularesLocal.FormattingEnabled = true;
            this.clbTitularesLocal.Location = new System.Drawing.Point(93, 126);
            this.clbTitularesLocal.Name = "clbTitularesLocal";
            this.clbTitularesLocal.Size = new System.Drawing.Size(127, 94);
            this.clbTitularesLocal.TabIndex = 7;
            // 
            // lblSuplentesLocal
            // 
            this.lblSuplentesLocal.AutoSize = true;
            this.lblSuplentesLocal.Location = new System.Drawing.Point(90, 250);
            this.lblSuplentesLocal.Name = "lblSuplentesLocal";
            this.lblSuplentesLocal.Size = new System.Drawing.Size(130, 13);
            this.lblSuplentesLocal.TabIndex = 8;
            this.lblSuplentesLocal.Text = "Suplentes Local (hasta 3):";
            // 
            // clbSuplentesLocal
            // 
            this.clbSuplentesLocal.FormattingEnabled = true;
            this.clbSuplentesLocal.Location = new System.Drawing.Point(93, 276);
            this.clbSuplentesLocal.Name = "clbSuplentesLocal";
            this.clbSuplentesLocal.Size = new System.Drawing.Size(127, 94);
            this.clbSuplentesLocal.TabIndex = 9;
            // 
            // clbTitularesVisitante
            // 
            this.clbTitularesVisitante.FormattingEnabled = true;
            this.clbTitularesVisitante.Location = new System.Drawing.Point(349, 126);
            this.clbTitularesVisitante.Name = "clbTitularesVisitante";
            this.clbTitularesVisitante.Size = new System.Drawing.Size(133, 94);
            this.clbTitularesVisitante.TabIndex = 11;
            // 
            // lblTitularesVisitante
            // 
            this.lblTitularesVisitante.AutoSize = true;
            this.lblTitularesVisitante.Location = new System.Drawing.Point(346, 100);
            this.lblTitularesVisitante.Name = "lblTitularesVisitante";
            this.lblTitularesVisitante.Size = new System.Drawing.Size(136, 13);
            this.lblTitularesVisitante.TabIndex = 10;
            this.lblTitularesVisitante.Text = "Titulares Visitante (elegir 5):";
            // 
            // clbSuplentesVisitante
            // 
            this.clbSuplentesVisitante.FormattingEnabled = true;
            this.clbSuplentesVisitante.Location = new System.Drawing.Point(349, 276);
            this.clbSuplentesVisitante.Name = "clbSuplentesVisitante";
            this.clbSuplentesVisitante.Size = new System.Drawing.Size(133, 94);
            this.clbSuplentesVisitante.TabIndex = 13;
            // 
            // lblSuplentesVisitante
            // 
            this.lblSuplentesVisitante.AutoSize = true;
            this.lblSuplentesVisitante.Location = new System.Drawing.Point(338, 250);
            this.lblSuplentesVisitante.Name = "lblSuplentesVisitante";
            this.lblSuplentesVisitante.Size = new System.Drawing.Size(144, 13);
            this.lblSuplentesVisitante.TabIndex = 12;
            this.lblSuplentesVisitante.Text = "Suplentes Visitante (hasta 3):";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(46, 444);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 13);
            this.lblFecha.TabIndex = 14;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(92, 438);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(218, 20);
            this.dtpFecha.TabIndex = 15;
            // 
            // lblHorario
            // 
            this.lblHorario.AutoSize = true;
            this.lblHorario.Location = new System.Drawing.Point(332, 440);
            this.lblHorario.Name = "lblHorario";
            this.lblHorario.Size = new System.Drawing.Size(44, 13);
            this.lblHorario.TabIndex = 16;
            this.lblHorario.Text = "Horario:";
            // 
            // txtHorario
            // 
            this.txtHorario.Location = new System.Drawing.Point(382, 437);
            this.txtHorario.Name = "txtHorario";
            this.txtHorario.Size = new System.Drawing.Size(100, 20);
            this.txtHorario.TabIndex = 17;
            // 
            // lblLugar
            // 
            this.lblLugar.AutoSize = true;
            this.lblLugar.Location = new System.Drawing.Point(200, 479);
            this.lblLugar.Name = "lblLugar";
            this.lblLugar.Size = new System.Drawing.Size(37, 13);
            this.lblLugar.TabIndex = 18;
            this.lblLugar.Text = "Lugar:";
            // 
            // txtLugar
            // 
            this.txtLugar.Location = new System.Drawing.Point(243, 477);
            this.txtLugar.Name = "txtLugar";
            this.txtLugar.Size = new System.Drawing.Size(100, 20);
            this.txtLugar.TabIndex = 19;
            // 
            // lblGolesLocal
            // 
            this.lblGolesLocal.AutoSize = true;
            this.lblGolesLocal.Location = new System.Drawing.Point(48, 392);
            this.lblGolesLocal.Name = "lblGolesLocal";
            this.lblGolesLocal.Size = new System.Drawing.Size(66, 13);
            this.lblGolesLocal.TabIndex = 20;
            this.lblGolesLocal.Text = "Goles Local:";
            // 
            // txtGolesLocal
            // 
            this.txtGolesLocal.Location = new System.Drawing.Point(120, 389);
            this.txtGolesLocal.Name = "txtGolesLocal";
            this.txtGolesLocal.Size = new System.Drawing.Size(100, 20);
            this.txtGolesLocal.TabIndex = 21;
            // 
            // txtGolesVisitante
            // 
            this.txtGolesVisitante.Location = new System.Drawing.Point(382, 386);
            this.txtGolesVisitante.Name = "txtGolesVisitante";
            this.txtGolesVisitante.Size = new System.Drawing.Size(100, 20);
            this.txtGolesVisitante.TabIndex = 23;
            // 
            // lblGolesVisitante
            // 
            this.lblGolesVisitante.AutoSize = true;
            this.lblGolesVisitante.Location = new System.Drawing.Point(296, 389);
            this.lblGolesVisitante.Name = "lblGolesVisitante";
            this.lblGolesVisitante.Size = new System.Drawing.Size(80, 13);
            this.lblGolesVisitante.TabIndex = 22;
            this.lblGolesVisitante.Text = "Goles Visitante:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnGuardar.Location = new System.Drawing.Point(212, 514);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(131, 34);
            this.btnGuardar.TabIndex = 24;
            this.btnGuardar.Text = "Registrar Partido";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FormPartidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 570);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtGolesVisitante);
            this.Controls.Add(this.lblGolesVisitante);
            this.Controls.Add(this.txtGolesLocal);
            this.Controls.Add(this.lblGolesLocal);
            this.Controls.Add(this.txtLugar);
            this.Controls.Add(this.lblLugar);
            this.Controls.Add(this.txtHorario);
            this.Controls.Add(this.lblHorario);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.clbSuplentesVisitante);
            this.Controls.Add(this.lblSuplentesVisitante);
            this.Controls.Add(this.clbTitularesVisitante);
            this.Controls.Add(this.lblTitularesVisitante);
            this.Controls.Add(this.clbSuplentesLocal);
            this.Controls.Add(this.lblSuplentesLocal);
            this.Controls.Add(this.clbTitularesLocal);
            this.Controls.Add(this.lblTitularesLocal);
            this.Controls.Add(this.cboVisitante);
            this.Controls.Add(this.lblVisitante);
            this.Controls.Add(this.cboLocal);
            this.Controls.Add(this.lblLocal);
            this.Controls.Add(this.cboCategoria);
            this.Controls.Add(this.lblCategoria);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPartidos";
            this.Text = "Registrar partidos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.ComboBox cboLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.ComboBox cboVisitante;
        private System.Windows.Forms.Label lblTitularesLocal;
        private System.Windows.Forms.CheckedListBox clbTitularesLocal;
        private System.Windows.Forms.Label lblSuplentesLocal;
        private System.Windows.Forms.CheckedListBox clbSuplentesLocal;
        private System.Windows.Forms.CheckedListBox clbTitularesVisitante;
        private System.Windows.Forms.Label lblTitularesVisitante;
        private System.Windows.Forms.CheckedListBox clbSuplentesVisitante;
        private System.Windows.Forms.Label lblSuplentesVisitante;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblHorario;
        private System.Windows.Forms.TextBox txtHorario;
        private System.Windows.Forms.Label lblLugar;
        private System.Windows.Forms.TextBox txtLugar;
        private System.Windows.Forms.Label lblGolesLocal;
        private System.Windows.Forms.TextBox txtGolesLocal;
        private System.Windows.Forms.TextBox txtGolesVisitante;
        private System.Windows.Forms.Label lblGolesVisitante;
        private System.Windows.Forms.Button btnGuardar;
    }
}