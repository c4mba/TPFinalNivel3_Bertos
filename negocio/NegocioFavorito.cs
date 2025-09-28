using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class NegocioFavorito
    {
        public List<Articulo> listarFavoritos(int idUser)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT A.Id, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio" + " FROM FAVORITOS F INNER JOIN ARTICULOS A ON F.IdArticulo = A.Id " + "WHERE F.IdUser = @idUser");
                datos.setParametro("@idUser", idUser);
                datos.getConsulta();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    arti.Id = (int)datos.Lector["Id"];
                    arti.Nombre = (string)datos.Lector["Nombre"];
                    arti.Descripcion = (string)datos.Lector["Descripcion"];
                    arti.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    arti.Precio = (decimal)datos.Lector["Precio"];

                    listaArticulos.Add(arti);

                }
                return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void agregarFavorito(int idUser, int idFavorito)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@idUser, @idArti)");
                datos.setParametro("@idUser", idUser);
                datos.setParametro("@idArti", idFavorito);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public bool esFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @idUser AND idArticulo = @idArti");
                datos.setParametro("@idUser", idUser);
                datos.setParametro("@idArti", idArticulo);
                datos.getConsulta();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0] > 0;
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void eliminarFavorito (int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM FAVORITOS WHERE IdUser = @idUser AND idArticulo = @idArti");
                datos.setParametro("@idUser", idUser);
                datos.setParametro("@idArti", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
