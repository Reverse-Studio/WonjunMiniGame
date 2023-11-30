using TMPro;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI bestScoreDisplay;

    private Camera mainCamera;
    private Rigidbody2D rigid;

    public float power;
    private int score;
    private int bestScore = 0;
    public float bounce;

    private void Awake()
    {
        mainCamera = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bestScore = DataManager.Instance.GetMaxScore(DataManager.Game.Bound);
    }

    private void Update()
    {
        bestScoreDisplay.text = "Best Score : " + bestScore.ToString();
        scoreDisplay.text = score.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Ray")))
            {
                float x = transform.position.x - ray.origin.x;

                x *= bounce;

                var force = new Vector2(x, 1).normalized * power;
                rigid.velocity = force;
                score++;

                bestScore = bestScore < score ? score : bestScore;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("bot"))
        {
            score = 0;
            DataManager.Instance.SetMaxScore(DataManager.Game.Bound, bestScore);
        }
    }
}