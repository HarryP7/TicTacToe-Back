using TicTacToe.Application.DTOs;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Interfaces;

public interface IRoomService
{
    Task<CreateRoomResponse> CreateRoomAsync(CancellationToken ct);

    Task<JoinRoomResponse> JoinRoomAsync(string roomCode, Guid? playerId, CancellationToken ct);

    Task<MakeMoveResponse> MakeMoveAsync(
        string roomCode,
        PlayerSymbol playerSymbol,
        int cellIndex,
        CancellationToken ct);

    Task RestartGameAsync(string roomCode, CancellationToken ct);
}
