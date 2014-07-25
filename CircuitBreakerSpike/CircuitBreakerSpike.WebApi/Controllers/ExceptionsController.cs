using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using CircuitBreakerSpike.Repositories;

namespace CircuitBreakerSpike.WebApi.Controllers
{
    public class ExceptionsController : ApiController
    {
        private IRepositoryExceptionThrowingState _repositoryExceptionThrowingState;

        public ExceptionsController(IRepositoryExceptionThrowingState repositoryExceptionThrowingState)
        {
            _repositoryExceptionThrowingState = repositoryExceptionThrowingState;
        }

        // GET api/exceptions
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // POST api/exceptions
        public void Post([FromBody] ExceptionOptions exceptionOptions)
        {
            Debug.WriteLine(string.Format("ExceptionOptions: {0}", exceptionOptions.Enabled));
            _repositoryExceptionThrowingState.ThrowExceptions = Boolean.Parse(exceptionOptions.Enabled);
            _repositoryExceptionThrowingState.SecondsToWaitBeforeThrowingException = exceptionOptions.SecondToWait;
        }

    }

    public class ExceptionOptions
    {
        public string Enabled { get; set; }
        public int SecondToWait { get; set; }
    }
}