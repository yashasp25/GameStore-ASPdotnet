using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";
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
app.MapGet("games/{id}", (int id) => {
    GameDto? game = games.Find(game => game.Id ==id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);
// (int id) => ...
// → Lambda function: takes id as input from the URL
// games.Find(game => game.Id == id)
// → Searches the games list for the first match where the game's ID equals the given id

// Handler
// app.MapGet("/", () => "Hello World!");




// POST/games
app.MapPost("games", (CreateGameDto newGame) => 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName,new {id= game.Id}, game);
        //  Name of the route that gets a game by ID,Route parameters , actual data (game object));
});


app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>{
    var index = games.FindIndex(game => game.Id == id);

    if(index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate

    );
    return Results.NoContent();
} );


// DELTE/games/1
app.MapDelete("games/{id}",(int id)=>
{
    games.RemoveAll(game => game.Id ==id);

    return Results.NoContent();
});



app.Run();
