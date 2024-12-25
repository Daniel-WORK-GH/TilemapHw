# Tile Map HW

[Link to itch.io](https://daniel-work-gh.itch.io/tilemap-ex1).

In this project i used most of the files from the lesson but i had to modify some of them.

### Spawn enemies - Left click

## Part 1 

I chose to implement the hexagonal map, i had to modify the [TilemapGraph](https://github.com/Daniel-WORK-GH/TilemapHw/blob/master/Assets/Scripts/Map/TilemapGraph.cs") file so it will give the correct 6 neighbours.
I also added my own hexagonal tiles so the spires wont overlap in weird ways.

Usually the player can move freely while on a tilemap (for example in game like terraria) so i made the player position not be bounded to the grid.


## Part 2

I chose to make sure the player doesnt spawn inside of a wall, most of the modifications i had to do is to add my own custom function that will check if the player can access X tiles,
[Link](https://github.com/Daniel-WORK-GH/TilemapHw/blob/3751261ce9b322a7a3fc7bc507ee1c272026e366/Assets/Scripts/Map/BFS.cs#L73).

If the player cant access X tiles he will respawn in a random location and try again, [Link](https://github.com/Daniel-WORK-GH/TilemapHw/blob/3751261ce9b322a7a3fc7bc507ee1c272026e366/Assets/Scripts/Player/Player.cs#L49C5-L49C19).
