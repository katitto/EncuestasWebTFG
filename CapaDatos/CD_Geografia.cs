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
                SqlCommand cmd = new SqlCommand("upn_ObtenerGeografia", oConexion);
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
                            CoordenadasY = Convert.ToDecimal(dr["CoordenadasY"].ToString())
                        }); ;
                    }
                    dr.Close();
                    return rptListaGeografia;
                } catch (Exception ex)
                {
                    rptListaGeografia = null;
                    return rptListaGeografia;
                }
            }

        }
    }
}
