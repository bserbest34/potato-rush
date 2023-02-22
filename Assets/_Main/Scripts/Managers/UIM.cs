using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using ElephantSDK;

public class UIM : Singleton<UIM>
{
    public enum CpiVideoEnum {
        CpiVideo,GamePlay
    }
    [Header("Replaces UI elements")]
    public CpiVideoEnum UI;
    private Button tTB;
    private Button tTR;
    private Button tTC;
    private GameObject sP;
    private GameObject fP;
    private TextMeshProUGUI lvlString;
    public UnityAction OnLevelStart;

    void Start()
    {
        CVido();
        lvlString = transform.Find("LevelBar").GetComponentInChildren<TextMeshProUGUI>();
        tTB = transform.Find("FullscreenButton").GetComponent<Button>();
        fP = transform.Find("FullscreenFail").gameObject;
        tTR = fP.GetComponentInChildren<Button>();
        sP = transform.Find("FullscreenSuccess").gameObject;
        tTC = sP.GetComponentInChildren<Button>();
        lvlString.SetText("LEVEL " + PlayerPrefs.GetInt("Level", 1).ToString());

        tTC.onClick.AddListener(TtC);
        tTR.onClick.AddListener(TtR);
        tTB.onClick.AddListener(TtS);

        sP.SetActive(false);
        fP.SetActive(false);
        tTB.gameObject.SetActive(true);
    }


    public void FG(){
        if (!fP.activeSelf)
            Elephant.LevelFailed(PlayerPrefs.GetInt("Level"));
        fP.SetActive(true);
    }
    private void TtS()
    {
        tTB.onClick.RemoveAllListeners();
        Elephant.LevelStarted(PlayerPrefs.GetInt("Level"));
        tTB.gameObject.SetActive(false);      
        GameManager.Instance.StartGame();
        GameObject.Find("Tutorial UI").SetActive(false);
    }
    private void TtC() {
        tTC.onClick.RemoveAllListeners();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        StartCoroutine(FindObjectOfType<LM>().LLevel());
    }
    private void TtR()
    {
        tTR.onClick.RemoveAllListeners();
        StartCoroutine(FindObjectOfType<LM>().LLevel());
    }
    public void SG()
    {
        if (!sP.activeSelf)
            Elephant.LevelCompleted(PlayerPrefs.GetInt("Level"));
        sP.SetActive(true);
        fP.SetActive(false);
    }

    public void CVido()
    {
        if (UI == CpiVideoEnum.CpiVideo){
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        else if (UI == CpiVideoEnum.GamePlay)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        
    }
}


