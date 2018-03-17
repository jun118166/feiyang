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
    public class CustomerTransationRepository : RepositoryBase<CustomerTransactionEntity>, ICustomerTransactionRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CustomerTransactionEntity>(t => t.Id == keyValue);
                db.Commit();
            }
        }

        public void SubmitForm(CustomerTransactionEntity customerTranEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    db.Insert(customerTranEntity);
                }
                else
                {
                    db.Update(customerTranEntity);
                }
            }
        }
    }
}
