using System;
using System.Collections;
using System.Collections.Generic;
using Mosquito.Stat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mosquito.Stat
{
    public enum StatModType
    {
        None,
        FlatAdd,
        PercentAdd,
        PercentMul,
    }

    [Serializable]
    public class StatModifier
    {
        [FormerlySerializedAs("StatType")] public StatType statDataModel;
        public StatModType modType;
        public float value;
    }
}
