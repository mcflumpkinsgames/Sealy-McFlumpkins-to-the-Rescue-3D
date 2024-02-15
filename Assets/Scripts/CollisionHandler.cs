using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
                int nextScene = currentSceneIndex+1;
                if (nextScene < SceneManager.sceneCountInBuildSettings) {
                    SceneManager.LoadScene(nextScene);
                } else {
                    Debug.Log("look at me look at me I'm a winner");
                }
                break;
            default:
                Debug.Log("Owwww wtf");
                ReloadScene();
                break;
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(currentSceneIndex);
    }

}
