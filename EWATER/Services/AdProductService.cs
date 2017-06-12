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
    public class AdProductService
    {
        private GenericRepository<Product> CustRepository;

        public AdProductService()
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

        public Product GetbyID(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Product ");
            spQuery.Append("WHERE ");
            spQuery.Append("Product.ProductID = {0}");

            return CustRepository.ExecuteQuerySingle(spQuery.ToString(), parameters);
        }

        public int Insert(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("INSERT INTO Product ( ");
            spQuery.Append("ProductName ");
            spQuery.Append(",Price ");
            spQuery.Append(",Image) ");
            spQuery.Append("VALUES( ");
            spQuery.Append("{0} ");
            spQuery.Append(",{1} ");
            spQuery.Append(",{2})");

            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }

        public int Update(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("UPDATE Product SET ");
            spQuery.Append("ProductName={1} ");
            spQuery.Append(",Price={2} ");
            if(parameters.Count() == 4)
            {
                spQuery.Append(",Image={3} ");
            }          
            spQuery.Append("WHERE ");
            spQuery.Append("ProductID={0}");

            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }

        public int Delete(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("DELETE FROM Product ");
            spQuery.Append("WHERE ");
            spQuery.Append("ProductID={0} ");

            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
    }
}