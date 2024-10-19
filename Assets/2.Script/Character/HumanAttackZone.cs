using System.Collections;
using System.Collections.Generic;
using Mosquito.AI;
using Mosquito.Character;
using Unity.VisualScripting;
using UnityEngine;
using State = Mosquito.Character.State;

public class HumanAttackZone : MonoBehaviour
{
    [SerializeField]private HumanController humanController;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && humanController.state != State.Attack)
        {
            humanController.attackZoneCol = true;
            //Debug.Log("AttackZone Detection");
        }
    }
}
