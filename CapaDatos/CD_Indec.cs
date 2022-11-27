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
    class CD_Indec
    {
        public static List<Indec> ObtenerIndec()
        {
            List<Indec> rptListaIndec = new List<Indec>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerIndec", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaIndec.Add(new Indec()
                        {
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString()),
                            IdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaIndec;
                } catch(Exception ex)
                {
                    rptListaIndec = null;
                    return rptListaIndec;
                }
            }
        }
    }
}
