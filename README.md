# Checkers

Used technologies: c#, .net core, websocket, javascript, bootstrap, sass, html5, grunt, webpack

## Need to do

You should adjust your connection string in appsettings.json 
```
"ConnectionStrings": {
    "Sql": "Data Source =.\\SQLEXPRESS; Initial Catalog = catalog; Integrated Security = True"
  },
```
and execute cmd command in your main directory where are all projects
```
dotnet ef database update --startup-project Application  --project Depot
```
 
## Short presentation

* How to execute cmd command

![alt text](https://media.giphy.com/media/PhMO6qxkaGApqQtQze/giphy.gif)

* You don't have to create user or table all data are in database

![alt text](https://media.giphy.com/media/0vIlXUKcEkjup5BeDo/giphy.gif)

* If you want to create your own user it is very easily

![alt text](https://media.giphy.com/media/0PQgz4o0jTV3Nryzqp/giphy.gif)

* Login is required, and after that you will get redirect to table view

![alt text](https://media.giphy.com/media/2kEXEFsHWfaTUoYrRY/giphy.gif)

* You can also create your own table but name is unique, and you will get redirect to table was created

![alt text](https://media.giphy.com/media/vUf28GlnN24NM2ZP7a/giphy.gif)

* Users can choose whatever room they want

![alt text](https://media.giphy.com/media/D9a5vir1jGR2aZaqj5/giphy.gif)

* Communication in rooms is independent so users are seeing messages in that specific room where they are

![alt text](https://media.giphy.com/media/hXhN6WEBF41QoYkIWf/giphy.gif)

* Game is also independent

![alt text](https://media.giphy.com/media/9WZVRIvWdlRgMDRufi/giphy.gif)

![alt text](https://media.giphy.com/media/48DN7r54afr6S8snJW/giphy.gif)

* There can be only 2 players and unlimited number of visitors

![alt text](https://media.giphy.com/media/nC38rPqkvWjBEt7G8l/giphy.gif)

* After player left visitor can take a sit and start the game with person who have been sitting

![alt text](https://media.giphy.com/media/qK39ZbyL1wq0OXgi6H/giphy.gif)

* Pawns can move backward after capture a pawn

![alt text](https://media.giphy.com/media/GsUrGs0JjRf5ef0LjD/giphy.gif)

* Queen can move diagonally and she stops after capture a pawn

![alt text](https://media.giphy.com/media/WrATXhzh1f2TLcd6uA/giphy.gif)

* When player win in both windows show communicate with basic data

![alt text](https://media.giphy.com/media/eajpYTSBUhXpkcZdtN/giphy.gif)

