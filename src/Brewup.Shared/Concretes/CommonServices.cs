namespace Brewup.Shared.Concretes;

public class CommonServices
{
	public static string GetErrorMessage(Exception ex)
	{
		return GetMessageFromException(ex);
	}

	public static string GetDefaultErrorTrace(Exception ex)
	{
		return $"Source: {ex.Source} StackTrace: {ex.StackTrace} Message: {GetMessageFromException(ex)}";
	}

	private static string GetMessageFromException(Exception ex)
	{
		while (true)
		{
			if (ex.InnerException == null)
				return ex.Message;

			ex = ex.InnerException;
		}
	}
}