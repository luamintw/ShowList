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
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;
        public ShowController(IShowService showService) => _showService = showService;

        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowResponse>>> GetShowList()
        {
            var results = await _showService.GetShowList();
            if (results == null || !results.Any())
            {
                return NotFound();
            }
            return Ok(results);
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult> AddShow(ShowRequest showRequest)
        {
            await _showService.AddShow(showRequest);
            return Ok();
        }

        [Route("edit")]
        [HttpPost]
        public async Task<ActionResult> EditShow(ShowRequest showRequest)
        {
            await _showService.EditShow(showRequest);
            return Ok();
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult> DeleteShow(int id)
        {
            await _showService.DeleteShow(id);
            return Ok();
        }
    }
}