using System;
using UnityEngine;

namespace Core.Services.Updater
{
    public class ProjectUpdater : MonoBehaviour, IProjectUpdater
    {
        public static IProjectUpdater Instance { get; private set; }

        public event Action UpdateCaller;
        public event Action FixedUpdateCaller;
        public event Action LateUpdateCaller;

        private bool _isPaused;
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (IsPaused == value)
                {
                    return;
                }
                _isPaused = value;
                Time.timeScale = _isPaused ? 0 : 1;
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (IsPaused)
            {
                return;
            }
            UpdateCaller?.Invoke();
        }

        private void FixedUpdate()
        {
            if (IsPaused)
            {
                return;
            }
            FixedUpdateCaller?.Invoke();
        }

        private void LateUpdate()
        {
            if (IsPaused)
            {
                return;
            }
            LateUpdateCaller?.Invoke();
        }
    }
}