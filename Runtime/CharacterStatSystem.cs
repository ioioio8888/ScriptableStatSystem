using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableStatSystem
{
    public class CharacterStatSystem : MonoBehaviour
    {
        public BaseStats BaseStats;
        [SerializeField]
        public StatSystem StatSystem;

        public void Awake()
        {
            if (BaseStats != null && StatSystem == null)
            {
                StatSystem = new StatSystem(BaseStats);
            }
        }
    }
}