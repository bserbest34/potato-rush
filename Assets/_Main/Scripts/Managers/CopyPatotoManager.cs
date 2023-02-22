using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;
using Dlite.Games.Managers;
using MoreMountains.NiceVibrations;

public class CopyPatotoManager : MonoBehaviour
{
    private Transform player;
    private Vector3 velocity = Vector3.zero;
    [HideInInspector] public float movementTime = 0;
    private PatotoListManager patotoListManager;
    private ConstraintSource cs;
    public bool controlBoolPatoto = false;

    // =============================

    private void Awake() 
    {
        patotoListManager = GameObject.Find("PATOTO_LIST").GetComponent<PatotoListManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 
        ref velocity, movementTime);
    }
    
    public void ParentCons(GameObject patoto)
    {
        patoto.gameObject.GetComponent<CopyPatotoManager>().enabled = true;
        patoto.gameObject.GetComponent<CopyPatotoManager>().movementTime = patotoListManager.patotos.Count * 0.03f;

        if (patotoListManager.patotos.Count <= 1)
            cs.sourceTransform = player;
        else
            cs.sourceTransform = patotoListManager.patotos[patotoListManager.patotos.Count - 2].transform;

        cs.weight = 1;
        patoto.GetComponent<ParentConstraint>().AddSource(cs);
        patoto.gameObject.GetComponent<ParentConstraint>().SetTranslationOffset(0, new Vector3(0, -1.5f, 0));
        patoto.gameObject.GetComponent<ParentConstraint>().enabled = true;
        patoto.gameObject.GetComponent<ParentConstraint>().constraintActive = true;
        patoto.gameObject.GetComponentInChildren<Patoto>().normalModel.GetComponent<TCP2_Demo_AutoRotate>().enabled = true;

        if (patotoListManager.patotos.Count > 2)
            StartCoroutine(AtmRushFailing());

    }

    private IEnumerator AtmRushFailing()
    {
        for (int i = 0; i < patotoListManager.patotos.Count-1; i++)
        {
            if (patotoListManager.patotos.Count > 1)
            {
                patotoListManager.patotos[patotoListManager.patotos.Count-1-i].transform.DOScale(1.1f, 0.05f);
                
                yield return new WaitForSeconds(0.03f);

                patotoListManager.patotos[patotoListManager.patotos.Count-1-i].transform.DOScale(1f, 0.05f);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        // COLLECT NEW PATOTO
       if (other.CompareTag("Collect"))
       {
            Patoto patoto = other.gameObject.GetComponentInChildren<Patoto>();
            if (patoto.throwed == false)
            {   
                other.tag ="Untagged";
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();

                patotoListManager.patotos.Add(other.gameObject);
                ParentCons(other.gameObject);
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.Warning);
            }

       }
       //  FINISH
        if (other.gameObject.CompareTag("Finish"))
            GameManager.Instance.FinishCup();
    }

}