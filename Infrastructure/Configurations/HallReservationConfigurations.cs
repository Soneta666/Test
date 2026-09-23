using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    internal class HallReservationConfigurations : IEntityTypeConfiguration<HallReservation>
    {
        public void Configure(EntityTypeBuilder<HallReservation> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(h => h.ConferenceHall)
                .WithMany(h => h.HallReservations)
                .HasForeignKey(h => h.ConferenceHallId);

            builder.HasMany(h => h.Services)
                .WithMany(h => h.HallReservations);
        }
    }
}
