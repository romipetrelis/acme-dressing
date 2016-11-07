using Acme.Rules;
using Acme.Dressing.Rules;
using System;
using System.Collections.Generic;

namespace Acme.Dressing
{
    internal class CanDressValidator
    {
        private readonly IEnumerable<Func<ValidationContext, bool>> _rulePack;

        public event DressRuleRanEventHandler RuleRan;
        public event DressCommandValidationEventHandler CommandValidated;

        public CanDressValidator(IEnumerable<Func<ValidationContext, bool>> rulePack)
        {
            _rulePack = rulePack;
        }

        public void Validate(TemperatureType temperatureType, ICollection<CommandType> commands)
        {
            var ctx = new ValidationContext(new List<CommandType>(), temperatureType, commands);

            var cancelValidation = false;
            foreach (var command in commands)
            {
                if (cancelValidation) break;

                ctx.CurrentCommand = command;

                var anyRulesFailed = false;
                var ruleRunner = new RuleRunner<ValidationContext>(_rulePack);

                ruleRunner.RuleRan += (object sender, RuleRanEventArgs<ValidationContext> e, ref bool cancel) =>
                {
                    if (!e.Passed) anyRulesFailed = true;

                    OnRuleRan(new RuleRanEventArgs(e.Rule.Method, e.Passed, command), ref cancel);
                };

                ruleRunner.Run(ctx);
                
                OnCommandValidated(new DressCommandValidationEventArgs(command, anyRulesFailed), ref cancelValidation);
                
                ctx.AlreadyExecuted.Add(command);
            }
        }

        private void OnRuleRan(RuleRanEventArgs e, ref bool cancel)
        {
            if (RuleRan != null)
            {
                RuleRan(this, e, ref cancel);
            }
        }

        private void OnCommandValidated(DressCommandValidationEventArgs e, ref bool cancel)
        {
            if (CommandValidated != null)
            {
                CommandValidated(this, e, ref cancel);
            }
        }
    }   
}
