using System;
using CapaModelo;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public static List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> rptListaUsuario = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerUsuarios", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUsuario.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            User = dr["User"].ToString(),
                            Contrasena = dr["Contrasena"].ToString(),
                            Email = dr["Email"].ToString(),
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString()),
                            IdRol = Convert.ToInt32(dr["IdRol"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaUsuario;

                }
                catch (Exception ex)
                {
                    rptListaUsuario = null;
                    return rptListaUsuario;
                }
            }
        }

        public static bool RegistrarUsuario(Usuario usu)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", usu.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", usu.Apellido);
                    cmd.Parameters.AddWithValue("User", usu.User);
                    cmd.Parameters.AddWithValue("Contrasena", usu.Contrasena);
                    cmd.Parameters.AddWithValue("IdEje", usu.IdEje);
                    cmd.Parameters.AddWithValue("IdRol", usu.IdRol);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public static bool ModificarUsuario(Usuario usu)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", usu.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", usu.Apellido);
                    cmd.Parameters.AddWithValue("User", usu.User);
                    cmd.Parameters.AddWithValue("Contrasena", usu.Contrasena);
                    cmd.Parameters.AddWithValue("IdEje", usu.IdEje);
                    cmd.Parameters.AddWithValue("IdRol", usu.IdRol);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public static bool EliminarUsuario(Usuario usu)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", usu.IdUsuario);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;
        }
    }


}
