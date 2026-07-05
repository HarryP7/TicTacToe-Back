namespace TicTacToe.Application.Models;

/// <summary>
/// Результат валидации
/// </summary>
public record ValidationResult(bool IsValid, string? Message = null)
{
    public static ValidationResult Success(string message) => new(true, message);
    public static ValidationResult Error(string message) => new(false, message);
}
