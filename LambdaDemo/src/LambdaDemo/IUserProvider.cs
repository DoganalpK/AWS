namespace LambdaDemo
{
    public interface IUserProvider
    {
        Task<User[]> GetUsersAsync();
    }
}