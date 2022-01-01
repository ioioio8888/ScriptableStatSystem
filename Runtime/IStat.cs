namespace ScriptableStatSystem
{
    public interface IStat
    {
        public float BaseValue { get;  set; }

        void UpdateBaseValue(float _value);
        void AddModifier(StatModifier mod);
        bool RemoveModifier(StatModifier mod);
        bool RemoveAllModifiersFromSource(object source);
    }
}