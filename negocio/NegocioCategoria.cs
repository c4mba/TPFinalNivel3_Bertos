using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class NegocioCategoria
    {
        public List<Categoria> listar()
        {
			List<Categoria> lista = new List<Categoria> ();
			AccesoDatos datos = new AccesoDatos ();
			try
			{
				datos.setConsulta("select Id, Descripcion from CATEGORIAS");
				datos.getConsulta();

				while (datos.Lector.Read())
				{
					Categoria aux = new Categoria();
					aux.Id = (int)datos.Lector["Id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];

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
				datos.CerrarConexion();
			}
        }
    }
}
