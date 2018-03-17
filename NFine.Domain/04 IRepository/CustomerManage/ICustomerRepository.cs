using NFine.Data;
using NFine.Domain.Entity.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._04_IRepository.CustomerManage
{
    public interface ICustomerRepository : IRepositoryBase<CustomerEntity>
    {

    }
}
