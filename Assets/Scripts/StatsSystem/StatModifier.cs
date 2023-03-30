﻿using System;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    [Serializable]
    public class StatModifier
    {
        [field: SerializeField] public Stat Stat { get; private set; }
        [field: SerializeField] public StatModifierType Type { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }

        public float StartTime { get; }

        public StatModifier(Stat stat, StatModifierType type, float duration, float startTime)
        {
            Stat = stat;
            Type = type;
            Duration = duration;
            StartTime = startTime;
        }
    }
}