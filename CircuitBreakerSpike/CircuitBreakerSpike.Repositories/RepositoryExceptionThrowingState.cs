namespace CircuitBreakerSpike.Repositories
{
    public class RepositoryExceptionThrowingState : IRepositoryExceptionThrowingState
    {
        public bool ThrowExceptions { get; set; }
        public int SecondsToWaitBeforeThrowingException { get; set; }
    }
}