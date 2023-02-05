using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaDemo;

public class DemoFunction
{
    /// <summary>
    /// A simple function that get datas from DynamoDb tables
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayProxyResponse> DemoFunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var userProvider = new UserProvider(new AmazonDynamoDBClient());
        var users = await userProvider.GetUsersAsync();

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonConvert.SerializeObject(users)
        };
    }
}
