namespace Application.Abstractions.Caching
{
    /// <summary>
    /// Represents a cache service.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves an object from the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        /// <param name="key">The key of the object in the cache.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The retrieved object, or null if the object is not found in the cache.</returns>
        Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Retrieves an object from the cache based on the specified key, or creates and stores a new object in the cache if the object is not found.
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve or create.</typeparam>
        /// <param name="key">The key of the object in the cache.</param>
        /// <param name="factory">The factory method to create and store a new object in the cache.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The retrieved or created object.</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Stores an object in the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the object to store.</typeparam>
        /// <param name="key">The key of the object in the cache.</param>
        /// <param name="value">The object to store in the cache.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Removes an object from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key of the object in the cache.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes all objects from the cache that have keys starting with the specified prefix.
        /// </summary>
        /// <param name="prefixKey">The prefix of the keys of the objects to remove from the cache.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
    }
}
