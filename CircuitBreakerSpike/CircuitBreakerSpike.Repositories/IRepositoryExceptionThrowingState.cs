namespace CircuitBreakerSpike.Repositories
{
    public interface IRepositoryExceptionThrowingState
    {
        bool ThrowExceptions { get; set; }
    }
}