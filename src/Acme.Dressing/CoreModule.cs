using System;
using System.Collections.Generic;
using Acme.Dressing.Rules;
using Autofac;

namespace Acme.Dressing
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DresserFacade>().AsImplementedInterfaces();
            builder.RegisterType<DefaultDresser>().As<IDresser>();
            builder.RegisterType<CanDressValidator>().AsSelf();
            
            var rules = new List<Func<ValidationContext, bool>>
            {
                CheckThat.YouCannotPutOnJacketWhenItIsHot,
                CheckThat.YouCannotPutOnSocksWhenItIsHot,
                CheckThat.SocksMustBePutOnBeforeFootwear,
                CheckThat.PantsMustBePutOnBeforeFootwear,
                CheckThat.ShirtMustBePutOnBeforeHeadwear,
                CheckThat.ShirtMustBePutOnBeforeJacket,
                CheckThat.Only1PieceOfEachTypeOfClothingMayBePutOn,
                CheckThat.PajamasMustBeTakenOffBeforeAnythingElseCanBePutOn,
                CheckThat.YouCannotLeaveTheHouseUntilAllItemsOfClothingAreOn
            };

            builder.Register(ctx => Helpers.CommandListParser).As<Func<string, IEnumerable<CommandType>>>();
            builder.Register(ctx => Helpers.TemperatureTypeParser).As<Func<string, TemperatureType>>();
            builder.Register(ctx => Helpers.ResponseFinder).As<Func<TemperatureType, CommandType, string>>();
            builder.Register(ctx => rules).As<IEnumerable<Func<ValidationContext, bool>>>();
        }
    }
}
