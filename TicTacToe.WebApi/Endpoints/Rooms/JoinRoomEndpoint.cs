/*using FastEndpoints;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.WebApi.Endpoints.Rooms;


// TODO: Удалить, делаем через Hub в SignalR
public class JoinRoomEndpoint(IRoomService service) : Endpoint<JoinRoomRequest>
{
    public override void Configure()
    {
        Post("/rooms/join");
        Summary(s =>
        {
            s.Summary = "Присоединиться к комнате";
            s.Response(200);
        });
        Version(1);
    }

    public override async Task HandleAsync(JoinRoomRequest req, CancellationToken ct)
    {
        await service.JoinRoomAsync(req.RoomCode, ct);

        await Send.OkAsync(cancellation: ct);
    }
}
*/