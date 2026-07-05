namespace TicTacToe.Domain.Enums;

public enum GameStatus
{
    /// <summary>
    /// В процессе
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Ничья
    /// </summary>
    Draw = 2,

    /// <summary>
    /// Закончена
    /// </summary>
    Finished = 3
}
