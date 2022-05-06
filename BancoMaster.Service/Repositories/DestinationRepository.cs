using BancoMaster.Domain.Interfaces;
using BancoMaster.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BancoMaster.Service.Repositories
{
    public class DestinationRepository : IDestinations
    {
        public async Task<List<Destination>> ListingValues()
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"select * from tb_destination FOR JSON PATH";

                List<Destination> list = new List<Destination>();
                using (var cmd = new SqlCommand(commandStr, conn))
                {
                    conn.Open();
                    var jsonResult = new StringBuilder();
                    var reader = await cmd.ExecuteReaderAsync();
                    if (!reader.HasRows)
                    {
                        jsonResult.Append("[]");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            jsonResult.Append(reader.GetValue(0).ToString());
                        }
                    }

                    list = JsonConvert.DeserializeObject<List<Destination>>(jsonResult.ToString());
                    conn.Close();
                }
                return list;
            }
            catch (System.Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
