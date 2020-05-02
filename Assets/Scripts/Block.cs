using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] AudioClip breakingSoundEffect;
    [SerializeField] GameObject blockSparklesVPX;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    LevelTracker level;
    GameSession gameStatus;

    // State variables
    [SerializeField] int timesHit; // TODO only serialized for debugging purposes

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
            HandleHit();
        }
    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array on " + gameObject.name);
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
