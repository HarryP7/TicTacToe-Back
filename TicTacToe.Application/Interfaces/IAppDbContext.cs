using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<Game> Games { get; }

    DbSet<Move> Moves { get; }

    DbSet<RoomPlayer> RoomPlayers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
