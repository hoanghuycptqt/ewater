using EWATER.DAL;
using EWATER.Models;
using EWATER.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EWATER.Services
{
    public class ClientProductService
    {
        private GenericRepository<Product> CustRepository;

        public ClientProductService()
        {
            this.CustRepository = new GenericRepository<Product>(new EWaterContext());
        }

        public IEnumerable<Product> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Product");

            return CustRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
    }
}