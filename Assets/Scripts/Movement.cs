using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMS
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;

    // CACHE
    Rigidbody rb;
    AudioSource audioSource;

    // STATE

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();    
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // relative to transform coordinates
            // Vector3.up = 0,1,0
            rb.AddRelativeForce(Vector3.up *  mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
        } else {
                audioSource.Stop();
            }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);

        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freeze physics
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
