using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRMInfo.Data
{
    public class JDEDataAccessLayer
    {
        public IConfiguration Configuration;
        private const string JDE_DATABASE = "JDE";
          public JDEDataAccessLayer(IConfiguration configuration)
        {
            Configuration = configuration; //Inject configuration to access Connection string from appsettings.json.
        }

        public async Task<List<VentasTotales>> GetVentasTotalesAnyo(double cliente)
        {
            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(JDE_DATABASE)))
            {
                db.Open();

                string SQL = @"select cliente, anyo, sum(cant1 + cant2 + cant3 + cant4 + cant5 + cant6 + cant7 + cant8 + cant9 + cant10 + cant11 + cant12) as total from estadisticas_v where cliente=" + cliente.ToString() +  " group by anyo,cliente";
                IEnumerable<VentasTotales> result = await db.QueryAsync<VentasTotales>(SQL);
                return result.ToList();
            }
        }

        public async Task<List<VentasTotales>> GetVentasTotalesMes(double cliente)
        {
            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(JDE_DATABASE)))
            {
                db.Open();

                string SQL = @"select anyo,cliente, sum(cant1 + cant2 + cant3 + cant4 + cant5 + cant6 + cant7 + cant8 + cant9 + cant10 + cant11 + cant12) as total from estadisticas_v where cliente=" + cliente.ToString() + " group by anyo,cliente";
                IEnumerable<VentasTotales> result = await db.QueryAsync<VentasTotales>(SQL);
                return result.ToList();
            }
        }



    }
}
