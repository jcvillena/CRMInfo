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
        private const string ESPECIALES_DATABASE = "ESPECIALES";
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

        public async Task<List<Especial>> GetEspeciales(double cliente)
        {
            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString(ESPECIALES_DATABASE)))
            {
                db.Open();

                string SQL = "select SequentialNumber as Oferta, CreatedOn As Fecha, FechaEnvio, UserName As CreadoPor, SuPedido,Estado as Codigo, " + 
                            "CHOOSE(Estado+1,'Inicial','Pendiente Presupuestar','Presupuestado','No viable','Comunicado no viable','Faltan datos','Cancelada','Enviado cliente', 'Rechazada','Caducada','Aceptada cliente', 'Informatizar','Pendiente ap.planos','Finalizada') as Estado," + 
                             "Total " + 
                             "from Oferta " +
                             "LEFT JOIN PermissionPolicyUser on PermissionPolicyUser.Oid=CreatedBy " +
                             "where Oferta.gcrecord is null and cliente=" + cliente.ToString() +  " " +
                             "order by Oferta.SequentialNumber Desc";
                IEnumerable<Especial> result = await db.QueryAsync<Especial>(SQL);
                return result.ToList();
            }
        }




    }
}
