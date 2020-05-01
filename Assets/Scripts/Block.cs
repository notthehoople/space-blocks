using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] AudioClip breakingSoundEffect;
    [SerializeField] GameObject blockSparklesVPX;

    // Cached reference
    LevelTracker level;
    GameSession gameStatus;

    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<LevelTracker>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakingSoundEffect, Camera.main.transform.position);
        TriggerSparklesVFX();
        Destroy(gameObject);
        level.RemoveBreakableBlock();
        gameStatus.AddToScore();
    }

    // TODO Probably want to get rid of this later
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVPX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
