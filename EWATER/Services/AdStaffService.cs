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
    public partial class AdStaffService
    {
        private GenericRepository<Staff> StaffRepository;
        private GenericRepository<StaffEntity> StaffEntityRepository;
        public AdStaffService()
        {
            this.StaffRepository = new GenericRepository<Staff>(new EWaterContext());
            this.StaffEntityRepository = new GenericRepository<StaffEntity>(new EWaterContext());
        }
        public IEnumerable<StaffEntity> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT d.StaffID,d.StaffName,d.StaffEmail,d.StaffPhone,d.JobTitle,isnull(c.TotalSales, 0) as TotalSales ");
            spQuery.Append("FROM( ");
            spQuery.Append("SELECT a.StaffID, SUM(b.Price*b.Quantity) AS TotalSales ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN OrderItem b ");
            spQuery.Append("on a.OrderID = b.OrderID ");
            spQuery.Append("WHERE a.StaffID IS NOT NULL ");
            spQuery.Append("GROUP BY a.StaffID) c ");
            spQuery.Append("RIGHT JOIN Staff d ");
            spQuery.Append("on c.StaffID = d.StaffID ");

            return StaffEntityRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
        public Staff GetbyID(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Staff ");
            spQuery.Append("WHERE ");
            spQuery.Append("Staff.StaffID = {0}");

            return StaffRepository.ExecuteQuerySingle(spQuery.ToString(), parameters);
        }
        public int Insert(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("INSERT INTO Staff ( ");
            spQuery.Append("StaffName ");
            spQuery.Append(",StaffEmail ");
            spQuery.Append(",StaffPhone ");
            spQuery.Append(",JobTitle) ");
            spQuery.Append("VALUES( ");
            spQuery.Append("{0} ");
            spQuery.Append(",{1} ");
            spQuery.Append(",{2} ");
            spQuery.Append(",{3})");

            return StaffRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
        public int Update(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("UPDATE Staff SET ");
            spQuery.Append("StaffName={1} ");
            spQuery.Append(",StaffEmail={2} ");
            spQuery.Append(",StaffPhone={3} ");
            spQuery.Append(",JobTitle={4} ");
            spQuery.Append("WHERE ");
            spQuery.Append("StaffID={0}");

            return StaffRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
        public int Delete(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("DELETE FROM Staff ");
            spQuery.Append("WHERE ");
            spQuery.Append("StaffID={0} ");

            return StaffRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
    }
}