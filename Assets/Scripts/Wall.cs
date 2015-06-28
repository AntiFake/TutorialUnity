using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Sprite damagedSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();         
    }

    public void DamageWall(int loss) 
    {
        spriteRenderer.sprite = damagedSprite;
        hp -= loss;
        if (hp <= 0)
            this.gameObject.SetActive(false);
    }
}