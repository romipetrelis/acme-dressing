using System.Collections.Generic;
using System.Web.Mvc;

namespace Acme.Dressing.Web.ViewModels
{
    public class DressFormViewModel
    {
        public string TemperatureTypeText { get; set; }
        public string CommandList { get; set; }
        public string ResponseMessage { get; set; }
        public IEnumerable<SelectListItem> AvailableTemperatureTypes = new[] {
            new SelectListItem {Text="HOT", Value="HOT"},
            new SelectListItem {Text="COLD", Value="COLD" } };
    }
}
