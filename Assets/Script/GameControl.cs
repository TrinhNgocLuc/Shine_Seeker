using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private ControlCharacter character;
    public GameObject blinkEffect;
    public GameObject loseScreen;
    public GameObject winScreen;
    private void Awake()
    {
        character = FindAnyObjectByType<ControlCharacter>();
    }
    private void Update()
    {
        if (character.coutOxi > 0 && character.coutOxi <=5) 
        {
            blinkEffect.SetActive(true);
        }
        else
        {
            blinkEffect.SetActive(false);
        }
        if (character.coutOxi == 0)
        {
            character.isLose = true;
        }
        StartCoroutine(CheckLose());
        StartCoroutine(CheckWin());
    }
    private IEnumerator CheckLose()
    {
        if (character.isLose)
        {
            yield return new WaitForSeconds(1f);
            blinkEffect.SetActive(false);
            loseScreen.SetActive(true);
        }
    }
    private IEnumerator CheckWin()
    {
        if (character.isWin)
        {
            yield return new WaitForSeconds(1f);
            blinkEffect.SetActive(false);
            winScreen.SetActive(true);
        }
    }
}
