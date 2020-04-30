using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public float BPM;
    public float speed;
    public GameObject canvas;
    public RectTransform[] judgePoints;
    public ScoreManager scoremanager;
    public AudioSource audioSource;

    public float start_delay;

    // LINE : left 1 | 2 | 3 | 4  right
    private float[] beatMap_1 = new float[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
    private float[] startMap_1;

    private float[] beatMap_2 = new float[] { 2, 5, 8, 11, 15, 16};
    private float[] startMap_2;

    private float[] beatMap_3 = new float[] { 3, 6, 9, 12, 14};
    private float[] startMap_3;

    private float[] beatMap_4 = new float[] { 4, 7, 10, 13};
    private float[] startMap_4;



    private bool[] isFinish;
    private float tmpTime;
    private int[] tmpIndex;
    public GameObject[] buttons;
    public ButtonPool poolManager;
    void Awake()
    {
        startMap_1 = new float[beatMap_1.Length];
        startMap_2 = new float[beatMap_2.Length];
        startMap_3 = new float[beatMap_3.Length];
        startMap_4 = new float[beatMap_4.Length];
        ComputeLeftStartArray();
    }
    // Start is called before the first frame update
    void Start()
    {
        start_delay = 1400.0f / speed;
        StartCoroutine(PlayMusic());
        tmpTime = 0;
        tmpIndex = new int[] { 0, 0, 0, 0 };
        isFinish = new bool[] { false, false, false, false };
    }

    // Update is called once per frame
    void Update()
    {   
        CheckLine1();
        CheckLine2();
        CheckLine3();
        CheckLine4();
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(start_delay);
        audioSource.Play();
    }

    private void ComputeLeftStartArray()
    {
        for (int i = 0; i < beatMap_1.Length; ++i)
        {
            startMap_1[i] = (beatMap_1[i] * 60.0f) / BPM ;

        }

        for (int i = 0; i < beatMap_2.Length; ++i)
        {
            startMap_2[i] = (beatMap_2[i] * 60.0f) / BPM ;

        }

        for (int i = 0; i < beatMap_3.Length; ++i)
        {
            startMap_3[i] = (beatMap_3[i]  * 60.0f) / BPM ;
        }

        for (int i = 0; i < beatMap_4.Length; ++i)
        {
            startMap_4[i] = (beatMap_4[i] * 60.0f) / BPM ;
        }
    }

    private void CheckLine1()
    {
        if (!isFinish[0] && audioSource.time >= startMap_1[tmpIndex[0]])
        {
            tmpIndex[0]++;
            if (tmpIndex[0] >= startMap_1.Length)
            {
                isFinish[0] = true;
                tmpIndex[0] = 0;
            }
            poolManager.Reuse(1);
            /* 
            GameObject g = Instantiate(buttons[0], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[0], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
            */
        }
    }

    private void CheckLine2()
    {
        if (!isFinish[1] && audioSource.time >= startMap_2[tmpIndex[1]])
        {
            tmpIndex[1]++;
            if (tmpIndex[1] >= startMap_2.Length)
            {
                isFinish[1] = true;
                tmpIndex[1] = 0;
            }
            poolManager.Reuse(2);
            /* 
            GameObject g = Instantiate(buttons[1], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[1], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
            */
        }
    }

    private void CheckLine3()
    {
        if (!isFinish[2] && audioSource.time >= startMap_3[tmpIndex[2]])
        {
            tmpIndex[2]++;
            if (tmpIndex[2] >= startMap_3.Length)
            {
                isFinish[2] = true;
                tmpIndex[2] = 0;
            }
            poolManager.Reuse(3);
            /* 
            GameObject g = Instantiate(buttons[2], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[2], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
            */
        }
    }

    private void CheckLine4()
    {
        if (!isFinish[3] && audioSource.time >= startMap_4[tmpIndex[3]])
        {
            tmpIndex[3]++;
            if (tmpIndex[3] >= startMap_4.Length)
            {
                isFinish[3] = true;
                tmpIndex[3] = 0;
            }
            poolManager.Reuse(4);
            /*
            GameObject g = Instantiate(buttons[3], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[3], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
             */
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
