using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LM : MonoBehaviour
{
    private int lv;
    public IEnumerator LLevel()
    {
        if (PlayerPrefs.GetInt("Level") <= (SceneManager.sceneCountInBuildSettings - 1) && PlayerPrefs.GetInt("Level") > 1)
        {
            lv = PlayerPrefs.GetInt("Level");
        }
        else if (PlayerPrefs.GetInt("Level") > (SceneManager.sceneCountInBuildSettings - 1))
        {
            lv = PlayerPrefs.GetInt("Level") % (SceneManager.sceneCountInBuildSettings - 1);
            if (lv == 0 && PlayerPrefs.GetInt("Level") == 0){
                lv = 1;
            }
            else if (lv == 0)
            {
                lv = (SceneManager.sceneCountInBuildSettings - 1);
            }
        } else
        {
            lv = 1;
            PlayerPrefs.SetInt("Level", 1);
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(lv);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
