using System;
using System.Collections;
using System.Collections.Generic;
using Mosquito.Stat;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Mosquito.Stat
{
    public class Stat
    {
        public StatType type;
        public event Action<float> onValueChanged;
        public event Action<float> onValueModifiedChanged;
        
        
        public float value
        {
            get => _value;
            set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }

        public float valueModified
        {
            get => _valueModified;
            set
            {
                _valueModified = value;
                onValueModifiedChanged?.Invoke(value);
            }
        }

        private float _value;
        private float _valueModified;
        private List<StatModifier> _modifiers = new List<StatModifier>();


        
        /// <summary>
        /// 100% = 10000.0??
        /// </summary>
        /// <returns></returns>
        private float CalcValueModified()
        {
            float sumFlatAdd = 0;
            float sumPercentAdd = 0;
            float sumPercentMul = 0;

            foreach (var modifier in _modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.FlatAdd:
                        sumFlatAdd += modifier.value;
                        break;
                    case StatModType.PercentAdd:
                        sumPercentAdd += modifier.value / 10000.0f;
                        break;
                    case StatModType.PercentMul:
                        sumPercentMul *= modifier.value / 10000.0f;
                        break;
                    default:
                        break;
                }
            }

            return ((value + sumFlatAdd) + (value * sumPercentAdd) + (value * sumPercentMul));
        }
        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            valueModified = CalcValueModified();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            valueModified = CalcValueModified();
        }
        
    }
    
}
