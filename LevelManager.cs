using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool lostLevel = false;
    public GameObject foreGroundParent;
    public GameObject foreGround;
    private string currentScene;

    private void Start()
    {
        foreGroundParent = GameObject.FindGameObjectWithTag("ForeGround");
        foreGround = foreGroundParent.transform.Find("Foreground").gameObject;
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (lostLevel)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
                lostLevel = false;
            }
        }
    }


    public void NextScene()
    {
        Debug.Log(currentScene);
        if(currentScene == "WelcomeScreen")
        {
            Debug.Log("test");
            SceneManager.LoadScene("Level1");
        }
        if (currentScene == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        if (currentScene == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        if (currentScene == "Level3")
        {
            SceneManager.LoadScene("End");
        }
        
    }

    public void RestartScene()
    {
        StartCoroutine("RestartLevel");
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);
        foreGround.SetActive(true);
        Time.timeScale = 0;
        lostLevel = true;
    }
}
