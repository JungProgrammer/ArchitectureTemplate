using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
    }
}