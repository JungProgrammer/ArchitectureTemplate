using _src.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}