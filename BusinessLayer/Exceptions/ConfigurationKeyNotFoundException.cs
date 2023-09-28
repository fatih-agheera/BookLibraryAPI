namespace BusinessLayer.Exceptions
{
	public class ConfigurationKeyNotFoundException : Exception
	{
		public ConfigurationKeyNotFoundException()
		{
		}

		public ConfigurationKeyNotFoundException(string message) : base(message)
		{
		}
	}
}