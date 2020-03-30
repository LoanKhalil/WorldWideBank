using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldWideBank.Models;

namespace WorldWideBank.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Number);
            builder.OwnsOne(x => x.Balance);
            builder.HasOne(x => x.Customer);
        }
    }
}
