using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class NegocioUser
    {
        public void actualizar(User usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
			{
				
				datos.setConsulta("Update USERS set urlImagenPerfil = @imagen, Nombre = @nombre, Apellido = @apellido Where Id = @id");
				//datos.setParametro("@imagen", usuario.UrilImagenPerfil != null ? usuario.UrilImagenPerfil : (object)DBNull.Value);
				datos.setParametro("@imagen", (object)usuario.UrilImagenPerfil ?? DBNull.Value);
				datos.setParametro("@nombre", usuario.Nombre);
				datos.setParametro("@apellido", usuario.Apellido);
				datos.setParametro("@id", usuario.Id);
				datos.ejecutarAccion();
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				datos.CerrarConexion();
			}
        }

        public int insertarNuevo (User nuevo)
        {
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setConsulta("INSERT INTO USERS (email, pass) OUTPUT INSERTED.Id VALUES (@email, @pass)");
				datos.setParametro("@email", nuevo.Email);
                datos.setParametro("@pass", nuevo.Pass);
				return datos.ejecutarAccionScalar();


                
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

		public bool Login(User usuario)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setConsulta("Select id, email, pass, admin, urlImagenPerfil, nombre, apellido from USERS Where email = @email And pass = @pass");
				datos.setParametro("@email", usuario.Email);
				datos.setParametro("@pass", usuario.Pass);
				datos.getConsulta();
				if (datos.Lector.Read())
				{
					usuario.Id = (int)datos.Lector["id"];
					usuario.esAdmin = (bool)datos.Lector["admin"];
					if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrilImagenPerfil = (string)datos.Lector["urlImagenPerfil"];
					if (!(datos.Lector["nombre"] is DBNull))
						usuario.Nombre = (string)datos.Lector["nombre"];
					if (!(datos.Lector["apellido"] is DBNull))
						usuario.Apellido = (string)datos.Lector["apellido"];


                    return true;
				}
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

		public bool existeCorreo(string correo)
		{
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setConsulta("SELECT COUNT(*) FROM USERS WHERE email = @email");
				datos.setParametro("@email", correo);
				datos.getConsulta();

				if (datos.Lector.Read())
				{
					int cont = (int)datos.Lector[0];
					return cont > 0;
				}
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

    }
}
