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
    private int[] beatMap_1 = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
    private float[] startMap_1;


    private int[] beatMap_2 = new int[] {2,  8, 11, 14, 15, 16, 17, 18,21 };
    private float[] startMap_2;

    private int[] beatMap_3 = new int[] {3,  6, 7 };
    private float[] startMap_3;

    private int[] beatMap_4 = new int[] {4,  7, 10 };
    private float[] startMap_4;



    private bool[] isFinish;
    private float tmpTime;
    private int[] tmpIndex;
    public GameObject[] buttons;
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
        tmpTime = 0;
        tmpIndex = new int[] { 0, 0, 0, 0 };
        isFinish = new bool[] { false, false, false, false };
    }

    // Update is called once per frame
    void Update()
    {   
        tmpTime += Time.deltaTime;
        CheckLine1();
        CheckLine2();
        CheckLine3();
        CheckLine4();
    }

    private void ComputeLeftStartArray()
    {
        for (int i = 0; i < beatMap_1.Length; ++i)
        {
            startMap_1[i] = ((float)beatMap_1[i] * 60.0f) / BPM ;

        }

        for (int i = 0; i < beatMap_2.Length; ++i)
        {
            startMap_2[i] = ((float)beatMap_2[i] * 60.0f) / BPM ;

        }

        for (int i = 0; i < beatMap_3.Length; ++i)
        {
            startMap_3[i] = ((float)beatMap_3[i] * 60.0f) / BPM ;
            Debug.Log(startMap_3[i]);
        }

        for (int i = 0; i < beatMap_4.Length; ++i)
        {
            startMap_4[i] = ((float)beatMap_4[i] * 60.0f) / BPM ;
            Debug.Log(startMap_4[i]);
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

            GameObject g = Instantiate(buttons[0], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[0], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
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

            GameObject g = Instantiate(buttons[1], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[1], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
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

            GameObject g = Instantiate(buttons[2], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[2], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
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

            GameObject g = Instantiate(buttons[3], canvas.transform);
            g.GetComponent<ButtonData>().SetJudgeAndScoreManager(judgePoints[3], scoremanager);
            g.GetComponent<ButtonData>().SetSpeed(speed);
        }
    }
}
