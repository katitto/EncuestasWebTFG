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
    class CD_Perfil
    {
        public static List<Perfil> ObtenerPerfil()
        {
            List<Perfil> rptListaPerfil = new List<Perfil>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPerfil", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPerfil.Add(new Perfil()
                        {
                            IdPerfil =  Convert.ToInt32(dr["IdPerfil"].ToString()),
                            RefPerfil = dr["RefPerfil"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                    dr.Close();
                    return rptListaPerfil;
                }catch (Exception ex)
                {
                    rptListaPerfil = null;
                    return rptListaPerfil;
                }
            }
        }
    }
}
