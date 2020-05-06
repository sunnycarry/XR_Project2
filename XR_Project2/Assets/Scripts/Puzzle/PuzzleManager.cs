using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] mapSpawnPosition;

    public GameObject[] puzzles;

    public GameObject[] mapInfos;

    private int[] mapDistribute;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ThrowPuzzle();
    }

    void Awake()
    {
        mapDistribute = new int[puzzles.Length];
        InitDistributeArray();
        RandomDistributeArray();
        DistributePosition();

    }

    private void InitDistributeArray()
    {
        for (int i = 0; i < mapDistribute.Length; ++i)
            mapDistribute[i] = i;
    }

    private void RandomDistributeArray()
    {

        for (int i = 0; i < mapDistribute.Length; ++i)
        {
            int tmp = mapDistribute[i];
            int randomIndex = Random.Range(i, mapDistribute.Length);
            mapDistribute[i] = mapDistribute[randomIndex];
            mapDistribute[randomIndex] = tmp;

        }
    }

    private void DistributePosition()
    {
        for (int i = 0; i < puzzles.Length; ++i)
        {
            Vector3 tmpPos = mapSpawnPosition[mapDistribute[i]].GetComponent<RectTransform>().position;
            puzzles[i].GetComponent<RectTransform>().position = tmpPos;
            puzzles[i].GetComponent<PuzzleDrag>().SetOriginPosition(tmpPos);
        }
    }

    public void SetMapPos(int i)
    {
        Vector3 tmpPos = mapSpawnPosition[mapDistribute[i-1]].GetComponent<RectTransform>().position;
        puzzles[i-1].GetComponent<RectTransform>().position = tmpPos;
    }

    public void ThrowPuzzle()
    {
        
        List<int> tmpIDs = new List<int>();
        for (int i = 0; i < puzzles.Length; ++i)
        {
            if (puzzles[i].GetComponent<PuzzleDrag>().GetPositionID() != -1)
            {
                tmpIDs.Add(i+1);
                Debug.Log(i + 1);
            }
        }
        if (tmpIDs.Count == 0)
            return;
        int randomPick = Random.Range(0, tmpIDs.Count);
        int puzzleID = tmpIDs[randomPick];
        mapInfos[puzzles[puzzleID - 1].GetComponent<PuzzleDrag>().GetPositionID() - 1].GetComponent<MapInfo>().setPlaceState(false);
        puzzles[puzzleID-1].GetComponent<PuzzleDrag>().SetPositionID(-1);
        SetMapPos(puzzleID);
        
        tmpIDs.Clear();
    }
}
