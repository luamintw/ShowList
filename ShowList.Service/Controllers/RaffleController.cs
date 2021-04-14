using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShowList.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaffleController : ControllerBase
    {
        [Route("getluckyman")]
        public void GetLuckyMan()
        {

        }
    }
}