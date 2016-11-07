using System.Collections.Generic;
using System.Linq;

namespace Acme.Dressing.Rules
{
    internal static class CheckThat
    {
        public static bool YouCannotPutOnSocksWhenItIsHot(ValidationContext ctx)
        {
            return ctx.CurrentCommand != CommandType.PutOnSocks || ctx.TemperatureType != TemperatureType.Hot;
        }

        public static bool YouCannotPutOnJacketWhenItIsHot(ValidationContext ctx)
        {
            return ctx.CurrentCommand != CommandType.PutOnJacket || ctx.TemperatureType != TemperatureType.Hot;
        }

        public static bool SocksMustBePutOnBeforeFootwear(ValidationContext ctx)
        {
            return VerifyThatXIsNotPrecededByY(ctx, CommandType.PutOnSocks, CommandType.PutOnFootwear);
        }
        
        private static bool VerifyThatXIsNotPrecededByY(ValidationContext ctx, CommandType x, CommandType y)
        {
            return ctx.CurrentCommand != x || !ctx.AlreadyExecuted.Contains(y);
        }

        public static bool PantsMustBePutOnBeforeFootwear(ValidationContext ctx)
        {
            return VerifyThatXIsNotPrecededByY(ctx, CommandType.PutOnPants, CommandType.PutOnFootwear);
        }

        public static bool ShirtMustBePutOnBeforeHeadwear(ValidationContext ctx)
        {
            return VerifyThatXIsNotPrecededByY(ctx, CommandType.PutOnShirt, CommandType.PutOnHeadwear);
        }

        public static bool ShirtMustBePutOnBeforeJacket(ValidationContext ctx)
        {
            return VerifyThatXIsNotPrecededByY(ctx, CommandType.PutOnShirt, CommandType.PutOnJacket);
        }

        public static bool Only1PieceOfEachTypeOfClothingMayBePutOn(ValidationContext ctx)
        {
            var clothingCommands = new[] { CommandType.PutOnFootwear, CommandType.PutOnHeadwear, CommandType.PutOnJacket, CommandType.PutOnPants, CommandType.PutOnShirt, CommandType.PutOnSocks };

            return !clothingCommands.Contains(ctx.CurrentCommand) || !ctx.AlreadyExecuted.Contains(ctx.CurrentCommand);
        }

        public static bool PajamasMustBeTakenOffBeforeAnythingElseCanBePutOn(ValidationContext ctx)
        {
            var requirePajamaRemoval = new[] { CommandType.PutOnFootwear, CommandType.PutOnHeadwear, CommandType.PutOnJacket, CommandType.PutOnPants, CommandType.PutOnShirt, CommandType.PutOnSocks };

            return !requirePajamaRemoval.Contains(ctx.CurrentCommand) || ctx.AlreadyExecuted.Contains(CommandType.RemovePajamas);
        }

        public static bool YouCannotLeaveTheHouseUntilAllItemsOfClothingAreOn(ValidationContext ctx)
        {
            if (ctx.CurrentCommand != CommandType.LeaveHouse) return true;

            var required = new List<CommandType> { CommandType.PutOnFootwear, CommandType.PutOnHeadwear, CommandType.PutOnJacket, CommandType.PutOnPants, CommandType.PutOnShirt, CommandType.PutOnSocks };
            
            if(ctx.TemperatureType == TemperatureType.Hot)
            {
                required.Remove(CommandType.PutOnJacket);
                required.Remove(CommandType.PutOnSocks);
            }

            return required.All(c => ctx.AlreadyExecuted.Contains(c));
        }
    }
}
