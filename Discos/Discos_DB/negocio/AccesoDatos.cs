using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Security.Policy;

namespace negocio
{
    public class AccesoDatos
    {

        private SqlConnection conexion; //conexion a la base de datos..
        private SqlCommand comando;
        private SqlDataReader lector;

        //aca almaceno los set de datos
        public SqlDataReader Lector
        {
            get { return lector; }
        } 


        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLSERVER; database=DISCOS_DB; integrated security=true"); //configuro la cadena de conexion
            comando = new SqlCommand(); //aca realizo acciones
        }

        public void setearConsulta(string consulta)
        {
            //para realiar la accion - inyecto la secuencia sql
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            //aca ejecuto la conexion
            comando.Connection = conexion;

            try
            {
                
                //armo la conexion
                conexion.Open();

                //realizo la lectura, como resultado me da un executeReader
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
 
        }

        public void cerrarConexion()
        {
            if(lector != null)
            {
                lector.Close();
                conexion.Close();
            }
        }




        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void setearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }












    }
}
