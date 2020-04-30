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

    public float distThreshold = 100;
    // id symbolized correct position(order)
    [SerializeField]
    private int id;

    [SerializeField]
    private Vector3 originPos;

    // should be 4*4 size, that is, having 16 children
    public GameObject puzzleMap;


    // Start is called before the first frame update
    void Start()
    {
        puzzleStatus = new PuzzleStatus();
        originPos = this.GetComponent<RectTransform>().position;

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

    }

    public void PointUp()
    {
        puzzleStatus = PuzzleStatus.Idle;

        //TODO
        //search for nearest position
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

       // if (!findNearestMap)
            // this.GetComponent<RectTransform>().position = originPos;
            //else
        if (findNearestMap)
            this.GetComponent<RectTransform>().position = puzzleMap.transform.GetChild(minIndex).GetComponent<RectTransform>().position;
        

    }
}
