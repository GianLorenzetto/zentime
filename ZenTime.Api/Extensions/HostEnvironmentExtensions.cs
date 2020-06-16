using System;
using Microsoft.Extensions.Hosting;

namespace ZenTime.Api.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment("Local");
        }
    }
}