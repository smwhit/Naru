﻿using System;
using System.Threading.Tasks;

using Agatha.Common;

using Common.Logging;

using Naru.Core;

namespace Naru.Agatha
{
    public class RequestTask : IRequestTask
    {
        private readonly ILog _log;
        private readonly Func<IRequestDispatcher> _requestDispatcher;

        public RequestTask(ILog log, Func<IRequestDispatcher> requestDispatcher)
        {
            _log = log;
            _requestDispatcher = requestDispatcher;
        }

        public Task<TResponse> Get<TResponse>(Request request) 
            where TResponse : Response
        {
            return Task.Factory.StartNew(() => Execute<TResponse>(request));
        }

        private TResponse Execute<TResponse>(Request request) 
            where TResponse : Response
        {
            using (var performanceTester = new PerformanceTester())
            {
                request.Id = Guid.NewGuid().ToString();

                _log.Debug(string.Format("Start RequestTask {0}, Id - {1}", typeof(TRequest), request.Id));

                using (var requestDispatcher = _requestDispatcher())
                {
                    var response = requestDispatcher.Get<TResponse>(request);
    
                    if (response.Exception != null)
                        throw new RequestException(response.Exception.Message);
    
                    _log.Debug(string.Format("Finished RequestTask {0}, Id - {1}. Duration {2}",
                        typeof(TRequest),
                        request.Id,
                        performanceTester.Result.Milliseconds));
    
                    return response;
                }
            }
        }
    }
}
