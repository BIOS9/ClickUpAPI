using RestSharp;
using RestSharp.Authenticators;

namespace BIOS9.ClickUp;

public class PersonalApiAuthenticator : IAuthenticator
{
    private readonly string _apiToken;

    public PersonalApiAuthenticator(string apiToken)
    {
        _apiToken = apiToken;
    }

    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        request.AddOrUpdateHeader("Authorization", _apiToken);
        return ValueTask.CompletedTask;
    }
}