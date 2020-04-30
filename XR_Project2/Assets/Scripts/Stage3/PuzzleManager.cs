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
        RandomInitMapPosition();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomInitMapPosition()
    {
        if(mapSpawnPosition.Length != maps.Length)
        {
            Debug.Log("Size different");
            return;
        }

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
            maps[i].GetComponent<RectTransform>().position = mapSpawnPosition[mapDistribute[i]].GetComponent<RectTransform>().position;
        }
    }

}
