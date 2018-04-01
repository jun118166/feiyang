using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.CustomerManage
{
    public class CustomerEntity : IEntity<CustomerEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string Id { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? CreatorTime { get; set; }
        public bool? DeleteMark { get; set; }
        public string DeleteUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string Name { get; set; }
        public string SourceCode { get; set; }
        public string SourceName { get; set; }
        public string Telphone { set; get; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Remark { set; get; }
        public string Version { get; set; }
        public string Salesman { get; set; }
        public string SalesmanCode { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
    }
}
