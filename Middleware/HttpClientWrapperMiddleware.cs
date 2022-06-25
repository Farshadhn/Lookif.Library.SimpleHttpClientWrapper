using Lookif.Library.SimpleHttpClientWrapper.ConcreteClass;
using Lookif.Library.SimpleHttpClientWrapper.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Lookif.Library.SimpleHttpClientWrapper.Middleware;

public static class HttpClientWrapperMiddleware
{
    public static void AddHttpClientWrapper(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

    }
}
