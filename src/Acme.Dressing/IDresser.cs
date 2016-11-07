using System.Collections.Generic;

namespace Acme.Dressing
{
    public interface IDresser
    {
        DressResult Dress(TemperatureType temperatureType, ICollection<CommandType> commands);
    }
}
