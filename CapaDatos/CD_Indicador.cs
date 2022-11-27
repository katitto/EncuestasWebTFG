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
    public class CD_Indicador
    {
        public static List<Indicador> ObtenerIndicador()
        {
            List<Indicador> rptListaIndicador = new List<Indicador>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerIndicador", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaIndicador.Add(new Indicador()
                        {
                            IdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString()),
                            Descripcion = dr["IdEncuesta"].ToString(),
                            Unidad = Convert.ToInt32(dr["Unidad"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaIndicador;
                } catch(Exception ex)
                {
                    rptListaIndicador = null;
                    return rptListaIndicador;
                }
            }
        }
    }
}
