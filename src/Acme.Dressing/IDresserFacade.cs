namespace Acme.Dressing
{
    public interface IDresserFacade
    {
        DressResult Process(string temperatureType, string commandList);
    }
}