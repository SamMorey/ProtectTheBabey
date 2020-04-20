using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public float minSizeMod = 0.8f;
    public float maxSizeMod = 1.5f;


    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Intialize()
    {
        SetSprite();
        SetSize();
        SetRotation();
    }

    public void SetSprite()
    {
        int random = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[random];
    }

    public void SetSize()
    {
        float size = Random.Range(minSizeMod, maxSizeMod);
        transform.localScale *= size;
    }

    public void SetRotation()
    {
        float random = Random.Range(-360f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, random);
    }

}
