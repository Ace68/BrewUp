namespace Brewup.Infrastructure.ReadModel.Abstracts;

public abstract class ModelBase : IModelBase
{
	public string Id { get; protected set; } = string.Empty;
	public bool IsDeleted { get; protected set; }
	public void MarkAsDeleted() => IsDeleted = true;
	public override int GetHashCode() => Id.GetHashCode();
	public override bool Equals(object obj) => Equals(obj as ModelBase);
	public bool Equals(ModelBase other) => (null != other) && (GetType() == other.GetType()) && (other.Id == Id);
	public static bool operator ==(ModelBase Dto1, ModelBase Dto2)
	{
		if ((object)Dto1 == null && (object)Dto2 == null)
			return true;
		if ((object)Dto1 == null || (object)Dto2 == null)
			return false;
		return ((Dto1.GetType() == Dto2.GetType()) && (Dto1.Id == Dto2.Id));
	}
	public static bool operator !=(ModelBase Dto1, ModelBase Dto2) => !(Dto1 == Dto2);
}