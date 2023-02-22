using UnityEngine;

public class FinishControl : MonoBehaviour
{

    private int x;
    private int y;
    private bool firstTime = true;
    public bool newWin = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Patoto"))
        {
            if (firstTime == true && newWin == false)
            {
                firstTime = false;
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
                x = patotoList.patotos.Count;
                Invoke("FinishNew", 2f);
            }
            
            other.gameObject.GetComponent<Patoto>().ControlFinish();
            FindObjectOfType<SI>()._moveSpeed = 11f;
        }
    }

    private void FinishNew()
    {
        PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
        y = patotoList.patotos.Count;

        if (x != y && GameManager.Instance.winControl == false)
            GameManager.Instance.GameOver();

         if (x == y && GameManager.Instance.winControl == false)
         {
             GameManager.Instance.GameOver();
         }

        // if (y > 1 && GameManager.Instance.winControl == true)
        // {
        //     Debug.Log("OLD CAMERA CHANGER METHODS");
        // }
    }
}
