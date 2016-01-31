using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class GameManager : MonoBehaviour
{
    public bool areAllPlayersInGame;
    public CharacterController[] Characters = new CharacterController[4];

    public enum state { MainMenu, GoingToReadyScreen, WaitingAtReadyScreen, DetermineBetrayer, AnimateBetrayerNotification, Game, VoteStart, VoteFinish, BetrayerFight, Finished };

    public state gameState = state.MainMenu;

    int betrayer;
    int level;
    int enemiesKilled;
    Transform camera;
    public Spawner spawner;
    const int totalPlayers = 4;

    //private List<CharacterController> readyPlayers = new List<CharacterController>( totalPlayers );
    public List<CharacterController> players;

    private int currentPlayerLookingController;

    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
        level = 0; //0, 1, or 2
        Object.DontDestroyOnLoad(transform.gameObject);

        //Player 1
        Physics2D.IgnoreCollision(Characters[0].gameObject.GetComponent<Collider2D>(), Characters[1].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[0].gameObject.GetComponent<Collider2D>(), Characters[2].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[0].gameObject.GetComponent<Collider2D>(), Characters[3].gameObject.GetComponent<Collider2D>());

        //Player 2
        Physics2D.IgnoreCollision(Characters[1].gameObject.GetComponent<Collider2D>(), Characters[0].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[1].gameObject.GetComponent<Collider2D>(), Characters[2].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[1].gameObject.GetComponent<Collider2D>(), Characters[3].gameObject.GetComponent<Collider2D>());

        //Player 3
        Physics2D.IgnoreCollision(Characters[2].gameObject.GetComponent<Collider2D>(), Characters[0].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[2].gameObject.GetComponent<Collider2D>(), Characters[1].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[2].gameObject.GetComponent<Collider2D>(), Characters[3].gameObject.GetComponent<Collider2D>());

        //Player 4
        Physics2D.IgnoreCollision(Characters[3].gameObject.GetComponent<Collider2D>(), Characters[0].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[3].gameObject.GetComponent<Collider2D>(), Characters[1].gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Characters[3].gameObject.GetComponent<Collider2D>(), Characters[2].gameObject.GetComponent<Collider2D>());

    }

    void FixedUpdate()
    {
        if (!areAllPlayersInGame)
        {
            InputDevice inputDevice = InputManager.ActiveDevice;

            if (JoinButtonWasPressedOnDevice(inputDevice))
            {
                if (ThereIsNoPlayerUsingDevice(inputDevice))
                {
                    ReadyPlayer(inputDevice);
                }
            }
        }
        else if (areAllPlayersInGame && gameState != state.DetermineBetrayer && (!Characters[0].isBetrayer && !Characters[1].isBetrayer && !Characters[2].isBetrayer && !Characters[3].isBetrayer))
        {
            //DetermineBetrayer
        }
        switch (gameState)
        {

            case state.MainMenu:
                break;

            case state.GoingToReadyScreen:
                if(Camera.main.transform.position.y <= -9.9)
                {
                    print("Shit's fucked");
                    gameState = state.WaitingAtReadyScreen;
                    Camera.main.transform.position =  new Vector3(0, -10f, 0);
                }
                break;

            case state.WaitingAtReadyScreen:
                if(areAllPlayersInGame)
                {
                    gameState = state.DetermineBetrayer;
                    print(gameState);
                }
                break;

            case state.DetermineBetrayer:
                for (int i = 0; i < 4; i++ )
                {
                    Characters[i].device.Vibrate(650000000);
                }
                betrayer = Random.Range(0, 3);
                Characters[betrayer].isBetrayer = true;
                Characters[int].device.
                Invoke("disableBetrayerVibrate", 5f);
                Invoke("disableOtherVibrates", 10f);
                
                print(gameState);
                break;

            case state.AnimateBetrayerNotification:
                //Play Animation
                gameState = state.Game;
                print(gameState);
                break;

            case state.Game:
                //move camera to wherever the actual game will be
                if(enemiesKilled >= 20)
                {
                    spawner.KillAll();
                    gameState = state.VoteStart;
                }
                break;

            case state.VoteStart:
                camera.position = new Vector3(0f, 0f, 0f);

                break;

            case state.VoteFinish:

                break;

            case state.BetrayerFight:

                break;

            case state.Finished:

                break;
        }
        print(gameState);
    }


    bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
    {
        return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;

    }

    void disableBetrayerVibrate()
    {

        Characters[betrayer].device.Vibrate(0);
    }
    void disableOtherVibrates()
    {
        for (int i = 0; i < 4; i++)
        {
            Characters[i].device.Vibrate(0);
        }
        gameState = state.AnimateBetrayerNotification;
    }

    CharacterController FindPlayerUsingDevice(InputDevice inputDevice)
    {

        int playerCount = currentPlayerLookingController;
        for (int i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.device == inputDevice)
            {
                return player;
            }
        }
        return null;
    }


    bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
    {
        //Debug.Log("fuck 02");
        return FindPlayerUsingDevice(inputDevice) == null;

    }

    void ReadyPlayer(InputDevice inputDevice)
    {

        if (currentPlayerLookingController < totalPlayers)
        {
            players[currentPlayerLookingController].ready = true;
            players[currentPlayerLookingController].device = inputDevice;
            currentPlayerLookingController++;
        }
        if (currentPlayerLookingController >= totalPlayers)
            areAllPlayersInGame = true;

        //return null;
    }



    // Use this for initialization


    public void BetrayerNotify()
    {

        for (int i = 0; i < 4; i++)
        {
            if (betrayer != i)
            {

                Debug.Log("Sent Vibration to Player " + i.ToString());
            }

        }

    }

    public static GameManager find()
    {
        return GameObject.FindObjectOfType<GameManager>();
    }
    public void WaitingAtReadyScreen()
    {
        gameState = state.WaitingAtReadyScreen;
    }
    

    public state State()
    {
        return gameState;
    }


    void StateSwitched()
    {

        //switch (gameState)
        //{

        //    case state.MainMenu:
        //        camera.position = new Vector3(0f, 18.75f, -10f);
        //        break;

        //    case state.DetermineBetrayer:
        //        Characters[Random.Range(0, 3)].isBetrayer = true;
        //        gameState = state.AnimateBetrayerNotification;
        //        break;

        //    case state.AnimateBetrayerNotification:
        //        gameState = state.Game;
        //        break;

        //    case state.Game:
        //        //move camera to wherever the actual game will be
        //        if()
        //        break;

        //    case state.VoteStart:
        //        camera.position = new Vector3(0f, 0f, 0f);
        //        break;


        //}
    }
}

