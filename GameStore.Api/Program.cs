using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new(
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992,7,15)
    ),
    new(
        2,
        "Final Fantasy II",
        "Roleplaying",
        59.99M,
        new DateOnly(2010,9,30)
    ),
    new(
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022,9,27)
    )
];

// lamda exp tells we are returning whatever in the games
// GET/games
app.MapGet("games", () => games);


// for any one specific game
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id ==id));


// Handler
// app.MapGet("/", () => "Hello World!");

app.Run();
