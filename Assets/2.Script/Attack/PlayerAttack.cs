using UnityEngine;

namespace Mosquito.Script
{
    public class PlayerAttack : MonoBehaviour
    {
        public int attackDamage = 10;
        public float knockback = 0.2f;

        public void TryAttack(Collider other)
        {
            Debug.Log(other);
        }
    }
}