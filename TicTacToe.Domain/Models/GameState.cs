namespace TicTacToe.Domain.Models;

public class GameState
{
    public string BoardState { get; set; } = string.Empty;

    public string CurrentTurn { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? Winner { get; set; }

    public int XWins { get; set; }

    public int OWins { get; set; }
}
