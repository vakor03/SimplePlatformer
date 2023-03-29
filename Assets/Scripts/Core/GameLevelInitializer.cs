using System.Collections.Generic;
using InputReader;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDeviceInputReader _externalDeviceInput;
        private PlayerBrain _playerBrain;

        private bool _onPause;

        private void Awake()
        {
            _externalDeviceInput = new ExternalDeviceInputReader();
            _playerBrain = new PlayerBrain(_playerEntity, new List<IEntityInputSource>()
            {
                _externalDeviceInput, _gameUIInputView
            });
        }

        private void Update()
        {
            if (_onPause)
            {
                return;
            }
            _externalDeviceInput.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (_onPause)
            {
                return;
            }
            _playerBrain.OnFixedUpdate();
        }
    }
}