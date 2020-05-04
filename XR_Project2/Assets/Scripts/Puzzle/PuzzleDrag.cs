using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PuzzleStatus
{
    Idle,
    Picked
}

public class PuzzleDrag : MonoBehaviour
{
    PuzzleStatus puzzleStatus;

    public float distThreshold = 150;
    // id symbolized correct position(order)
    [SerializeField]
    private int id;

    // save the area id
    [SerializeField]
    private int positionID;


    [SerializeField]
    private Vector3 originPos;

    // should be 4*4 size, that is, having 16 children
    public GameObject puzzleMap;

    public MapManager mapManager;

    public PuzzleManager puzzleManager;


    // Start is called before the first frame update
    void Start()
    {
        positionID = -1;
        puzzleStatus = new PuzzleStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleStatus == PuzzleStatus.Picked)
        {
            Vector3 pointPos = Input.mousePosition;
            this.GetComponent<RectTransform>().position = pointPos;

        }
    }

    public void PointDown()
    {
        puzzleStatus = PuzzleStatus.Picked;
        mapManager.PlayMusic("pickup");
        if (positionID != -1)
        {
            puzzleMap.transform.GetChild(positionID-1).GetComponent<MapInfo>().setPlaceState(false);
        }
    }

    public void PointUp()
    {
        puzzleStatus = PuzzleStatus.Idle;
        mapManager.PlayMusic("putdown");

        bool findNearestMap = false;
        float minDist = 200;
        int minIndex = 0;
        int curIndex = 0;
        foreach (Transform child in puzzleMap.transform)
        {
            Vector3 mapPos = child.GetComponent<RectTransform>().position;
            Vector3 touchPos = this.GetComponent<RectTransform>().position;
            float tmpDist = Vector3.Distance(mapPos, touchPos);

            if (tmpDist < distThreshold)
            {
                findNearestMap = true;
                if (minDist > tmpDist)
                {
                    minDist = tmpDist;
                    minIndex = curIndex;
                }
            }
            curIndex++;
        }

        if (!findNearestMap || puzzleMap.transform.GetChild(minIndex).GetComponent<MapInfo>().getPlaceState())
        {
            puzzleManager.SetMapPos(this.id);
            //this.gameObject.GetComponent<RectTransform>().position = originPos;
            positionID = -1;
        }

        else
        {
            if (findNearestMap)
            {
                this.GetComponent<RectTransform>().position = puzzleMap.transform.GetChild(minIndex).GetComponent<RectTransform>().position;
                positionID = puzzleMap.transform.GetChild(minIndex).GetComponent<MapInfo>().getMapID();
                puzzleMap.transform.GetChild(minIndex).GetComponent<MapInfo>().setPlaceState(true);
            }
            if (mapManager.checkMapFull())
            {
                if (mapManager.checkAnswerCorrect())
                    mapManager.ShowPass();
                else
                    mapManager.ShowFailed();
            }

        }
            
    }

    public bool compareAns()
    {
        if (positionID == id)
            return true;
        return false;
    }

    public void SetOriginPosition(Vector3 pos)
    {
        originPos = pos;
    }

}
