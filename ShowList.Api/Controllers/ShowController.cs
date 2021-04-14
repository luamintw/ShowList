using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowList.Api.Model;
using ShowList.Api.Services.Interfaces;

namespace ShowList.Api.Controllers
{
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowController(IShowService showService) => _showService = showService;
        
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ShowResponse>>> GetShowList()
        {
            var result = await _showService.GetShowList();
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShow(ShowRequest showRequest)
        {
            await _showService.AddShow(showRequest);
            return Ok();
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> EditShow(ShowRequest showRequest)
        {
            await _showService.EditShow(showRequest);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            await _showService.DeleteShow(id);
            return Ok();
        }
    }
}