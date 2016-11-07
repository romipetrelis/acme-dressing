using System.Web.Mvc;
using Acme.Dressing.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Dressing.Web.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IDresserFacade _dressingService;

        public DefaultController(IDresserFacade dresser)
        {
            _dressingService = dresser;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new DressFormViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DressFormViewModel model)
        {
            var result = _dressingService.Process(model.TemperatureTypeText, model.CommandList);

            model.ResponseMessage = result.Message;

            var errors = result.RuleResults.Where(r => !r.Passed);

            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Command.ToString(), DecodeRuleName(error.RuleName));
            }

            return View(model);
        }

        private string DecodeRuleName(string ruleName)
        {
            return ErrorMessages.ResourceManager.GetString(ruleName) ?? ruleName;
        }
    }
}
