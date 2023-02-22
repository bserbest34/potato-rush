using UnityEngine;
using UnityEngine.Animations;
using Dlite.Games.Managers;
using MoreMountains.NiceVibrations;

public class Cups : MonoBehaviour
{
    private ParticleSystem coinFx;
    private MeshRenderer fakePatoto;

    private void Start() 
    {
        coinFx = GetComponentInChildren<ParticleSystem>();
        GameObject x = transform.GetChild(1).gameObject;
        fakePatoto = x.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Patoto"))
        {
            if (other.gameObject.GetComponent<Patoto>().throwed == false)
            {
                coinFx.Play();
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                Patoto patoto = other.gameObject.GetComponent<Patoto>();
                if (patoto.mainPatoto == false)
                {
                    patoto.mainModel.GetComponent<CopyPatotoManager>().enabled = false;
                    patoto.mainModel.GetComponent<ParentConstraint>().enabled = false;
                }
                patoto.CupsDpown();
                fakePatoto.enabled = true;
                GameManager.Instance.FinishLevel();
                GameManager.Instance.winControl = true;

                if (patoto.mainPatoto == true)
                    GameManager.Instance.isGameStarted = false;

            }

            HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.Success);
        }
    }

}