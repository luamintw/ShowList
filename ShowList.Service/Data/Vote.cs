using Amazon.DynamoDBv2.DataModel;

namespace ShowList.Service.Data
{
    [DynamoDBTable("vote")]
    public class Vote
    {
        [DynamoDBProperty("phone")]
        [DynamoDBHashKey]
        public string Phone { get; set; }

        [DynamoDBProperty("showIds")]
        public int[] ShowIds { get; set; }
    }
}