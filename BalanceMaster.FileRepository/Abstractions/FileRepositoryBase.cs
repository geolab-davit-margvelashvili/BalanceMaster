using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Domain.Models.Abstractions;
using System.Text.Json;

namespace BalanceMaster.FileRepository.Abstractions;

/// <summary>
/// Generic file repository used for storing and retrieving domain object in/from files
/// </summary>
/// <typeparam name="TEntity">Type of domain object</typeparam>
/// <typeparam name="TId">Type of domain object id</typeparam>
public abstract class FileRepositoryBase<TEntity, TId>
    where TEntity : DomainEntity<TId>
    where TId : IComparable<TId>
{
    #region Fields

    private readonly string _filePath;
    private readonly List<TEntity> _entities;

    #endregion Fields

    #region Contructors

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="filePath">Path of the file where data is saved and retrieved</param>
    protected FileRepositoryBase(string filePath)
    {
        _filePath = filePath;
        _entities = LoadEntitiesFromFile();
    }

    #endregion Contructors

    #region Public Methods

    /// <summary>
    /// Returns object by id.
    /// Throws ObjectNotFoundException if object not found
    /// </summary>
    /// <param name="id">Object identifier</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public async Task<TEntity> GetByIdAsync(TId id)
    {
        var entity = await GetByIdOrDefaultAsync(id);
        if (entity is null)
        {
            throw new ObjectNotFoundException(id.ToString() ?? string.Empty, nameof(TEntity));
        }

        return entity;
    }

    /// <summary>
    /// Returns object by id.
    /// Returns null if object not found
    /// </summary>
    /// <param name="id">Object identifier</param>
    public Task<TEntity?> GetByIdOrDefaultAsync(TId id)
    {
        var entity = _entities.FirstOrDefault(entity => entity.Id.CompareTo(id) == 0);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Returns collection of all entities
    /// </summary>
    /// <returns></returns>
    public Task<List<TEntity>> ListAsync()
    {
        return Task.FromResult(_entities);
    }

    /// <summary>
    /// Adds new entity in file
    /// </summary>
    /// <param name="entity">Entity to be saved</param>
    public async Task<TId> CreateAsync(TEntity entity)
    {
        entity.Id = await GenerateIdAsync();
        _entities.Add(entity);
        SaveEntitiesToFile();
        return entity.Id;
    }

    /// <summary>
    /// Updates existing entity in file
    /// </summary>
    /// <param name="entity">Entity to be saved</param>
    public Task UpdateAsync(TEntity entity)
    {
        var index = _entities.FindIndex(a => a.Id.CompareTo(entity.Id) == 0);
        if (index < 0)
            throw new ObjectNotFoundException(entity.Id.ToString() ?? string.Empty, typeof(TEntity).Name);

        _entities[index] = entity;
        SaveEntitiesToFile();

        return Task.CompletedTask;
    }

    #endregion Public Methods

    #region Protected Methods

    protected abstract Task<TId> GenerateIdAsync();

    #endregion Protected Methods

    #region Private Methods

    private List<TEntity> LoadEntitiesFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TEntity>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TEntity>>(json) ?? new List<TEntity>();
    }

    private void SaveEntitiesToFile()
    {
        var json = JsonSerializer.Serialize(_entities, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    #endregion Private Methods
}