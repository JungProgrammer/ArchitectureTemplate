using _src.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _src.CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}