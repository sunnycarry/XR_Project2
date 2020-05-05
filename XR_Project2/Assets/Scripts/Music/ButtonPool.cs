using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPool : MonoBehaviour
{
    public GameObject btn;
    public Queue<GameObject> btnPool;

    public ButtonManager buttonManager;

    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private RectTransform[] judgePoints;
    [SerializeField]
    private Sprite[] btnImg;
    [SerializeField]
    public GameObject note;

    public Text dist;
    private float speed;
    private float[] spawnPosX = new float[] { -239, -80, 84, 232 };

    public bool isCreate = false;

    void Awake()
    {
       // speed = buttonManager.GetSpeed();
    }
    // Start is called before the first frame update
    void Start()
    {

       // CreatePool(30);
    }

    public void Reuse(int line)
    {
        GameObject b = btnPool.Dequeue();
        b.SetActive(true);
        ButtonData bd = b.GetComponent<ButtonData>();

        bd.SetOriginPos(spawnPosX[line-1], 800);
        bd.SetJudgePoint(judgePoints[line-1]);
        bd.SetDistT(dist);
        b.GetComponent<Image>().sprite = btnImg[(line-1) %2];

    }

    public void Recycle(GameObject b)
    {
        btnPool.Enqueue(b);
        b.SetActive(false);
    }

    public void SetSpeed(float s)
    {
        this.speed = s;
    }

    public void CreatePool(int num)
    {
        // use one stack to reverse the order of accessing Button
        // because the priority in Canvas is from top(first access) to bottom(last success)
        // we should Reuse the button from bottom to top, 
        // when button overlapped, player can click the button below(more close to the end line, create earlier, accessing order is first)
        btnPool = new Queue<GameObject>();
        Stack<GameObject> tmpBtnQueue = new Stack<GameObject>();
        for (int i = 0; i < num; ++i)
        {
            GameObject b = Instantiate(btn, note.transform) as GameObject;
            tmpBtnQueue.Push(b);
            //btnPool.Enqueue(b);
            ButtonData tmpBD = b.GetComponent<ButtonData>();
            tmpBD.SetScoreManager(scoreManager);
            tmpBD.SetPoolManager(this);
            tmpBD.SetSpeed(speed);
            b.SetActive(false);
        }
        for (int i = 0; i < num; ++i)
        {
            btnPool.Enqueue(tmpBtnQueue.Pop());
        }
        isCreate = true;
    }

    public void SetButtonSpeed()
    {
        for (int i = 0; i < 30; ++i)
        {
            GameObject g = btnPool.Dequeue();
            g.GetComponent<ButtonData>().SetSpeed(speed);
            btnPool.Enqueue(g);
        }
    }

    public void SetButtonSpeed(float s)
    {
        for (int i = 0; i < 30; ++i)
        {
            GameObject g = btnPool.Dequeue();
            g.GetComponent<ButtonData>().SetSpeed(s);
            btnPool.Enqueue(g);
        }
    }
}
