using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] mapSpawnPosition;

    public GameObject[] maps;

    private int[] mapDistribute;

    void Awake()
    {
        mapDistribute = new int[maps.Length];
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
        for (int i = 0; i < maps.Length; ++i)
        {
            Vector3 tmpPos = mapSpawnPosition[mapDistribute[i]].GetComponent<RectTransform>().position;
            maps[i].GetComponent<RectTransform>().position = tmpPos;
            maps[i].GetComponent<PuzzleDrag>().SetOriginPosition(tmpPos);
        }
    }

    public void SetMapPos(int i)
    {
        Vector3 tmpPos = mapSpawnPosition[mapDistribute[i-1]].GetComponent<RectTransform>().position;
        maps[i-1].GetComponent<RectTransform>().position = tmpPos;
    }
}
