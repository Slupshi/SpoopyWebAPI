using Microsoft.AspNetCore.Mvc;
using SpoopyWebAPI.Models;

namespace SpoopyWebAPI.Controllers
{
    [ApiController]
    [Route("status")]
    public class StatusController : ControllerBase
    {
        private SpoopyStatus _spoopyStatus;
        public StatusController() 
        {
            _spoopyStatus = SpoopyStatusInstance.SpoopyStatus;
        }

        [Route("status", Name = "getStatus")]
        [HttpGet(Name = "GetSpoopyStatus")]
        public bool GetSpoopyStatus() => _spoopyStatus.IsRunning;

        [Route("uptime")]
        [HttpGet(Name = "GetSpoopyUptime")]
        public TimeSpan GetSpoopyUptime() => _spoopyStatus.Uptime;

        [Route("runtime")]
        [HttpGet(Name = "GetSpoopyRuntime")]
        public TimeSpan GetSpoopyRuntime() => _spoopyStatus.Runtime;

        [Route("serverscount")]
        [HttpGet(Name = "GetSpoopyServersCount")]
        public int GetSpoopyServersCount() => _spoopyStatus.ServersCount;

        [Route("status/post", Name = "postStatus")]
        [HttpPost(Name = "PostSpoopyStatus")]
        public void PostSpoopyStatus(SpoopyStatus model)
        {
            SpoopyStatusInstance.SpoopyStatus = model;
        }

    }
}
