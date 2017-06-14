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
    public partial class ContactService
    {
        private GenericRepository<ContactClient> ContactRepository;
        public ContactService()
        {
            this.ContactRepository = new GenericRepository<ContactClient>(new EWaterContext());

        }

        public IEnumerable<ContactClient> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM ContactClient ");          

            return ContactRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
        public int Insert(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("INSERT INTO ContactClient ( ");
            spQuery.Append("Name ");
            spQuery.Append(",Phone ");
            spQuery.Append(",Email ");
            spQuery.Append(",Content) ");
            spQuery.Append("VALUES( ");
            spQuery.Append("{0} ");
            spQuery.Append(",{1} ");
            spQuery.Append(",{2} ");
            spQuery.Append(",{3})");

            return ContactRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
    }
}