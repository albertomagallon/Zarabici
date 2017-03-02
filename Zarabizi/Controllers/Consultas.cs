using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zarabizi.Models.Metadata;
using System.Data.SqlClient;
using System.Data;

namespace Zarabizi.Controllers
{
    public class Consultas
    {
        private string connectionString;

        public Consultas(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal IList<Recorrido> RecorridosHabituales()
        {
            using (SqlConnection sqlconnection =
                 new SqlConnection(connectionString))
            {
                try
                {
                    sqlconnection.Open();

                    String consulta = "SELECT  TOP(10) idEstacionInicio, idEstacionFinal,  COUNT(CAST(idEstacionInicio AS CHAR)+ CAST(idEstacionFinal AS CHAR)) as Recorridos FROM Recorrido GROUP BY idEstacionInicio, idEstacionFinal ORDER BY Recorridos desc";

                    //Leer datos
                    SqlCommand sqlCommand = new SqlCommand(consulta, sqlconnection);
                    //Añadimos el parámetro a la consulta con la gestión de parámetros del comando para evitar SQLInyection
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    IList<Recorrido> result = new List<Recorrido>();
                    while (reader.Read())
                    {
                        ///Crea un objeto ingreso y le envia a su constructor los datos del reader con
                        ///sus respectivos cast
                        Recorrido recorrido = new Recorrido((int)reader["idRecorrido"], (Double)reader["distanciaRecorrido"],
                            (DateTime)reader["fechaSalidaRecorrido"], (DateTime)reader["fechaLlegadaRecorrido"], (int)reader["idEstacionInicio"], 
                            (int)reader["idEstacionFinal"], (int)reader["idBicicleta"], (int)reader["idSocio"]);
                        result.Add(recorrido);
                    }


                    return (result);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (sqlconnection.State != ConnectionState.Closed)
                        sqlconnection.Close();
                }
            }
            ///El using con las llaves es cómo hacer un sqlconnection.dispose()
        }
    }
}