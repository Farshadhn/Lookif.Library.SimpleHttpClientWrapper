using Lookif.Layers.WebFramework.Api;
using Lookif.Library.Common.Exceptions;
using Lookif.Library.Common.Utilities;
using Lookif.Library.SimpleHttpClientWrapper.Interface;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Lookif.Library.SimpleHttpClientWrapper.ConcreteClass;

public class HttpClientWrapper : IHttpClientWrapper
{
    private IHttpClientFactory HttpClientFactory { get; }
    public HttpClient GetHttpClient(string name)
    {
        return HttpClientFactory.CreateClient(name);
    }

    public HttpClientWrapper(IHttpClientFactory httpClientFactory)
    {
        HttpClientFactory = httpClientFactory;
    }

    public async Task<TReturnType> GetAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class
    {
        var client = name switch { "" or null => HttpClientFactory.CreateClient(), _ => HttpClientFactory.CreateClient(name) };

        PrepareClient(token, client);
        using var response = await client.GetAsync(add, cancellationToken);
        var responseInString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode is not true)
            throw GetCorrespondingException(response.StatusCode, responseInString);




        var responseInT = DeserializeObject<TReturnType>(responseInString);
        return responseInT;


    }
 


 

    public async Task PostAsync<TInputType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "")
    {
        var client = name switch { "" or null => HttpClientFactory.CreateClient(), _ => HttpClientFactory.CreateClient(name) };

        PrepareClient(token, client);
        StringContent stringContent = new(SerializeObject(Data), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(add, stringContent, cancellationToken);
        var responseInString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode is not true)
            throw GetCorrespondingException(response.StatusCode, responseInString);
    }
    public async Task<TReturnType> PostAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class
    {
        var client = name switch { "" or null => HttpClientFactory.CreateClient(), _ => HttpClientFactory.CreateClient(name) };

        PrepareClient(token, client);
        StringContent stringContent = new(SerializeObject(Data), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(add, stringContent, cancellationToken);
        var responseInString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode is not true)
            throw GetCorrespondingException(response.StatusCode, responseInString);

         

        var responseInT = DeserializeObject<TReturnType>(responseInString);
        return responseInT;
    }








    public async Task<bool> PutAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class
    {
        var client = name switch { "" or null => HttpClientFactory.CreateClient(), _ => HttpClientFactory.CreateClient(name) };

        PrepareClient(token, client);
        StringContent stringContent = new(SerializeObject(Data), Encoding.UTF8, "application/json");
        using var response = await client.PutAsync(add, stringContent, cancellationToken);
        var responseInString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode is not true)
            throw GetCorrespondingException(response.StatusCode, responseInString);
        int state = (int)response.StatusCode;
        return state == 204 || state == 200 || state == 201;
    }


    public async Task<bool> PutAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class
    {
        var client = name switch { "" or null => HttpClientFactory.CreateClient(), _ => HttpClientFactory.CreateClient(name) };

        PrepareClient(token, client);
        using var response = await client.PutAsync(add, null, cancellationToken);
        var responseInString = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode is not true)
            throw GetCorrespondingException(response.StatusCode, responseInString);
        var responseInT = DeserializeObject<ApiResult<TReturnType>>(responseInString);
        return responseInT.IsSuccess is not true ? throw new Exception("IsNotSuccess") : responseInT.IsSuccess;
    }

    #region ... Private functions ... 
    private void PrepareClient(string token, HttpClient client)
    {
        if (token.HasValue())
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
    }
    private Exception GetCorrespondingException(HttpStatusCode httpStatusCode, string response)
          => httpStatusCode switch
          {
              HttpStatusCode.BadRequest => new BadRequestException(response),
              HttpStatusCode.NotFound => new NotFoundException(response),
              _ => new Exception(response)
          };


    #endregion
}