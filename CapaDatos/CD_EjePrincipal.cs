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
                SqlCommand cmd = new SqlCommand("usp_ObtenerEjePrincipal", oConexion);
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
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                            IdGeografia = Convert.ToInt32(dr["IdGeografia"].ToString())
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
        public static bool RegistrarEjePrincipal(EjePrincipal oEjePrincipal)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarEjePrincipal", oConexion);
                    cmd.Parameters.AddWithValue("RefEje", oEjePrincipal.RefEje);
                    cmd.Parameters.AddWithValue("Nivel", oEjePrincipal.Nivel);
                    cmd.Parameters.AddWithValue("Nombre", oEjePrincipal.Nombre);
                    cmd.Parameters.AddWithValue("IdEjePadre", oEjePrincipal.Nombre);
                    cmd.Parameters.AddWithValue("IdPerfil", oEjePrincipal.Nombre);
                    cmd.Parameters.AddWithValue("IdGeografia", oEjePrincipal.Nombre);
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

        public static bool ModificarEjePrincipal(EjePrincipal oEjePrincipal)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarEjePrincipal", oConexion);
                    cmd.Parameters.AddWithValue("IdEje", oEjePrincipal.IdEje);
                    cmd.Parameters.AddWithValue("RefEje", oEjePrincipal.RefEje);
                    cmd.Parameters.AddWithValue("Nivel", oEjePrincipal.Nivel);
                    cmd.Parameters.AddWithValue("Nombre", oEjePrincipal.Nombre);
                    cmd.Parameters.AddWithValue("Nombre", oEjePrincipal.IdEjePadre);
                    cmd.Parameters.AddWithValue("Nombre", oEjePrincipal.IdPerfil);
                    cmd.Parameters.AddWithValue("Nombre", oEjePrincipal.IdGeografia);
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
        public static List<EjePrincipal> ObtenerHijosEjePrincipal(int IdEje)
        {
            List<EjePrincipal> rptListaEjePrincipal = new List<EjePrincipal>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerHijosEjePrincipal", oConexion);
                cmd.Parameters.AddWithValue("IdEje", IdEje);
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
                            Nivel = Convert.ToInt32(dr["IdEje"].ToString()),
                            Nombre = dr["RefEje"].ToString(),
                            IdEjePadre = Convert.ToInt32(dr["IdEje"].ToString()),
                            IdPerfil = Convert.ToInt32(dr["IdEje"].ToString()),
                            IdGeografia = Convert.ToInt32(dr["IdEje"].ToString())
                        }); ;
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
    }
}

