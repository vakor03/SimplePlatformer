﻿using Core.Enums;
using Player;
using UnityEngine;

namespace Core.Animation
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