namespace Tests.Data
{
    public class TestDataModel
    {
        public List<string> ValidUsernames { get; set; } = new();
        public List<string> InvalidUsernames { get; set; } = new();
        public List<string> ValidPasswords { get; set; } = new();
        public List<string> InvalidPasswords { get; set; } = new();
    }
}
