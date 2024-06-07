using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<TData>
{
    private int _code = 200; //responsavel pela padronização de status code

    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;
    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;        
    }
    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore] // ignora a prop na serialização
    public bool IsSuccess => _code is >= 200 and < 300;
}
