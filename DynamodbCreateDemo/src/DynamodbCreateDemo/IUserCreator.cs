namespace DynamodbCreateDemo
{
    public interface IUserCreator
    {
        Task<bool> CreateUserAsync(User user);
    }
}