using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;
namespace TicTacToe.WebApi.Hubs;

public class GameHub(IDistributedCache cache, IRoomService service) : Hub<IGameClient>
{
    private readonly IDistributedCache _cache = cache;

    /// <summary>
    /// Присоединение в комнату
    /// </summary>
    public async Task JoinRoom(string roomCode, Guid? playerId) // string userName,
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);

        await _cache.SetStringAsync(Context.ConnectionId, roomCode, Context.ConnectionAborted);

        var result = await service.JoinRoomAsync(roomCode, playerId, Context.ConnectionAborted);
        await Clients.Group(roomCode).ReceiveMessage(result.ValidationResult.Message, result.Player);
    }

    /// <summary>
    /// Сделать ход
    /// </summary>
    public async Task MakeMove(MakeMoveRequest req)
    {
        var result = await service.MakeMoveAsync(
                req.RoomCode,
                req.PlayerSymbol,
                req.CellIndex,
                Context.ConnectionAborted);

        await Clients.Group(req.RoomCode).GameUpdated(result.ValidationResult.Message, result.State);
    }

    /// <summary>
    /// Завершение игры, выход из комнаты игроков
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var roomCode = await _cache.GetStringAsync(Context.ConnectionId);

        if (roomCode is not null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomCode);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
