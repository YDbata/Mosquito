using UnityEngine;

namespace _2.Script
{
    public class ㅅㄷㄴㅅ : MonoBehaviour
    {
        /*
         *      mPosition = Input.mousePosition;
             mosquitoPosition = mosquito.transform.position;
             
             // 마우스의 z축
             mPosition.z = mosquitoPosition.z - Camera.main.transform.position.z;
             
             // 화면의 픽셀단위 위치를 유니티 좌표로 변환
             Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
             
             float dy = target.y - mosquitoPosition.y;
             float dx = target.x - mosquitoPosition.x;
             float dz = target.z - 0.15f;
             float rotateDegreey =  Mathf.Asin(0.15f/dx)*Mathf.Rad2Deg;
             Vector3 mosquitoEuler = mosquito.transform.rotation.eulerAngles;
             // if (rotateDegreey < mosquitoEuler.y -90)
             // {
             //     rotateDegreey = mosquitoEuler.y -90 + rotateDegreey % (mosquitoEuler.y -90);
             // } else if (rotateDegreey > mosquitoEuler.y + 90)
             // {
             //     rotateDegreey = mosquitoEuler.y + 90 - rotateDegreey % (mosquitoEuler.y + 90);
             //     
             // }

             Debug.Log(dz +" "+dx+" "+rotateDegreey +" "+target+" "+mPosition);
             //Debug.Log(dz + " "+dx + " "+rotateDegreey + " " + old);
             float rotateDegreex = -Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
             if (rotateDegreex < mosquito.transform.rotation.x -90)
             {
                 rotateDegreex = mosquito.transform.rotation.x -90 + rotateDegreex % (mosquito.transform.rotation.x - 90);
             } else if(rotateDegreex > mosquito.transform.rotation.x +90)
                 rotateDegreex = mosquito.transform.rotation.x + 90 - rotateDegreex % (mosquito.transform.rotation.x + 90);
             //Debug.Log(rotateDegreey);
             //Debug.Log(rotateDegreey+" "+rotateDegreex);
             mosquito.transform.rotation = 
                 Quaternion.Euler (0, 
                      rotateDegreey, 0f);
         *
         * 
         */
    }
}