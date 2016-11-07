using Autofac;
using System;
using Xunit;

namespace Acme.Dressing.Tests
{
    public class TemperatureTypeParserTests : IClassFixture<TemperatureTypeParserTests.Fixture>
    {
        private readonly Func<string, TemperatureType> _sut;

        public TemperatureTypeParserTests(Fixture fixture)
        {
            _sut = fixture.Parser;
        }

        [Fact]
        public void Parses_Hot()
        {
            var actual = _sut("  HOt ");
            Assert.Equal(TemperatureType.Hot, actual);
        }

        [Fact]
        public void Parses_Cold()
        {
            var actual = _sut(" cOlD  ");
            Assert.Equal(TemperatureType.Cold, actual);
        }

        [Fact]
        public void Throws_NotSupportedException_For_Mild()
        {
            try
            {
                _sut("miLd ");
            }
            catch (NotSupportedException ex)
            {
                Assert.Equal("Temperature type 'miLd ' is not supported", ex.Message);
            }
        }

        public class Fixture
        {
            public readonly Func<string, TemperatureType> Parser;

            public Fixture()
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule(new CoreModule());

                var container = builder.Build();
                Parser = container.Resolve<Func<string, TemperatureType>>();
            }
        }
    }
}
