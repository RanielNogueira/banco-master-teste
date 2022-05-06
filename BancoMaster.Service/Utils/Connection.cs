using System.Data.SqlClient;

namespace BancoMaster.Service.Utils
{
    public static class Connection
    {
        private static SqlConnection _connection;

        public static SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection("Server=tcp:bancomaster-teste.database.windows.net,1433;Initial Catalog=BancoMasterTeste;Persist Security Info=False;User ID=masterkey;Password=2Cbd8924@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }

            return _connection;
        }
    }
}
