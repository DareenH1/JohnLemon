using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime> 0)
        {
            remainingTime -= Time.deltaTime;
        }
            else if (remainingTime < 0)
        {
            remainingTime = 0;
            // GameOver();
            clockText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        clockText.text = $"{minutes:00}:{seconds:00}";
    }
}

