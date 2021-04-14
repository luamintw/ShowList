using Amazon.DynamoDBv2.DataModel;

namespace ShowList.Service.Data
{
    [DynamoDBTable("show")]
    public class Show
    {
        [DynamoDBProperty("id")]
        [DynamoDBHashKey]//primary key
        public int Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }
    }
}