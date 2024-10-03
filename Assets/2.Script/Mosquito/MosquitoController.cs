using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.Utility;
using Mosquito.CommonSystem;
using Mosquito.Script;
using Mosquito.Stat;
using Mosquito.UI;
using Mosquito.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

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

    [Header("Attack&Hit")]
    [SerializeField] private float AttackCoolDown = 2f;

    private bool isAttack;
    private PlayerAttack attack;

    private float HitCoolDown;
    [SerializeField] private float MaxHitCoolDown = 2f;

    [Header("Stat")] public InGameObjectSpecification stat;
    public float StaminaDecrease = 1f;
    private float StaminaUpdate = 0;

    private float speed = 5f;
    public HP Hp;
    public Stamina stamina;
    private bool IsRest = false;

    [Header("Camera")] [SerializeField] private GameObject basicCamera;
    [SerializeField] private GameObject focusingCamera;
    private bool isBasicCamera = true;

    private LocalizedString LocalizadString; 

    
    
    [SerializeField] private bool isAlive = true;

    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.IsAlive, isAlive);
            Debug.Log("IsAlive " + isAlive);
        }
    }

    public bool lockMove
    {
        get { return animator.GetBool(AnimationStrings.LockMove); }
        set
        {
            animator.SetBool(AnimationStrings.LockMove, value);
        }
    }

    public bool IsAttack
    {
        get { return animator.GetBool(AnimationStrings.IsAttack); }
        set
        {
            animator.SetBool(AnimationStrings.IsAttack, value);
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

    public bool LockRotate
    {
        get { return animator.GetBool(AnimationStrings.LockRotate); }
    }
    
    private MouseController mouseController;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mouseController = GetComponent<MouseController>();
        Hp = new HP(stat[StatType.hp]);
        stamina = new Stamina(stat[StatType.stamina]);
        speed = stat[StatType.speed];
        attack = GetComponent<PlayerAttack>();
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

        #region Hit

        if (HitCoolDown > 0)
        {
            HitCoolDown -= Time.deltaTime;
        }
        
        #endregion

        #region Stamina

        StaminaUpdate = StaminaDecrease*Time.deltaTime;
        

        #endregion
        // 위아리좌우 이동 구현
        Vector3 direction = new Vector3(dx, dy, dz);
        Vector3 velocity;
        Vector3 height = (Vector3.left * (viewportPoint.y - 0.5f) * 180) * (Convert.ToInt32(!LockRotate));
        if (Input.GetKey(KeyCode.W))
        {
            velocity = direction;//dx*30
        }
        else
        {
            velocity = new Vector3(0, 0, 0);
        }
        mosquitoRotation = Quaternion.Euler(height + new Vector3(0, transform.eulerAngles.y, 0));
        
        // 좌우 구현(가속)
        if (Input.GetKey(KeyCode.A))
        {
            Rdirection -= accrotation;
            //velocity = direction;
            mosquitoRotation = Quaternion.Euler(height + (Vector3.up*(Rdirection)*rotationspeed + new Vector3(0, transform.eulerAngles.y, 0)));
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rdirection += accrotation;
            //velocity = direction;
            mosquitoRotation = Quaternion.Euler(height + (Vector3.up*(Rdirection)*rotationspeed + new Vector3(0, transform.eulerAngles.y, 0)));
        }
        
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (stamina.value > 0)
            {
                velocity *= fastSpeed;
                StaminaUpdate += StaminaDecrease * Time.deltaTime;
            }
            else
            {
                TextChange.isStaminaAlert = true;
            }
        }
        
        //공격
        if (Input.GetMouseButtonDown(0) && !isRest)
        {
            // 기본 공격
            velocity = new Vector3(0f, 0f, 0f);
            animator.SetTrigger(AnimationStrings.Attack);
            StaminaUpdate += 5f;

        }
        else
        {
            velocity *= speed;
        }
        
        // 강공격
        if (Input.GetMouseButtonDown(1) && !isRest)
        {
            if (stamina.value > 0)
            {
                // 강공
                velocity = new Vector3(0f, 0f, 0f);
                animator.SetTrigger(AnimationStrings.SAttack);
                StaminaUpdate += 10f;
            }
            else
            {
                TextChange.isStaminaAlert = true;
            }
            
        }
        
        transform.rotation = mosquitoRotation;
        if (!lockMove)
        {
            
            rb.velocity = velocity;
        }

        #region Rest

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // 휴식 키 클릭
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            if (!isRest){
                if (mouseController.currentCursorType == CursorType.Rest)
                {
                    mouseController.SetCursor(CursorType.None);
                    isRest = true;
                    Physics.Raycast(ray, out hit);
                    transform.position = hit.point;
                }
            }
            else
            {
                
                mouseController.SetCursor(CursorType.None);
                isRest = false;
            }
        }
        // 휴식 전에 F키 보이기 및 휴식
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
            // 휴식중
            mouseController.SetCursor(CursorType.None);
            // 스테미나 충전
            StaminaUpdate = -StaminaDecrease * Time.deltaTime*2;
        }
        
        #endregion

        #region StaminaUpdate

        stamina.value -= StaminaUpdate;
        StaminaUpdate = 0;
        

        #endregion

        #region CameraChange

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            basicCamera.SetActive(!isBasicCamera);
            focusingCamera.SetActive(isBasicCamera);

            isBasicCamera = !isBasicCamera;
        }

        #endregion

    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("IsAttack "+IsAttack);
        if (IsAttack)
        {
            attack.TryAttack(other);
        }
        else if (other.gameObject.layer == 7 && HitCoolDown <= 0)
        {
            if (IsAlive)
            {
                Hp.value -= other.GetComponent<ColliderDamage>().attackDamage;
                if (Hp.value <= 0)
                {
                    IsAlive = false;
                    lockMove = true;
                }
                Vector3 knockbackValue = (other.gameObject.transform.position - Vector3.forward).normalized*knockback;
                //transform.position += knockbackValue;
                animator.SetTrigger("Hit");
                Debug.Log("Hit "+Hp.value);
                HitCoolDown = MaxHitCoolDown;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - (transform.forward*knockback), knockbackSpeed*Time.deltaTime);
            
        }
        if(HitCoolDown > 0)
            HitCoolDown -= Time.deltaTime;
    }
    

}
