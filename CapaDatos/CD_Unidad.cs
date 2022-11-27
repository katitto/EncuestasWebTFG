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
    class CD_Unidad
    {
        public static List<Unidad> ObtenerUnidad()
        {
            List<Unidad> rptListaUnidad = new List<Unidad>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerUnidad", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUnidad.Add(new Unidad()
                        {
                            IdUnidad = Convert.ToInt32(dr["IdUnidad"].ToString()),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaUnidad;
                }catch(Exception ex)
                {
                    rptListaUnidad = null;
                    return rptListaUnidad;
                }
            }
        }
    }
}
