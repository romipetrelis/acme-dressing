using System.Collections.Generic;

namespace Acme.Dressing
{
    public class DressResult
    {
        public List<DressCommandResult> CommandResults { get; set; }
        public List<DressCommandRuleResult> RuleResults { get; set; }
        public string Message { get; set; }
    }
}
