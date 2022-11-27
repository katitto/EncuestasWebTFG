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
    public class CD_Unidad
    {
        public static List<Unidad> ObtenerUnidad()
        {
            List<Unidad> rptListaUnidad = new List<Unidad>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerUnidad", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUnidad.Add(new Unidad()
                        {
                            IdUnidad = Convert.ToInt32(dr["IdUnidad"].ToString()),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaUnidad;
                }catch(Exception ex)
                {
                    rptListaUnidad = null;
                    return rptListaUnidad;
                }
            }
        }

        public static bool RegistrarUnidad(Unidad uni)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarUnidad", oConexion);
                    cmd.Parameters.AddWithValue("IdUnidad", uni.IdUnidad);
                    cmd.Parameters.AddWithValue("Tipo", uni.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", uni.Descripcion);
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

        public static bool ModificarUnidad(Unidad uni)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarUnidad", oConexion);
                    cmd.Parameters.AddWithValue("IdUnidad", uni.IdUnidad);
                    cmd.Parameters.AddWithValue("Tipo", uni.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", uni.Descripcion);
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

        public static bool EliminarUnidad(Unidad uni)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarUnidad", oConexion);
                    cmd.Parameters.AddWithValue("IdUnidad", uni.IdUnidad);
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
