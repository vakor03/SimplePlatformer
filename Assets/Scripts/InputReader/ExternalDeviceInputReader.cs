using Player;
using UnityEngine;

namespace InputReader
{
    public class ExternalDeviceInputReader : IEntityInputSource
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }

        public void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Attack = true;
            }
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
            Attack = false;
        }
    }
}