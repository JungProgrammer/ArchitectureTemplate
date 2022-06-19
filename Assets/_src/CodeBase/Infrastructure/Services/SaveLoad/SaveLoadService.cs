using _src.CodeBase.Data;
using UnityEngine;

namespace _src.CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Progress = "Progress";
        
        
        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(Progress)?.ToDeserialized<PlayerProgress>();
        }
    }
}