// using Microsoft.EntityFrameworkCore;
// using Personalized_Library_System.Models;

// namespace Personalized_Library_System.Services;

// public static class UserService
// {
//     static List<User> Users { get; }
//     static int nextId = 5;
//     static UserService()
//     {
//         Users =
//         [
//             new() { Id = 1, Name = "John Doe", Email = "john.doe@hotmail.com", Username = "j.doe"},
//             new() { Id = 2, Name = "Jane Doe", Email = "jane.doe@hotmail.com", Username = "jn.doe"},
//             new() { Id = 3, Name = "John Smith", Email = "john.smith@hotmail.com", Username = "j.smith"},
//             new() { Id = 4, Name = "Jane Smith", Email = "jane.smith@hotmail.com", Username = "jn.smith"}
//         ];
//     }

//     public static List<User> GetAll() => Users;

//     public static User? Get(int id) => Users.FirstOrDefault(p => p.Id == id);

//     public static void Add(User user)
//     {
//         user.Id = nextId++;
//         Users.Add(user);
        
//     }

//     public static User? GetByEmail(string email)
//     {
//         return Users.FirstOrDefault(user => user.Email == email);
//     }

//     public static void Delete(int id)
//     {
//         var user = Get(id);
//         if(user is null)
//             return;

//         Users.Remove(user);
//     }

//     public static void Update(User user)
//     {
//         var index = Users.FindIndex(p => p.Id == user.Id);
//         if (index == -1)
//             return;
//         // Users.Insert(index, user);
//         Users[index] = user;
//     }
// }