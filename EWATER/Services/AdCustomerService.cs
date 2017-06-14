using EWATER.DAL;
using EWATER.Entity;
using EWATER.Models;
using EWATER.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EWATER.Services
{
    public partial class AdCustomerService
    {
        private GenericRepository<Customer> CustRepository;
        private GenericRepository<CustomerEntity> CustEntityRepository;
        public AdCustomerService()
        {
            this.CustRepository = new GenericRepository<Customer>(new EWaterContext());
            this.CustEntityRepository = new GenericRepository<CustomerEntity>(new EWaterContext());
        }
        public IEnumerable<CustomerEntity> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT d.CustomerID,d.CustomerName,d.CustomerEmail,d.PhoneNumber,d.Address,d.Ward,isnull(c.TotalSales, 0) as TotalSales ");
            spQuery.Append("FROM( ");
            spQuery.Append("SELECT a.CustomerID, SUM(b.Price*b.Quantity) AS TotalSales ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN OrderItem b ");
            spQuery.Append("on a.OrderID = b.OrderID ");
            spQuery.Append("GROUP BY a.CustomerID) c ");
            spQuery.Append("RIGHT JOIN Customer d ");
            spQuery.Append("on c.CustomerID = d.CustomerID ");

            return CustEntityRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
        public Customer GetbyID(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Customer ");
            spQuery.Append("WHERE ");
            spQuery.Append("Customer.CustomerID = {0}");
            
            return CustRepository.ExecuteQuerySingle(spQuery.ToString(), parameters);
        }
        public int Insert(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("INSERT INTO Customer ( ");
            spQuery.Append("CustomerName ");
            spQuery.Append(",CustomerEmail ");
            spQuery.Append(",PhoneNumber ");
            spQuery.Append(",Address ");
            spQuery.Append(",Ward) ");
            spQuery.Append("VALUES( ");
            spQuery.Append("{0} ");
            spQuery.Append(",{1} ");
            spQuery.Append(",{2} ");
            spQuery.Append(",{3} ");
            spQuery.Append(",{4})");       

            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
        public int Update(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("UPDATE Customer SET ");
            spQuery.Append("CustomerName={1} ");
            spQuery.Append(",CustomerEmail={2} ");
            spQuery.Append(",PhoneNumber={3} ");
            spQuery.Append(",Address={4} ");
            spQuery.Append(",Ward={5} ");
            spQuery.Append("WHERE ");
            spQuery.Append("CustomerID={0}");
            
            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
        public int Delete(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("DELETE FROM Customer ");
            spQuery.Append("WHERE ");
            spQuery.Append("CustomerID={0} ");
                    
            return CustRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }

        public Customer GetByPhone(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Customer ");
            spQuery.Append("WHERE ");
            spQuery.Append("Customer.PhoneNumber = {0}");

            return CustRepository.ExecuteQuerySingle(spQuery.ToString(), parameters);
        }
    }
}