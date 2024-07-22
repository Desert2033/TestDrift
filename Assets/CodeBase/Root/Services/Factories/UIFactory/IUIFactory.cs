using UnityEngine;

public interface IUIFactory : IService
{
    SessionItemInfoUI CreateSessionItem(Transform parent);
}