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
    public class CD_Geografia
    {
        public static List<Geografia> ObtenerGeografia()
        {
            List<Geografia> rptListaGeografia = new List<Geografia>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerGeografia", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaGeografia.Add(new Geografia()
                        {
                            IdGeografia = Convert.ToInt32(dr["IdGeografia"].ToString()),
                            Pais = dr["Pais"].ToString(),
                            CoordenadasX = Convert.ToDecimal(dr["CoordenadasX"].ToString()),
                            CoordenadasY = Convert.ToDecimal(dr["CoordenadasY"].ToString()),
                            Padre = Convert.ToInt32(dr["Padre"].ToString())

                        });
                        
                    }
                    
                    dr.Close();
                    return rptListaGeografia;
                }
                catch (Exception ex)
                {
                   if(rptListaGeografia == null)
                    { 
                    for (int i = 0; i < rptListaGeografia.Count; i++)
                    {
                        
                    }
                    }
                    return rptListaGeografia;
                }
            }

        }
        /*REGISTRAR GEOGRAFIA*/
        public static bool RegistrarGeografia(Geografia oGeografia)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarGeografia", oConexion);
                    cmd.Parameters.AddWithValue("Pais", oGeografia.Pais);
                    cmd.Parameters.AddWithValue("CoordenadasX", oGeografia.CoordenadasX);
                    cmd.Parameters.AddWithValue("CoordenadasY", oGeografia.CoordenadasY);
                    if (oGeografia.Padre !=0)
                    {
                        cmd.Parameters.AddWithValue("Padre", oGeografia.Padre);
                    }                    
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

        public static bool ModificarGeografia(Geografia oGeografia)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarGeografia", oConexion);
                    cmd.Parameters.AddWithValue("IdGeografia", oGeografia.IdGeografia);
                    cmd.Parameters.AddWithValue("Pais", oGeografia.Pais);
                    cmd.Parameters.AddWithValue("CoordenadasX", oGeografia.CoordenadasX);
                    cmd.Parameters.AddWithValue("CoordenadasY", oGeografia.CoordenadasY);
                    if (oGeografia.Padre != 0) { 
                        cmd.Parameters.AddWithValue("Padre", oGeografia.Padre);
                    }
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

        public static bool EliminarGeografia(int IdGeografia)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarGeografia", oConexion);
                    cmd.Parameters.AddWithValue("IdGeografia", IdGeografia);
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
        public static List<Geografia> ObtenerPadresGeografia()
        {
            List<Geografia> rptListaGeografia = new List<Geografia>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ObtenerPadresGeografia", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaGeografia.Add(new Geografia()
                        {
                            IdGeografia = Convert.ToInt32(dr["IdGeografia"].ToString()),
                            Pais = dr["Pais"].ToString(),
                            CoordenadasX = Convert.ToDecimal(dr["CoordenadasX"].ToString()),
                            CoordenadasY = Convert.ToDecimal(dr["CoordenadasY"].ToString()),
                            Padre = Convert.ToInt32(dr["Padre"].ToString())
                        }); 
                    }
                    dr.Close();
                    return rptListaGeografia;
                }
                catch (Exception ex)
                {
                    rptListaGeografia = null;
                    return rptListaGeografia;
                }
            }
        }
        public static List<Geografia> ObtenerBusquedaGeografia(string Pais)
        {
            List<Geografia> rptListaGeografia = new List<Geografia>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    
                    SqlCommand cmd = new SqlCommand("usp_ObtenerBusquedaGeografia", oConexion);
                    cmd.Parameters.AddWithValue("Pais", Pais);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        rptListaGeografia.Add(new Geografia()
                        {
                            Pais = dr["Pais"].ToString()

                        }); 
                    }
                    dr.Close();
                    return rptListaGeografia;
                }
                catch (Exception ex)
                {
                    rptListaGeografia = null;
                    return rptListaGeografia;
                }
            }
        }

    }
}
