using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
                Debug.Log("I flumped to the finish now my reward is fibsh");
                break;
            default:
                Debug.Log("Owwww wtf");
                ReloadScene();
                break;
        }
    }

    void ReloadScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
