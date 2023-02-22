using UnityEngine;
using Dlite.Games.Managers;
using MoreMountains.NiceVibrations;
using DG.Tweening;

public class CookGate : MonoBehaviour
{
    [Header("MATERIALS")]
    [Space]
    [SerializeField] Material chopNoShellFry;
    [SerializeField] Material chopShellFry;
    [SerializeField] Material chopNoShell;

    [SerializeField] Material noShellFry;
    [SerializeField] Material normalFry;

    [Header("VARIABLES")]
    [Space]
    [Tooltip("1=PEELING // 2=CHOPPING // 3=FRYING ")]
    [SerializeField] int cookType;

    // =========================== 

    private void OnTriggerEnter(Collider other)
    {
#region PEEL
        // ======== TRIGGER PATOTO && COOK GATE
        if (other.gameObject.CompareTag("Patoto") || other.gameObject.CompareTag("Player"))
        {
            int patotosType = other.gameObject.GetComponent<Patoto>().patotoType;
            // ========= PEELING POTATOES WITH SHELLS ***
            if (cookType == 1) 
            {              
                if (other.gameObject.GetComponent<Patoto>().peel == false)
                    GetComponentInChildren<ParticleSystem>().Play();

                if (patotosType == 0)
                {
                    Patoto patoto = other.gameObject.GetComponent<Patoto>();
                    patoto.peel = true;
                    patoto.shellModel.enabled = false;
                    patoto.chopModel.SetActive(false);
                    patoto.noShellModel.SetActive(true);
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopNoShell;
                    }
                    patoto.patotoType = 1;
                    if (other.gameObject.GetComponent<Patoto>().fry == true)
                        other.gameObject.GetComponent<Patoto>().noShellModel.transform.GetComponent<MeshRenderer>().material = noShellFry;
                }    
                if (patotosType == 2)  
                {
                    Patoto patoto = other.gameObject.GetComponent<Patoto>();
                    patoto.peel = true;
                    patoto.chopModel.SetActive(true);
                    patoto.noShellModel.SetActive(false);
                    patoto.patotoType = 3;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopNoShell;
                    }
                    if (patoto.fry == true)
                    {
                        patoto.shellModel.material = chopNoShellFry;
                        for (int i = 0; i < patoto.chopMesh.Length; i++)
                        {
                            patoto.chopMesh[i].material = chopNoShellFry;
                        }
                    }
                }
                
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.RigidImpact); // HAPTIC   
            }
#endregion
#region FRY
            // ========= FRYING POTATOES PROCESS ***
            else if (cookType == 2)
            {
                Patoto patoto = other.gameObject.GetComponent<Patoto>();
                patoto.FryDown();
                GetComponentInChildren<ParticleSystem>().Play();
                
                if (patotosType == 0) // === FRY FRIES WITH SHELLS
                {
                     patoto.shellModel.material = normalFry;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopShellFry;
                    }
                } 
                else if (patotosType == 1) // === FRY PEELED POTATOES
                {
                    patoto.noShellModel.transform.GetComponent<MeshRenderer>().material = noShellFry;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopNoShellFry;
                    }
                    patoto.shellModel.material = chopShellFry;
                } 
                else if (patotosType == 2) // ===  FRY SHELL CHOPPING
                {
                    patoto.shellModel.material = chopShellFry;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopShellFry;
                    }
                }
                else if (patotosType == 3) // FRY NO SHELL CHOPPING
                {
                    patoto.shellModel.material = chopNoShellFry;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopNoShellFry;
                    }
                    patoto.shellModel.material = chopShellFry;
                    
                }
                patoto.fry = true; // THIS PATOTO FRYED
            } 
#endregion
#region CHOP
            // ========= CHOPPING POTATOES PROCESS ***
            else if (cookType == 3) 
            {
                if (other.gameObject.GetComponent<Patoto>().chop == false)
                    GetComponentInChildren<ParticleSystem>().Play();
                    
                if (patotosType == 0)
                {
                    Patoto patoto = other.gameObject.GetComponent<Patoto>();
                    patoto.chop = true;
                    patoto.chopModel.SetActive(true);
                    patoto.noShellModel.SetActive(false);
                    patoto.shellModel.enabled = false;
                    patoto.patotoType = 2;
                }
                if (patotosType == 1)
                {
                    Patoto patoto = other.gameObject.GetComponent<Patoto>();
                    patoto.chop = true;
                    patoto.chopModel.SetActive(true);
                    patoto.noShellModel.SetActive(false);
                    patoto.shellModel.enabled = false;
                    patoto.patotoType = 3;
                }
                if (patotosType == 1 && other.gameObject.GetComponent<Patoto>().fry == true)
                {
                    Patoto patoto = other.gameObject.GetComponent<Patoto>();
                    patoto.chop = true;
                    patoto.shellModel.material = chopNoShellFry;
                    for (int i = 0; i < patoto.chopMesh.Length; i++)
                    {
                        patoto.chopMesh[i].material = chopNoShellFry;
                    }
                    patoto.shellModel.material = chopShellFry;
                }
                
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.RigidImpact);
            }
#endregion
        }
    }
}