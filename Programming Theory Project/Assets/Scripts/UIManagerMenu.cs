using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown difChooseMainMenu;
    [SerializeField] TMP_Dropdown charChooseMainMenu;

    public void StartGame()
    {
        GameManager.GM.StartGame();
    }
    public void Difficulty()
    {
        if (difChooseMainMenu.value == 0)
            GameManager.GM.Easy();
        if (difChooseMainMenu.value == 1)
            GameManager.GM.Normal();
        if (difChooseMainMenu.value == 2)
            GameManager.GM.Hard();
    }
    public void ExitFromGame()
    {
        GameManager.GM.ExitGame();
    }
    public void ChooseCharacter()
    {
        if (charChooseMainMenu.value == 0)
        {
            GameManager.GM.charWrangler = true;
            GameManager.GM.charFarmer = false;
            GameManager.GM.charWife = false;
            GameManager.GM.charDaughter = false;
        }
        if (charChooseMainMenu.value == 1)
        {
            GameManager.GM.charWrangler = false;
            GameManager.GM.charFarmer = true;
            GameManager.GM.charWife = false;
            GameManager.GM.charDaughter = false;
        }
        if (charChooseMainMenu.value == 2)
        {
            GameManager.GM.charWrangler = false;
            GameManager.GM.charFarmer = false;
            GameManager.GM.charWife = true;
            GameManager.GM.charDaughter = false;
        }
        if (charChooseMainMenu.value == 3)
        {
            GameManager.GM.charWrangler = false;
            GameManager.GM.charFarmer = false;
            GameManager.GM.charWife = false;
            GameManager.GM.charDaughter = true;
        }
    }
}
