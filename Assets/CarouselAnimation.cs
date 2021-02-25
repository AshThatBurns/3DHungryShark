using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselAnimation : MonoBehaviour
{
    public Vector3 speed;
    public float distFromCentre;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).localPosition = new Vector3(distFromCentre, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
