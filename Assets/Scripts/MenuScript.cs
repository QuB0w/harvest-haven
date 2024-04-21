using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuScript : MonoBehaviour
{
    public Image loadingBar;
    public GameObject loadingBarOBJ;
    public Text loadingText;

    public GameObject playButton;
    public GameObject supportButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadingBarAdd());
    }

    // Update is called once per frame
    void Update()
    {
        loadingText.text = Convert.ToUInt32(loadingBar.fillAmount * 100) + "%";

        if(loadingBar.fillAmount >= 1)
        {
            StopAllCoroutines();
            playButton.SetActive(true);
            supportButton.SetActive(true);
            loadingBarOBJ.SetActive(false);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator loadingBarAdd()
    {
        loadingBar.fillAmount += 0.1f;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(loadingBarAdd());
    }
}
