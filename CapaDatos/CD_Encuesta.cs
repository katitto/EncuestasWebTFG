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
    public class CD_Encuesta
    {
        public static List<Encuesta> ObtenerEncuesta()
        {
            List<Encuesta> rptListaEncuesta = new List<Encuesta>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerEncuesta", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaEncuesta.Add(new Encuesta()
                        {
                            IdEncuesta = Convert.ToInt32(dr["IdEncuesta"].ToString()),
                            Descripcion = dr["Descripcion"].ToString(),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"].ToString()),
                            Fecha_Final = Convert.ToDateTime(dr["Fecha_Final"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaEncuesta;


                }catch (Exception ex)
                {
                    rptListaEncuesta = null;
                    return rptListaEncuesta;
                }
            }
        }
    }
}
