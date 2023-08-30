using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dominio;


namespace negocio
{
    public class DiscoNegocio
    {


        
        //metodo listar "Discos"
        public List<Disco> Listar()
        {
            //aca creo la lista "de Discos"
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("SELECT D.Id, Titulo,FechaLanzamiento,CantidadCanciones, UrlImagenTapa, E.Descripcion, E.Id,T.Descripcion, T.Id FROM  DISCOS D, ESTILOS E, TIPOSEDICION T WHERE D.IdEstilo = E.Id and d.IdTipoEdicion = t.Id and D.Activo=1");
                datos.ejecutarLectura();


                while (datos.Lector.Read()) 
                { 
                
                    Disco aux = new Disco();

                    
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Fecha = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    aux.UrlTapa = (string)datos.Lector["UrlImagenTapa"];


                    //aca muestro los datos relacionados en la tabla DISCOS
                    aux.TipoEsti = new Estilo();
                    aux.TipoEsti.Id = (int)datos.Lector[6];
                    aux.TipoEsti.Descripcion = (string)datos.Lector[5];

                    aux.TipoEdi = new TipoEdicion();
                    aux.TipoEdi.Descripcion = (string)datos.Lector[7];
                    aux.TipoEdi.Id = (int)datos.Lector[8];


                    //listo los datos leidos
                    lista.Add(aux);
                }
  
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }  
        }


        public void agregar(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // datos.setearConsulta("INSERT INTO DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion) VALUES ('" + nuevo.Titulo + "', '" + nuevo.Fecha + "', '" + nuevo.CantidadCanciones + "', '" + nuevo.UrlTapa + "', @idEstilo , @idTipoEdicion)");

                //datos.setearParametros("@idEstilo", nuevo.TipoEsti.Id);
                //datos.setearParametros("@idTipoEdicion", nuevo.TipoEdi.Id);
               
                datos.setearConsulta("INSERT INTO DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion) VALUES (@Titulo, @Fecha, @CantidadCanciones, @UrlTapa, @idEstilo, @idTipoEdicion)");
                datos.setearParametros("@Titulo", nuevo.Titulo);
                datos.setearParametros("@Fecha", nuevo.Fecha);
                datos.setearParametros("@CantidadCanciones", nuevo.CantidadCanciones);
                datos.setearParametros("@UrlTapa", nuevo.UrlTapa);
                datos.setearParametros("@idEstilo", nuevo.TipoEsti.Id);
                datos.setearParametros("@idTipoEdicion", nuevo.TipoEdi.Id);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // LISTAS DESPLEGABLES..

        public List<Estilo> listarEstilos()
        {
            List<Estilo> tipoEstilo = new List<Estilo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {


                datos.setearConsulta("select * from ESTILOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estilo estilo = new Estilo();

                    estilo.Id = (int)datos.Lector["Id"];
                    estilo.Descripcion = (string)datos.Lector["Descripcion"];

                    tipoEstilo.Add(estilo);
                }

                return tipoEstilo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public List<TipoEdicion> listarEdicion()
        {
            List<TipoEdicion> edicion = new List<TipoEdicion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {


                datos.setearConsulta("SELECT * FROM TIPOSEDICION");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoEdicion tipoEdicion = new TipoEdicion();

                    tipoEdicion.Id = (int)datos.Lector["Id"];
                    tipoEdicion.Descripcion = (string)datos.Lector["Descripcion"];

                    edicion.Add(tipoEdicion);
                }

                return edicion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                datos.setearConsulta("UPDATE DISCOS SET Titulo = @Titulo, FechaLanzamiento = @Fecha, CantidadCanciones = @CantidadCanciones, UrlImagenTapa = @UrlTapa, IdEstilo = @idEstilo, IdTipoEdicion = @idTipoEdicion WHERE Id = @Id");
                
                datos.setearParametros("@Titulo", disco.Titulo);
                datos.setearParametros("@Fecha", disco.Fecha);
                datos.setearParametros("@CantidadCanciones", disco.CantidadCanciones);
                datos.setearParametros("@UrlTapa", disco.UrlTapa);
                datos.setearParametros("@idEstilo", disco.TipoEsti.Id);
                datos.setearParametros("@idTipoEdicion", disco.TipoEdi.Id);
                datos.setearParametros("@id", disco.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM DISCOS WHERE Id = @Id");
                datos.setearParametros("@id", disco.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update DISCOS set Activo = 0 where Id = @Id");
                datos.setearParametros("@id", id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { 
                datos.cerrarConexion();
            }
        }
    }
}
