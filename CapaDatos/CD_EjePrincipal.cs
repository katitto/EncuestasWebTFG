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
    public class CD_EjePrincipal
    {
        public static List<EjePrincipal> ObtenerEjePrincipal()
        {
            List<EjePrincipal> rptListaEjePrincipal = new List<EjePrincipal>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerEjesRel", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaEjePrincipal.Add(new EjePrincipal()
                        {
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString()),
                            RefEje = dr["RefEje"].ToString(),
                            Nivel = Convert.ToInt32(dr["Nivel"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            IdEjePadre = Convert.ToInt32(dr["IdEjePadre"].ToString()),
                            oPerfil = new Perfil() { 
                                IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                                Descripcion = dr["Descripcion"].ToString()
                            },
                            oGeografia = new Geografia(){
                                IdGeografia = Convert.ToInt32(dr["IdGeografia"].ToString()),
                                Pais = dr["Pais"].ToString()
                            } 
                        });

                    }
                    dr.Close();
                    return rptListaEjePrincipal;
                }
                catch (Exception ex)
                {
                    rptListaEjePrincipal = null;
                    return rptListaEjePrincipal;
                }
            }
        }

        /*REGISTRAR EjePrincipal*/
        public static bool RegistrarEjePrincipal(EjePrincipal objeto)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarEjePrincipal", oConexion);
                    cmd.Parameters.AddWithValue("RefEje", objeto.RefEje);
                    cmd.Parameters.AddWithValue("Nivel", objeto.Nivel);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("IdEjePadre", objeto.IdEjePadre);
                    cmd.Parameters.AddWithValue("IdPerfil", objeto.oPerfil.IdPerfil);
                    cmd.Parameters.AddWithValue("IdGeografia", objeto.oGeografia.IdGeografia);
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

        public static bool ModificarEjePrincipal(EjePrincipal objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarEjePrincipal", oConexion);
                    cmd.Parameters.AddWithValue("IdEje", objeto.IdEje);
                    cmd.Parameters.AddWithValue("RefEje", objeto.RefEje);
                    cmd.Parameters.AddWithValue("Nivel", objeto.Nivel);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("IdEjePadre", objeto.IdEjePadre);
                    cmd.Parameters.AddWithValue("IdPerfil", objeto.oPerfil.IdPerfil);
                    cmd.Parameters.AddWithValue("IdGeografia", objeto.oGeografia.IdGeografia);
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

        public static bool EliminarEjePrincipal(int IdEje)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarEjePrincipal", oConexion);
                    cmd.Parameters.AddWithValue("IdEje", IdEje);
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
        /*al final este no se va a usar  porque son objetos en la definición de ejes*/
 

    }
}

