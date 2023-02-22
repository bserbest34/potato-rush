using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Animations;

public class Patoto : MonoBehaviour
{
    [Header("VARIABLES")]
    [Space]
    [Tooltip("0=NORMAL SHELL // 1=NORMAL NO SHELL // 2= SHELL CHOPPING // 3=NO SHELL CHOPPING")]
    public int patotoType;

    public bool mainPatoto;
    [HideInInspector] public bool rubbish;
    [HideInInspector] public bool throwed = false;
    [HideInInspector] public bool fry;
    [HideInInspector] public bool peel = false;
    [HideInInspector] public bool chop = false;
    [HideInInspector] public bool inChild = false;
    [HideInInspector] public GameObject chopModel;
    [HideInInspector] public GameObject noShellModel;
    public SkinnedMeshRenderer[] chopMesh;
    public MeshRenderer shellModel;
    public GameObject mainModel;
    public GameObject normalModel;

    // =========================== 

    private void Start() 
    {
       chopModel = transform.GetChild(0).gameObject;
       noShellModel = transform.GetChild(1).gameObject;
       for (int i = 0; i < 20; i++)
       {
           chopMesh[i] = chopModel.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>();
       }
      Destroy(GameObject.Find("[DOTween]"));
    }

    public void FryDown()
    {    
        if (mainPatoto == false)  
            mainModel.GetComponent<ParentConstraint>().translationAxis = Axis.Z;

        if (patotoType == 2 || patotoType == 3)
        {
            if (throwed == false || mainPatoto == true)
            {
                mainModel.transform.DOLocalMoveY(-0.5f, 0.5f).OnComplete(() =>
                {
                    mainModel.transform.DOLocalMoveY(1.4f, 0.5f);
                });;;
            }
            else if (throwed == true)
            {
                DOTween.Kill(this.gameObject);
                mainModel.transform.DOLocalMoveZ(mainModel.transform.position.z + 2f, 0.5f);
                mainModel.transform.DOLocalMoveY(-0.5f, 0.5f).OnComplete(() => 
                {
                    if (mainPatoto == false) 
                    {
                        mainModel.GetComponent<ParentConstraint>().translationAxis = Axis.Z | Axis.Y;
                        throwed = false;
                    } 

               });;;
            }
        }
        else if (patotoType == 0 || patotoType == 1)
        {
            if (throwed == false)
            {
                mainModel.transform.DOLocalMoveY(-0.5f, 0.5f).OnComplete(() =>
                {
                    mainModel.transform.DOLocalMoveY(1.7f, 0.5f);
                });;;
            }
            else if (throwed == true)
            {
                DOTween.Kill(this.gameObject);
                mainModel.transform.DOLocalMoveZ(mainModel.transform.position.z + 2f, 0.5f);
                mainModel.transform.DOLocalMoveY(-0.5f, 0.5f).OnComplete(() => 
                {
                    if (mainPatoto == false) 
                    {
                        mainModel.GetComponent<ParentConstraint>().translationAxis = Axis.Z | Axis.Y;
                        throwed = false;
                    }                        
               });;;;
            }
        }
    }

    public void CupsDpown()
    {
        mainModel.transform.DOKill();
        mainModel.transform.DOLocalMoveY(-0.5f, 0.5f);
        for (int i = 0; i < chopMesh.Length; i++)
        {
           chopMesh[i].enabled = false;
        }
    }

