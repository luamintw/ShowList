using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowList.Service.Model;
using ShowList.Service.Services.Interfaces;

namespace ShowList.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost("voteshow")]
        public async Task<ActionResult> VoteShow(VoteRequest request)
        {
            await _voteService.VoteShow(request);
            return Ok();
        }

        [HttpGet("result")]
        public async Task<ActionResult<IEnumerable<VoteResponse>>> GetVoteResult()
        {
            var results = await _voteService.GetVoteResult();
            if (results == null || !results.Any())
            {
                return new EmptyResult();
            }
            return Ok(results);
        }
    }
}