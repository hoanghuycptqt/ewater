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
    public partial class AdStaffService
    {
        private GenericRepository<Staff> StaffRepository;
        public AdStaffService()
        {
            this.StaffRepository = new GenericRepository<Staff>(new EWaterContext());
        }
        public IEnumerable<Staff> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Staff");

            return StaffRepository.ExecuteQuery(spQuery.ToString(), parameters);
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