using NFine.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.CustomerManage
{
    public class CustomerTransactionSimple
    {
        [DataFieldAttribute(DescName = "时间")]
        public DateTime? BillDate { get; set; }
        [DataFieldAttribute(DescName = "客户来源")]
        public string SourceName { get; set; }
        [DataFieldAttribute(DescName = "客户姓名")]
        public string CustomerName { get; set; }
        [DataFieldAttribute(DescName = "客户电话")]
        public string CustomerTelphone { get; set; }
        [DataFieldAttribute(DescName = "客户地址")]
        public string CustomerAddress { get; set; }
        private bool _tranStatus;
        [DataFieldAttribute(DescName = "交易情况")]
        public string TranStatus {
            get;set;
        }
        [DataFieldAttribute(DescName = "备注")]
        public string Remark { get; set; }
    }
}
