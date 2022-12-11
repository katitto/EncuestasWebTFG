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
        public static List<Encuesta> Obtener()
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
                            Activo = Convert.ToBoolean(dr["Activo"]),
                            Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"].ToString()),
                            Fecha_Final = Convert.ToDateTime(dr["Fecha_Final"].ToString()),
                            oUsuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                                Email = dr["Email"].ToString()
                            }
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

        public static bool Registrar(Encuesta objeto)
        {

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarEncuesta", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion);
                    cmd.Parameters.AddWithValue("Fecha_Inicio", objeto.Fecha_Inicio);
                    cmd.Parameters.AddWithValue("Fecha_Final", objeto.Fecha_Final);
                    cmd.Parameters.AddWithValue("IdUsuarioRegistro", objeto.oUsuario.IdUsuario);
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

        public static bool Modificar(Encuesta objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarEncuesta", oConexion);
                    cmd.Parameters.AddWithValue("IdEncuesta", objeto.IdEncuesta);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion);
                    cmd.Parameters.AddWithValue("Fecha_Inicio", objeto.Fecha_Inicio);
                    cmd.Parameters.AddWithValue("Fecha_Final", objeto.Fecha_Final);
                    cmd.Parameters.AddWithValue("Activo", objeto.Activo);
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

