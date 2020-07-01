using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.API.Models.Context;

namespace Users.API.Infrastructure
{
    public class HealthCheck : IHealthCheck
    {
        private readonly dbContext _context;

        public HealthCheck(dbContext context)
        {
            _context = context;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _context.Database.CanConnectAsync(cancellationToken)
              ? HealthCheckResult.Healthy()
              : HealthCheckResult.Unhealthy();
        }
    }
}
