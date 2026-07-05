using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Models;

namespace TicTacToe.Application.GameLogic;

/// <summary>
/// Ядро логики игры
/// </summary>
public class GameEngine : IGameEngine
{
    public GameResult ProcessMove(string board, int cell, PlayerSymbol symbol)
    {
        // 1. Проверяем клетку, что пустая
        if (board[cell] != '-')
            throw new InvalidOperationException("Ячейка уже занята");

        var chars = board.ToCharArray();

        // 2. Ставим символ
        chars[cell] = symbol == PlayerSymbol.X ? 'X' : 'O';

        var newBoard = new string(chars);

        // 3. Проверяем победу
        foreach (var line in WinLines.Lines)
        {
            var a = newBoard[line[0]];
            var b = newBoard[line[1]];
            var c = newBoard[line[2]];

            if (a == '-')
                continue;

            if (a == b && b == c)
            {
                // Победа
                return new GameResult
                {
                    BoardState = newBoard,
                    IsFinished = true,
                    Winner = symbol
                };
            }
        }

        // 4.Проверяем ничью
        if (!newBoard.Contains('-'))
        {
            return new GameResult
            {
                BoardState = newBoard,
                IsFinished = true,
                IsDraw = true
            };
        }

        // 5.Передаем ход
        return new GameResult
        {
            BoardState = newBoard,
            NextTurn = symbol == PlayerSymbol.X
                ? PlayerSymbol.O
                : PlayerSymbol.X
        };
    }
}
