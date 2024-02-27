using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float flumpingPower = 100.0f;
    [SerializeField] float rotationSpeed = 100.0f;

    Rigidbody rbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StopAudio();
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
            rbody.AddRelativeForce(Vector3.up * flumpingPower * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else
        {
            StopAudio();
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
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(Vector3.back);
        }
    }

    void ApplyRotation(Vector3 direction) 
    {
        rbody.freezeRotation = true;
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        rbody.freezeRotation = false;
    }
}
