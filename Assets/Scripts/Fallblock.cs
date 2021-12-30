using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class Fallblock : MonoBehaviour
{
    public GameObject[] blocks;
    void Start()
    {

    }

    private System.Random random = new System.Random();
    private bool safe;
    public bool spawned = false;
    public int score = -1;
    private int maxscore = 0;
    public bool first = true;

    public Text scoreText;
    public Text maxScoreText;
    void Update()
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
            maxscore = maxscore > score ? maxscore : score;
            GameObject block = blocks[random.Next(blocks.Length)];
            Instantiate(block, transform.position, Quaternion.Euler(0, 0, 0));
            spawned = true;
            first = false;
        }

        scoreText.text = score.ToString();
        maxScoreText.text = "Best Score : " + maxscore.ToString();
    }
}
