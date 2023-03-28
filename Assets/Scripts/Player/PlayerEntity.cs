using Core.Enums;
using Core.Tools;
using Player.PlayerAnimation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;

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

        private bool _isRunning;
        private AnimationType _currentAnimationType;

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

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Run, _isRunning);
            _animationController.PlayAnimation(AnimationType.Jump, !_isGrounded);
            _animationController.PlayAnimation(AnimationType.Fall, !_isGrounded && _rigidbody.velocity.y <= 0);
        }

        public void MoveHorizontally(float direction)
        {
            if (direction == 0)
            {
                _isRunning = false;
                    return;
            }

            _isRunning = true;
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