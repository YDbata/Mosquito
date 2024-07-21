using System;
using UnityEngine;

namespace Mosquito.Stat
{
    public class HP : Stat
    {
        public float MaxHp;
        public event Action<float> onHpChanged;
        
        public float value
        {
            get => _value;
            set
            {
                Debug.Log(value);
                if (value < 0)
                {
                    _value = 0;
                }
                else if (value <= MaxHp && 0 <= value)
                {
                    _value = value;
                    
                    onHpChanged?.Invoke(value);
                }
                
            }
        }

        private float _value;
        
        public HP(float value)
        {
            this.type = StatType.hp;
            this.MaxHp = value;
            this.value = value;
        }
    }
}