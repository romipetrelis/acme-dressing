using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Dressing
{
    internal static class Helpers
    {
        public static Func<string, IEnumerable<CommandType>> CommandListParser = (commandList) =>
        {
            var args = commandList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return args.Select(s => (CommandType)int.Parse(s));
        };

        public static Func<string, TemperatureType> TemperatureTypeParser = (text) =>
        {
            switch (text.ToLower().Trim())
            {
                case "hot":
                    return TemperatureType.Hot;
                case "cold":
                    return TemperatureType.Cold;
                default:
                    throw new NotSupportedException($"Temperature type '{text}' is not supported");
            }
        };

        public static Func<TemperatureType, CommandType, string> ResponseFinder = (temperatureType, command) =>
        {
            var entry = PossibleResponses[command];

            switch (temperatureType)
            {
                case TemperatureType.Cold:
                    return entry.WhenCold;
                case TemperatureType.Hot:
                    return entry.WhenHot;
                default:
                    return string.Empty;
            }
        };

        private static readonly Dictionary<CommandType, DressCommandResponse> PossibleResponses = new Dictionary<CommandType, DressCommandResponse>
                {
                    {
                        CommandType.LeaveHouse,
                        new DressCommandResponse
                        {
                            WhenCold = Resources.LeavingHouseLiteral,
                            WhenHot = Resources.LeavingHouseLiteral
                        }
                    },
                    {
                        CommandType.PutOnFootwear,
                        new DressCommandResponse {WhenCold = Resources.BootsLiteral, WhenHot = Resources.SandalsLiteral}
                    },
                    {
                        CommandType.PutOnHeadwear,
                        new DressCommandResponse {WhenCold = Resources.HatLiteral, WhenHot = Resources.SunglassesLiteral}
                    },
                    {
                        CommandType.PutOnJacket,
                        new DressCommandResponse {WhenCold = Resources.JacketLiteral, WhenHot = string.Empty}
                    },
                    {
                        CommandType.PutOnPants,
                        new DressCommandResponse {WhenCold = Resources.PantsLiteral, WhenHot = Resources.ShortsLiteral}
                    },
                    {
                        CommandType.PutOnShirt,
                        new DressCommandResponse {WhenCold = Resources.ShirtLiteral, WhenHot = Resources.ShirtLiteral}
                    },
                    {
                        CommandType.PutOnSocks,
                        new DressCommandResponse {WhenCold = Resources.SocksLiteral, WhenHot = string.Empty}
                    },
                    {
                        CommandType.RemovePajamas,
                        new DressCommandResponse
                        {
                            WhenCold = Resources.RemovingPajamasLiteral,
                            WhenHot = Resources.RemovingPajamasLiteral
                        }
                    }
                };
    }
}
