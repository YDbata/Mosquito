using System.Collections;
using System.Collections.Generic;
using Mosquito.Stat;
using UnityEngine;
using UnityEngine.Events;

public class Damegeable : MonoBehaviour
{

    public UnityEvent<Vector3> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<float, float> healthChanged;

    private Animator animator;

    private MosquitoController player;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<MosquitoController>();
    }


    // public bool GetHit(float attackDamage, Vector3 attackloc, float knockback)
    // {
    //     if (player.IsAlive)
    //     {
    //         player.Hp.value -= attackDamage;
    //         // 넉백 주기(캐릭터 - 모기) 방향으로
    //         Vector3 knockbackValue = (attackloc - Vector3.forward).normalized*knockback;
    //         player.transform.position += knockbackValue;
    //         animator.SetTrigger("Hit");
    //         Debug.Log("Hit");
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
