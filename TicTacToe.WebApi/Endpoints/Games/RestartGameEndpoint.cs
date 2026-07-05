using FastEndpoints;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.WebApi.Endpoints.Games;

public class RestartGameEndpoint(IRoomService _service) : Endpoint<RestartGameRequest>
{
    public override void Configure()
    {
        Post("/games/restart");
        Summary(s =>
        {
            s.Summary = "Начать игру заново";
            s.Response(200);
        });
        Version(1);
    }

    public override async Task HandleAsync(RestartGameRequest req, CancellationToken ct)
    {
        await _service.RestartGameAsync(req.RoomCode, ct);

        await Send.OkAsync(cancellation: ct);
    }
}
