using MassTransit;
using MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;
using MasLazu.AspNet.Authentication.Core.Consumer.Consumers;
using MasLazu.AspNet.Verification.Abstraction.Events;

namespace MasLazu.AspNet.Authentication.Core.Base.Tests.Consumers;

public class VerificationCompletedConsumerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly VerificationCompletedConsumer _consumer;

    public VerificationCompletedConsumerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _consumer = new VerificationCompletedConsumer(_userServiceMock.Object);
    }

    [Fact]
    public async Task Consume_WithSuccessfulVerification_ShouldCallVerifyEmailAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var verificationEvent = new VerificationCompletedEvent
        {
            UserId = userId,
            IsSuccessful = true,
            PurposeCode = "email_verification"
        };

        var contextMock = new Mock<ConsumeContext<VerificationCompletedEvent>>();
        contextMock.Setup(x => x.Message).Returns(verificationEvent);
        contextMock.Setup(x => x.CancellationToken).Returns(CancellationToken.None);

        _userServiceMock
            .Setup(x => x.VerifyEmailAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _consumer.Consume(contextMock.Object);

        // Assert
        _userServiceMock.Verify(
            x => x.VerifyEmailAsync(userId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Consume_WithUnsuccessfulVerification_ShouldNotCallVerifyEmailAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var verificationEvent = new VerificationCompletedEvent
        {
            UserId = userId,
            IsSuccessful = false,
            PurposeCode = "email_verification"
        };

        var contextMock = new Mock<ConsumeContext<VerificationCompletedEvent>>();
        contextMock.Setup(x => x.Message).Returns(verificationEvent);
        contextMock.Setup(x => x.CancellationToken).Returns(CancellationToken.None);

        // Act
        await _consumer.Consume(contextMock.Object);

        // Assert
        _userServiceMock.Verify(
            x => x.VerifyEmailAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Consume_ShouldPassCancellationTokenToService()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var cancellationTokenSource = new CancellationTokenSource();
        var verificationEvent = new VerificationCompletedEvent
        {
            UserId = userId,
            IsSuccessful = true,
            PurposeCode = "email_verification"
        };

        var contextMock = new Mock<ConsumeContext<VerificationCompletedEvent>>();
        contextMock.Setup(x => x.Message).Returns(verificationEvent);
        contextMock.Setup(x => x.CancellationToken).Returns(cancellationTokenSource.Token);

        _userServiceMock
            .Setup(x => x.VerifyEmailAsync(userId, cancellationTokenSource.Token))
            .Returns(Task.CompletedTask);

        // Act
        await _consumer.Consume(contextMock.Object);

        // Assert
        _userServiceMock.Verify(
            x => x.VerifyEmailAsync(userId, cancellationTokenSource.Token),
            Times.Once);
    }
}
