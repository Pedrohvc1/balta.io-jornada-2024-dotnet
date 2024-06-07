namespace Fina.Core.Requests;

// aqui vai estar contido as propriedades que são comuns a todas as requisições
public abstract class Request
{
    public string UserId { get; set; } = string.Empty;
}
