using TicTacToe.Application.Models;
namespace TicTacToe.Application.DTOs;

public class JoinRoomResponse(ValidationResult result, PlayerDto? player = null)
{
    public ValidationResult ValidationResult { get; set; } = result;
    public PlayerDto? Player { get; set; } = player;
}
