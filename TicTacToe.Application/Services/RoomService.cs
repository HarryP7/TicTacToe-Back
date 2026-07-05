using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.DTOs;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Models;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services;

public class RoomService(
    IAppDbContext dbContext,
    IRoomCodeGenerator generator,
    IGameEngine engine) : IRoomService
{
    /// <summary>
    /// Создание комнаты
    /// </summary>
    public async Task<CreateRoomResponse> CreateRoomAsync(CancellationToken ct)
    {
        // Создаем комнату
        var room = new Room(code: generator.Generate());

        // Создаем 1ю партию
        var game = new Game(room.Id);
        //room.CurrentGameId = game.Id;

        // Добавляем создателя комнаты как первого игрока
        var player = new RoomPlayer(room.Id, PlayerSymbol.X);

        dbContext.Rooms.Add(room);
        dbContext.Games.Add(game);
        dbContext.RoomPlayers.Add(player);

        await dbContext.SaveChangesAsync(ct);

        return new()
        {
            RoomCode = room.Code,
            // TODO: по хорошему, сделать авторизацию, тогда не надо будет это возвращать
            Player = new()
            {
                Id = player.Id,
                Symbol = player.Symbol,
            }
        };
    }

    /// <summary>
    /// Присоединение в комнату
    /// </summary>
    public async Task<JoinRoomResponse> JoinRoomAsync(string roomCode, Guid? playerId, CancellationToken ct)
    {
        var roomId = await dbContext.Rooms
            .AsNoTracking()
            .Where(x => x.Code == roomCode)
            .Select(x => x.Id)
            .SingleOrDefaultAsync(ct);

        if (Guid.Empty == roomId)
            //throw new RoomNotFoundException("Комната не найдена");
            return new(ValidationResult.Error("Комната не найдена"));

        if (playerId is not null)
            return new(ValidationResult.Success("Игрок присоединился"));
         
        var count = await dbContext.RoomPlayers
            .AsNoTracking()
            .CountAsync(x => x.RoomId == roomId, ct);

        if (count >= 2)
            //throw new Exception("В комнате уже есть 2 игрока");
            return new(ValidationResult.Error("В комнате уже есть 2 игрока"));

        var player = new RoomPlayer(roomId, PlayerSymbol.O);

        dbContext.RoomPlayers.Add(player);

        await dbContext.SaveChangesAsync(ct);
            
        return new(
            ValidationResult.Success("Новый игрок присоединился"),
            new()
            {
                Id = player.Id,
                Symbol = player.Symbol
            }
        );
    }

    /// <summary>
    /// Сделать ход
    /// </summary>
    public async Task<MakeMoveResponse> MakeMoveAsync(
        string roomCode,
        PlayerSymbol playerSymbol,
        int cellIndex,
        CancellationToken ct)
    {
        var roomId = await dbContext.Rooms
            .AsNoTracking()
            .Where(x => x.Code == roomCode)
            .Select(x => x.Id)
            .SingleAsync(ct);

        var game = await dbContext.Games
            .Where(x => x.RoomId == roomId)
            .OrderByDescending(x => x.StartedAtUtc)
            .FirstAsync(ct);

        if (game.Status != GameStatus.InProgress)
            return new(ValidationResult.Error("Игра закончена"));

        if (game.CurrentTurn != playerSymbol)
            return new(ValidationResult.Error("Еще не твоя очередь"));

        var result = engine.ProcessMove(game.BoardState, cellIndex, playerSymbol);

        game.SetMove(result);

        dbContext.Moves.Add(new Move(game.Id, cellIndex, playerSymbol));

        await dbContext.SaveChangesAsync(ct);

        var state = await BuildGameState(roomId, game, ct);

        return new(ValidationResult.Success("Ход выполнен"), state);
    }

    public async Task RestartGameAsync(string roomCode, CancellationToken ct)
    {
        var roomId = await dbContext.Rooms
            .AsNoTracking()
            .Where(x => x.Code == roomCode)
            .Select(x => x.Id)
            .SingleAsync(ct);

        dbContext.Games.Add(new Game(roomId));

        await dbContext.SaveChangesAsync(ct);

        var game = await dbContext.Games
            .AsNoTracking()
            .Where(x => x.RoomId == roomId)
            .OrderByDescending(x => x.StartedAtUtc)
            .FirstAsync(ct);

        var state = await BuildGameState(roomId, game, ct);
    }

    /// <summary>
    /// Получить состояние игры
    /// </summary>
    private async Task<GameStateDto> BuildGameState(Guid roomId, Game game, CancellationToken ct)
    {
        var xWins = await dbContext.Games
            .AsNoTracking()
            .Where(x => x.RoomId == roomId && x.Winner == PlayerSymbol.X)
            .CountAsync(ct);

        var oWins = await dbContext.Games
            .AsNoTracking()
            .Where(x => x.RoomId == roomId && x.Winner == PlayerSymbol.O)
            .CountAsync(ct);

        return new GameStateDto
        {
            BoardState = game.BoardState,
            CurrentTurn = game.CurrentTurn.ToString(),
            Status = game.Status.ToString(),
            Winner = game.Winner?.ToString(),
            XWins = xWins,
            OWins = oWins
        };
    }
}
