using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float flumpingPower = 100.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] ParticleSystem tailParticles;
    [SerializeField] ParticleSystem rightFinParticles;
    [SerializeField] ParticleSystem leftFinParticles;


    Rigidbody rbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StopAudio();
        tailParticles.Stop();
        rightFinParticles.Stop();
        leftFinParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessFlumps();
        ProcessRotation();
    }

    void ProcessFlumps() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Flump();
        }
        else
        {
            StopFlumping();
        }
    }

    private void StopFlumping()
    {
        StopAudio();
        tailParticles.Stop();
    }

    private void Flump()
    {
        rbody.AddRelativeForce(Vector3.up * flumpingPower * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!tailParticles.isPlaying)
        {
            tailParticles.Play();
        }
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateSeal(Vector3.forward, rightFinParticles);
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateSeal(Vector3.back, leftFinParticles);
        } else
        {
            StopParticles();
        }
    }

    private void StopParticles()
    {
        leftFinParticles.Stop();
        rightFinParticles.Stop();
    }

    private void RotateSeal(Vector3 rotationVector, ParticleSystem particles) {
        ApplyRotation(rotationVector);
        if (!particles.isPlaying) {
            particles.Play();
        }
    }

    void ApplyRotation(Vector3 direction) 
    {
        rbody.freezeRotation = true;
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        rbody.freezeRotation = false;
    }
}
