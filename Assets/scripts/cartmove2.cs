using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class cartmove2 : MonoBehaviour {

    // Settings
    public float MoveSpeed = 66;
    public float MaxSpeed = 20;
    public float Drag = 0.98f;
    public float SteerAngle = 14;
    public float Traction = 0.1f;
    public GameObject[] obj; 
    private Rigidbody rb;
         // prefabs
    // objects in scene

    // Variables
    private Vector3 MoveForce;
    
    public float BounceStrength = 0.7f;
public float MinimumBounceSpeed = 3f;
void Start()
{
    rb = GetComponent<Rigidbody>();
}
   


    // Update is called once per frame
  void FixedUpdate() {

        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        transform.position += MoveForce * Time.fixedDeltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.fixedDeltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.fixedDeltaTime) * MoveForce.magnitude;

  }

  private void OnCollisionEnter(Collision collision)
{
    // Get the first point where the cart hit the object
    ContactPoint contact = collision.contacts[0];

    // Collision normal points away from the object
    Vector3 normal = contact.normal;

    // Only bounce from mostly vertical walls/objects
    if (Mathf.Abs(normal.y) < 0.5f)
    {
        // Reflect the movement direction
        MoveForce = Vector3.Reflect(MoveForce, normal);

        // Keep approximately the same speed
        MoveForce *= BounceStrength;

        // Make sure the cart doesn't bounce too weakly
        if (MoveForce.magnitude < MinimumBounceSpeed)
        {
            MoveForce = MoveForce.normalized * MinimumBounceSpeed;
        }

        // Apply the new velocity immediately
        rb.linearVelocity = new Vector3(
            MoveForce.x,
            rb.linearVelocity.y,
            MoveForce.z
        );
    }
}
    // =========================
    // Traction
    // =========================



    // =========================
    // Give movement to Rigidbody
    // =========================

 



        
    //     GameObject[] currentObjects = GameObject.FindGameObjectsWithTag("collective");


    //     if(currentObjects.Length < 25)
    //     Instantiate
    //         (
    //             obj[UnityEngine.Random.Range(0, obj.Length)],
    //             new Vector3(RandomPlace(), 1f, RandomPlace()),
    //             Quaternion.identity
    //         );
    //     // StartCoroutine(Create3dObjects());
        
        

    // }

    // void Start()
    // {
       

    //     for(int i = 0; i < 25; i++)
    //     {
            
    //         Instantiate
    //         (
    //             obj[UnityEngine.Random.Range(0, obj.Length)],
    //             new Vector3(RandomPlace(), 1f, RandomPlace()),
    //             Quaternion.identity
    //         );

            
    //     }
    // }

   

    // private void OnTriggerEnter(Collider other)
    // {
    //    if(other.gameObject.tag == "collective")
    //     {
            
            
    //         Destroy(other.gameObject);
    //     }
    // }


    

    // private float RandomPlace()
    // {
    //     return UnityEngine.Random.Range(-33f, 33f);
    // }

   



    // private IEnumerator Create3dObjects()
    // {
    //     GameObject[] currentObjects = GameObject.FindGameObjectsWithTag("collective");
    //     while (currentObjects.Length < 25)
    //     {
    //         Instantiate
    //         (
    //             obj[UnityEngine.Random.Range(0, obj.Length)],
    //             new Vector3(RandomPlace(), 1f, RandomPlace()),
    //             Quaternion.identity
    //         );
    //         yield return new WaitForSeconds(3f);    
    //     }

    
       
        
    //     // Instantiate
    //     //     (
    //     //         obj[UnityEngine.Random.Range(0, obj.Length)],
    //     //         new Vector3(RandomPlace(), 1f, RandomPlace()),
    //     //         Quaternion.identity
    //     //     );

    //     // yield return new WaitForSeconds(3f);    
       
    // }

    
}