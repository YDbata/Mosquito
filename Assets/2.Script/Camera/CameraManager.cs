using UnityEngine;

namespace Mosquito.Camera
{
    
    public class CameraManager : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private GameObject basicCamera;
        [SerializeField] private GameObject focusingCamera;
        private bool isBasicCamera = true;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                basicCamera.SetActive(!isBasicCamera);
                focusingCamera.SetActive(isBasicCamera);

                isBasicCamera = !isBasicCamera;
            }
            
        }    
        
    }
}