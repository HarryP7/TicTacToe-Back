
using TicTacToe.Application.Models;

namespace TicTacToe.Application.DTOs
{
    public class MakeMoveResponse(ValidationResult result, GameStateDto? state = null)
    {
        public ValidationResult ValidationResult { get; set; } = result;

        public GameStateDto? State { get; set; } = state;
    }
}
