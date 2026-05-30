using ApiWebInscripcion.Models;
using ApiWebInscripcion.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiWebInscripcion.Data
{
    public class PartidoData
    {
        public static Respuesta<List<ResumenSerieDTO>> ResumenSeries(int IdTorneo, int IdCategoria)
        {
            // 1. Iniciamos la respuesta por defecto en "Error" por si algo falla
            Respuesta<List<ResumenSerieDTO>> rpt = new Respuesta<List<ResumenSerieDTO>>()
            {
                Estado = false,
                Data = new List<ResumenSerieDTO>(), // Lista vacía, no nula
                Mensaje = "Error desconocido"
            };

            try
            {
                // Usamos la cadena limpia del Web.config
                using (SqlConnection con = new SqlConnection(Conexion.RutaConexion))
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerResumenSeries", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@IdTorneo", IdTorneo);
                        comando.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rpt.Data.Add(new ResumenSerieDTO
                                {
                                    IdSerie = Convert.ToInt32(dr["IdSerie"]),
                                    NombreSerie = dr["NombreSerie"].ToString(),
                                    NroEquipos = Convert.ToInt32(dr["NroEquipos"]),
                                    NroPartidos = Convert.ToInt32(dr["NroPartidos"])
                                });
                            }
                        }
                    }
                }

                // Si todo salió bien, actualizamos la respuesta
                rpt.Estado = true;
                rpt.Mensaje = "Lista obtenida correctamente";
            }
            catch (Exception ex)
            {
                // Si hay error, el frontend sabrá exactamente qué pasó
                rpt.Estado = false;
                rpt.Mensaje = $"Error en BD: {ex.Message}";
            }

            return rpt;
        }

    }
}