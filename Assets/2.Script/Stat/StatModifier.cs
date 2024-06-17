using System;
using System.Collections;
using System.Collections.Generic;
using Mosquito.Stat;
using UnityEngine;

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
        public StatID statID;
        public StatModType modType;
        public int value;
    }
}
