using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // rocket parameters and stats
    float mainThrust = 1000.0f;
    float rotationThrust = 100.0f;
    bool isFlying = false;

    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem mainEngineParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            StartThrusting();
        else
            StopThrusting();
    }
    void ProcessRotation()
    {
        // Making sure the rocket is not able to rotate before leaving the launching pad.
        if (!isFlying)
            return;

        if (Input.GetKey(KeyCode.A))
            RotateLeft();
        else if (Input.GetKey(KeyCode.D))
            RotateRight();
        else
            StopRotating();
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        if (!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }
    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }
    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over. 
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friendly"))
            isFlying = true;
    }


}
