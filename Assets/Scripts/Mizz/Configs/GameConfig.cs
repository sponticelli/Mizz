using LiteNinja.Configs;
using UnityEngine;

namespace Mizz.Configs
{
    
    [CreateAssetMenu(menuName = "Mizz/Configs/GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : AConfig
    {
        public static GameConfig Load()
        {
            return Resources.Load<GameConfig>("Configs/GameConfig");
        }
    }
}