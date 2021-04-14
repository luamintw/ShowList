using Amazon.DynamoDBv2;
using ShowList.Service.Services.Interfaces;

namespace ShowList.Service.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public RaffleService(IAmazonDynamoDB amazonDynamoDb)
        {
            _amazonDynamoDb = amazonDynamoDb;
        }

        public void GetLuckyMan()
        {
            throw new System.NotImplementedException();
        }
    }
}