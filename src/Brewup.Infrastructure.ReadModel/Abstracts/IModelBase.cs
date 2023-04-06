namespace Brewup.Infrastructure.ReadModel.Abstracts;

public interface IModelBase
{
	string Id { get; }
	bool IsDeleted { get; }
}