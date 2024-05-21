using Personalized_Library_System.Models;

namespace Personalized_Library_System.Services;

public static class UserService
{
    static List<User> Users { get; }
    static int nextId = 5;
    static UserService()
    {
        Users =
        [
            new() { id = 1, name = "John Doe", email = "john.doe@hotmail.com", username = "j.doe"},
            new() { id = 2, name = "Jane Doe", email = "jane.doe@hotmail.com", username = "jn.doe"},
            new() { id = 3, name = "John Smith", email = "john.smith@hotmail.com", username = "j.smith"},
            new() { id = 4, name = "Jane Smith", email = "jane.smith@hotmail.com", username = "jn.smith"}
        ];
    }

    public static List<User> GetAll() => Users;

    public static User? Get(int id) => Users.FirstOrDefault(p => p.id == id);

    public static void Add(User user)
    {
        user.id = nextId++;
        Users.Add(user);
    }

    public static User? GetByEmail(string email)
    {
        return Users.FirstOrDefault(user => user.email == email);
    }

    public static void Delete(int id)
    {
        var user = Get(id);
        if(user is null)
            return;

        Users.Remove(user);
    }

    public static void Update(User user)
    {
        var index = Users.FindIndex(p => p.id == user.id);
        if (index == -1)
            return;
        // Users.Insert(index, user);
        Users[index] = user;
    }
}