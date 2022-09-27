namespace SpoopyWebAPI.Models
{
    public class SpoopyRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public SpoopyRole() { }

        public SpoopyRole(string name, int memberCount, DateTime createdAt)
        {
            Name = name;
            MemberCount = memberCount;
            CreatedAt = createdAt;
        }
    }
}
