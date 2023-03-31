using Core.Movement.Data;
using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;
        private readonly IStatValueGiver _statValueGiver;

        public Jumper(Rigidbody2D rigidbody, JumpData jumpData, IStatValueGiver statValueGiver)
        {
            _jumpData = jumpData;
            _rigidbody = rigidbody;
            _statValueGiver = statValueGiver;
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _statValueGiver.GetStatValue(StatType.JumpForce), ForceMode2D.Impulse);
        }
    }
}