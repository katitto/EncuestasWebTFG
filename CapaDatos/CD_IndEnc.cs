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
   public class CD_IndEnc
    {
        public static List<IndEnc> ObtenerIndEnc()
        {
            List<IndEnc> rptListaIndec = new List<IndEnc>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerIndEnc", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaIndec.Add(new IndEnc()
                        {
                            IdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString()),
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                            IdTipo = Convert.ToInt32(dr["IdTipo"].ToString()),
                            IdUnidad = Convert.ToInt32(dr["IdUnidad"].ToString()),
                            RefIndicador = dr["RefIndicador"].ToString(),
                            Tipo = dr["Tipo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            RefPerfil = dr["RefPerfil"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaIndec;
                } catch(Exception ex)
                {
                    rptListaIndec = null;
                    return rptListaIndec;
                }
            }
        }


        public static bool RegistrarIndEnc(IndEnc objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarIndEnc", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador", objeto.IdIndicador);
                    cmd.Parameters.AddWithValue("IdEncuesta", objeto.IdEncuesta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
        public static bool EliminarIndEnc(int IdIndicador, int IdEncuesta)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarIndEnc", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador", IdIndicador);
                    cmd.Parameters.AddWithValue("IdEncuesta", IdEncuesta);
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



        public static List<IndEnc> ObtenerDatosDesplegarEncuesta(int idencuesta)
        {
            List<IndEnc> rptListaEje = new List<IndEnc>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerDatosDesplegarEncuesta", oConexion);
                cmd.Parameters.AddWithValue("IdEncuesta", idencuesta);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaEje.Add(new IndEnc()
                        {
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString()),
                            IdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString()),                           
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),                           
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaEje;

                }
                catch (Exception ex)
                {
                    rptListaEje = null;
                    return rptListaEje;
                }
            }

        }

    }
}

