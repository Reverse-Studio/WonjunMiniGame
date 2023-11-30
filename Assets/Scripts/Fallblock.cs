using UnityEngine;
using TMPro;

public class Fallblock : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI maxScoreDisplay;

    private System.Random random = new System.Random();

    private bool safe;
    public bool spawned = false;
    public int score = -1;
    private int maxscore = 0;
    public bool first = true;

    private void Start()
    {
        maxscore = PlayerPrefs.GetInt("Tetris", 0);
    }

    private void Update()
    {
        var x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        transform.position = new Vector3(x, Camera.main.transform.position.y + 2, 0);

        var wonjuns = GameObject.FindGameObjectsWithTag("wonjun");
        safe = true;

        foreach (var wonjun in wonjuns)
        {
            var rigid = wonjun.GetComponent<Rigidbody2D>();
            var velo = rigid.velocity;
            var pos = rigid.position;

            safe = safe && velo == Vector2.zero;
        }

        if (safe && !spawned)
        {
            if (!first)
                score++;
            if (maxscore < score)
            {
                maxscore = score;

                PlayerPrefs.SetInt("Tetris", maxscore);
            }
            maxscore = maxscore > score ? maxscore : score;
            GameObject block = blocks[random.Next(blocks.Length)];
            Instantiate(block, transform.position, Quaternion.Euler(0, 0, 0));
            spawned = true;
            first = false;
        }

        scoreDisplay.text = score.ToString();
        maxScoreDisplay.text = "Best Score : " + maxscore.ToString();
    }
}
