﻿using NFine.Code;
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
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Telphone.Contains(keyword));
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
                customerEntity.Create();
            }
            service.SubmitForm(customerEntity, keyValue);
        }
    }
}
