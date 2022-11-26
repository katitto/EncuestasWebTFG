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
    class CD_PerInd
    {
        public static List<PerInd> ObtenerPerInd()
        {
            List<PerInd> rptListaPerInd = new List<PerInd>();
            using (SqlConnection oConexion =  new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPerInd", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPerInd.Add(new PerInd()
                        {
                            IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaPerInd;
                }catch (Exception ex)
                {
                    rptListaPerInd = null;
                    return rptListaPerInd;
                }
            }
        }
    }
}
