using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Infrastructure.Configurations;

public class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CellIndex)
            .IsRequired();

        builder.Property(x => x.Symbol)
            .IsRequired();

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Moves)
            .HasForeignKey(x => x.GameId);
    }
}
