using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    //private float[] beatMap_1 = new float[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
    private float[] mainStep = new float[] { 3.946644f, 4.287982f, 4.458662f, 4.62932f, 4.94932f, 5.397324f, 6.165329f, 6.783991f, 7.381315f, 8.106644f, 8.725328f, 9.279978f, 9.962653f, 10.70932f, 11.32798f, 11.94664f, 12.58664f, 13.37599f, 13.99465f, 14.57066f, 15.21066f, 17.53599f, 17.85599f, 18.23998f, 18.55998f, 18.85866f, 19.19998f, 19.54132f, 20.01066f, 20.37331f, 20.54399f, 20.86399f, 21.16265f, 21.50399f, 21.86664f, 22.22932f, 22.69866f, 23.06132f, 23.23199f, 23.53066f, 23.85066f, 24.17066f, 24.49066f, 24.76798f, 25.30132f, 25.64265f, 25.81331f, 26.13331f, 26.47465f, 26.79465f, 27.15732f, 27.47732f, 27.96798f, 28.30932f, 28.47998f, 28.79998f, 29.11998f, 29.46132f, 29.82399f, 30.14399f, 30.65599f, 30.99732f, 31.16798f, 31.50932f, 31.82932f, 32.17066f, 32.512f, 32.81066f, 33.25866f, 33.57866f, 33.77066f, 34.06932f, 34.432f, 34.77331f, 35.13599f, 35.43465f, 35.92533f, 36.33066f, 36.50132f, 36.82132f, 37.22664f, 37.52533f, 37.84533f, 38.25066f, 38.80533f, 39.08265f, 39.27465f, 39.70132f, 39.89331f, 40.21331f, 40.38399f, 40.55465f, 41.152f, 41.51465f, 41.72798f, 42.112f, 42.53866f, 42.87998f, 43.09331f, 43.26399f, 43.90399f, 44.15998f, 44.35199f, 44.73598f, 45.20533f, 45.37599f, 45.58932f, 45.78131f, 46.16533f, 46.57066f, 46.86932f, 47.03998f, 47.18932f, 47.50932f, 47.82932f, 48.44798f, 49.13066f, 49.49331f, 49.68533f, 50.02665f, 50.38932f, 50.73066f, 50.87998f, 51.05066f, 51.37066f, 51.86132f, 52.22399f, 52.41599f, 52.71465f, 53.01331f, 53.58932f, 54.57066f, 54.91199f, 55.10399f, 55.38131f, 55.72265f, 56.02132f, 56.21331f, 56.38399f, 56.66132f, 57.152f, 57.45066f, 57.64265f, 57.85599f, 58.17599f, 58.49599f, 59.30664f, 59.81866f, 60.13866f, 60.33066f, 60.50132f, 60.77866f, 61.11998f, 61.35465f, 61.54664f, 61.69599f, 62.01599f, 62.48533f, 62.82664f, 62.97599f, 63.29599f, 63.44533f, 63.61599f, 64.04266f, 64.25599f, 64.63998f, 65.06664f, 65.40798f, 65.59998f, 65.85599f, 66.21866f, 66.55997f, 66.752f, 66.94399f, 67.34932f, 67.79733f, 68.22399f, 68.39465f, 68.69331f, 68.88533f, 69.05598f, 69.61066f, 70.20798f, 70.93331f, 71.78664f, 72.14932f, 72.31998f, 72.66132f, 72.95998f, 73.32265f, 73.62132f, 73.98399f, 74.47465f, 74.77331f, 74.98664f, 75.28533f, 75.62664f, 75.96798f, 76.26665f, 76.62932f, 77.14131f, 77.50399f, 77.69598f, 77.99465f, 78.33598f, 78.63465f, 78.97598f, 79.29599f, 79.76533f, 80.38399f, 81.04533f, 81.66399f, 82.51733f, 83.02932f, 83.62664f, 84.28798f, 85.11998f, 85.65331f, 86.20798f, 86.95465f, 87.70132f, 88.08533f, 88.27732f, 88.59732f, 88.95998f, 89.27998f, 89.59998f, 90.26131f, };
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
        start_delay = 1400.0f / speed;
        AddDelayToMainStep();
        RandomDistributeStartLine();

    }
    // Start is called before the first frame update
    void Start()
    {

        tmpTime = 0;
        tmpIndex = new int[] { 0, 0, 0, 0 };
        isFinish = new bool[] { false, false, false, false };
        int index = 1;
        while (File.Exists(fileName + index+".txt"))
            index++;

        writer = new StreamWriter(fileName + index+".txt", true);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            writer.Write(audioSource.time + "f ,");
        }
        else if (Input.GetKeyDown(KeyCode.A))
            writer.Close();
        CheckLine1();
        CheckLine2();
        CheckLine3();
        CheckLine4();
    }

    private void AddDelayToMainStep()
    {
        for (int i = 0; i < mainStep.Length; ++i)
            mainStep[i] -= (start_delay + 0.1f);
    }

    private void RandomDistributeStartLine()
    {
        for (int i = 0; i < mainStep.Length; ++i)
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
}
