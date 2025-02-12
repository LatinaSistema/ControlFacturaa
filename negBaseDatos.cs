using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapaControl.CustomerDatagrid;

namespace latinasoft
{
    public class negBaseDatos : NegClaseBase.NegClaseBase
    {
        private string strFechaDesde;
        private string strFechaHasta;
        private string strCodCliente;
        private string strCodPrecio;
        private string strCodItems;
        private string strCodBodega;
        private string strPedido;
        private string strUsuario;
        private int intLinea;
        private decimal decMaxDescuento;
        private string strFactura;
        private string strCodVendedor;
        private string strTipoPedido;
   
        public string Factura
        {
            get
            {
                return strFactura;
            }
            set
            {
                strFactura = value;
            }
        }





        public DataSet Buscar(SqlConnection con)
        {
            DataSet dtsDatos = new DataSet();
            base.LimpiarParametrosStore();

            
           
            base.AgregarParametroStore("@CodigoFactura", strFactura, DbType.String);

           
            dtsDatos = base.EjecutarBusquedaDs("spConsultaDatosFacturaControlVentas", ref con, true);

            return dtsDatos;

        }



  



  

        

       

        




        

        

        //public void AprobarDescuentos(SqlConnection con, SqlTransaction trans)
        //{

        //    base.LimpiarParametrosStore();

        //    base.AgregarParametroStore("@Accion", "ADP", DbType.String);
        //    base.AgregarParametroStore("@Pedido", Pedido, DbType.String);
        //    base.AgregarParametroStore("@Usuario", Usuario, DbType.String);

        //    base.EjecutarStoreProcedure("spMantOrdenesVenta", ref con, ref trans, true);

        //}



        

    }


}
