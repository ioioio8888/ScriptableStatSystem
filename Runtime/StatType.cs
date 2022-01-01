using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableStatSystem
{
    [CreateAssetMenu(fileName = "New Stat Type", menuName = "Stats/Stat Type")]
    public class StatType : ScriptableObject, IStatType
    {
        [SerializeField] private new string name = "New Stat Name";
        [SerializeField] private float defaultValue = 0f;

        public string Name => name;
        public float DefaultValue => defaultValue;
    }
}