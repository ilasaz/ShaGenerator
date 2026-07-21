using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShaGenerator.Domain.Hashes;

namespace ShaGenerator.Infrastructure.Hashes;

public class HashConfigurations : IEntityTypeConfiguration<Hash>
{
    public void Configure(EntityTypeBuilder<Hash> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Date);

        builder.Property(h => h.Sha1);
    }
}
