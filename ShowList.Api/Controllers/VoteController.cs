using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowList.Api.Model;
using ShowList.Api.Services.Interfaces;

namespace ShowList.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService) => _voteService = voteService;
        
        [HttpPost("vote")]
        public async Task<IActionResult> VoteShow(VoteRequest voteRequest)
        {
            await _voteService.VoteShow(voteRequest);
            return Ok();
        }

        [HttpGet("result")]
        public async Task<ActionResult<IEnumerable<VoteResponse>>>  GetVoteResult()
        {
            var result = await _voteService.GetVoteResult();
            return Ok(result);
        }
    }
}