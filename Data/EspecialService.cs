using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMInfo.Data
{
    public class EspecialService
    {
        private JDEDataAccessLayer _dataLayer;
        public EspecialService(JDEDataAccessLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<List<Especial>> GetEspeciales(double cliente)
        {
            List<Especial> lista = await _dataLayer.GetEspeciales(cliente);


            return lista; // Task.FromResult(lista);
        }
    }
}
