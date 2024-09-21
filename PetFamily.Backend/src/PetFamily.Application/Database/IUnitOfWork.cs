﻿using System.Data;

namespace Application.Database;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    
    Task SaveChanges(CancellationToken cancellationToken = default);
}