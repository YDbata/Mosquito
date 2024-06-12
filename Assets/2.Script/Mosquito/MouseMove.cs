using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    //private Vector3 mPosition;
    //private Vector3 mosquitoPosition;
    public GameObject mosquito;
    [SerializeField] private float speed = 5f;
    [SerializeField]private float rotationspeed = 1f;
    [Header("바라보는 각도 조절")] public float zPositionAlpha;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 mosquitoPosition = mosquito.transform.position;
             
        // 마우스의 z축
        //mPosition.z = mosquitoPosition.z - (mosquitoPosition.z - Camera.main.transform.position.z); //Mathf.Tan(Mathf.Asin((mPosition.x - mosquitoPosition.x)/0.15f))*(mPosition.x - mosquitoPosition.x);//- Camera.main.transform.position.z;

        
        // 화면의 픽셀단위 위치를 유니티 좌표로 변환
        // ScreenToViewportPoint : Nomal로 들어옴
        // mosquito 가 0.5,0.5 로 두고 모기 기준 target을 비교하여 좌우 상하를 구해서 
        // target.x*회전속도* mosquito.transform.farword
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(mPosition);
        Vector3 target = mosquito.transform.position + 
                         mosquito.transform.forward + (mosquito.transform.right * (viewportPoint.x-0.5f)*rotationspeed) 
                                                    + (Vector3.up * (viewportPoint.y-0.5f)*rotationspeed);
        //Vector3 target = Camera.main.ScreenToViewportPoint(mPosition);
             
        float dy = target.y - mosquitoPosition.y;
        float dx = target.x - mosquitoPosition.x;
        //target.z = mosquitoPosition.x + dx;//mosquitoPosition.z + (Mathf.Tan(Mathf.Asin(dx/7f))*dx);
        float dz = target.z - mosquitoPosition.z;
        float rotateDegreey =  (Mathf.Atan2(dx, 0.06f)*Mathf.Rad2Deg)*2; 
        Vector3 mosquitoEuler = mosquito.transform.rotation.eulerAngles;


        Debug.Log(target+" "+mosquito.transform.position);
        transform.rotation *= Quaternion.Euler(Vector3.left * (viewportPoint.y - 0.5f) * rotationspeed);
        transform.rotation *= Quaternion.Euler(Vector3.up*(viewportPoint.x-0.5f)*rotationspeed);
        //transform.LookAt(target);
        
        // 위아리좌우 이동 구현
        //Debug.Log(dx+" "+dy);
        Vector3 direction = new Vector3(dx, dy, dz);
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector3 velocity;
        if (Input.GetKey(KeyCode.W))
        {
            velocity = direction;//dx*30
        }
        else
        {
            velocity = new Vector3(0, 0, 0);
        }
        velocity *= speed;
        rb.velocity = velocity;

    }
}
