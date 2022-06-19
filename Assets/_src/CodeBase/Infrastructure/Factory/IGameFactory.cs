using System.Collections.Generic;
using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
    }
}