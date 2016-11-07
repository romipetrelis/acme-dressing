using System;
using System.Collections.Generic;

namespace Acme.Rules
{
    public class RuleRunner<T>
    {
        private readonly IEnumerable<Func<T, bool>> _rules;
        public event RuleRanEventHandler<T> RuleRan;

        public RuleRunner(IEnumerable<Func<T,bool>> rules)
        {
            _rules = rules;
        }

        public void Run(T context)
        {
            var cancel = false;

            foreach (var rule in _rules)
            {
                if (cancel) break;

                var passed = rule(context);

                OnRuleRan(new RuleRanEventArgs<T>(passed, rule), ref cancel);
            }
        }

        private void OnRuleRan(RuleRanEventArgs<T> e, ref bool cancel)
        {
            RuleRan?.Invoke(this, e, ref cancel);
        }
    }

    public delegate void RuleRanEventHandler<T>(object sender, RuleRanEventArgs<T> e, ref bool cancel);

    public class RuleRanEventArgs<T>
    {
        public readonly bool Passed;
        public readonly Func<T, bool> Rule;

        public RuleRanEventArgs(bool passed, Func<T,bool> rule)
        {
            Passed = passed;
            Rule = rule;
        }
    }
}
