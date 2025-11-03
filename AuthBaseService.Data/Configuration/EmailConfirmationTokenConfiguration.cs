using AuthBaseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Data.Configuration
{
    public class EmailConfirmationTokenConfiguration : IEntityTypeConfiguration<EmailConfirmationToken>
    {
        public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Token)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(e => e.Expiration)
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
