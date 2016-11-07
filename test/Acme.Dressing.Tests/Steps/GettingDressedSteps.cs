using Autofac;
using TechTalk.SpecFlow;
using Xunit;

namespace Acme.Dressing.Tests.Steps
{
    [Binding]
    public class GettingDressedSteps
    {
        private readonly ScenarioContext _ctx;
        public GettingDressedSteps(ScenarioContext ctx)
        {
            _ctx = ctx;
        }

        [Given(@"a temperature type (.*)")]
        public void GivenATemperatureType(string temperatureType)
        {
            _ctx.TemperatureType = temperatureType;
        }
        
        [Given(@"a CSV command list (.*)")]
        public void GivenACsvCommandList(string commandList)
        {
            _ctx.CommandList = commandList;
        }

        [When("I process the commands")]
        public void WhenIProcessTheCommands()
        {
            _ctx.ActualResult = _ctx.Sut.Process(_ctx.TemperatureType, _ctx.CommandList);
        }
        
        [Then(@"I should get the expected result (.*)")]
        public void ThenIShouldGetTheExpectedResult(string expectedResult)
        {
            Assert.Equal(expectedResult, _ctx.ActualResult.Message);
        }

        public class ScenarioContext
        {
            public ScenarioContext()
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule(new CoreModule());

                var container = builder.Build();

                Sut = container.Resolve<IDresserFacade>();
            }

            public string TemperatureType { get; set; }
            public string CommandList { get; set; }
            public DressResult ActualResult { get; set; }
            public readonly IDresserFacade Sut;
        }
    }
}
