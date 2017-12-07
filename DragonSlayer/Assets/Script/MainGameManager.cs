using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{        
    [SerializeField]
    BalleManager balleManager;

    [SerializeField]
    GameObject restartButton;

    private void Awake()
    {
        bool flag = false;

        if (balleManager == null) flag = true;
        if (restartButton == null) flag = true;

        if(flag)
        {
            this.enabled = false;
        }
        restartButton.SetActive(false);

    }

    private void Update()
    {
        if(balleManager.IsMatchDeside)
        {
            restartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
