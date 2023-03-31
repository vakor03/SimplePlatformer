using System;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class GroundCheckerData
    {
        [field: SerializeField] public Transform GroundChecker { get; private set; }
        [field: SerializeField] public float GroundCheckRadius { get; private set; }
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}