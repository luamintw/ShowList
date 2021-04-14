using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ShowList.Service.Data;
using ShowList.Service.Exceptions;
using ShowList.Service.Model;
using ShowList.Service.Services.Interfaces;

namespace ShowList.Service.Services
{
    public class ShowService : IShowService
    {
        private readonly DynamoDBContext _context;
        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public ShowService(IAmazonDynamoDB amazonDynamoDb)
        {
            _amazonDynamoDb = amazonDynamoDb;
            _context = new DynamoDBContext(amazonDynamoDb);
        }

        public async Task<IEnumerable<ShowResponse>> GetShowList()
        {
            var shows = await _context.ScanAsync<Show>(new List<ScanCondition>()).GetNextSetAsync();
            return shows.Select(s => new ShowResponse(s.Id, s.Name));
        }

        public async Task AddShow(Model.ShowRequest showRequest)
        {
            var foundShow = await FindShowById(showRequest.Id);
            if (foundShow != null)
            {
                throw new ShowOperationException($"The show is already exists.");
            }
            await _context.SaveAsync<Show>(new Show { Id = showRequest.Id, Name = showRequest.Name });
        }

        public async Task EditShow(Model.ShowRequest showRequest)
        {
            var foundShow = await FindShowById(showRequest.Id);
            if (foundShow == null)
            {
                throw new ShowOperationException($"The show is not exists.");
            }

            var show = new Show
            {
                Id = showRequest.Id,
                Name = showRequest.Name
            };
            await _context.SaveAsync<Show>(show);
        }

        public async Task DeleteShow(int id)
        {
            var foundShow = await FindShowById(id);
            if (foundShow == null)
            {
                throw new ShowOperationException($"The show is not exists.");
            }

            await _context.DeleteAsync<Show>(id);
        }

        public async Task<ShowResponse> FindShowById(int id)
        {
            var show = await _context.LoadAsync<Show>(id);
            if (show != null)
            {
                return new ShowResponse(show.Id, show.Name);
            }

            return null;
        }

        // public async Task<IEnumerable<Show>> FindShowByIds(int[] ids)
        // {
        //     // var response = await _amazonDynamoDb.QueryAsync(new QueryRequest
        //     // {
        //     //     TableName = "show",
        //     //     KeyConditionExpression = "id = :v_id",
        //     //     ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
        //     //     {
        //     //         {":v_id", new AttributeValue(){ NS = ids.Select(i => i.ToString()).ToList()}}
        //     //     }
        //     // });
        //     // var shows = new List<Show>();
        //     // response.Items.ForEach(item =>
        //     // {
        //     //     var show = new Show()
        //     //     {
        //     //         Id = int.Parse(item["id"].N),
        //     //         Name = item["name"].S
        //     //     };
        //     //     shows.Add(show);
        //     // });
        //     //
        //
        //     var queryFilter = new QueryFilter("id", QueryOperator.Equal,
        //         ids.Select(x => new AttributeValue {N = x.ToString()}).ToList());
        //     var shows = await _context.QueryAsync<Show>(queryFilter).GetNextSetAsync();
        //     
        //     var shows = await _context.ScanAsync<Show>(new List<ScanCondition>
        //     {
        //         new ScanCondition("id", ScanOperator)
        //     }).GetNextSetAsync();
        //     _context.GetTargetTable<Show>().Scan()
        //     return shows;
        // }
    }
}