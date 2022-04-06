using MediatR;
using Navigator.Actions;
using Navigator.Context;

namespace FOSCBot.Core.Domain.Interactivity.Ping;

public class PingInteractiveActionHandler : ActionHandler<PingInteractiveAction>
{
    public PingInteractiveActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(PingInteractiveAction request, CancellationToken cancellationToken)
    {
        var currentTime = DateTime.Now;
        var requestTime = request.MessageTimestamp;
        var delaySinceMessageWasSent = currentTime - requestTime; // ToDo Test timezone differences

        if (delaySinceMessageWasSent.TotalSeconds < 12)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟩 toy refinisimo bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        } 
        else if (delaySinceMessageWasSent.TotalSeconds < 30)
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟧 toy F bro. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        }
        else
        {
            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), $"🟥 toy joya sosio arreglame ya por dio. Delay: {delaySinceMessageWasSent.TotalSeconds}s", cancellationToken: cancellationToken, replyToMessageId: request.MessageId);
        }

        return Unit.Value;
    }
}