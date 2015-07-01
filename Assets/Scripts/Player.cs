using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int pointsForFood = 10;
    public int pointsForSoda = 20;
    public float restartLevelDelay = 1f;
    public Text foodText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameoverSound;

    private Animator animator;
    private int food;

    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit)) {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
            SoundManager.instance.PlaySingle(gameoverSound);
            SoundManager.instance.musicSource.Stop();
        }
    }

    void Update()
    {
        if (!GameManager.instance.playersTurn)
            return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        //preventing the player to move diagonally
        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        foodText.text = "Food: " + food;
        CheckIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            this.enabled = false;
        }
        if (collider.tag == "Food")
        {
            food += pointsForFood;
            foodText.text = "Food: " + food;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            collider.gameObject.SetActive(false);
        }
        if (collider.tag == "Soda")
        {
            food += pointsForSoda;
            foodText.text = "Food: " + food;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            collider.gameObject.SetActive(false);
        }
    }
}
