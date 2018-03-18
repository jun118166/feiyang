using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;
using NFine.Domain.IRepository.CustomerManage;
using NFine.Domain.Entity.CustomerManage;
using NFine.Repository.CustomerManage;

namespace NFine.Application.CustomerManage
{
    public class CustomerTransactionApp
    {
        private ICustomerTransactionRepository service = new CustomerTransationRepository();
        public List<CustomerTransactionEntity> GetList(Pagination pagination, string keyword, DateTime? beginTime, DateTime? endTime, bool? tranStatus)
        {
            var expression = ExtLinq.True<CustomerTransactionEntity>();
            //非管理员只可查看自己的客户
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            if (!"admin".Equals(loginInfo.UserCode))
            {
                expression = expression.And(t => t.SalesmanCode.Contains(loginInfo.UserCode));
            }
            if (beginTime != null)
            {
                expression = expression.And(t => t.BillDate >= beginTime);
            }
            if (endTime != null)
            {
                DateTime? e = endTime.Value.AddDays(1);
                expression = expression.And(t => t.BillDate < endTime);
            }
            if (tranStatus != null)
            {
                expression = expression.And(t => t.TradStatus == tranStatus);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CustomerName.Contains(keyword));
                expression = expression.Or(t => t.Salesman.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public List<CustomerTransactionEntity> GetList(DateTime? beginTime,DateTime? endTime,bool? tranStatus)
        {
            var expression = ExtLinq.True<CustomerTransactionEntity>();
            if (beginTime!=null)
            {
                expression = expression.And(t => t.BillDate >= beginTime);
            }
            if (endTime!=null)
            {
                DateTime? e = endTime.Value.AddDays(1);
                expression = expression.And(t => t.BillDate < endTime);
            }
            if (tranStatus!=null)
            {
                expression = expression.And(t => t.TradStatus.Equals(tranStatus));
            }
            return service.IQueryable(expression).OrderByDescending(t => t.CreatorTime).ToList();
        }
        public CustomerTransactionEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(CustomerTransactionEntity customerEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                customerEntity.Modify(keyValue);
            }
            else
            {
                customerEntity.Create();
            }
            service.SubmitForm(customerEntity, keyValue);
        }
    }
}
