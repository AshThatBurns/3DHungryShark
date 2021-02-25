using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class PreyController : MonoBehaviour
{
    float wanderRadius = 20f;
    float wanderTimer = 2f;

    bool captured;
    float movementSpeed = 25f;
    float moveToMouthSpeed = 20f;
    private float timer;

    Transform target;
    ParticleSystem blood;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.mouthAnchor;
        blood = transform.GetComponentInChildren<ParticleSystem>();
        nav = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        nav.baseOffset = 0;
        nav.height = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // move body to shark mouth
        if (captured)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, moveToMouthSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, moveToMouthSpeed * Time.deltaTime);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                nav.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void getCaptured()
    {
        nav.enabled = false;
        captured = true;
    }

    public void Die()
    {
        transform.position = new Vector3( transform.position.x, 1.5f, transform.position.z);
        Destroy(this);
    }

    public void startBlood()
    {
        blood.Play();
    }
    
}
