using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("Canvas")] [SerializeField] private Canvas MainCanvas;
    [SerializeField] private Canvas MenuCanvas;

    private bool menuCanvasActive = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuCanvas.enabled = !menuCanvasActive;
            MainCanvas.enabled = menuCanvasActive;
            Time.timeScale = Convert.ToInt64(menuCanvasActive);
            menuCanvasActive = !menuCanvasActive;
        }
    }
}
