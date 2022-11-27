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
    public class CD_PerfilEje
    {
        public static List<PerfilEje> ObtenerPerfilEje()
        {
            List<PerfilEje> rptListaPerfilEje = new List<PerfilEje>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPerfilEje", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPerfilEje.Add(new PerfilEje()
                        {
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaPerfilEje;
                } catch(Exception ex)
                {
                    rptListaPerfilEje = null;
                    return rptListaPerfilEje;
                }
            }
        }
    }
}
