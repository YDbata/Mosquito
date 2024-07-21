using Unity.VisualScripting;
using UnityEngine;

namespace Mosquito.Stat
{
    public class Stamina : Stat
    {
        public float MaxStamina;

        public Stamina(float value)
        {
            this.value = value;
            this.MaxStamina = value;
        }
    }
}