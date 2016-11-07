using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Dressing
{
    internal class DresserFacade : IDresserFacade
    {
        private readonly Func<string, IEnumerable<CommandType>> _commandListParser;
        private readonly Func<string, TemperatureType> _temperatureTypeParser;
        private readonly IDresser _dresser;


        public DresserFacade(
            Func<string,IEnumerable<CommandType>> commandListParser, 
            Func<string, TemperatureType> temperatureTypeParser,
            IDresser commandProcessor
            )
        {
            _commandListParser = commandListParser;
            _temperatureTypeParser = temperatureTypeParser;
            _dresser = commandProcessor;
        }

        public DressResult Process(string temperatureTypeText, string commandList)
        {
            try
            {
                var temperatureType = _temperatureTypeParser(temperatureTypeText);
                var commands = _commandListParser(commandList).ToList();
                var result = _dresser.Dress(temperatureType, commands);

                return result;
            }
            catch
            {
                return new DressResult { CommandResults = new List<DressCommandResult>(), RuleResults = new List<DressCommandRuleResult>(), Message = Resources.FailLiteral };
            }
        }
    }
}
