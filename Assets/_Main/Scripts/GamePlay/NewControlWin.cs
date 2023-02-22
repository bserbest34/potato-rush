using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewControlWin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Patoto"))
        {
            PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
            if (other.gameObject.GetComponent<Patoto>().rubbish == false)
            {
                FindObjectOfType<FinishControl>().newWin = true;

                if (patotoList.patotos.Count == 2)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[1].transform;
                else if (patotoList.patotos.Count == 3)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[2].transform;
                else if (patotoList.patotos.Count == 4)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[3].transform;
                else if (patotoList.patotos.Count == 5)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[4].transform;
                else if (patotoList.patotos.Count == 6)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[5].transform; 
                else if (patotoList.patotos.Count >= 6)
                    GameManager.Instance.finishCam.m_Follow = patotoList.patotos[5].transform;    
            }
        }
    }

}
