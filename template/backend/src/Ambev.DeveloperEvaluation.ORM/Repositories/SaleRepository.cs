using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {

        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id,cancellationToken);

            if (sale == null)
                return false;

            _context.SaleItems.RemoveRange(sale.Items);
            _context.Sales.Remove(sale);
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<(IEnumerable<Sale> Sales, long TotalRows)> GetByPaginedFilterAsync(short page, short pageSize)
        {

            return (await _context.Sales.AsNoTrackingWithIdentityResolution()
                                        .OrderBy(x => x.TotalValue)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync(),
                                        
                    await _context.Sales.LongCountAsync());
        }

        public async Task UpdateAsync(Sale Sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(Sale);
            await SaveChangesAsync(cancellationToken);
        }

        private Task SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    }
}
