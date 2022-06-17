using _src.CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets assets;
        

        public GameFactory(IAssets assets)
        {
            this.assets = assets;
        }

        
        public GameObject CreateHero(GameObject at)
        {
            return assets.Instantiate(AssetClass.HeroPath, at: at.transform.position);
        }


        public void CreateHud()
        {
            assets.Instantiate(AssetClass.HudPath);
        }
    }
}