using UnityEngine;
using DG.Tweening;

public class FinishZero : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Patoto"))
        {
            if (other.gameObject.GetComponent<Patoto>().throwed == false)
            {
                FindObjectOfType<SI>().spX = 0f;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").transform.DOMoveX(0f, 0.2f);
            }
        }
    }
}
