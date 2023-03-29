using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Parallax
{
    public class InfiniteParallaxEffect : MonoBehaviour
    {
        [SerializeField] private List<ParallaxPart> _parts;
        [SerializeField] private Transform _targetTransform;

        private List<InfiniteParallaxLayer> _layers;

        private Vector2 _previousTargetPosition;
        
        private void Start()
        {
            _previousTargetPosition = _targetTransform.position;
            _layers = new List<InfiniteParallaxLayer>();
            foreach (var part in _parts)
            {
                Transform layerParent = new GameObject().transform;
                layerParent.transform.parent = transform;
                part.SpriteRenderer.transform.parent = layerParent;
                _layers.Add(new InfiniteParallaxLayer(part.SpriteRenderer, part.Speed, layerParent));
            }
        }
        
        private void LateUpdate()
        {
            Vector2 targetPosition = _targetTransform.position;
            Vector2 deltaMovement = _previousTargetPosition - targetPosition;
            
            foreach (var layer in _layers)
            {
                layer.UpdateLayer(targetPosition, _previousTargetPosition);
            }
            _previousTargetPosition = targetPosition;
        }
        [Serializable]
        private class ParallaxPart
        {
            [field:SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
            [field:SerializeField] public float Speed { get; private set; }
        }
    }
}