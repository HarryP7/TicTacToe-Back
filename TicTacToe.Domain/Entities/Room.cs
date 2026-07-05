namespace TicTacToe.Domain.Entities;

/// <summary>
/// Комната где проходит игра
/// </summary>
public class Room
{
    private Room() { }

    public Room(string code)
    {
        Code = code;
    }

    public Guid Id { get; private set; }

    /// <summary>
    /// Уникальный код комнаты
    /// </summary>
    public string Code { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    //// <summary>
    //// Активность комнаты, при выходе обоих игроков, считается не активной
    //// </summary>
    //public bool IsActive { get; set; }

    //public Guid CurrentGameId { get; set; }

    //public Game CurrentGame { get; set; }

    public ICollection<Game> Games { get; private set; } = [];
}
