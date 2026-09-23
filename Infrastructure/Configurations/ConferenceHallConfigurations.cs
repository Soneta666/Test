using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    internal class ConferenceHallConfigurations : IEntityTypeConfiguration<ConferenceHall>
    {
        public void Configure(EntityTypeBuilder<ConferenceHall> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasMany(h => h.Services)
                .WithMany(s => s.ConferenceHalls);

            builder.HasMany(h => h.HallReservations)
                .WithOne(h => h.ConferenceHall)
                .HasForeignKey(h => h.ConferenceHallId);
        }
    }
}
