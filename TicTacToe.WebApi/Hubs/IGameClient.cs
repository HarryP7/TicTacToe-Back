using TicTacToe.Application.DTOs;

namespace TicTacToe.Application.Interfaces;

public interface IGameClient
{
    Task ReceiveMessage(string? messages, PlayerDto? Player);

    Task GameUpdated(string? messages, GameStateDto? state);
}
