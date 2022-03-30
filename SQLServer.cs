using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStateEmail
{
    internal class SQLServer
    {
        public List<Tienda> getTiendas()
        {
            List<Tienda> list = new List<Tienda>();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "10.128.10.24";
            builder.UserID = "SapPsi";
            builder.Password = "kTIX3wxO8?";
            builder.InitialCatalog = "Db_Util";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT Id_Store, Desc_Store, Ip_Sap " +
                    "FROM Ctg_Store WHERE Id_Company = 1 AND Id_Store NOT IN (244, 359, 360, 881, 3000)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Connected to server 10.128.10.24");
                        Log.writeLog("Connected to server 10.128.10.24");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Tienda()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1).Trim(),
                                    Ip = reader.GetString(2).Trim()
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error connection to server 24: {1}", ex.Message);
                        Log.writeLog($"Error connection to server 24: {ex.Message}");
                        return null;
                    }

                }
            }
            return list;
        }

        public string getServerState(int id, string server)
        {
            string tmp = "";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.UserID = "sa";
            builder.Password = "Sa@p0$d3$";
            builder.InitialCatalog = "backoff";
            builder.ConnectTimeout = 30;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string query = "SELECT businessdate, storeopen FROM Server_State";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Connected to server " + (id+1000));
                        Log.writeLog("Connected to server " + (id + 1000));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {                                
                                if (reader.GetString(1).Trim().Equals("Y"))
                                {
                                    tmp = $"Unidad {id + 1000} Abierta con businessdate {reader.GetValue(0)}";
                                    Log.writeLog($"Unidad {id + 1000} Abierta con businessdate {reader.GetValue(0)}");
                                } else
                                {
                                    Log.writeLog($"Unidad {id + 1000} Cerrada");
                                }
                            }
                        }
                    } catch (Exception ex)
                    {
                        tmp = "Error";
                        Console.WriteLine("Error connection to server {0}: {1}", (id + 1000), ex.Message);
                        Log.writeLog($"Error connection to server {id + 1000}: {ex.Message}");
                    }
                }
            }
            return tmp;
        }
    }
}
