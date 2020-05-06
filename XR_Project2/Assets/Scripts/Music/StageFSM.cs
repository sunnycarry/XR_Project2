using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFSM : MonoBehaviour
{
    public MusicChooser musicChooser;
    public ButtonPool buttonPool;
    public ButtonManager buttonManager;
    public ScoreManager scoreManager;

    public GameObject StartPanel;
    public GameObject EndPanel;

    [SerializeField]
    private AudioClip passAudioClip;

    [SerializeField]
    private AudioSource AudioFX;
    // Start is called before the first frame update
    void Start()
    {
        StartPanel.SetActive(true);
        EndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickStart()
    {
        StartPanel.SetActive(false);
        // start to play
        musicChooser.RandomChooseMusic();
        while (!musicChooser.checkFinish) continue;
        if (!buttonPool.isCreate)
        {
            buttonPool.CreatePool(30);
        }
        buttonPool.SetButtonSpeed();
        buttonManager.ResetArrayData();
        buttonManager.audioSource.Play();
        buttonManager.isStart = true;
        scoreManager.ResetScore();
    }

    public void StageEnd()
    {
        buttonManager.isStart = true;
        musicChooser.checkFinish = false;
        EndPanel.SetActive(true);
        AudioFX.clip = passAudioClip;
        AudioFX.Play();
    }

    public void ClickRestart()
    {
        EndPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
}
