using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Rigidbody rb;

    void Awake()
    {
        // Creates the floor.
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.localScale = new Vector3(6.0f, 1.0f, 6.0f);
        floor.transform.position = new Vector3(0.0f, -0.5f, 0.0f);

        Material matColor = new Material(Shader.Find("Standard"));
        matColor.color = new Color32(32, 32, 128, 255);
        floor.GetComponent<Renderer>().material = matColor;

        transform.position = new Vector3(-3.0f, 0.0f, 0.0f);

        Camera.main.transform.position = new Vector3(6.0f, 4.0f, 6.0f);
        Camera.main.transform.localEulerAngles = new Vector3(26.0f, -135.0f, 0.0f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Moves the GameObject using it's transform.
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        // Moves the GameObject to the left of the origin.
        if (transform.position.x > 3.0f)
        {
            transform.position = new Vector3(-3.0f, 0.0f, 0.0f);
        }

        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
    }
}