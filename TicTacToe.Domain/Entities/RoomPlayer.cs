using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Entities;

/// <summary>
/// Игрок комнаты
/// </summary>
public class RoomPlayer
{
    private RoomPlayer() { }

    public RoomPlayer(Guid roomId, PlayerSymbol symbol)
    {
        Id = roomId;
        Symbol = symbol;
    }

    public Guid Id { get; private set; }

    public Guid RoomId { get; private set; }

    public Room Room { get; private set; } = null!;

    public PlayerSymbol Symbol { get; private set; }

    //public bool IsConnected { get; set; }

    //public DateTime LastSeenUtc { get; set; }
}
