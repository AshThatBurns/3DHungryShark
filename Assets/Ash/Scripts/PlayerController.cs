using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float slowSpeed;
    public float fastSpeed;

    private bool autoMove = true;
    private bool mouseCamControl = true;

    private bool hasPrey = false;
    public float shakeVel = 1500f;

    private float currSpeed;
    private float translation;
    private Rigidbody rgb;
    private Vector3 dir;
    private Vector2 oldMouseAxis;
    bool isKillingPrey = false;

    [Header("Object References")]
    public PreyController prey;
    public MouseLook mouseLook;
    public Transform mouthAnchor;
    public Animation anim;

    public static PlayerController instance;

    void Start()
    {
        instance = this;
        rgb = GetComponent<Rigidbody>();

        anim.Play("Armature|slowSwimAnim");
        
        currSpeed = 0;
    }

    void Update()
    {
        #region CAM CONTROL
        // enable/disable auto movement
        if (Input.GetKeyDown(KeyCode.Alpha1))
            autoMove = !autoMove;

        // enable/disable mouse cam control
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mouseCamControl = !mouseCamControl;
        }
        Cursor.lockState = mouseCamControl ? CursorLockMode.Locked : CursorLockMode.None;
        mouseLook.camControl = mouseCamControl;
        #endregion

        #region ANIMATION & KILLING
        if (hasPrey && !isKillingPrey)
        {
            #region SHAKE DETECTION TO KILL
            Vector2 mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (((mouseAxis - oldMouseAxis) / Time.deltaTime).x >= shakeVel)
            {
                StartCoroutine(KillPrey());
            }
            this.oldMouseAxis = mouseAxis;
            #endregion
        }
        else
        {
            // Sprint Animation Script
            if (Input.GetKey(KeyCode.Space))
            {
                if (!anim.IsPlaying("Armature|fastSwimAnim"))
                    anim.CrossFade("Armature|fastSwimAnim", 0.25f);
            }
            else
            {
                if (!anim.IsPlaying("Armature|slowSwimAnim"))
                    anim.CrossFade("Armature|slowSwimAnim", 0.25f);
            }
        }
        #endregion

        #region SPRINT SCRIPT
        // Sprint Script
        if (Input.GetKey(KeyCode.Space))
        {
            currSpeed = fastSpeed;
        }
        else
        {
            currSpeed = slowSpeed;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        translation = autoMove ? (currSpeed * Time.fixedDeltaTime) : 0f;
        dir = transform.forward * translation;
        rgb.MovePosition(transform.position + dir);
    }

    public IEnumerator KillPrey()
    {
        isKillingPrey = true;
        prey.startBlood();

        yield return new WaitForSeconds(2f);

        hasPrey = false;
        isKillingPrey = false;

        prey.Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prey"))
        {
            if (hasPrey == false)
            {
                hasPrey = true; // lock the prey in 
                prey = collision.gameObject.GetComponent<PreyController>();

                prey.GetComponent<Collider>().enabled = false;   // Disable colliders
                prey.getCaptured();
            }
        }
    }
}