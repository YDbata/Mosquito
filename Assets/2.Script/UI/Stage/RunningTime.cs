using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class RunningTime : MonoBehaviour
{
    [Header("UI")] [SerializeField] private TextMeshProUGUI text;

    public Stopwatch sw;
    // Start is called before the first frame update
    void Start()
    {
        sw = new Stopwatch();
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = zeroAdd(sw.Elapsed.Minutes.ToString()) 
                    + ":" + zeroAdd(sw.Elapsed.Seconds.ToString()) 
                    + ":" + zeroAdd(sw.Elapsed.Milliseconds.ToString());
    }
    
    /// <summary>
    /// 타이머에 시간이 한자리 수일때 0을 붙여주는 함수
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private string zeroAdd(string time)
    {
        if (time.Length == 1)
        {
            return "0" + time;
        }

        return time;
    } 
}
