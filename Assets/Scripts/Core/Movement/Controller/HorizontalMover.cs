using Core.Enums;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class HorizontalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly HorizontalMovementData _horizontalMovementData;

        public Direction Direction { get; private set; }
        public bool IsMoving { get; private set; }

        public HorizontalMover(Rigidbody2D rigidbody, HorizontalMovementData horizontalMovementData)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
            _horizontalMovementData = horizontalMovementData;
            Direction = _horizontalMovementData.Direction;
        }

        public void MoveHorizontally(float direction)
        {
            if (direction == 0)
            {
                IsMoving = false;
                return;
            }

            IsMoving = true;
            SetDirection(direction);
            Vector2 velocity = new Vector2(direction * _horizontalMovementData.HorizontalSpeed, 0);
            _rigidbody.velocity += velocity;
        }
        
        private void SetDirection(float direction)
        {
            if (( Direction == Direction.Right) != direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}