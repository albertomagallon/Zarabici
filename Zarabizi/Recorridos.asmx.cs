using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace Zarabizi
{
    /// <summary>
    /// Descripción breve de Recorridos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Recorridos : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public string Start(int userid, int stationid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString())){
            try{
	            con.Open();
	            SqlCommand command = new SqlCommand();
                command.CommandText = "cp_iniciarRecorrido";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = con;
                command.Parameters.Add("usuario",userid);
                command.Parameters.Add("estacion", stationid);

            return((string)command.ExecuteScalar());

            }catch{
	            throw;  
            }finally{
                if (con.State != System.Data.ConnectionState.Closed)
            
            con.Close();
            con.Dispose();
            }   
               
            }
        }

        public string Stop(int idBici, int anclaje)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "cp_finalizarRecorrido";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = con;
                    command.Parameters.Add("idBici", idBici);
                    command.Parameters.Add("anclaje", anclaje);

                    return ((string)command.ExecuteScalar());

                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)

                        con.Close();
                    con.Dispose();
                }

            }
        }
    }
}
