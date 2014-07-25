namespace CircuitBreakerSpike.Repositories
{
    public class RepositoryExceptionThrowingState : IRepositoryExceptionThrowingState
    {
        public bool ThrowExceptions { get; set; }
    }
}