using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DynamodbCreateDemo;

public class Function
{
    public async Task<APIGatewayProxyResponse> CreatorFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var user = JsonConvert.DeserializeObject<User>(request.Body);

        if (user == null) return new APIGatewayProxyResponse { StatusCode = 400 };

        var userCreator = new UserCreator(new AmazonDynamoDBClient());

        if (await userCreator.CreateUserAsync(user))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200
            };
        }
        else
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 400
            };
        }        
    }
}
