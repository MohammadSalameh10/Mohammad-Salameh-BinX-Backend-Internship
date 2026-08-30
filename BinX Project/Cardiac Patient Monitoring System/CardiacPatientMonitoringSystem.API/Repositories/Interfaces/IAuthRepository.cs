namespace CardiacPatientMonitoringSystem.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}