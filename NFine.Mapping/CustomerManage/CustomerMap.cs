using NFine.Domain.Entity.CustomerManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.CustomerManage
{
    public class CustomerMap : EntityTypeConfiguration<CustomerEntity>
    {
        public CustomerMap()
        {
            this.ToTable("Buz_Customer");
            this.HasKey(t => t.Id);
        }
    }
}
