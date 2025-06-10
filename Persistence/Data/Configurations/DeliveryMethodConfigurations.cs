﻿using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(d => d.Price)
                    .HasColumnType("decimal(8,2)");

            builder.Property(d => d.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(d => d.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(d => d.DeliveryTime)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

        }
    }
}
