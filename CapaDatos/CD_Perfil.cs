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
    public class CD_Perfil
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
                            Descripcion = dr["Descripcion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"]),
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
        /*REGISTRAR ROL*/
        public static bool RegistrarPerfil(Perfil oPerfil)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarPerfil", oConexion);
                    cmd.Parameters.AddWithValue("RefPerfil", oPerfil.RefPerfil);
                    cmd.Parameters.AddWithValue("Descripcion", oPerfil.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public static bool ModificarPerfil(Perfil oPerfil)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarPerfil", oConexion);
                    cmd.Parameters.AddWithValue("IdPerfil", oPerfil.IdPerfil);
                    cmd.Parameters.AddWithValue("RefPerfil", oPerfil.RefPerfil);
                    cmd.Parameters.AddWithValue("Descripcion", oPerfil.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", oPerfil.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;

        }

        public static bool EliminarPerfil(int IdPerfil)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarPerfil", oConexion);
                    cmd.Parameters.AddWithValue("IdPerfil", IdPerfil);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }



    }
}
