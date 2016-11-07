namespace Acme.Dressing
{
    public class DressCommandRuleResult
    {
        public CommandType Command { get; set; }
        public string RuleName { get; set; }
        public bool Passed { get; set; }
    }
}
