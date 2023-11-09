using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using BIOS9.ClickUp.Rest.V2.Util.Json;
using BIOS9.ClickUp.Rest.V2.Util.Net;
using Newtonsoft.Json;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2;

public class ClickUpClient : IClickUpClient
{
    protected readonly string _personalApiToken;
    protected readonly JsonSerializer _serializer;
    protected readonly RestClient _restClient;
    
    public ClickUpClient(string personalApiToken)
    {
        _personalApiToken = personalApiToken;
        _serializer = new JsonSerializer { ContractResolver = new ClickUpContractResolver() };
        _restClient = new RestClient(new RestClientOptions
        {
            BaseUrl = new Uri("https://api.clickup.com/api/v2/"),
            Authenticator = new PersonalApiAuthenticator(_personalApiToken),
            ThrowOnAnyError = true,
            ThrowOnDeserializationError = true
        });
    }

    public async Task<IReadOnlyCollection<ITeam>> GetAuthorizedTeamsAsync()
    {
        var response = await RequestAsync<GetAuthorizedTeamsResponse>(Method.Get, "team");
        return response.Teams.Select(t => new ClickUpTeam(t, this)).ToImmutableList();
    }

    internal async Task RequestAsync(Method method, string endpoint, object? payload = null)
    {
        var request = new RestRequest(endpoint, method);
        if (payload != null)
        {
            var json = SerializeJson(payload);
            request.AddStringBody(json, DataFormat.Json);
        }
        
        var response = await _restClient.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Server returned an error: {response.ErrorMessage}\n\n{response.Content}");
        }
    }
    
    internal async Task<TResponse> RequestAsync<TResponse>(Method method, string endpoint, IEnumerable<Parameter>? parameters = null, object? payload = null)
    {
        var request = new RestRequest(endpoint, method);
        if (payload != null)
        {
            var json = SerializeJson(payload);
            request.AddStringBody(json, DataFormat.Json);
        }

        if (parameters != null)
        {
            request.AddOrUpdateParameters(parameters);
        }
        
        var response = await _restClient.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Server returned an error: {response.ErrorMessage}\n\n{response.Content}");
        }

        return DeserializeJson<TResponse>(response.Content ?? throw new Exception("Server response empty"));
    }
    
    protected string SerializeJson(object value)
    {
        var sb = new StringBuilder(256);
        using (TextWriter text = new StringWriter(sb, CultureInfo.InvariantCulture))
        using (JsonWriter writer = new JsonTextWriter(text))
            _serializer.Serialize(writer, value);
        return sb.ToString();
    }
    protected T DeserializeJson<T>(string json)
    {
        using (StringReader text = new StringReader(json))
        using (JsonReader reader = new JsonTextReader(text))
            return _serializer.Deserialize<T>(reader);
    }
}