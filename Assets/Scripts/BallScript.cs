using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rigid;

    public float power;
    private int score;
    private int bestScorei = 0;
    public float bounce;
    void Start()
    {
        mainCamera = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        bestScorei = PlayerPrefs.GetInt("Fry", 0);
    }
    public Text text;
    public Text bestScore;

    private bool flying = true;

    private bool click = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) click = true;

        bestScore.text = "Best Score : " + bestScorei.ToString();
        text.text = score.ToString();
    }

    private void FixedUpdate()
    {
        if (click)
        {
            click = false;
            
            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(pos, Vector2.zero, LayerMask.GetMask("Ray"));

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    float x = transform.position.x - pos.x;

                    x *= bounce;

                    var force = new Vector2(x, 1).normalized * power;
                    rigid.velocity = force;
                    score++;

                    flying = true;

                    bestScorei = bestScorei < score ? score : bestScorei;
                }
            }
        }
        if (!flying)
            rigid.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "bot")
        {
            score = 0;
            PlayerPrefs.SetInt("Fry", bestScorei);
            flying = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "left")
        {
            Debug.Log("l");
            rigid.AddForce(new Vector2(0.5f, 0), ForceMode2D.Impulse);
        }
        else if (collision.collider.tag == "right")
        {
            Debug.Log("r");
            rigid.AddForce(new Vector2(-0.5f, 0), ForceMode2D.Impulse);
        }
    }
}