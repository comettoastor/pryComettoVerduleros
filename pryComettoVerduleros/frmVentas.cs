﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryComettoVerduleros
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }

        public string Conexion = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = VERDULEROS.mdb";

        private void frmVentas_Load(object sender, EventArgs e)
        {
            clsBaseDeDatos clsBaseDeDatos = new clsBaseDeDatos();
            clsBaseDeDatos.CargarTabla(Conexion, "Ventas", dgvVentas);
            clsBaseDeDatos.CargarProductos(Conexion, "Productos", cmbProducto);
            clsBaseDeDatos.CargarVendedores(Conexion, "Vendedores", cmbVendedor);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsBaseDeDatos clsBaseDeDatos = new clsBaseDeDatos();
            clsBaseDeDatos.GrabarVentas(Conexion, "Ventas", int.Parse(cmbVendedor.Text.Split(',')[0]), cmbProducto.SelectedIndex + 1, dtpFecha.Value, int.Parse(txtKilos.Text));
            clsBaseDeDatos.CargarTabla(Conexion, "Ventas", dgvVentas);
        }
    }
}
