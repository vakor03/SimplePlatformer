using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class GroundChecker
    {
        private readonly GroundCheckerData _groundCheckerData;

        public GroundChecker(GroundCheckerData groundCheckerData)
        {
            _groundCheckerData = groundCheckerData;
        }

        public bool CheckGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheckerData.GroundChecker.position, _groundCheckerData.GroundCheckRadius, _groundCheckerData.GroundLayer);
        }
    }
}