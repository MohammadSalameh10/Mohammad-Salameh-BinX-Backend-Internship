using CardiacPatientMonitoringSystem.API.Data;
using CardiacPatientMonitoringSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CardiacPatientMonitoringSystem.API.Repositories.Classes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                return;

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();

            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }
}