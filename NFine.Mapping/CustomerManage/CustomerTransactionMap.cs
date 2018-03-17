using NFine.Domain.Entity.CustomerManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.CustomerManage
{
    public class CustomerTransactionMap : EntityTypeConfiguration<CustomerTransactionEntity>
    {
        public CustomerTransactionMap()
        {
            this.ToTable("Buz_CustomerTransaction");
            this.HasKey(t => t.Id);
        }
    }
}
