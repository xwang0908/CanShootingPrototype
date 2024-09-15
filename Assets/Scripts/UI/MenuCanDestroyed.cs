using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuCanDestroyed : MonoBehaviour
{
    public enum MenuAction
    {
        StartGame,
        Quit,
        RestartGame,
        ReturnToStartMenu,
        Credits
    }
    
    [Tooltip("The amount of time to wait between the can being destroyed and the event taking place")] [SerializeField]
    private float SceneLoadDelay;

    [Tooltip("The event that should take place when the menu can is destroyed")] [SerializeField]
    private MenuAction Action;
    
    [Tooltip("For making the credits appear awesome")] [SerializeField]
    private TextMeshProUGUI[] Credits;

    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Fade ForegroundFade;

    private int _dcbIndex;
    private int _creditsIndex;

    public void Hit()
    {
        if (Action == MenuAction.Credits && _creditsIndex < Credits.Length)
        {
            Credits[_creditsIndex].enabled = true;
            _creditsIndex++;
        }
    }
    
    public void DoYourThing()
    {
        StartCoroutine(DoYourThingCoroutine());
    }

    private IEnumerator DoYourThingCoroutine()
    {
        Text.enabled = false;
        
        if (Action == MenuAction.RestartGame)
        {
            GameManager.Instance.RestartGame();
            yield break;
        }

        if (Action == MenuAction.ReturnToStartMenu)
        {
            yield return new WaitForSecondsRealtime(2.0f);
            SceneManager.LoadScene(0);
            yield break;
        }

        if (Action == MenuAction.Credits)
            yield break;
        
        ForegroundFade.Play();
        yield return new WaitForSeconds(SceneLoadDelay);

        if (Action == MenuAction.StartGame)
            SceneManager.LoadScene(1);
        else if(Action == MenuAction.Quit)
            Application.Quit();
    }
}
