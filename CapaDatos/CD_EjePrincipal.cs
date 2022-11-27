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
    class CD_EjePrincipal
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
                            IdEjePadre = Convert.ToInt32(dr["IdEjePadre"].ToString()),
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
    }
}
