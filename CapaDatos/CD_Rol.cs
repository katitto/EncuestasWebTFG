using CapaModelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Rol
    {

        public static List<Rol> ObtenerRoles()
        {
            List<Rol> rptListaRol = new List<Rol>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerRoles", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaRol.Add(new Rol()
                        {
                            IdRol = Convert.ToInt32(dr["IdRol"].ToString()),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                        });
                    }
                    dr.Close();

                    return rptListaRol;

                }
                catch (Exception ex)
                {
                    rptListaRol = null;
                    return rptListaRol;
                }
            }
        }
    }
}

