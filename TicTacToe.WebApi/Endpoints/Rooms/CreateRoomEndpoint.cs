using FastEndpoints;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.WebApi.Endpoints.Rooms;

public class CreateRoomEndpoint(IRoomService _service) : EndpointWithoutRequest<CreateRoomResponse>
{
    public override void Configure()
    {
        Post("/rooms");
        Summary(s =>
        {
            s.Summary = "Создать комнату";
            s.Response<CreateRoomResponse>(200);
        });
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _service.CreateRoomAsync(ct);

        await Send.OkAsync(result, ct);
    }
}