    public void ControlFinish()
    {
        if (patotoType == 0 || patotoType == 1 || patotoType == 2 || fry == false)
        {
            rubbish = true;
            if (mainPatoto == false)
            {
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
                mainModel.transform.DOLocalMoveX(-5f, 0.5f);   
                this.gameObject.tag = "Untagged";
                mainModel.GetComponent<CopyPatotoManager>().enabled = false;
                mainModel.GetComponent<ParentConstraint>().enabled = false;
                patotoList.patotos.Remove(this.mainModel);
            }
            else if (mainPatoto == true)
            {
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
                if (patotoList.patotos[0].transform == null)
                {
                    GameManager.Instance.playerCam.m_Follow = null;
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[1].transform;
                }
                mainModel.transform.DOLocalMoveX(-5f, 0.5f);   
                this.gameObject.tag = "Untagged";
                GameManager.Instance.isGameStarted = false;
                
                if (GameManager.Instance.winControl == true && throwed == false)
                {
                    GameManager.Instance.FinishLevel();
                    if (rubbish == true)
                    {
                        GameManager.Instance.playerCam.m_Follow = null;
                        GameManager.Instance.finishCam.m_Follow = patotoList.patotos[1].transform;
                    }
                }
            }
        }
        else
        {
            if (mainPatoto == false)
            {
                GameManager.Instance.playerCam.m_Follow = null;
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
                int x = patotoList.patotos.IndexOf(this.gameObject.GetComponent<Patoto>().mainModel);
               // Debug.Log(x);
                GameManager.Instance.finishCam.m_Follow = patotoList.patotos[x].transform;
                mainModel.GetComponent<ParentConstraint>().constraintActive = false;
                mainModel.GetComponent<CopyPatotoManager>().enabled = false;
                mainModel.GetComponent<ParentConstraint>().enabled = false;
                mainModel.transform.DOMoveZ(mainModel.transform.position.z + 100f, 15f);
            }
        }
    }

    /// =========== ***

    public void FirstTouch()
    {    
        if (mainPatoto == false)
        {
            PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
            int index =  patotoList.patotos.IndexOf(this.gameObject.GetComponent<Patoto>().mainModel);

            // FIRST OBJECT
            if (index == patotoList.patotos.Count - 1)
            {      
                patotoList.patotos.Remove(this.mainModel);
                Destroy(this.mainModel);
            }
            // ANY MID OBJECT TOUCH
            else
            {
                //Debug.Log(" INDEX " + index);     
                for (int i = index + 1; i < patotoList.patotos.Count; i++)
                { 
                    patotoList.patotos[i].GetComponentInChildren<Patoto>().Jump();
                }
                
                if (index > 0)
                {
                    patotoList.patotos.RemoveRange(index, patotoList.patotos.Count - index);
                    Debug.Log(" INDEX " + index); 
                }
                patotoList.patotos.Remove(mainModel);
                Destroy(mainModel);

                if (patotoList.patotos.Count == 0)
                  patotoList.patotos.Add(GameObject.Find("_Patoto_Main"));
            }
        }
    
    }

    public void Jump()
    { 
        if (mainPatoto == false)
        {
            throwed = true;
            PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
            this.gameObject.tag = "Untagged";
            mainModel.tag = "Collect";

            mainModel.GetComponent<CopyPatotoManager>().enabled = false;
            mainModel.GetComponent<ParentConstraint>().enabled = false;
            mainModel.GetComponent<ParentConstraint>().constraintActive = false;

            if (mainModel.GetComponent<ParentConstraint>().sourceCount > 0)
                mainModel.GetComponent<ParentConstraint>().RemoveSource(0);

            //CALCULATE RANDOM JUMP POS && PATOTO HIT THE WALL AND SCATTER
            float x = Random.Range(2.5f, -2.5f);
            float z = Random.Range(15f, 20f);
            Vector3 jumpPos = new Vector3(x, 0f, z);
            Vector3 currentPos = mainModel.transform.position;
            mainModel.transform.DOJump(currentPos + jumpPos, 1f, 1, 0.5f).SetId(this.gameObject);
            this.gameObject.tag = "Patoto";
            StartCoroutine(EnableCollect());
        }
    }
    
    private IEnumerator EnableCollect()
    {
        yield return new WaitForSeconds(1.5f);
        throwed = false;
    }
    
    public void RubbishWall()
    {    
        if (mainPatoto == false)
        {
            PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
            int index =  patotoList.patotos.IndexOf(this.gameObject.GetComponent<Patoto>().mainModel);

            if (index == patotoList.patotos.Count - 1)
            {      
                patotoList.patotos.Remove(this.mainModel);
                Destroy(this.mainModel);
            }
        }
  
    }

}