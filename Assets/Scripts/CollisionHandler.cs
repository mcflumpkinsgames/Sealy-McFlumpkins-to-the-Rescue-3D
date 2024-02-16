using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;

    AudioSource audioSource;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
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

    void ReloadScene() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
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
        audioSource.PlayOneShot(defeat);
        GetComponent<Movement>().enabled = false;
        Debug.Log("Womp womp, you died");
        Invoke("ReloadScene", loadDelay);
    }

    void StartVictorySequence()
    {
        audioSource.PlayOneShot(victory);
        GetComponent<Movement>().enabled = false;
        Debug.Log("You can't fight the power of the seal team");
        Invoke("LoadNextScene", loadDelay);
    }

}
