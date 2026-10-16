namespace Application.Abstractions;

/// <summary>Puerto de salida: registro de mensajes. La implementación vive en Infrastructure.</summary>
public interface IAppLogger
{
    void Info(string message);
}
