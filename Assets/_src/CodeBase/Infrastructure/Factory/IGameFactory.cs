using System;
using System.Collections.Generic;
using _src.CodeBase.Infrastructure.Services;
using _src.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        event Action HeroCreated;
        GameObject HeroGameObject { get; }
        
        GameObject CreateHero(GameObject at);
        
        GameObject CreateHud();
        void CleanUp();
    }
}