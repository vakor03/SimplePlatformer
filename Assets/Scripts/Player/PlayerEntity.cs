using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _distanceToCheck = 0.5f;
        [SerializeField] private bool _isGrounded;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private float _prevYPosition;

        private void Update()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, _distanceToCheck))
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
            if (direction == 0 || !_isGrounded)
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
            if (_faceRight != direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
    }
}