using System;
using Core.Services.Updater;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputReader
{
    public class ExternalDeviceInputReader : IEntityInputSource, IDisposable
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }

        public ExternalDeviceInputReader()
        {
            ProjectUpdater.Instance.UpdateCaller += OnUpdate;
        }

        public void ResetOneTimeActions()
        {
            Jump = false;
            Attack = false;
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCaller -= OnUpdate;
        }

        private void OnUpdate()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
            }

            if (!IsPointerOverUI && Input.GetButtonDown("Fire1"))
            {
                Attack = true;
            }
        }

        private bool IsPointerOverUI => EventSystem.current.IsPointerOverGameObject();
    }
}