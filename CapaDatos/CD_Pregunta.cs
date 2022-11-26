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
    class CD_Pregunta
    {
        public static List<Pregunta> ObtenerPregunta()
        {
            List<Pregunta> rptListaPregunta = new List<Pregunta>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPregunta", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPregunta.Add(new Pregunta()
                        {
                            IdPregunta = Convert.ToInt32(dr["IdPregunta"].ToString()),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaPregunta;
                }catch(Exception ex)
                {
                    rptListaPregunta = null;

                    return rptListaPregunta;
                }
            }
        }
    }
}
