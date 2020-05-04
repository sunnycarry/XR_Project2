using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public AudioClip[] soundFX_AC;
    public AudioClip[] soundBGM_AC;
    public AudioSource soundFX;
    public AudioSource soundBGM;
    public PuzzleDrag[] puzzles;
    public MapInfo[] maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkAnswerCorrect()
    {
        for (int i = 0; i < puzzles.Length; ++i)
        {
            if (!puzzles[i].compareAns())
                return false;
        }
        return true;
    }

    public bool checkMapFull()
    {
        for (int i = 0; i < maps.Length; ++i)
        {
            if (maps[i].getPlaceState() == false)
                return false;
        }
        return true;
    }

    public void ShowPass()
    {
        PlayMusic("correct");
        Debug.Log("pass");
    }

    public void ShowFailed()
    {
        PlayMusic("wrong");
        Debug.Log("fail");
    }

    public void PlayMusic(string s)
    {
        for (int i = 0; i < soundFX_AC.Length; ++i)
        {
            if (soundFX_AC[i].name == s)
                soundFX.clip = soundFX_AC[i];
        }
        soundFX.Play();
    }
}

