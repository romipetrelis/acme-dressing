using System.Collections.Generic;

namespace Acme.Dressing.Rules
{
    internal class ValidationContext
    {
        public CommandType CurrentCommand { get; set; }
        public readonly ICollection<CommandType> AlreadyExecuted;
        public readonly TemperatureType TemperatureType;
        public readonly ICollection<CommandType> RequestedCommands;

        public ValidationContext(ICollection<CommandType> alreadyExecuted, TemperatureType temperatureType,
            ICollection<CommandType> requestedCommands)
        {
            AlreadyExecuted = alreadyExecuted;
            TemperatureType = temperatureType;
            RequestedCommands = requestedCommands;
        }
    }
}
