using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Infrastructure.Persistance.SQl.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(f => f.Id);
        builder.Property(f => f.Username).IsRequired();
        builder.Property(f => f.Password).IsRequired();
    }
}