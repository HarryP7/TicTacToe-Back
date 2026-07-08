## Генерация миграции
в корне проекта:
> `dotnet ef migrations add Init-DB --project TicTacToe.Infrastructure --startup-project TicTacToe.WebApi --context AppDbContext`

## Применение миграции к PostgreSQL
> `dotnet ef database update --project TicTacToe.Infrastructure --startup-project TicTacToe.WebApi --context AppDbContext`
