using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latinasoft
{
    public partial class FrmRegistroFactura :  Maestro.FrmMaestro
    {
        public FrmRegistroFactura()
        {
            InitializeComponent();
        }

        DataTable dtGridDetalle = new DataTable();

        private void txtFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) {

                Buscar(txtFactura.Text);
            }
        }

         bool Buscar(string NumFactura)
        {
            SqlConnection con = new SqlConnection();


            DataSet dtdatos = new DataSet();

            negBaseDatos negBaseDatos = new negBaseDatos();

            negBaseDatos.BaseDatos = Base;

            con = negBaseDatos.Conectar();

            negBaseDatos.Factura = NumFactura;
            dtdatos = negBaseDatos.Buscar(con);

            if (dtdatos.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Factura no existe. Verifique", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            foreach (DataRow rs in dtdatos.Tables[0].Rows)
             {
                txtEstado.Text = rs["nombreEstado"].ToString();

            }
            return true; 

        }

        private void pklChofer_Load(object sender, EventArgs e)
        {

        }

        private void FrmRegistroFactura_Load(object sender, EventArgs e)
        {
            IniciaGrid();
        }

        void IniciaGrid()
        {

            dtGridDetalle.Columns.Add("NumFactura", typeof(string)).ReadOnly = true;
            dtGridDetalle.Columns.Add("CodTrans", typeof(string)).ReadOnly = true;
            dtGridDetalle.Columns.Add("Eliminar", typeof(Image)).ReadOnly = true;
            dtGridDetalle.Columns.Add("DescTrans", typeof(string)).ReadOnly = true;
            dtGridDetalle.Columns.Add("Observacion", typeof(string)).ReadOnly = true;

            


            dtgDatos.DataSource = dtGridDetalle;





        }

        private void bntAñadirDetalle_Click(object sender, EventArgs e)
        {
            // Verifica si el DataGridView tiene un DataSource válido
            DataTable dt = dtgDatos.DataSource as DataTable;

            if (dt != null)
            {
                string nuevoNumFactura = txtFactura.Text; // Cambia esto según tu lógica

                //verificar si factura no es ingresadas
                if (nuevoNumFactura=="")
                {
                    MessageBox.Show("Debe ingresar número de factura", "Factura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Salir del método
                }

                if (!Buscar(nuevoNumFactura))
                {
                   return;
                }
                // Verificar si NumFactura ya existe en el DataTable
                bool existe = dt.AsEnumerable().Any(row => row.Field<string>("NumFactura") == nuevoNumFactura);

                if (existe)
                {
                    MessageBox.Show("El número de factura ya está ingresado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Salir del método si ya existe
                }

                if (pklChofer.Codigo == null || pklChofer.Codigo.ToString()=="")
                {
                    MessageBox.Show("Debe ingresar chofer", "Chofer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear una nueva fila
                DataRow nuevaFila = dt.NewRow();
                nuevaFila["NumFactura"] = nuevoNumFactura;
                nuevaFila["CodTrans"] = pklChofer.Codigo;
                nuevaFila["Eliminar"] = null; // Para imágenes puedes manejarlo de otra manera
                nuevaFila["DescTrans"] = pklChofer.Descripcion;
                nuevaFila["Observacion"] = "Sin observaciones";

                // Agregar la fila al DataTable
                dt.Rows.Add(nuevaFila);

                // Refrescar el DataGridView
                dtgDatos.Refresh();
            }
            else
            {
                MessageBox.Show("El DataGridView no tiene un DataSource asignado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
