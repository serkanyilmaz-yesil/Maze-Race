using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public GameObject nextButton, tapButton;
    public int level;


    // Start is called before the first frame update
    void Start()
    {
        TinySauce.OnGameStarted();
        nextButton.SetActive(false);
        tapButton.SetActive(true);
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.ctrl.tap)
        {
            tapButton.SetActive(false);

        }

        if (PlayerControl.ctrl.finish || PlayerControl.ctrl.number >=3)
        {
            StartCoroutine(NextDelay());
        }

        Save();
    }


    IEnumerator NextDelay()
    {
        yield return new WaitForSeconds(3);
        nextButton.SetActive(true);

    }

    public void NextButton()
    {
        level++;
        if (level < 4)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            level = 0;
            SceneManager.LoadScene(level);

        }
    }


    public void Save()
    {
        PlayerPrefs.SetInt("Level", level);
    }


    public void Load()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }

    }

}
