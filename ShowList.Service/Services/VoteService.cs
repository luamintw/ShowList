using System;
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
    public class VoteService : IVoteService
    {
        private readonly DynamoDBContext _context;
        private readonly IShowService _showService;
        public VoteService(IAmazonDynamoDB amazonDynamoDb,
            IShowService showService)
        {
            _context = new DynamoDBContext(amazonDynamoDb);
            _showService = showService;
        }

        public async Task VoteShow(VoteRequest voteRequest)
        {
            await ValidateVoteRequest(voteRequest);

            var voteByPhone = await GetVoteResultByPhone(voteRequest.Phone);
            if (voteByPhone != null && voteByPhone.ShowIds.Count() == 3)
            {
                throw new VoteOperationException("You have voted three shows.");
            }

            var vote = new Vote
            {
                Phone = voteRequest.Phone,
                ShowIds = voteByPhone != null
                    ? voteByPhone.ShowIds.Union(voteRequest.ShowIds).Distinct().ToArray()
                    : voteRequest.ShowIds
            };
            await _context.SaveAsync<Vote>(vote);
        }

        public async Task<IEnumerable<VoteResponse>> GetVoteResult()
        {
            var votes = await _context.ScanAsync<Vote>(new List<ScanCondition>()).GetNextSetAsync();
            var showList = await _context.ScanAsync<Show>(new List<ScanCondition>()).GetNextSetAsync();
            var voteResult = votes.SelectMany(x => x.ShowIds)
                .GroupBy(i => i)
                .Select(show => new VoteResponse()
                {
                    ShowId = show.Key,
                    ShowName = showList.First(x => x.Id == show.Key)?.Name,
                    Count = show.Count()
                })
                .OrderByDescending(s => s.Count);

            return voteResult;
        }

        private async Task<Vote> GetVoteResultByPhone(string phone)
        {
            return await _context.LoadAsync<Vote>(phone);
        }

        private async Task ValidateVoteRequest(VoteRequest voteRequest)
        {
            if (string.IsNullOrEmpty(voteRequest.Phone))
            {
                throw new ArgumentNullException(nameof(voteRequest.Phone));
            }
            if (voteRequest.ShowIds == null || !voteRequest.ShowIds.Any())
            {
                throw new ArgumentNullException(nameof(voteRequest.ShowIds));
            }

            if (voteRequest.ShowIds.Length > 3)
            {
                throw new VoteOperationException("You only can vote for three shows.");
            }

            var allShows = _showService.GetShowList().Result.Select(x => x.Id);
            if (voteRequest.ShowIds.Any(x => !allShows.Contains(x)))
            {
                throw new VoteOperationException("Please make sure the shows you selected are exists in our list.");
            }
        }
    }
}