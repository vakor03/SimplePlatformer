﻿using System;
using UnityEngine;

namespace Player.PlayerAnimation
{
    [RequireComponent(typeof(Animator))]
    public class UnityAnimationController : AnimationController
    {
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
    }
}