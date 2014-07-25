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


        // POST api/exceptions
        public void Post([FromBody] ExceptionOptions exceptionOptions)
        {
            Debug.WriteLine(string.Format("ExceptionOptions: {0}", exceptionOptions.Enabled));
            _repositoryExceptionThrowingState.ThrowExceptions = Boolean.Parse(exceptionOptions.Enabled);
            _repositoryExceptionThrowingState.SecondsToWaitBeforeThrowingException = exceptionOptions.SecondsToWaitBeforeThrowingException;
        }

    }

    public class ExceptionOptions
    {
        public string Enabled { get; set; }
        public int SecondsToWaitBeforeThrowingException { get; set; }
    }
}