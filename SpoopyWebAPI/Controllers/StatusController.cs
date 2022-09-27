using Microsoft.AspNetCore.Mvc;
using SpoopyWebAPI.Models;

namespace SpoopyWebAPI.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : Controller
    {
        private SpoopyStatus _spoopyStatus;
        public StatusController() 
        {
            _spoopyStatus = SpoopyStatusInstance.SpoopyStatus;
        }
                
        [HttpGet]
        [Route("status")]
        public bool GetSpoopyStatus() => _spoopyStatus.IsRunning;

        [HttpGet]
        [Route("uptime")]        
        public TimeSpan GetSpoopyUptime() => _spoopyStatus.Uptime;

        [HttpGet]
        [Route("runtime")]        
        public TimeSpan GetSpoopyRuntime() => _spoopyStatus.Runtime;

        [HttpGet]
        [Route("serverscount")]
        public int GetSpoopyServersCount() => _spoopyStatus.ServersCount;
        
        [HttpPost]
        [Route("status")]
        public void PostSpoopyStatus(SpoopyStatus model)
        {
            SpoopyStatusInstance.SpoopyStatus = model;
        }

    }
}
