using EWATER.DAL;
using EWATER.Entity;
using EWATER.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EWATER.Services
{
    public partial class AdReportService
    {
        private GenericRepository<ReportEntity> ReportRepository;
        public AdReportService()
        {
            this.ReportRepository = new GenericRepository<ReportEntity>(new EWaterContext());
        }
        public IEnumerable<ReportEntity> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT a.OrderID,c.CustomerName,c.PhoneNumber,c.Address,d.ProductName,b.Quantity,b.Price,e.StaffName,a.OrderDate ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN OrderItem b ");
            spQuery.Append("on a.OrderID=b.OrderID ");
            spQuery.Append("INNER JOIN Customer c ");
            spQuery.Append("on a.CustomerID = c.CustomerID ");
            spQuery.Append("INNER JOIN Product d ");
            spQuery.Append("on b.ProductID = d.ProductID ");
            spQuery.Append("LEFT JOIN Staff e ");
            spQuery.Append("on a.StaffID = e.StaffID ");
            spQuery.Append("ORDER BY a.OrderDate DESC ");

            return ReportRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
    }
}