using NFine.Data;
using NFine.Domain.Entity.CustomerManage;
using NFine.Domain.IRepository.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.CustomerManage
{
    public class CustomerRepository : RepositoryBase<CustomerEntity>, ICustomerRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CustomerEntity>(t=>t.Id==keyValue);
                db.Commit();
            }
        }

        public void SubmitForm(CustomerEntity customerEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    db.Insert(customerEntity);
                }
                else
                {
                    db.Update(customerEntity);
                }
                db.Commit();
            }
        }
    }
}
