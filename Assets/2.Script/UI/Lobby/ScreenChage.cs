using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenChage : MonoBehaviour
{

    [Header("Background Info")]
    [SerializeField] private Image leftImg;
    [SerializeField] private Image rightImg;

    [SerializeField] private TextMeshProUGUI choiceBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(viewportPoint);
        // left
        if (viewportPoint.x < 0.5)
        {
            leftImg.enabled = false;
            if (Input.GetMouseButtonDown(0))
            {
                choiceBtn.SetText("MOSQUITO!");
                
            }
        }
        else
        {
            
            leftImg.enabled = true;
        }
    }
}
