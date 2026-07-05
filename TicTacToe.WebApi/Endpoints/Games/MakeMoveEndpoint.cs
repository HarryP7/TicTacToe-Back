/*using FastEndpoints;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.WebApi.Endpoints.Games;

// TODO: Удалить, делаем через Hub в SignalR
public class MakeMoveEndpoint(IRoomService service) : Endpoint<MakeMoveRequest, GameStateDto>
{
    public override void Configure()
    {
        Post("/games/move");
        Summary(s =>
        {
            s.Summary = "Сделать ход";
            s.Response<GameStateDto>(200);
        });
        Version(1);
    }

    public override async Task HandleAsync(MakeMoveRequest req, CancellationToken ct)
    {
        var result = await service.MakeMoveAsync(
                req.RoomCode,
                req.PlayerSymbol,
                req.CellIndex,
                ct);

        await Send.OkAsync(result.State, ct);
    }
}*/
