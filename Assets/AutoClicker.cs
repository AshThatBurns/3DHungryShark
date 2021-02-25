using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    public int clicks;
    public int numOfAutoclickers = 1;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f / numOfAutoclickers)
        {
            clicks += 1;
            timer = 0f;
            print(clicks);
        }
    }
}
