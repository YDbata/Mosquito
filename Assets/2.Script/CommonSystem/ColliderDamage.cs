using Unity.VisualScripting;
using UnityEngine;

namespace Mosquito.CommonSystem
{
    public class ColliderDamage : MonoBehaviour
    {
        public int attackDamage = 10;
        public float knockback = 0.2f;
        public bool hasTakeDamage=false;
    }
}