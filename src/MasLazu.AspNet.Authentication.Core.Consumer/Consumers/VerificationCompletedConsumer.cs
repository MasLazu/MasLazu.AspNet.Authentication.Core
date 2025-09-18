using MassTransit;
using MasLazu.AspNet.Verification.Abstraction.Events;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

namespace MasLazu.AspNet.Authentication.Core.Consumer.Consumers;

public class VerificationCompletedConsumer : IConsumer<VerificationCompletedEvent>
{
    private readonly IUserService _userService;

    public VerificationCompletedConsumer(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Consume(ConsumeContext<VerificationCompletedEvent> context)
    {
        VerificationCompletedEvent message = context.Message;

        if (message.IsSuccessful)
        {
            await _userService.VerifyEmailAsync(message.UserId, context.CancellationToken);
        }
    }
}
