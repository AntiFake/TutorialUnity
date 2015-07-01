using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Sprite damagedSprite;
    public int hp = 4;
    public AudioClip chop1;
    public AudioClip chop2;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();         
    }

    public void DamageWall(int loss) 
    {
        spriteRenderer.sprite = damagedSprite;
        hp -= loss;
        SoundManager.instance.RandomizeSfx(chop1, chop2);
        if (hp <= 0)
            this.gameObject.SetActive(false);
    }
}