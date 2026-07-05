using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Models;

namespace TicTacToe.Domain.Entities;

/// <summary>
/// Одна партия игры
/// </summary>
public class Game
{
    private Game() { }

    public Game(Guid roomId)
    {
        RoomId = roomId;
    }

    public Guid Id { get; private set; }

    public Guid RoomId { get; private set; }

    public Room Room { get; private set; } = null!;

    /// <summary>
    /// Состояние доски
    /// </summary>
    public string BoardState { get; private set; } = "---------";

    /// <summary>
    /// Текущий ход
    /// </summary>
    public PlayerSymbol CurrentTurn { get; private set; } = PlayerSymbol.X;

    public GameStatus Status { get; private set; } = GameStatus.InProgress;

    /// <summary>
    /// Победитель
    /// </summary>
    public PlayerSymbol? Winner { get; private set; }

    public DateTime? StartedAtUtc { get; private set; }

    public DateTime? FinishedAtUtc { get; private set; }

    public ICollection<Move> Moves { get; private set; } = [];

    /// <summary>
    /// Установить выполненый ход
    /// </summary>
    public void SetMove(GameResult result)
    {
        BoardState = result.BoardState;
        CurrentTurn = result.NextTurn;

        if (result.IsFinished)
        {
            Status = result.IsDraw ? GameStatus.Draw : GameStatus.Finished;
            Winner = result.Winner;
            FinishedAtUtc = DateTime.UtcNow;
        }
    }
}
