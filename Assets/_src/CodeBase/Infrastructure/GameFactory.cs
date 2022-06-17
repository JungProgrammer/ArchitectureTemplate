using _src.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _src.CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        
        public GameObject CreateHero(GameObject at)
        {
            return _assetProvider.Instantiate(AssetClass.HeroPath, at: at.transform.position);
        }


        public void CreateHud()
        {
            _assetProvider.Instantiate(AssetClass.HudPath);
        }
    }
}