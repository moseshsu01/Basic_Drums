using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fade;
    public GameObject menu;
    public GameObject instructionsMenu;
    public GameObject modeSelect;

    public void modeSelectMenu()
    {
        menu.SetActive(false);
        modeSelect.SetActive(true);
    }

    public void Hard()
    {
        StartCoroutine(fadeOutHard());
    }

    public void Regular()
    {
        StartCoroutine(fadeOutRegular());
    }

    public IEnumerator fadeOutHard()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameHard");
    }

    public IEnumerator fadeOutRegular()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameRegular");
    }

    public void instructions()
    {
        menu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void back()
    {
        instructionsMenu.SetActive(false);
        modeSelect.SetActive(false);
        menu.SetActive(true);
    }
}
