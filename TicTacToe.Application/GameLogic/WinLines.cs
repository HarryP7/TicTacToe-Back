namespace TicTacToe.Application.GameLogic;

public class WinLines
{
    /// <summary>
    /// Все возможные выигрышные комбинации
    /// </summary>
    public static readonly int[][] Lines =
    [
        // Линии по горизонтали
        [0,1,2],
        [3,4,5],
        [6,7,8],
        // Линии по вертикали
        [0,3,6],
        [1,4,7],
        [2,5,8],

        // Диагонали
        [0,4,8],
        [2,4,6]
    ];
}
