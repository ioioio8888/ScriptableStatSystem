using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableStatSystem
{
    public class StatSystem
    {
        [SerializeField]
        public readonly Dictionary<IStatType, Stat> stats = new Dictionary<IStatType, Stat>();

        public StatSystem() { 

        }

        public StatSystem(BaseStats baseStats) {
            foreach (var baseStat in baseStats.Stats)
            {
                stats.Add(baseStat.StatType, new Stat(baseStat.Value));
            }
        }

        public void AddModifier(IStatType type, StatModifier mod) {
            if (!stats.TryGetValue(type, out Stat stat)) {
                stat = new Stat(type);
                stats.Add(type, stat);
            }
            stat.AddModifier(mod);
        }

        public Stat GetStat(IStatType type)
        {
            if (!stats.TryGetValue(type, out Stat stat))
            {
                stat = new Stat(type);
                stats.Add(type, stat);
            }
            return stat;
        }

        public void RemoveModifier(IStatType type, StatModifier mod) {
            if (!stats.TryGetValue(type, out Stat stat))
            {
                return;
            }
            stat.RemoveModifier(mod);
        }
    }
}
