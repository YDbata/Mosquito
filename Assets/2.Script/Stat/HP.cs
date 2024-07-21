using UnityEngine;

namespace Mosquito.Stat
{
    public class HP : Stat
    {
        public float MaxHp;
        public float value
        {
            get => _value;
            set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }
        
        private float _value;
        public HP(float value)
        {
            this.type = StatType.hp;
            this.value = value;
            this.MaxHp = value;
        }
    }
}