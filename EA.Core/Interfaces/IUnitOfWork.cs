using EA.Core.Entities;

namespace EA.DataAccess.Repositories;

public interface IUnitOfWork
{
    Task<int> CompleteAsync();
    void Dispose();
}