using NFine.Code;
using NFine.Domain.Entity.CustomerManage;
using NFine.Domain.IRepository.CustomerManage;
using NFine.Repository.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.CustomerManage
{
    public class CustomerApp
    {
        private ICustomerRepository service = new CustomerRepository();

        public List<CustomerEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<CustomerEntity>();
            //非管理员只可查看自己的客户
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            if (!"admin".Equals(loginInfo.UserCode))
            {
                expression = expression.And(t => t.SalesmanCode.Contains(loginInfo.UserCode));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Telphone.Contains(keyword));
                expression = expression.Or(t => t.Address.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public CustomerEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(CustomerEntity customerEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                customerEntity.Modify(keyValue);
            }
            else
            {
                //先根据客户名称
                var expression = ExtLinq.True<CustomerEntity>();
                expression = expression.And(t => t.Name.Contains(customerEntity.Name));
                expression = expression.And(t => t.Telphone.Contains(customerEntity.Telphone));
                var lst = service.IQueryable(expression);
                if (lst != null & lst.Count() > 0)
                {
                    throw new Exception("存在相同的客户信息!!!");
                }
                customerEntity.Create();
            }
            service.SubmitForm(customerEntity, keyValue);
        }
    }
}
