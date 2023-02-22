using UnityEngine;
using DG.Tweening;
using System.Collections;
using Dlite.Games.Managers;
using MoreMountains.NiceVibrations;

public class Obstacle : MonoBehaviour
{
    [Header("OBSTACLE TYPE")]
    [Tooltip("0=RUBBISH // 1=WALL // 2=MESHER //3= RUBBISH WALL ")]
    [SerializeField] int obstacleType;

    [SerializeField] bool move;
    [SerializeField] ParticleSystem meshVFX;
    [SerializeField] ParticleSystem wallVFX;
    [SerializeField] GameObject meshedPatoto;
    [SerializeField] GameObject meshedPatotoFry;
    [SerializeField] GameObject patotoMesher;
    Vector3 colliderDiscard = new Vector3(0f, 0f, 0.8f);
    
    // =========================== 

    private void Start() 
    {
        Invoke("MoveDelay", 1f);
    }

    private void MoveDelay()
    {
        if (move && obstacleType == 1)
            transform.DOLocalMoveX(-2, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        else if(obstacleType == 2)
          patotoMesher.transform.DOLocalMoveY(1, 0.3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Patoto"))
        {
            // ==================== RUBBISH
            if (obstacleType == 0) 
            {
                PatotoListManager patotoList = FindObjectOfType<PatotoListManager>();
                patotoList.patotos.Remove(other.gameObject.GetComponent<Patoto>().mainModel);
                Destroy(other.gameObject.GetComponent<Patoto>().mainModel);

                if (other.gameObject.GetComponent<Patoto>().mainPatoto == true)
                    GameManager.Instance.GameOver();   

                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.LightImpact); // HAPTIC
            }
            // ==================== WALL
            else if (obstacleType == 1) 
            {
                wallVFX.Play();
                other.gameObject.GetComponent<Patoto>().FirstTouch();
                if (other.gameObject.GetComponent<Patoto>().mainPatoto == true)
                {
                    GameObject player = GameObject.Find("_Player");
                    player.transform.DOLocalMoveZ(player.transform.localPosition.z - 10f, 2f).SetEase(Ease.Linear).SetId(5);
                    StartCoroutine(StopMoving());
                    if (player.transform.localPosition.y > 3f)
                    {
                       // Debug.Log("WALL BUG");
                        player.transform.DOLocalMoveY(1.4f, 0.1f);
                    }
                        
                }
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.HeavyImpact); // HAPTIC
            }   
            // ==================== PATOTO MESHER
            else if (obstacleType == 2) 
            {
                meshVFX.Play();
                if (other.gameObject.GetComponent<Patoto>().fry == false)
                {
                    meshedPatoto.SetActive(true);
                    meshedPatotoFry.SetActive(false);
                }              
                else if (other.gameObject.GetComponent<Patoto>().fry == true)
                {
                    meshedPatotoFry.SetActive(true);
                    meshedPatoto.SetActive(false);
                }
                other.gameObject.GetComponent<Patoto>().FirstTouch();
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.MediumImpact); // HAPTIC
            }
            else if (obstacleType == 3) 
            {
                wallVFX.Play();
                other.gameObject.GetComponent<Patoto>().RubbishWall();
                if (other.gameObject.GetComponent<Patoto>().mainPatoto == true)
                {
                    GameObject player = GameObject.Find("_Player");
                    player.transform.DOLocalMoveZ(player.transform.localPosition.z - 10f, 2f).SetEase(Ease.Linear).SetId(5);
                    StartCoroutine(StopMoving());
                }
                HapticManager.Haptic((Dlite.Games.HapticType)HapticTypes.MediumImpact); // HAPTIC
            }                
        }
    } 

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(0.7f);
        DOTween.Kill(5);
    }

}