using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data.Common;

namespace pryComettoVerduleros
{
    internal class clsBaseDeDatos
    {
        public OleDbCommand Comando = new OleDbCommand();
        public OleDbDataReader Lector;
        public OleDbConnection Conexion = new OleDbConnection();

        public void CargarTabla(string cadenaConexion, string tabla, DataGridView dgvTablas)
        {
            try
            {
                Conexion.ConnectionString = cadenaConexion;

                Comando.Connection = Conexion;
                Comando.CommandText = tabla;
                Comando.CommandType = CommandType.TableDirect;
                Comando.Connection.Open();

                Lector = Comando.ExecuteReader();

                DataTable DataTable = new DataTable();
                DataTable.Load(Lector);

                dgvTablas.DataSource = DataTable;

                Comando.Connection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void CargarVendedores(string cadenaConexion, string tabla, ComboBox cmbVendedores)
        {
            try
            {
                Conexion.ConnectionString = cadenaConexion;

                Comando.Connection = Conexion;
                Comando.CommandText = tabla;
                Comando.CommandType = CommandType.TableDirect;
                Comando.Connection.Open();

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {
                    DataTable DataTable = new DataTable();
                    DataTable.Load(Lector);

                    cmbVendedores.DataSource = DataTable;
                    cmbVendedores.ValueMember = "IdVendedor";
                    cmbVendedores.DisplayMember = "NombreVendedor";
                }

                Comando.Connection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public void CargarProductos(string cadenaConexion, string tabla, ComboBox cmbProductos)
        {
            try
            {
                Conexion.ConnectionString = cadenaConexion;

                Comando.Connection = Conexion;
                Comando.CommandText = tabla;
                Comando.CommandType = CommandType.TableDirect;
                Comando.Connection.Open();

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {
                    DataTable DataTable = new DataTable();
                    DataTable.Load(Lector);

                    cmbProductos.DataSource = DataTable;
                    cmbProductos.ValueMember = "IdProducto";
                    cmbProductos.DisplayMember = "NomProducto";
                }
             
                Comando.Connection.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + "Cargar Productos");
            }
        }

        public void GrabarVentas(string cadenaConexion, string tabla, int idVendedor, int idProducto, DateTime fecha, int kilos)
        {
            try
            {
                Conexion.ConnectionString = cadenaConexion;
                string query = "INSERT INTO Ventas (`Cod Vendedor`,`Cod Producto`,Fecha,Kilos) VALUES (?,?,?,?)";

                using (Comando = new OleDbCommand(query, Conexion))
                {
                    Comando.Parameters.AddWithValue("Cod Vendedor", idVendedor);
                    Comando.Parameters.AddWithValue("Cod Producto", idProducto);
                    Comando.Parameters.AddWithValue("Fecha", fecha.Date);
                    Comando.Parameters.AddWithValue("Kilos", kilos);

                    Comando.Connection.Open();

                    Comando.ExecuteNonQuery();

                    Comando.Connection.Close();
                }

                MessageBox.Show("Venta ingresada correctamente!","Venta ingresada",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + error.Data + error.Source);
            }
        }
    }
}
