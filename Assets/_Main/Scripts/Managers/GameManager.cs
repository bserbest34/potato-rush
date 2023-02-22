using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    
    [Header("OBJECT")]
    [Space]
    [HideInInspector] public ParticleSystem bigMoney;
    [HideInInspector] public CinemachineVirtualCamera finishCam;
    [HideInInspector] public CinemachineVirtualCamera playerCam;
    [HideInInspector] public bool isGameStarted;
    [HideInInspector] public bool finishControl;
    [HideInInspector] public bool winControl;
    private bool finishFirst = false;

    // ======================= ***

    public void StartGame()
    {
        isGameStarted = true;
        bigMoney = GameObject.Find("BigMoney").GetComponent<ParticleSystem>();
        finishCam = GameObject.Find("_FinishCam").GetComponent<CinemachineVirtualCamera>();
        playerCam = GameObject.Find("_Playercam").GetComponent<CinemachineVirtualCamera>();

        GameObject x = GameObject.Find("_Patoto_Main").transform.GetChild(2).gameObject;
        x.GetComponent<TCP2_Demo_AutoRotate>().enabled = true;
    }

    public void FinishCup()
    {       
        finishControl = false;
        finishCam.m_Priority = 20;
    }
    
    public void FinishLevel()
    {
        if (finishFirst == false)
        {
            finishFirst = true;
            bigMoney.Play();
            FindObjectOfType<UIM>().SG();
        }
    }

    public void GameOver()
    {
        isGameStarted = false;
        FindObjectOfType<UIM>().FG();
        Debug.Log("GAME OVER");
    }

}