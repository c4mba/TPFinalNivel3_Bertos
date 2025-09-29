using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Configuration;

namespace negocio
{
    public class NegocioArticulo
    {
        public List<Articulo> listar(string id= "")
        {
            {
                List<Articulo> lista = new List<Articulo>();
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector;


                try
                {
                    conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true";
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdCategoria, A.IdMarca, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id = A.IdMarca ";
                    if (id != "")
                        comando.CommandText += " and A.Id = " + id;
                    comando.Connection = conexion;

                    conexion.Open();
                    lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Articulo aux = new Articulo();
                        aux.Id = (int)lector["Id"];
                        aux.Codigo = (string)lector["Codigo"];
                        aux.Nombre = (string)lector["Nombre"];
                        aux.Descripcion = (string)lector["Descripcion"];
                        if (!(lector.IsDBNull(lector.GetOrdinal("ImagenUrl"))))
                            aux.UrlImagen = (string)lector["ImagenUrl"];
                        aux.Precio = (decimal)lector["Precio"];
                        aux.categoria = new Categoria();
                        aux.categoria.Id = (int)lector["IdCategoria"];
                        aux.categoria.Descripcion = (string)lector["Categoria"];
                        aux.marca = new Marca();
                        aux.marca.Id = (int)lector["IdMarca"];
                        aux.marca.Descripcion = (string)lector["Marca"];

                        lista.Add(aux);
                    }
                    conexion.Close();
                    return lista;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public Articulo obtenerArticulo (int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Articulo arti = null;

            try
            {
                datos.setConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M Where A.Id = @id And C.Id = A.IdCategoria And M.Id = A.IdMarca ");
                datos.setParametro("@Id", id);
                datos.getConsulta();
                if (datos.Lector.Read())
                {
                    arti = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"],
                        UrlImagen = datos.Lector["ImagenUrl"].ToString(),
                        categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = datos.Lector["Categoria"].ToString()
                        },
                        marca = new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Descripcion = datos.Lector["Marca"].ToString()
                        }
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return arti;
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdCategoria, IdMarca, ImagenUrl)" + "values (@Codigo, @Nombre, @Descripcion, @Precio, @IdCategoria, @IdMarca, @ImagenUrl)");
                datos.setParametro("@Codigo", nuevo.Codigo);
                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Descripcion", nuevo.Descripcion);
                datos.setParametro("@Precio", nuevo.Precio);
                datos.setParametro("@IdCategoria", nuevo.categoria.Id);
                datos.setParametro("@IdMarca", nuevo.marca.Id);
                datos.setParametro("@ImagenUrl", nuevo.UrlImagen);
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
        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, ImagenUrl = @url, Precio = @precio, IdCategoria = @idCat, IdMarca = @idMar where Id = @id");
                datos.setParametro("@codigo", modificar.Codigo);
                datos.setParametro("@nombre", modificar.Nombre);
                datos.setParametro("@descripcion", modificar.Descripcion);
                datos.setParametro("@url", modificar.UrlImagen);
                datos.setParametro("@precio", modificar.Precio);
                datos.setParametro("@idCat", modificar.categoria.Id);
                datos.setParametro("@idMar", modificar.marca.Id);
                datos.setParametro("@id", modificar.Id);

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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setConsulta("delete from ARTICULOS where Id = @id");
                datos.setParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string traducirCampo(string campo)
        {
            return campo.Equals("Código", StringComparison.OrdinalIgnoreCase) ? "A.Codigo" :
                   campo.Equals("Nombre", StringComparison.OrdinalIgnoreCase) ? "A.Nombre" :
                   campo.Equals("Descripción", StringComparison.OrdinalIgnoreCase) ? "A.Descripcion" :
                   campo.Equals("Marca", StringComparison.OrdinalIgnoreCase) ? "M.Descripcion" :
                   campo.Equals("Categoria", StringComparison.OrdinalIgnoreCase) ? "C.Descripcion" :
                   throw new Exception("Campo invalido.");
        }

       

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            if (string.IsNullOrWhiteSpace(campo) || string.IsNullOrWhiteSpace(criterio) || string.IsNullOrWhiteSpace(filtro))
            {
                throw new Exception("Los valores no pueden estar vacíos.");
            }

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdCategoria, A.IdMarca, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M Where C.Id = A.IdCategoria And M.Id = A.IdMarca And ";

                if (campo.Equals("Precio", StringComparison.OrdinalIgnoreCase))
                {
                    if (!decimal.TryParse(filtro, out decimal filtroDecimal))
                        throw new Exception("El filtro de Precio deber ser un número.");

                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > @filtroPrecio";
                            break;
                        case "Menor a":
                            consulta += "Precio < @filtroPrecio";
                            break;
                        default:
                            consulta += "Precio = @filtroPrecio";
                            break;
                    }
                    datos.setConsulta(consulta);
                    datos.setParametro("@filtroPrecio", filtroDecimal);
                }
                else
                {
                    string campoElegido = traducirCampo(campo);
                    consulta += completarConsulta(campoElegido, criterio);

                    
                    string parametroFiltro = darFormatoFiltro(criterio, filtro);
                    datos.setConsulta(consulta);
                    datos.setParametro("@filtro", parametroFiltro);
                }

                datos.getConsulta();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.categoria = new Categoria();
                    aux.categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.marca = new Marca();
                    aux.marca.Id = (int)datos.Lector["IdMarca"];
                    aux.marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        

        private string completarConsulta(string campo, string criterio)
        {
            return $"{campo} LIKE @filtro";
        }

        private string darFormatoFiltro(string criterio, string filtro)
        {
            switch (criterio)
            {
                case "Comienza con":
                    return filtro + "%";
                case "Termina con":
                    return "%" + filtro;
                default:
                    return "%" + filtro + "%";
            }
        }

        public List<Articulo> filtrar2(string campo, string criterio, string filtro, string campo2, string criterio2, string filtro2)
        {
            if (string.IsNullOrWhiteSpace(campo) || string.IsNullOrWhiteSpace(campo2) ||
                string.IsNullOrWhiteSpace(criterio) || string.IsNullOrWhiteSpace(criterio2) ||
                string.IsNullOrWhiteSpace(filtro) || string.IsNullOrWhiteSpace(filtro2))
            {
                throw new Exception("Todos los campos deben estar completos.");
            }

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, " +
                                 "M.Descripcion Marca, A.IdCategoria, A.IdMarca, A.Id " +
                                 "FROM ARTICULOS A, CATEGORIAS C, MARCAS M " +
                                 "WHERE C.Id = A.IdCategoria AND M.Id = A.IdMarca AND ";

                
                if (campo.Equals("Precio", StringComparison.OrdinalIgnoreCase))
                {
                    if (!decimal.TryParse(filtro, out decimal filtroDecimal))
                        throw new Exception("El filtro de Precio deber ser un número.");

                    consulta += $"Precio {getOperadorPrecio(criterio)} @filtroPrecio";
                    datos.setParametro("@filtroPrecio", filtroDecimal);
                }
                else
                {
                    string campoTraducido = traducirCampo(campo);
                    consulta += $"{campoTraducido} LIKE @filtro1";
                    datos.setParametro("@filtro1", darFormatoFiltro(criterio, filtro));
                }

                
                consulta += " AND ";

                
                if (campo2.Equals("Precio", StringComparison.OrdinalIgnoreCase))
                {
                    if (!decimal.TryParse(filtro2, out decimal filtroDecimal2))
                        throw new Exception("El segundo filtro de Precio debe ser un número.");

                    consulta += $"Precio {getOperadorPrecio(criterio2)} @filtroPrecio2";
                    datos.setParametro("@filtroPrecio2", filtroDecimal2);
                }
                else
                {
                    string campoTraducido2 = traducirCampo(campo2);
                    consulta += $"{campoTraducido2} LIKE @filtro2";
                    datos.setParametro("@filtro2", darFormatoFiltro(criterio2, filtro2));
                }

                datos.setConsulta(consulta);
                datos.getConsulta();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        UrlImagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")) ? null : (string)datos.Lector["ImagenUrl"],
                        Precio = (decimal)datos.Lector["Precio"],
                        categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = (string)datos.Lector["Categoria"]
                        },
                        marca = new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Descripcion = (string)datos.Lector["Marca"]
                        }
                    };

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el segundo filtro: " + ex.Message);
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private string getOperadorPrecio(string criterio)
        {
            switch (criterio)
            {
                case "Mayor a": return ">";
                case "Menor a": return "<";
                default: return "=";
            }
        }

        
    }
 }
    

