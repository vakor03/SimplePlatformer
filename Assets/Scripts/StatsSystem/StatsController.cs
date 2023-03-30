using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    public class StatsController : IDisposable, IStatValueGiver
    {
        private readonly List<Stat> _currentStats;
        private readonly List<StatModifier> _activeModifiers;

        public StatsController(List<Stat> currentStats)
        {
            _currentStats = currentStats;
            _activeModifiers = new List<StatModifier>();
            ProjectUpdater.Instance.UpdateCaller += OnUpdate;
        }

        public float GetStatValue(StatType statType)
        {
            return _currentStats.Find(stat => stat.Type == statType).Value;
        }

        public void ProcessModifier(StatModifier modifier)
        {
            var stat = _currentStats.Find(stat => stat.Type == modifier.Stat.Type);
            if (stat == null)
            {
                return;
            }

            var addedValue = modifier.Type == StatModifierType.Additive
                ? modifier.Stat + stat
                : stat * modifier.Stat;

            stat.SetStatValue(addedValue);
            if (modifier.Duration < 0)
            {
                return;
            }

            if (_activeModifiers.Contains(modifier))
            {
                _activeModifiers.Remove(modifier);
            }
            else
            {
                var addedStat = new Stat(modifier.Stat.Type, -addedValue);
                var tempModifier = new StatModifier(addedStat, StatModifierType.Additive, modifier.Duration, Time.time);
                _activeModifiers.Add(tempModifier);
            }
        }
        
        private void OnUpdate()
        {
            if (_activeModifiers.Count == 0)
            {
                return;
            }
           
            var expiredModifiers = 
                _activeModifiers.Where(m => m.StartTime+ m.Duration >= Time.time);

            foreach (var modifier in expiredModifiers)
            {
                ProcessModifier(modifier);
            }
        }

        public void Dispose()
        {
            ProjectUpdater.Instance.UpdateCaller -= OnUpdate;
        }
    }
}