﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Services.Database.Base;
using RealEstate.Services.Database.Tables;

namespace RealEstate.Services.Database.Configuration
{
    public class OwnershipConfig : BaseEntityConfiguration<Ownership>
    {
        public override void Configure(EntityTypeBuilder<Ownership> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Ownerships)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.PropertyOwnership)
                .WithMany(x => x.Ownerships)
                .HasForeignKey(x => x.PropertyOwnershipId);
        }
    }
}