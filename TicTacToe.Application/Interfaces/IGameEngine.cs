using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Models;

namespace TicTacToe.Application.Interfaces;

public interface IGameEngine
{
    GameResult ProcessMove(string board, int cell, PlayerSymbol symbol);
}
