using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Shared.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace Brewup.Infrastructure.ReadModel.MongoDb.Repositories;

public class EventStorePositionRepository : IEventStorePositionRepository
{
	private readonly IMongoDatabase _database;
	private readonly ILogger<EventStorePositionRepository> _logger;

	public EventStorePositionRepository(ILogger<EventStorePositionRepository> logger, MongoDbSettings mongoDbSettings)
	{
		_logger = logger;
		var client = new MongoClient(mongoDbSettings.ConnectionString);
		_database = client.GetDatabase(mongoDbSettings.DatabaseName);
	}

	public async Task<IEventStorePosition> GetLastPosition()
	{
		try
		{
			var collection = _database.GetCollection<LastEventPosition>(nameof(LastEventPosition));
			var filter = Builders<LastEventPosition>.Filter.Eq("_id", "EventStoreCommitPosition");
			var result = await collection.CountDocumentsAsync(filter) > 0 ? (await collection.FindAsync(filter)).First() : null;
			if (result == null)
			{
				result = new LastEventPosition("EventStoreCommitPosition");
				await collection.InsertOneAsync(result);
			}

			return new EventStorePosition(result.CommitPosition, result.PreparePosition);
		}
		catch (Exception e)
		{
			_logger.LogError($"EventStorePositionRepository: Error getting LastSavedPostion, Message: {e.Message}, StackTrace: {e.StackTrace}");
			throw;
		}
	}

	public async Task Save(IEventStorePosition position)
	{
		try
		{
			var collection = _database.GetCollection<LastEventPosition>(nameof(LastEventPosition));
			var filter = Builders<LastEventPosition>.Filter.Eq("_id", "EventStoreCommitPosition");
			var entity = await collection.Find(filter).FirstOrDefaultAsync();
			if (entity == null)
			{
				entity = new LastEventPosition("EventStoreCommitPosition");
				await collection.InsertOneAsync(entity);
			}
			else
			{
				if (position.CommitPosition > entity.CommitPosition && position.PreparePosition > entity.PreparePosition)
				{
					entity.CommitPosition = position.CommitPosition;
					entity.PreparePosition = position.PreparePosition;
					await collection.FindOneAndReplaceAsync(filter, entity);
				}
			}
		}
		catch (Exception e)
		{
			_logger.LogError($"EventStorePositionRepository: Error while updating commit position: {e.Message}, StackTrace: {e.StackTrace}");
			throw;
		}
	}
}