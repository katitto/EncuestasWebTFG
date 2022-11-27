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
    public class CD_GeoEje
    {
        public static List<GeoEje> ObtenerGeoEje()
        {
            List<GeoEje> rptListaGeoEje = new List<GeoEje>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerGeoEje", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaGeoEje.Add(new GeoEje()
                        {
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString()),
                            IdGeografia = Convert.ToInt32(dr["IdGeografia"].ToString()),
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaGeoEje;
                }catch (Exception ex)
                {
                    rptListaGeoEje = null;
                    return rptListaGeoEje;
                }
            }
        }
    }
}
