using System.Reflection;

namespace Acme.Dressing.Rules
{
    public class RuleRanEventArgs
    {
        public readonly MethodInfo Rule;
        public readonly bool Passed;
        public readonly CommandType Command;

        public RuleRanEventArgs(MethodInfo rule, bool passed, CommandType command)
        {
            Rule = rule;
            Passed = passed;
            Command = command;
        }
    }
}