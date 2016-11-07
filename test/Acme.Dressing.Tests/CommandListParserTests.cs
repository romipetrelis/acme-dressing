using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Xunit;

namespace Acme.Dressing.Tests
{
    public class CommandListParserTests : IClassFixture<CommandListParserTests.Fixture>
    {
        private readonly Func<string, IEnumerable<CommandType>> _sut;

        public CommandListParserTests(Fixture fixture)
        {
            _sut = fixture.Parser;
        }

        [Fact]
        public void Parses_Csv()
        {
            // arrange
            var input = "8,4, 5,  6, 1, 2, 3,   7";
            var expected = new[]
            {
                CommandType.RemovePajamas, CommandType.PutOnShirt, CommandType.PutOnJacket, CommandType.PutOnPants,
                CommandType.PutOnFootwear, CommandType.PutOnHeadwear, CommandType.PutOnSocks, CommandType.LeaveHouse
            };

            // act
            var actual = _sut(input).ToArray();

            // assert
            Assert.Equal(expected, actual);
        }

        public class Fixture
        {
            public readonly Func<string, IEnumerable<CommandType>> Parser;
            public Fixture()
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule(new CoreModule());

                var container = builder.Build();
                Parser = container.Resolve<Func<string, IEnumerable<CommandType>>>();
            }
        }
    }
}
