using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuGUI : MonoBehaviour
{
    bool gameStarted = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.State() == GameManager.state.GoingToReadyScreen)
        {
            Camera.main.transform.position = Camera.main.transform.position + new Vector3(0f, -10.0f, 0f) * Time.fixedDeltaTime;
            if (Camera.main.transform.position.y < -10)
            {
                Camera.main.transform.position = new Vector3(0f, -10f, -10f);
                GameManager.find().WaitForPlayers();
            }
        }
        if (GameManager.State() == GameManager.state.MainMenu)
        {
            GetComponent<Canvas>().enabled = true;
        }
        else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }



    public void StartGame()
    {
        GameManager.SetState(GameManager.state.GoingToReadyScreen);

    }

}
