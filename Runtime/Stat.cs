using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using UnityEngine;

namespace ScriptableStatSystem
{

    [Serializable]
    public class Stat : IStat
    {
        [SerializeField]public float BaseValue { get; set; }

        public virtual float value
        {
            get
            {
                if (isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    lastValue = CalculateFinalValue();
                    isDirty = false;
                }
                return lastValue;
            }

        }

        protected bool isDirty = true;
        protected float lastValue;
        protected float lastBaseValue;
        protected readonly List<StatModifier> statModifiers;
        protected readonly ReadOnlyCollection<StatModifier> readOnlyStatModifiers;

        public Stat()
        {
            statModifiers = new List<StatModifier>();
            readOnlyStatModifiers = statModifiers.AsReadOnly();
        }

        public Stat(float _baseValue) : this()
        {
            BaseValue = _baseValue;
        }

        public Stat(IStatType statType) : this()
        {
            BaseValue = statType.DefaultValue;
        }

        public virtual void UpdateBaseValue(float _value) {
            isDirty = true;
            BaseValue = _value;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        protected int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }
            else
            {
                return 0; //a.order == b.order
            }
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i] == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }


        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];
                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}