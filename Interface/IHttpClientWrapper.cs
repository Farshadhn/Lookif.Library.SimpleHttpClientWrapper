namespace Lookif.Library.SimpleHttpClientWrapper.Interface;
public interface IHttpClientWrapper
{
    Task<List<TReturnType>> GetAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "");
    Task<List<TReturnType>> GetAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "");
    HttpClient GetHttpClient(string name);
    Task<TReturnType> GetSpecificAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<TReturnType> GetSpecificAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;

    Task PostAsync<TInputType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "");
    Task<TReturnType> PostAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<bool> PutAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<bool> PutAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
}


