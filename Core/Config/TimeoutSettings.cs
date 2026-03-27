namespace Core.Config
{
    public sealed class TimeoutSettings
    {
        public required int ImplicitWait { get; set; }
        public required int ExplicitWait { get; set; }
        public required int PageLoad { get; set; }
    }
}
