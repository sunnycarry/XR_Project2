using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreBoard;

    public float score;

    public float blackOffset;
    public float blackLimit;
    [SerializeField]
    private RectTransform topblack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBoard.text = score.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
            TopBlackBig();

    }

    public void TopBlackBig()
    {
        if(topblack.localScale.y < blackLimit)
            topblack.localScale += new Vector3(blackOffset, blackOffset, blackOffset);
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
        RefreshScore();
    }

    public void RefreshScore()
    {
        Debug.Log("TODO  "+score);

    }
}
