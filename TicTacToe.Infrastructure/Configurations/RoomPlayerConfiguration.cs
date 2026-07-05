using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Infrastructure.Configurations;

public class RoomPlayerConfiguration : IEntityTypeConfiguration<RoomPlayer>
{
    public void Configure(EntityTypeBuilder<RoomPlayer> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.Property(x => x.ConnectionId)
        //    .HasMaxLength(200);

        builder.Property(x => x.Symbol)
            .IsRequired();

        builder.HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId);
    }
}
