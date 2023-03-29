using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Core.Enums;

namespace Core.Tools
{
    [Serializable]
    public class DirectionalCamerasPair
    {
        [SerializeField] private CinemachineVirtualCamera _leftCamera;
        [SerializeField] private CinemachineVirtualCamera _rightCamera;

        private Dictionary<Direction, CinemachineVirtualCamera> _directionalCameras;

        public Dictionary<Direction, CinemachineVirtualCamera> DirectionCameras =>
            _directionalCameras ??= new Dictionary<Direction, CinemachineVirtualCamera>()
            {
                { Direction.Left, _leftCamera },
                { Direction.Right, _rightCamera },
            };
    }
}