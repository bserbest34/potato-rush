using System.Collections.Generic;
using UnityEngine;

public class PatotoListManager : MonoBehaviour
{

    public List<GameObject> patotos;

    private void Start()
    {
        patotos.Add(GameObject.FindGameObjectWithTag("Player").transform.Find("_Patoto_Main").gameObject);
    }

}
