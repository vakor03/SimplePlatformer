using Core.Animation;
using Core.Enums;
using Core.Movement.Controller;
using Core.Movement.Data;
using Core.Tools;
using StatsSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private HorizontalMovementData _horizontalMovementData;
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private GroundCheckerData _groundCheckerData;
        [SerializeField] private DirectionalCamerasPair _cameras;

        private Rigidbody2D _rigidbody;

        private bool _isGrounded;

        private AnimationType _currentAnimationType;
        private HorizontalMover _horizontalMover;
        private Jumper _jumper;
        private GroundChecker _groundChecker;

        public void Initialize(IStatValueGiver statValueGiver)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _horizontalMover = new HorizontalMover(_rigidbody, _horizontalMovementData, statValueGiver);
            _jumper = new Jumper(_rigidbody, _jumpData, statValueGiver);
            _groundChecker = new GroundChecker(_groundCheckerData);
        }

        private void Update()
        {
            _isGrounded = _groundChecker.CheckGrounded();

            UpdateAnimations();
            UpdateCameras();
        }

        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionCameras)
            {
                cameraPair.Value.enabled = cameraPair.Key == _horizontalMover.Direction;
            }
        }

        private void UpdateAnimations()
        {
            _animationController.PlayAnimation(AnimationType.Idle, true);
            _animationController.PlayAnimation(AnimationType.Run, _horizontalMover.IsMoving);
            _animationController.PlayAnimation(AnimationType.Jump, !_isGrounded);
            _animationController.PlayAnimation(AnimationType.Fall, !_isGrounded && _rigidbody.velocity.y <= 0);
        }

        public void MoveHorizontally(float direction)
        {
            _horizontalMover.MoveHorizontally(direction);
        }

        public void Jump()
        {
            if (!_isGrounded)
            {
                return;
            }

            _jumper.Jump();
        }
    }
}