﻿using System;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public Stat(StatType type, float value)
        {
            Type = type;
            Value = value;
        }
        
        public void SetStatValue(float value)
        {
            Value = value;
        }
        
        public static implicit operator float(Stat stat)
        {
            return stat?.Value ?? 0;
        }
        
        public Stat GetCopy()
        {
            return new Stat(Type, Value);
        }
    }
}