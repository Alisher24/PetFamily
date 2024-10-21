namespace PetFamily.Core.Messaging;

public interface IMessageQueue<TMessage>
{
    Task WriteAsync(TMessage paths, CancellationToken cancellationToken = default);

    Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
}