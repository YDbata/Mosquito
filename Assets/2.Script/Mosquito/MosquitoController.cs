using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Mosquito.Stat;
using Mosquito.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class MosquitoController : MonoBehaviour
{
    //private Vector3 mPosition;
    //private Vector3 mosquitoPosition;
    public GameObject mosquito;
    
    [Header("Move")]
    [SerializeField] Animator animator;
    
    [SerializeField] private float fastSpeed = 5f;
    [SerializeField]private float rotationspeed = 1f;
    [SerializeField] private float accrotation = 0.5f;
    [SerializeField] private float knockback = 2f;
    [SerializeField] private float knockbackSpeed = 5f;
    [SerializeField] private float restPointDistance = 0.4f;

    [Header("Attack")]
    [SerializeField] private float AttackCoolDown = 2f;

    [Header("Stat")] public InGameObjectSpecification stat;

    private float speed = 5f;
    float hp;
    private float stamina;
    private bool IsRest = false;
    

    public bool lockMove
    {
        get { return animator.GetBool(AnimationStrings.LockMove); }
        set
        {
            animator.SetBool(AnimationStrings.LockMove, value);
        }
    }

    public bool isRest
    {
        get { return animator.GetBool(AnimationStrings.IsRest); }
        set
        {
            IsRest = value;
            animator.SetBool(AnimationStrings.IsRest, value);
        }
    }
    
    private MouseController mouseController;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseController = GetComponent<MouseController>();
        hp = stat[StatType.hp];
        stamina = stat[StatType.stamina];
        speed = stat[StatType.speed];
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
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (!isRest)
        {
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer != 7)
            {
                if (Vector3.Distance(transform.position, hit.point) < restPointDistance)
                {
                    mouseController.SetCursor(CursorType.Rest);
                }
                else
                {
                    mouseController.SetCursor(CursorType.None);
                }
                //Debug.Log(hit.transform.name+" "+ hit.rigidbody.gameObject.layer);
                

            }
        }
        else
        {
            mouseController.SetCursor(CursorType.None);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            if (!isRest)
            {

                if (mouseController.currentCursorType == CursorType.Rest)
                {
                    mouseController.SetCursor(CursorType.None);
                    isRest = true;
                }
            }
            else
            {
                mouseController.SetCursor(CursorType.None);
                isRest = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position - (transform.forward*knockback), knockbackSpeed*Time.deltaTime);
    }
}
