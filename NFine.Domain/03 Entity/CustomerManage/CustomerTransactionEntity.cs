using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.CustomerManage
{
    public class CustomerTransactionEntity : IEntity<CustomerTransactionEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string Id { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? CreatorTime { get; set; }
        public bool? DeleteMark { get; set; }
        public string DeleteUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool? TradStatus { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string Salesman { get; set; }
        public string SalesmanCode { get; set; }
        public string Remark { get; set; }
        public int Version { get; set; }
        public decimal? Total { get; set; }
        public DateTime? BillDate { get; set;}
        public string Telphone { get; set; }
        public string Address { get; set; }
    }
}