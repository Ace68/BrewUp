using System.Linq.Expressions;

namespace Brewup.Infrastructure.ReadModel.Abstracts;

public interface IPersister
{
	string Type { get; }
	Task<T> GetByIdAsync<T>(string id, CancellationToken cancellationToken) where T : ModelBase;
	Task InsertAsync<T>(T dtoToInsert, CancellationToken cancellationToken) where T : ModelBase;
	Task ReplaceAsync<T>(T dtoToUpdate, CancellationToken cancellationToken) where T : ModelBase;
	Task UpdateOneAsync<T>(string id, Dictionary<string, object> propertiesToUpdate, CancellationToken cancellationToken) where T : ModelBase;

	Task DeleteAsync<T>(string id, CancellationToken cancellationToken) where T : ModelBase;
	Task DeleteManyAsync<T>(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) where T : ModelBase;
	Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>>? filter = null) where T : ModelBase;
}