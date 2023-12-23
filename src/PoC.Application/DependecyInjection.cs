using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Application
{
    public static class DependecyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection serviceProvider)
        {
            ServiceCollection services = AddQueryAndCommandReferences();

            return services;
        }


        /// <summary>
        /// Add query and command references for MediatR
        /// </summary>
        /// <returns></returns>
        private static ServiceCollection AddQueryAndCommandReferences()
        {
            var services = new ServiceCollection();

            var assembly = Assembly.GetExecutingAssembly();

            var requestTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
                );

            foreach (var requestType in requestTypes)
            {
                var handlerType = assembly.GetTypes()
                    .SingleOrDefault(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) && i.GetGenericArguments()[0] == requestType));

                if (handlerType != null)
                    services.AddTransient(requestType, handlerType);

            }

            return services;
        }
    }
}
