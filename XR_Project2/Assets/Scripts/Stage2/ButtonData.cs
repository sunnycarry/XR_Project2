using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    // -239, -80, 84, 232
    // y = 800
    public float speed;
    public float x_pos;
    public float y_pos;

    public float spawnSecondFromStart;

    public RectTransform judgePoint;
    
    private RectTransform rect;

    public ScoreManager scoreManager;

    public ButtonPool poolManager;

    void Start()
    {

    }
    void OnEnable()
    {
        rect = this.GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        Vector3 tmp = rect.localPosition;
        tmp.y -= speed * Time.deltaTime;
        rect.localPosition = tmp;
        if (tmp.y < -800.0f)
        {
            Handheld.Vibrate();
            poolManager.Recycle(this.gameObject);
        }

    }

    public void click()
    {
        Vector3 tmp1 = judgePoint.position;
        tmp1.z = 0;
        Vector3 tmp2 = rect.position;
        tmp2.z = 0;
        float dist = Vector3.Distance(tmp1,tmp2);
        if (dist < 15.0f)
        {
            scoreManager.AddScore(50);


        }
        else if (dist < 30.0f)
        {
            scoreManager.AddScore(34);


        }
        else if(dist > 50.0f)
        {
            scoreManager.AddScore(0);
            Handheld.Vibrate();
        }
        this.gameObject.SetActive(false);
        Debug.Log(dist);
        poolManager.Recycle(this.gameObject);
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

    public void SetJudgePoint(RectTransform rt)
    {
        this.judgePoint = rt;
    }

    public void SetScoreManager(ScoreManager sm)
    {
        this.scoreManager = sm;
    }

    public void SetPoolManager(ButtonPool bp)
    {
        this.poolManager = bp;
    }
}
