using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] int breakableBlocks; // Serialized for debugging purposes

    // Cached Reference
    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void RemoveBreakableBlock()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}
