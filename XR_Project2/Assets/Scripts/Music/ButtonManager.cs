using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public float speed;

    public RectTransform[] judgePoints;
    public ScoreManager scoremanager;
    public AudioSource audioSource;

    public float start_delay;
    // LINE : left 1 | 2 | 3 | 4  right
    //private float[] beatMap_1 = new float[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
    private List<float> mainStep = new List<float>() { };
    private List<float> beatMap_1 = new List<float>();


    private List<float> beatMap_2 = new List<float>();


    private List<float> beatMap_3 = new List<float>();


    private List<float> beatMap_4 = new List<float>();




    private bool[] isFinish;
    private float tmpTime;
    private int[] tmpIndex;
    public GameObject[] buttons;
    public ButtonPool poolManager;

    public string fileName = "forest_";
    StreamWriter writer;

    void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        start_delay = 1400.0f / speed;
        AddDelayToMainStep();
        RandomDistributeStartLine();
        tmpTime = 0;
        tmpIndex = new int[] { 0, 0, 0, 0 };
        isFinish = new bool[] { false, false, false, false };

        /*int index = 1;
        while (File.Exists(fileName + index+".txt"))
            index++;

        writer = new StreamWriter(fileName + index+".txt", true);
        */
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            writer.Write(audioSource.time + "f ,");
        }
        else if (Input.GetKeyDown(KeyCode.A))
            writer.Close();
    */
        CheckLine1();
        CheckLine2();
        CheckLine3();
        CheckLine4();
    }

    private void AddDelayToMainStep()
    {
        for (int i = 0; i < mainStep.Count; ++i)
            mainStep[i] -= (start_delay + 0.1f);
    }

    private void RandomDistributeStartLine()
    {
        for (int i = 0; i < mainStep.Count; ++i)
        {
            int line = Random.Range(0, 4);
            if (line == 0)
                beatMap_1.Add(mainStep[i]);
            else if(line == 1)
                beatMap_2.Add(mainStep[i]);
            else if(line == 2)
                beatMap_3.Add(mainStep[i]);
            else if(line == 3)
                beatMap_4.Add(mainStep[i]);
        }
    }

    private void CheckLine1()
    {
        if (!isFinish[0] && audioSource.time >= beatMap_1[tmpIndex[0]])
        {
            tmpIndex[0]++;
            if (tmpIndex[0] >= beatMap_1.Count)
            {
                isFinish[0] = true;
                tmpIndex[0] = 0;
            }
            poolManager.Reuse(1);

        }
    }

    private void CheckLine2()
    {
        if (!isFinish[1] && audioSource.time >= beatMap_2[tmpIndex[1]])
        {
            tmpIndex[1]++;
            if (tmpIndex[1] >= beatMap_2.Count)
            {
                isFinish[1] = true;
                tmpIndex[1] = 0;
            }
            poolManager.Reuse(2);

        }
    }

    private void CheckLine3()
    {
        if (!isFinish[2] && audioSource.time >= beatMap_3[tmpIndex[2]])
        {
            tmpIndex[2]++;
            if (tmpIndex[2] >= beatMap_3.Count)
            {
                isFinish[2] = true;
                tmpIndex[2] = 0;
            }
            poolManager.Reuse(3);

        }
    }

    private void CheckLine4()
    {
        if (!isFinish[3] && audioSource.time >= beatMap_4[tmpIndex[3]])
        {
            tmpIndex[3]++;
            if (tmpIndex[3] >= beatMap_4.Count)
            {
                isFinish[3] = true;
                tmpIndex[3] = 0;
            }
            poolManager.Reuse(4);

        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float s)
    {
        this.speed = s;
    }

    public void SetMainStep(float[] step)
    {
        for (int i = 0; i < step.Length; ++i)
        {
            mainStep.Add(step[i]);
        }
    }
}
