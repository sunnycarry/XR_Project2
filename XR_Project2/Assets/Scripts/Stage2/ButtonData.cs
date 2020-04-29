using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    // -239, -80, 84, 232
    // y = 1000
    public float speed;
    public float x_pos;
    public float y_pos;

    public float spawnSecondFromStart;

    public RectTransform judgePoint;

    private RectTransform rect;

    public ScoreManager scoreManager;

    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        SetOriginPos(x_pos, y_pos);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 tmp = rect.localPosition;
        tmp.y -= speed * Time.deltaTime;
        rect.localPosition = tmp;
    }

    public void click()
    {
        Vector3 tmp1 = judgePoint.position;
        tmp1.z = 0;
        Vector3 tmp2 = rect.position;
        tmp2.z = 0;
        float dist = Vector3.Distance(tmp1,tmp2);
        if (dist < 8.0f)
        {
            scoreManager.AddScore(50);


        }
        else if (dist < 15.0f)
        {
            scoreManager.AddScore(34);


        }
        else
        {
            scoreManager.AddScore(0);

        }
        this.gameObject.SetActive(false);

    }

    public void SetOriginPos(float x, float y)
    {
        Vector3 tmp = rect.localPosition;
        tmp.x = x;
        tmp.y = y;
        rect.localPosition = tmp;
    }

    public void SetSpeed(float s)
    {
        this.speed = s;
    }

    public void SetJudgeAndScoreManager(RectTransform rt, ScoreManager sm)
    {
        this.judgePoint = rt;
        this.scoreManager = sm;
    }
}
