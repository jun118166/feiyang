using NFine.Data;
using NFine.Domain.Entity.CustomerManage;

namespace NFine.Domain.IRepository.CustomerManage
{
    public interface ICustomerTransactionRepository : IRepositoryBase<CustomerTransactionEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(CustomerTransactionEntity customerTranEntity, string keyValue);
    }
}
