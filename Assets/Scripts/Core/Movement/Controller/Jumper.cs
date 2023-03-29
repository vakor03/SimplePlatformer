using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;

        public Jumper(Rigidbody2D rigidbody, JumpData jumpData)
        {
            _jumpData = jumpData;
            _rigidbody = rigidbody;
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpData.JumpForce, ForceMode2D.Impulse);
        }
    }
}