# Blog

1. Install MongoDB

2. Create database:
```
use blog
db.users.remove({})
db.users.insertOne({UserId:"1", Login:"user1", Password:"password1"})
db.users.insertOne({UserId:"2", Login:"user2", Password:"password2"})
db.users.insertOne({UserId:"3", Login:"user3", Password:"password3"})
db.users.find()
db.posts.remove({})
db.posts.insertOne({PostId:"1", UserId:"1", Username:"user1", Message:"Welcome message of user1", Tags:["first", "message"]})
db.posts.insertOne({PostId:"2", UserId:"2", Username:"user2", Message:"Welcome message of user2", Tags:[]})
db.posts.insertOne({PostId:"3", UserId:"3", Username:"user3", Message:"Welcome message of user3", Tags:["first"]})
db.posts.find()
```

3. Open **Blog.sln** solution in Visual Studio 2017

4. Restore necessary NuGet packages

5. Set **Blog.Service.csproj** as startup project

6. Modify *Blog.Data.Repositories.Mongo.MongoDbSettings.cs* if needed

7. Rebuild **Blog.sln** solution

8. Run **Blog.Service.csproj** project using *Blog.Service* debug profile in a self-hosting mode

9. Open http://localhost:5000/

10. Login. Enter user details: login/password (i.e. user1/password1) and press Login button

11. Create post. Press Create link, input message and tags (optionally; space symbol as delimiter) and press Create button

12. Search posts. Press Search link, input filter data (username and/or tags and/or text) and press Search button


Keywords
-
ASP.NET Core WebAPI, .Net Core 2.1, dependency injection, MongoDB
