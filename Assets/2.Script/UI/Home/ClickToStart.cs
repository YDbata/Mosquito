using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickToStart : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]
    private TextMeshProUGUI clicktostartText;

    [SerializeField] private float blinkSpeed = 1f;

    private Color textColor;

    private bool toggle = true; 
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        textColor = clicktostartText.color;
        if (toggle)
        {
            textColor.a -= blinkSpeed*Time.deltaTime;
            if (textColor.a <= 0)
            {
                toggle = !toggle;
            }
        }
        else
        {
            textColor.a += blinkSpeed * Time.deltaTime;
            if (textColor.a >= 1f)
            {
                toggle = !toggle;
            }
        }

        clicktostartText.color = textColor;
        
    }
}
