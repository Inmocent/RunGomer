using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine.AI;
public class cartmove : MonoBehaviour {

    // Settings
    public float MoveSpeed = 66;
    public float MaxSpeed = 20;
    public float Drag = 0.98f;
    public float SteerAngle = 14;
    public float Traction = 0.1f;

    public TextMeshProUGUI money;
    public TextMeshProUGUI end;

    public AudioSource moveSound;
    public float fadeSpeed = 2f;
    

    private int moneysaved = 0;
    public GameObject[] obj; 
         // prefabs
    // objects in scene

    // Variables
    private Vector3 MoveForce;
    




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



        
        GameObject[] currentObjects = GameObject.FindGameObjectsWithTag("collective");


        if(currentObjects.Length < 25)
        Instantiate
            (
                obj[UnityEngine.Random.Range(0, obj.Length)],
                new Vector3(RandomPlace(), 1f, RandomPlace()),
                Quaternion.identity
            );



        
        
        float moveInput = Input.GetAxis("Vertical");

// If pressing W or S → play sound
if (Mathf.Abs(moveInput) > 0.1f)
{
    if (!moveSound.isPlaying)
        moveSound.Play();

    // Fade IN
    moveSound.volume = Mathf.Lerp(moveSound.volume, 1f, fadeSpeed * Time.deltaTime);
}
else
{
    // Fade OUT
    moveSound.volume = Mathf.Lerp(moveSound.volume, 0f, fadeSpeed * Time.deltaTime);

    // Stop when almost silent
    if (moveSound.volume < 0.05f)
    {
        moveSound.Stop();
    }
}

    }

    void Start()
    {
       

        for(int i = 0; i < 25; i++)
        {
            
            Instantiate
            (
                obj[UnityEngine.Random.Range(0, obj.Length)],
                new Vector3(RandomPlace(), 1f, RandomPlace()),
                Quaternion.identity
            );

            
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "collective")
        {
            
            moneysaved += RandomAmount();
            Destroy(other.gameObject);
            money.text = "money saved: " + moneysaved + " $";
        }

        if(other.gameObject.tag == "Finish")
        {
            end.text = "You earned: " + moneysaved + " $";
            GameOver();
        }
    }



    


    

    private float RandomPlace()
    {
        return UnityEngine.Random.Range(-33f, 33f);
    }

    private int RandomAmount()
    {
        return UnityEngine.Random.Range(19, 299);
    }

    void GameOver()
    {
    // Stop time
    Time.timeScale = 0f;

    Debug.Log("Game Over!");
    }

   

    
}