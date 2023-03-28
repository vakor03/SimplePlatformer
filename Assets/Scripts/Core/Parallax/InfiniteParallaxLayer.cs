using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Parallax
{
    public class InfiniteParallaxLayer
    {
        private readonly float _speed;
        private readonly List<Transform> _layers;
        private readonly Vector2 _layerSize;

        public InfiniteParallaxLayer(SpriteRenderer initialPart, float speed, Transform parentTransform)
        {
            _speed = speed;

            Sprite sprite = initialPart.sprite;
            _layerSize = new Vector2(sprite.texture.width / sprite.pixelsPerUnit,
                sprite.texture.height / sprite.pixelsPerUnit); //Might be a bug

            _layers = new List<Transform>()
            {
                initialPart.transform,
            };

            Vector2 secondPartPosition = (Vector2)_layers[0].position + new Vector2(_layerSize.x, 0);
            Transform secondPart =
                Object.Instantiate(initialPart, secondPartPosition, Quaternion.identity).transform;

            secondPart.parent = parentTransform;
            _layers.Add(secondPart);
        }
        

        public void UpdateLayer(Vector2 targetPosition, Vector2 previousTargetPosition)
        {
            MoveParts(targetPosition, previousTargetPosition);
            FixLayerPositions(targetPosition);
        }

        private void MoveParts(Vector2 targetPosition, Vector2 previousTargetPosition)
        {
            var deltaMovement = previousTargetPosition - targetPosition;
            foreach (var layer in _layers)
            {
                Vector2 layerPosition = layer.position;
                layerPosition += deltaMovement * _speed;
                layer.position = layerPosition;
            }
        }

        private void FixLayerPositions(Vector2 targetPosition)
        {
            Transform activeLayer = _layers.Find(layer=>IsLayerActive(layer,targetPosition));
            Transform layerToMove = _layers.Find(layer => !IsLayerActive(layer,targetPosition));
            if (activeLayer == null || layerToMove == null)
            {
                return;
            }

            Vector2 relativePosition = activeLayer.position;
            int directionX = (int)Mathf.Sign(targetPosition.x - relativePosition.x);
            int directionY = (int)Mathf.Sign(targetPosition.y - relativePosition.y);
            directionY = 0;

            if ((layerToMove.position.x > relativePosition.x && directionX > 0 ||
                 layerToMove.position.x < relativePosition.x && directionX < 0) &&
                (layerToMove.position.y > relativePosition.y && directionY > 0 ||
                 layerToMove.position.y < relativePosition.y && directionY < 0))
            {
                return;
            }

            layerToMove.position = new Vector3(relativePosition.x + _layerSize.x * directionX,
                relativePosition.y + _layerSize.y * directionY, 0);
        }

        private bool IsLayerActive(Transform layer, Vector2 targetPosition)
        {
            var layerPosition = layer.position;

            bool insideX = Mathf.Abs(layerPosition.x - targetPosition.x) < _layerSize.x / 2;
            bool insideY = Mathf.Abs(layerPosition.y - targetPosition.y) < _layerSize.y / 2;
            return insideX && insideY;
        }
    }
}