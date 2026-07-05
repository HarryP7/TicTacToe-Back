using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.DTOs
{
    public class MakeMoveRequest : BaseRoomDto
    {
        public int CellIndex { get; set; }
        public PlayerSymbol PlayerSymbol { get; set; }
    }
}
