using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Mosquito.Stat;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MosquitoController : MonoBehaviour
{
    //private Vector3 mPosition;
    //private Vector3 mosquitoPosition;
    public GameObject mosquito;
    
    [Header("Move")]
    [SerializeField] Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fastSpeed = 5f;
    [SerializeField]private float rotationspeed = 1f;
    [SerializeField] private float accrotation = 0.5f;

    [Header("Attack")]
    [SerializeField] private float AttackCoolDown = 2f;

    [Header("Stat")] [SerializeField] StatID Hp;
    [SerializeField] private StatID Stamina;
    [SerializeField] private StatID Speed;
    
    private Stat _hp;
    private Stat _stamina;
    private Stat _speed;
    public bool lockMove
    {
        get { return animator.GetBool(AnimationStrings.LockMove); }
        set
        {
            animator.SetBool(AnimationStrings.LockMove, value);
        }
    }
    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _hp = new Stat();
        _hp.id = Hp;
        _hp.value = Hp.initValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Input.mousePosition;
        Quaternion mosquitoRotation;
        
        // 화면의 픽셀단위 위치를 유니티 좌표로 변환
        // ScreenToViewportPoint : Nomal로 들어옴
        // mosquito 가 0.5,0.5 로 두고 모기 기준 target을 비교하여 좌우 상하를 구해서 
        // target.x*회전속도* mosquito.transform.farword
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(mPosition);
        Vector3 target = mosquito.transform.position + 
                         mosquito.transform.forward + (mosquito.transform.right * (viewportPoint.x-0.5f)*rotationspeed) 
                                                    + (Vector3.up * (viewportPoint.y-0.5f)*rotationspeed);
             
        float dy = target.y - transform.position.y;
        float dx = target.x - transform.position.x;
        float dz = target.z - transform.position.z;
        float Rdirection = viewportPoint.x - 0.5f;
        
        
        //transform.rotation *= Quaternion.Euler(Vector3.up*(viewportPoint.x-0.5f)*90*rotationspeed);
        
        // 위아리좌우 이동 구현
        Vector3 direction = new Vector3(dx, dy, dz);
        Vector3 velocity;
        if (Input.GetKey(KeyCode.W))
        {
            velocity = direction;//dx*30
        }
        else
        {
            velocity = new Vector3(0, 0, 0);
        }
        
        // 좌우 구현(가속)
        if (Input.GetKey(KeyCode.A))
        {
            Rdirection -= accrotation;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rdirection += accrotation;
        }
        
        mosquitoRotation = Quaternion.Euler((Vector3.left * (viewportPoint.y - 0.5f)*180) 
                                            + (Vector3.up*(Rdirection)*rotationspeed + new Vector3(0, transform.eulerAngles.y, 0)));
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            velocity *= fastSpeed;
        }
        else
        {
            velocity *= speed;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // 기본 공격
            velocity = new Vector3(0f, 0f, 0f);
            animator.SetTrigger(AnimationStrings.Attack);
        }

        if (Input.GetMouseButtonDown(1))
        {
            // 강공
            velocity = new Vector3(0f, 0f, 0f);
            animator.SetTrigger(AnimationStrings.SAttack);
        }
        
        transform.rotation = mosquitoRotation;
        if (!lockMove)
        {
            
            rb.velocity = velocity;
        }
    }
}
