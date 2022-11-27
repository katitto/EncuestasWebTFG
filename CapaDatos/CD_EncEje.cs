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
    public class CD_EncEje
    {
        public static List<EncEje> ObtenerEncEje()
        {
            List<EncEje> rptListaEje = new List<EncEje>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerEncEje", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaEje.Add(new EncEje()
                        {
                            IdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString()),
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaEje;

                }catch (Exception ex)
                {
                    rptListaEje = null;
                    return rptListaEje;
                }
            }

        }
    }
}
