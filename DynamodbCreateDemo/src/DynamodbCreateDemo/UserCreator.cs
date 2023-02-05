using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamodbCreateDemo
{
    public class UserCreator : IUserCreator
    {
        private readonly IAmazonDynamoDB dynamoDB;

        public UserCreator(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            var request = new PutItemRequest
            {
                TableName = "user-table",
                Item = new Dictionary<string, AttributeValue>
                {
                    { "City", new AttributeValue{S = user.City} },
                    { "Email", new AttributeValue(user.Email) },
                    { "Phone", new AttributeValue(user.Phone) },
                    { "Address", new AttributeValue(user.Address) },
                }
            };

            var response = await dynamoDB.PutItemAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
