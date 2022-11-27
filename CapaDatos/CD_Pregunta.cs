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
    public class CD_Pregunta
    {
        public static List<Pregunta> ObtenerPregunta()
        {
            List<Pregunta> rptListaPregunta = new List<Pregunta>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPregunta", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPregunta.Add(new Pregunta()
                        {
                            IdPregunta = Convert.ToInt32(dr["IdPregunta"].ToString()),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaPregunta;
                }catch(Exception ex)
                {
                    rptListaPregunta = null;

                    return rptListaPregunta;
                }
            }
        }

        public static bool RegistrarPregunta(Pregunta pre)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarPregunta", oConexion);
                    cmd.Parameters.AddWithValue("IdPregunta", pre.IdPregunta);
                    cmd.Parameters.AddWithValue("Tipo", pre.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", pre.Descripcion);
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

        public static bool ModificarPregunta(Pregunta pre)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarPregunta", oConexion);
                    cmd.Parameters.AddWithValue("IdPregunta", pre.IdPregunta);
                    cmd.Parameters.AddWithValue("Tipo", pre.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", pre.Descripcion);
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

        public static bool EliminarPregunta(Pregunta pre)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarPregunta", oConexion);
                    cmd.Parameters.AddWithValue("IdPregunta", pre.IdPregunta);
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
