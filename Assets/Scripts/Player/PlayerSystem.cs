using System.Collections.Generic;
using Assets.Scripts.InputReader;

namespace Assets.Scripts.Player
{
    class PlayerSystem
    {
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;

        public PlayerSystem(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(_playerEntity, inputSources);
        }

    }
}
