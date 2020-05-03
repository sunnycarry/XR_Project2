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
    public GameObject canvas;

    public Text dist;
    private float speed;
    private float[] spawnPosX = new float[] { -239, -80, 84, 232 };
    void Awake()
    {
        speed = buttonManager.GetSpeed();
    }
    // Start is called before the first frame update
    void Start()
    {
        btnPool = new Queue<GameObject>();
        CreatePool(30);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void CreatePool(int num)
    {
        for (int i = 0; i < num; ++i)
        {
            GameObject b = Instantiate(btn, canvas.transform) as GameObject;
            btnPool.Enqueue(b);
            ButtonData tmpBD = b.GetComponent<ButtonData>();
            tmpBD.SetScoreManager(scoreManager);
            tmpBD.SetPoolManager(this);
            tmpBD.SetSpeed(speed);
            b.GetComponent<LayoutElement>().layoutPriority = num - i;
            b.SetActive(false);

        }
    }
}
