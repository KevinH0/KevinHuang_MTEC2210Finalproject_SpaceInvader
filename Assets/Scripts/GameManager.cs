using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int addScore;
    public TextMeshPro scoreText;
    public Transform scoreValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score\n" + score.ToString();
    }
    public void IncreaseScore(int value)
    {
        score += value;
    }
}
