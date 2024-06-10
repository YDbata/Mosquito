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
        mPosition.z = mosquitoPosition.z - (mosquitoPosition.z - Camera.main.transform.position.z); //Mathf.Tan(Mathf.Asin((mPosition.x - mosquitoPosition.x)/0.15f))*(mPosition.x - mosquitoPosition.x);//- Camera.main.transform.position.z;

        
        // 화면의 픽셀단위 위치를 유니티 좌표로 변환
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
             
        float dy = target.y - mosquitoPosition.y;
        float dx = target.x - mosquitoPosition.x;
        //target.z = mosquitoPosition.x + dx;//mosquitoPosition.z + (Mathf.Tan(Mathf.Asin(dx/7f))*dx);
        float dz = target.z - mosquitoPosition.z;
        float rotateDegreey =  (Mathf.Atan2(dx, 0.06f)*Mathf.Rad2Deg)*2; 
        Vector3 mosquitoEuler = mosquito.transform.rotation.eulerAngles;


        Debug.Log(target+" "+mosquito.transform.position);
        transform.LookAt(target);
        
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
