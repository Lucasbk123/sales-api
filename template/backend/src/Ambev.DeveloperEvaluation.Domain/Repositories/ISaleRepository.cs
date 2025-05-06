using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale,CancellationToken cancellationToken = default);

    Task UpdateAsync(Sale Sale, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Sale> Sales, long TotalRows)> GetByPaginedFilterAsync(short page,short pageSize);
}
