using System.Collections.Generic;
using InputReader;

namespace Player
{
    public class PlayerSystem
    {
        private readonly PlayerBrain _playerBrain;
        private readonly PlayerEntity _playerEntity;
        
        public PlayerSystem(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(playerEntity, inputSources);
        }
    }
}