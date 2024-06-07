using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    private Vector3 mPosition;
    private Vector3 mosquitoPosition;
    public GameObject mosquito;
    [Header("바라보는 각도 조절")] public float zPositionAlpha;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mPosition = Input.mousePosition;
        mosquitoPosition = mosquito.transform.position;
        
        // 마우스의 z축
        mPosition.z = mosquitoPosition.z - Camera.main.transform.position.z;
        
        // 화면의 픽셀단위 위치를 유니티 좌표로 변환
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        
        float dy = target.y - mosquitoPosition.y;
        float dx = target.x - mosquitoPosition.x;
        float dz = target.z - mosquitoPosition.z;
        float rotateDegreey =  Mathf.Atan2(dx, dz)*Mathf.Rad2Deg;
        if (rotateDegreey < -90)
        {
            rotateDegreey = -90 - rotateDegreey % 90;
        } else if(rotateDegreey > 90)
            rotateDegreey = 90 - rotateDegreey % 90;

        float rotateDegreex = -Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        if (rotateDegreex < -90)
        {
            rotateDegreex = -90 - rotateDegreex % 90;
        } else if(rotateDegreex > 90)
            rotateDegreex = 90 - rotateDegreex % 90;
        //Debug.Log(rotateDegreey);
        mosquito.transform.rotation = Quaternion.Euler (rotateDegreex, rotateDegreey, 0f);
        
    }
}
