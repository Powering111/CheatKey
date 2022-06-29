using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    private static UserInterface instance;
    public Image HP_bar;
    public GameObject dieMessage;
    bool gameover = false;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        gameover = false;
    }

    private void Update()
    {

        if (gameover && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public static UserInterface Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void updateUI()
    {
        HP_bar.fillAmount = (float)PlayerController.Instance.HP/PlayerController.Instance.max_HP;
    }

    public void gameOver()
    {

        gameover = true;

        CameraController.Instance.GetComponent<CameraController>().zoomOut();
        dieMessage.SetActive(true);
    }
}
