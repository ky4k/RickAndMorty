namespace RickAndMorty.Interfaces
{
    public interface IRequester<T>
    {
        Task<List<T>> GetResponseAsync(string url);
    }
}
