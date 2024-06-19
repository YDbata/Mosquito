using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Mosquito.Stat
{
    
    public enum StatType
    {
        hp,
        stamina,
        speed
    }
    
    [Serializable]
    public class StatDataModel
    {
        public StatType type;
        public float value;
    }
}