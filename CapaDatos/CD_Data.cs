using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Data
    
    {
        public static List<Data> ObtenerResultados(int idencuesta)
        {
            List<Data> Lista = new List<Data>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerTotal", oConexion);
                cmd.Parameters.AddWithValue("IdEncuesta", idencuesta);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Lista.Add(new Data()
                        {
                            Total = Convert.ToInt32(dr["Total"].ToString()),
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString())
                        });
                    }
                    dr.Close();

                    return Lista;

                }
                catch (Exception ex)
                {
                    Lista = null;
                    return Lista;
                }
            }
        }


        public static bool RegistrarDesplegarEncuesta(Data objeto)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarDesplegarEncuesta", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador", objeto.IdIndicador);
                    cmd.Parameters.AddWithValue("IdEncuesta", objeto.IdEncuesta);
                    cmd.Parameters.AddWithValue("IdPerfil", objeto.IdPerfil);
                    cmd.Parameters.AddWithValue("IdEje", objeto.IdEje);
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

        public static List<Data> ObtenerEncuestas(int id)
        {
            List<Data> Lista = new List<Data>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerDatosDesplegarEncuestaByEje", oConexion);
                cmd.Parameters.AddWithValue("IdEje", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Lista.Add(new Data()
                        {
                            IdEje= Convert.ToInt32(dr["IdEje"].ToString()),
                            IdData = Convert.ToInt32(dr["IdData"].ToString()),
                            DescripcionEncuesta = dr["DescripcionEncuesta"].ToString(),
                            DescripcionIndicador = dr["DescripcionIndicador"].ToString(),
                            Respuesta = Convert.ToInt32(dr["Respuesta"].ToString())
                        });
                    }
                    dr.Close();

                    return Lista;

                }
                catch (Exception ex)
                {
                    Lista = null;
                    return Lista;
                }
            }
        }

        public static bool ActualizarRespuesta(Data objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ActualizarRespuesta", oConexion);
                    cmd.Parameters.AddWithValue("IdData", objeto.IdData);
                    cmd.Parameters.AddWithValue("Respuesta", objeto.Respuesta);
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