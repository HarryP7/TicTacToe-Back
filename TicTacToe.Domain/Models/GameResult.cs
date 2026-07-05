using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Models;

public class GameResult
{
    public string BoardState { get; init; } = string.Empty;

    public bool IsFinished { get; init; }

    public bool IsDraw { get; init; }

    public PlayerSymbol? Winner { get; init; }

    public PlayerSymbol NextTurn { get; init; }
}
