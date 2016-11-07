namespace Acme.Dressing
{
    public class DressCommandValidationEventArgs
    {
        public readonly CommandType Command;
        public readonly bool AnyRulesFailed;

        public DressCommandValidationEventArgs(CommandType command, bool anyRulesFailed)
        {
            Command = command;
            AnyRulesFailed = anyRulesFailed;
        }
    }
}