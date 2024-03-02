using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    CollisionHandler collisionHandler;

    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            collisionHandler.LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisionsEnabled();
        }

    }

    private void ToggleCollisionsEnabled()
    {
        if (collisionHandler.enabled)
        {
            collisionHandler.enabled = false;
            Debug.Log("collisions disabled");
        }
        else
        {
            collisionHandler.enabled = true;
            Debug.Log("collisions enabled");
        }
    }
}
