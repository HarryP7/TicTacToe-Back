using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Entities;

/// <summary>
/// Ход игры
/// </summary>
public class Move
{
    private Move() { }

    public Move(Guid gameId, int cellIndex, PlayerSymbol playerSymbol)
    {
        GameId = gameId;
        CellIndex = cellIndex;
        Symbol = playerSymbol;
    }

    public Guid Id { get; private set; }

    public Guid GameId { get; private set; }

    public Game Game { get; private set; } = null!;

    public PlayerSymbol Symbol { get; private set; }

    public int CellIndex { get; private set; }

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

}
