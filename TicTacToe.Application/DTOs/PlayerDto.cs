using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.DTOs;

public class PlayerDto
{
    public Guid Id { get; set; }
    public PlayerSymbol Symbol { get; set; }
}
