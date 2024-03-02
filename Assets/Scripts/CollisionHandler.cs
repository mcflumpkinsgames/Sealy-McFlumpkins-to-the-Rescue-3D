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

    void Start()
    {
        isTransitioning = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isTransitioning && isActiveAndEnabled) 
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("this thing is friendly");
                    break;
                case "Fibsh":
                    Debug.Log("Fibsh!!");
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
        } else {
            Debug.Log("look at me look at me I'm a winner");
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(defeat);
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadScene", defeat.length);
    }

    void StartVictorySequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(victory);
        Debug.Log("You can't fight the power of the seal team");
        victoryParticles.Play();
        Invoke("LoadNextScene", victory.length);
    }

}
