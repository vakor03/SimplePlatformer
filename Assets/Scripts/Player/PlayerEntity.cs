using Core.Enums;
using Core.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _isGrounded;
        
        [Header("GroundChecker")]
        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private DirectionalCamerasPair _cameras;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            if (Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer))
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }

        public void MoveHorizontally(float direction)
        {
            if (direction == 0)
            {
                return;
            }

            SetDirection(direction);
            Vector2 velocity = new Vector2(direction * _horizontalSpeed, 0);
            _rigidbody.velocity += velocity;
        }

        public void Jump()
        {
            if (!_isGrounded)
            {
                return;
            }

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right) != direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach (var cameraPair in _cameras.DirectionCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            }
        }
    }
}