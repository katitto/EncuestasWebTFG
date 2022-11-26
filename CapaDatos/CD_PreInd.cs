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
    class CD_PreInd
    {
        public static List<PreInd> ObtenerPreInd()
        {
            List<PreInd> rptListaPreInd = new List<PreInd>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPreInd", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPreInd.Add(new PreInd()
                        {
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString()),
                            IdPregunta = Convert.ToInt32(dr["IdPregunta"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaPreInd;
                }catch(Exception ex)
                {
                    rptListaPreInd = null;
                    return rptListaPreInd;
                }
            }
        }
    }
}
