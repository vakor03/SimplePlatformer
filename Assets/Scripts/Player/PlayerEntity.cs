using Core.Enums;
using Core.Tools;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
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
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Run, _isRunning);
             PlayAnimation(AnimationType.Jump, !_isGrounded);
            PlayAnimation(AnimationType.Fall, !_isGrounded && _rigidbody.velocity.y <= 0);
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

        private void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle|| _currentAnimationType != animationType)
                {
                    return;
                }

                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }
            if (_currentAnimationType > animationType)
            {
                return;
            }

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
    }
}