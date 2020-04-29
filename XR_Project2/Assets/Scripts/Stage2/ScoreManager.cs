using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreBoard;

    public float score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBoard.text = score.ToString();
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public void RefreshScore()
    {
        Debug.Log("TODO  "+score);

    }
}
