using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{

    private static InGameMenuManager instance;

    [Header("Menus:")]
    public GameObject InGameMenu;
    public GameObject InGameUI;
    public GameObject OptionsMenu;
    public GameObject HouseSelectionMenu;
    public GameObject RejoinMenu;
    public GameObject EndScreen;
    public GameObject PauseScreenWithButton;
    public GameObject PauseScreenNoButton;

    public GameObject[] forbiddenMenus;

    [Header("EndScreen")]
    public Text statisticsText;

    [Header("HouseSelection:")]
    public Toggle option1;
    public Toggle option2;
    public GameObject confirmButton;

    private string option1Name;
    private string option2Name;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static InGameMenuManager getInstance()
    {
        if(instance == null)
        {
            instance = new InGameMenuManager();
        }
        return instance;
    }

    private void Start()
    {
        ActivateMenu(InGameUI);
    }

    /// <summary>
    /// this method gets called by the SERVER to end the game and show the statistics
    /// </summary>
    /// <param name="statistics"></param>
    public void DemandEndGame(string statistics)
    {
        statisticsText.text = statistics;
        Time.timeScale = 0f;
        ActivateMenu(EndScreen);
    }

    /// <summary>
    /// this method gets called when the Player should have the option to rejoin, for example at a disconnect
    /// </summary>
    public void DemandRejoinOption()
    {
        ActivateMenu(RejoinMenu);
    }

    /// <summary>
    /// this method is called by a BUTTON to rejoin the game
    /// </summary>
    public void RequestRejoinGame()
    {
        Debug.Log("Rejoining");
        //TODO send REJOIN

    }

    /// <summary>
    /// this method is called when Rejoin gets accepted
    /// </summary>
    public void DemandAcceptRejoin()
    {
        ActivateMenu(InGameUI);
    }

    /// <summary>
    /// this method is called by the SERVER to start the HouseSelcetion with two options
    /// </summary>
    /// <param name="houseName1"></param>
    /// <param name="houseName2"></param>
    public void DemandStartHouseSelection(string houseName1, string houseName2)
    {
        SetOptionText(1, houseName1);
        SetOptionText(2, houseName2);

        option1Name = houseName1;
        option2Name = houseName2;

        ActivateMenu(HouseSelectionMenu);
    }

    //THIS METHOD IS TEMPORARY AND ONLY MENT FOR THE BUTTON ACTIVATION OF THE HOUSE SELECTEION ToDo delete
    public void StartHouseSelection()
    {
        DemandStartHouseSelection("option 1", "option 2");
    }

    /// <summary>
    /// this method is called by a BUTTON to select one of the two house options
    /// </summary>
    public void RequestSelectOption()
    {
        if (option1.isOn)
        {
            Debug.Log(option1.GetComponentInChildren<Text>().text + " was selected!");
            //TODO send message to server

            SessionHandler.messageController.DoRequestHouse(option1Name);
            //DemandEndHouseSelection();//TODO trigger by server instead
        }
        else if (option2.isOn)
        {
            Debug.Log(option2.GetComponentInChildren<Text>().text + " was selected!");
            //TODO send message to server
            SessionHandler.messageController.DoRequestHouse(option2Name);
            //DemandEndHouseSelection();//TODO trigger by server instead
        }
    }

    /// <summary>
    /// this method is called by a TOGGLE to toggle the confirmButton.isActive
    /// </summary>
    public void ToggleChange()
    {
        if (!option1.isOn && !option2.isOn)
        {
            confirmButton.SetActive(false);
        }
        else
        {
            confirmButton.SetActive(true);
        }
    }

    /// <summary>
    /// this method is a HELPER-METHOD to set the textFields of the house options
    /// </summary>
    /// <param name="index">first or second option</param>
    /// <param name="houseName">name</param>
    private void SetOptionText(int index, string houseName)
    {
        if (index == 1)
        {
            option1.GetComponentInChildren<Text>().text = houseName;
        }
        else if (index == 2)
        {
            option2.GetComponentInChildren<Text>().text = houseName;
        }
    }

    /// <summary>
    /// this method is called by the SERVER to end the HouseSelection when house gets acknowkledged
    /// </summary>
    public void DemandEndHouseSelection()
    {
        //TODO
        ActivateMenu(InGameUI);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// this method is called by a button to send a pause request
    /// </summary>
    public void RequestPauseGame()
    {


        //TODO send message to server for a pause request
        DemandPauseGame(false);//TODO delete
    }

    /// <summary>
    /// this method is called by a server message to pause the game
    /// </summary>
    public void DemandPauseGame(bool forced)
    {
        ActivateMenu(null);//deactivate all menus

        PauseScreenWithButton.SetActive(!forced);
        PauseScreenNoButton.SetActive(forced);
 

        Time.timeScale = 0f;
    }

    /// <summary>
    /// this method is called by a button to send an unpause request
    /// </summary>
    public void RequestUnpauseGame()
    {
        //TODO send message to server for a unpause request
        DemandUnpauseGame();//TODO delete
    }

    /// <summary>
    /// this method is called by a server message to unpause the game
    /// </summary>
    public void DemandUnpauseGame()
    {
        ActivateMenu(InGameUI);

        PauseScreenWithButton.SetActive(false);
        PauseScreenNoButton.SetActive(false);

        Time.timeScale = 1f;
    }

    /// <summary>
    /// this method is a HELPER-METHOD to change the .isActive trade of the menus
    /// </summary>
    /// <param name="menuToActivate">maybe null</param>
    private void ActivateMenu(GameObject menuToActivate)
    {
        InGameMenu.SetActive(false);
        InGameUI.SetActive(false);
        OptionsMenu.SetActive(false);
        HouseSelectionMenu.SetActive(false);
        RejoinMenu.SetActive(false);
        EndScreen.SetActive(false);

        if(menuToActivate != null)
        {
            menuToActivate.SetActive(true);
        }
        if(!SessionHandler.isPlayer)
        {
            foreach(GameObject obj in forbiddenMenus) {
                obj.SetActive(false);
            }
        }
    }

    //BUTTON SWITCH MENU METHODS ------------

    /// <summary>
    /// this method is called by a BUTTON to switch to the MainMenu
    /// </summary>
    public void SwitchToInGameMenu()
    {
        ActivateMenu(InGameMenu);
    }

    /// <summary>
    /// this method is called by a BUTTON to switch to the InGameUi
    /// </summary>
    public void SwitchToInGameUI()
    {
        ActivateMenu(InGameUI);
    }

    /// <summary>
    /// this method is called by a BUTTON to switch to the OptionsMenu
    /// </summary>
    public void SwitchToOptionsMenu()
    {
        ActivateMenu(OptionsMenu);
    }
}