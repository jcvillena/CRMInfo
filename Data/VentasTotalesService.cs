using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMInfo.Data
{
    public class VentasTotalesService
    {
        private JDEDataAccessLayer _dataLayer;
        public VentasTotalesService(JDEDataAccessLayer bugDataAccessLayer)
        {
            _dataLayer = bugDataAccessLayer;
        }

        public async Task<List<VentasTotales>> GetVentasTotales(double cliente)
        {
            List<VentasTotales> lista = await _dataLayer.GetVentasTotalesAnyo(cliente);


            return lista; // Task.FromResult(lista);
        }
        public async Task<List<VentasTotales>> GetVentasTotalesMes(double cliente)
        {
            List<VentasTotales> lista = await _dataLayer.GetVentasTotalesMes(cliente);


            return lista; // Task.FromResult(lista);
        }

    }
}
