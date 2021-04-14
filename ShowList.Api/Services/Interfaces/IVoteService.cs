using System.Collections.Generic;
using System.Threading.Tasks;
using ShowList.Api.Model;

namespace ShowList.Api.Services.Interfaces
{
    public interface IVoteService
    {
        Task VoteShow(VoteRequest voteRequest);
        Task<IEnumerable<VoteResponse>> GetVoteResult();
    }
}