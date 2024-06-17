using System;
using System.Collections;
using System.Collections.Generic;
using Mosquito.Stat;
using TMPro;
using UnityEngine;

namespace Mosquito.Stat
{
    public class Stat
    {
        public StatID id;

        public float value
        {
            get => _value;
            set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }

        public int valueModified
        {
            get => _valueModified;
            set
            {
                _valueModified = value;
                onValueModifiedChanged?.Invoke(value);
            }
        }

        private float _value;
        private int _valueModified;
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public event Action<float> onValueChanged;
        public event Action<int> onValueModifiedChanged;
        
        /// <summary>
        /// 100% = 10000.0??
        /// </summary>
        /// <returns></returns>
        private int CalcValueModified()
        {
            double sumFlatAdd = 0;
            double sumPercentAdd = 0;
            double sumPercentMul = 0;

            foreach (var modifier in _modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.FlatAdd:
                        sumFlatAdd += modifier.value;
                        break;
                    case StatModType.PercentAdd:
                        sumPercentAdd += modifier.value / 10000.0;
                        break;
                    case StatModType.PercentMul:
                        sumPercentMul *= modifier.value / 10000.0;
                        break;
                    default:
                        break;
                }
            }

            return (int)((value + sumFlatAdd) + (value * sumPercentAdd) + (value * sumPercentMul));
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
