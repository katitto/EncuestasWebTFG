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
                SqlCommand cmd = new SqlCommand("usp_ObtenerIndicadorRel", oConexion);
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
                            oUnidad = new Unidad()
                            {
                                IdUnidad = Convert.ToInt32(dr["IdUnidad"].ToString()),
                                Tipo = dr["Tipo"].ToString()
                            },
                            oTipo = new Tipo() {
                                IdTipo = Convert.ToInt32(dr["IdTipo"].ToString()),
                                Nombre = dr["Nombre"].ToString()
                            },
                            oPerfil = new Perfil()
                            {
                                IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString()),
                                RefPerfil = dr["RefPerfil"].ToString()
                            },

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
        public static bool RegistrarIndicador(Indicador objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarIndicador", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador ", objeto.IdIndicador);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion);
                    cmd.Parameters.AddWithValue("Unidad", objeto.oUnidad.IdUnidad);
                    cmd.Parameters.AddWithValue("Tipo", objeto.oTipo.IdTipo);
                    cmd.Parameters.AddWithValue("Perfil", objeto.oPerfil.IdPerfil);
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

        public static bool ModificarIndicador(Indicador objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarIndicador", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador ", objeto.IdIndicador);
                    cmd.Parameters.AddWithValue("Descripcion", objeto.Descripcion);
                    cmd.Parameters.AddWithValue("Unidad", objeto.oUnidad.IdUnidad);
                    cmd.Parameters.AddWithValue("Tipo", objeto.oTipo.IdTipo);
                    cmd.Parameters.AddWithValue("Perfil", objeto.oPerfil.IdPerfil);
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

        public static bool EliminarIndicador(int IdIndicador)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarIndicador", oConexion);
                    cmd.Parameters.AddWithValue("IdIndicador", IdIndicador);
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

