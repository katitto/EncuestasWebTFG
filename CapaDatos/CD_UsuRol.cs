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
    class CD_UsuRol
    {
        public static List<UsuRol> ObtenerUsuRol()
        {
            List<UsuRol> rptListaUsuRol = new List<UsuRol>();
            using(SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerUsuRol", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUsuRol.Add(new UsuRol()
                        {
                            IdEje = Convert.ToInt32(dr["IdEje"].ToString()),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString())
                        });
                    }
                    dr.Close();
                    return rptListaUsuRol;
                }catch(Exception ex)
                {
                    rptListaUsuRol = null;
                    return rptListaUsuRol;
                }
            }
        }
    }
}
