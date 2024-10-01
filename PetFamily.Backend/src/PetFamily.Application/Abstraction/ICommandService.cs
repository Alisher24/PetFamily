﻿using Domain.Shared;

namespace Application.Abstraction;

public interface ICommandService<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse>> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandService<in TCommand> where TCommand : ICommand
{
    public Task<Result> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}