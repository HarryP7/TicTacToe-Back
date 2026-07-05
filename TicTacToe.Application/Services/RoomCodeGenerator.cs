using System.Security.Cryptography;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class RoomCodeGenerator : IRoomCodeGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int CodeLength = 8;

    public string Generate()
    {
        Span<char> buffer = stackalloc char[CodeLength];

        for (int i = 0; i < CodeLength; i++)
        {
            buffer[i] = Chars[RandomNumberGenerator.GetInt32(Chars.Length)];
        }

        return new string(buffer);
    }
}
