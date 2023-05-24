namespace Lookif.Library.SimpleHttpClientWrapper.Interface;
public interface IHttpClientWrapper
{
    Task<TReturnType> GetAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<TReturnType> FilterAsync<TFilter,TReturnType>(string add, TFilter filter, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    HttpClient GetHttpClient(string name);
   
    Task PostAsync<TInputType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "");
    Task<TReturnType> PostAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<bool> PutAsync<TInputType, TReturnType>(string add, TInputType Data, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
    Task<bool> PutAsync<TReturnType>(string add, CancellationToken cancellationToken, string name, string token = "") where TReturnType : class;
}


