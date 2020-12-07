namespace domain.translate.Contracts.Security
{
    using System.Net;

	public interface IToken
	{
		object GenerateTokenAuthorization(string username, string[]userGroups, bool permission = false);
		object GenerateNTLMCredentials(string username, string password);
		object GenerateNTLMCredentials(string domain, string username, string password);
		object GenerateNetworkCredentials(NetworkCredential networkCredential);
		string ReadAuthorizationToken(string token, string tag);
	}
}
