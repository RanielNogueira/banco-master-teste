using BancoMaster.Domain.Interfaces;
using BancoMaster.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BancoMaster.Service.Repositories
{
    public class RouteRepository : IRoutes
    {
        public async Task Add(Routes route)
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"insert into tb_route values({route.OriginId},{route.DestinyId},{route.Price})";
                using (var cmd = new SqlCommand(commandStr, conn))
                {
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
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
        public async Task Delete(int id)
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"delete from tb_route where id = {id}";
                using (var cmd = new SqlCommand(commandStr, conn))
                {
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
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
        public Routes Listing(int Id)
        {
            throw new System.NotImplementedException();
        }
        public async Task<List<RoutesViewModel>> ListingValues(string Origin, string Destiny)
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"select R.Id , O.name OriginName, O.abbreviated OriginAbbr, DT.name DestinyName, DT.abbreviated DestinyAbbr, R.Price,
                Connections = (select CC.id, DD.name, DD.abbreviated abbrName from tb_connections CC inner join TB_DESTINATION DD 
                on DD.id = CC.destination_id where CC.route_id = R.id FOR JSON PATH)
                from
                TB_ROUTE R
                inner join TB_DESTINATION O on O.id = R.origin_id
                inner join TB_DESTINATION DT on DT.id = R.destiny_id
                where O.abbreviated = '{Origin}' and DT.abbreviated = '{Destiny}'
                FOR JSON PATH";

                List<RoutesViewModel> list = new List<RoutesViewModel>();
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

                    list = JsonConvert.DeserializeObject<List<RoutesViewModel>>(jsonResult.ToString());
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

        public async Task<RoutesViewModel> BestPrice(string Origin, string Destiny)
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"select top 1 R.Id , O.name OriginName, O.abbreviated OriginAbbr, DT.name DestinyName, DT.abbreviated DestinyAbbr, R.Price,
                Connections = (select CC.id, DD.name, DD.abbreviated abbrName from tb_connections CC inner join TB_DESTINATION DD 
                on DD.id = CC.destination_id where CC.route_id = R.id FOR JSON PATH)
                from
                TB_ROUTE R
                inner join TB_DESTINATION O on O.id = R.origin_id
                inner join TB_DESTINATION DT on DT.id = R.destiny_id
                where O.abbreviated = '{Origin}' and DT.abbreviated = '{Destiny}' order by R.price asc
                FOR JSON PATH";

                RoutesViewModel result;
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

                    if(JsonConvert.DeserializeObject<List<RoutesViewModel>>(jsonResult.ToString()).Count > 0)
                        result = JsonConvert.DeserializeObject<List<RoutesViewModel>>(jsonResult.ToString())[0];
                    else
                        result = new RoutesViewModel();

                    conn.Close();
                }
                return result;
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

        public async Task Update(Routes route)
        {
            var conn = Utils.Connection.GetConnection();
            try
            {
                string commandStr = $@"update tb_route set origin_id = {route.OriginId}, destiny_id = {route.DestinyId}, price = {route.Price} where id = {route.Id}";
                using (var cmd = new SqlCommand(commandStr, conn))
                {
                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
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
