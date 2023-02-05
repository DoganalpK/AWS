using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Numerics;

namespace LambdaDemo
{
    public class UserProvider : IUserProvider
    {
        private readonly IAmazonDynamoDB dynamoDB;

        public UserProvider(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }
        public async Task<User[]> GetUsersAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = "user-table"
            };

            var result = await dynamoDB.ScanAsync(scanRequest);

            if (result != null && result.Items != null)
            {
                List<User> users = new List<User>();

                foreach (var item in result.Items)
                {
                    item.TryGetValue("City", out var city);
                    item.TryGetValue("Address", out var address);
                    item.TryGetValue("Email", out var email);
                    item.TryGetValue("Phone", out var phone);

                    users.Add(new User
                    {
                        City = city?.S,
                        Address = address?.S,
                        Email = email?.S,
                        Phone = phone?.S,
                    });
                }

                return users.ToArray();
            }

            return Array.Empty<User>();
        }
    }
}
