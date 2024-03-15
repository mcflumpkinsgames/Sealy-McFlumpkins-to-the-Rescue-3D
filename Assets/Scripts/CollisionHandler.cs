using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;

    [SerializeField] ParticleSystem victoryParticles;
    [SerializeField] ParticleSystem crashParticles;

    int currentSceneIndex;
    AudioSource audioSource;

    bool isTransitioning;
    bool collisionsEnabled = true;

    void Start()
    {
        isTransitioning = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessCheats();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isTransitioning) 
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartVictorySequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        int nextScene = currentSceneIndex+1;
        if (nextScene < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextScene);
        } 
    }

    void StartCrashSequence()
    {
        if (collisionsEnabled) 
        {
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(defeat);
            GetComponent<Movement>().enabled = false;
            crashParticles.Play();
            Invoke("ReloadScene", defeat.length);
        }
    }

    void StartVictorySequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victory);
        victoryParticles.Play();
        Invoke("LoadNextScene", victory.length);
    }

    void ProcessCheats()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisionsEnabled();
        }
    }

    private void ToggleCollisionsEnabled()
    {
        if (collisionsEnabled) 
        {
            collisionsEnabled = false;
            Debug.Log("collisions disabled");
        } else {
            collisionsEnabled = true;
            Debug.Log("collisions enabled");
        }
    }
}
