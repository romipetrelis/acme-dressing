using Acme.Dressing.Rules;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Acme.Dressing
{
    internal class DefaultDresser : IDresser
    {
        private readonly CanDressValidator _validator;
        private readonly Func<TemperatureType, CommandType, string> _responseFinder;

        public DefaultDresser(
            CanDressValidator validator,
            Func<TemperatureType, CommandType, string> responseFinder)
        {
            _validator = validator;
            _responseFinder = responseFinder;
        }

        public DressResult Dress(TemperatureType temperatureType, ICollection<CommandType> commands)
        {
            var commandResults = new List<DressCommandResult>();
            var ruleResults = new List<DressCommandRuleResult>();

            Action setupValidator = () =>
            {
                _validator.RuleRan += (object sender, RuleRanEventArgs e, ref bool cancel) =>
                {
                    if (!e.Passed) cancel = true;
                    ruleResults.Add(new DressCommandRuleResult { Command = e.Command, Passed = e.Passed, RuleName = e.Rule.Name });
                };

                _validator.CommandValidated += (object sender, DressCommandValidationEventArgs e, ref bool cancel) =>
                {
                    if (e.AnyRulesFailed) cancel = true;

                    commandResults.Add(new DressCommandResult { Command = e.Command, Passed = !e.AnyRulesFailed });
                };
            };

            setupValidator();
            _validator.Validate(temperatureType, commands);

            Func<IEnumerable<string>> determineResponseForEachCommand = () =>
            {
                return commandResults.Select(r => r.Passed ? _responseFinder(temperatureType, r.Command) : Resources.FailLiteral);
            };

            var responses = determineResponseForEachCommand(); 

            return new DressResult {
                CommandResults = commandResults,
                RuleResults = ruleResults,
                Message=string.Join(", ", responses)
            };
        }
    }
}
