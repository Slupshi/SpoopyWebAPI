namespace SpoopyWebAPI.Models
{
    public class SpoopyStatus
    {  
        public TimeSpan Uptime { get; set; }
        public bool IsRunning { get; set; }
        public TimeSpan Runtime { get; set; }
        public int ServersCount { get; set; }

        public SpoopyStatus(){}
    }

    public static class SpoopyStatusInstance
    {
        public static SpoopyStatus SpoopyStatus { get; set; }

        static SpoopyStatusInstance() => SpoopyStatus = new SpoopyStatus();
    }
}
