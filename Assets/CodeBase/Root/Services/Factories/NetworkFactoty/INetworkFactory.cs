using Fusion;

public interface INetworkFactory : IService
{
    NetworkRunner CreateNetworkRunner();
}