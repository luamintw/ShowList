using System.Collections.Generic;
using System.Threading.Tasks;
using ShowList.Service.Model;

namespace ShowList.Service.Services.Interfaces
{
    public interface IVoteService
    {
        Task VoteShow(VoteRequest voteRequest);
        Task<IEnumerable<VoteResponse>> GetVoteResult();
    }
}