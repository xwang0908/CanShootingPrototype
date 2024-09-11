using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuCanDestroyed : MonoBehaviour
{
    public enum MenuAction
    {
        StartGame,
        Quit
    }
    
    [Tooltip("The amount of time to wait between the can being destroyed and the event taking place")] [SerializeField]
    private float SceneLoadDelay;

    [Tooltip("The event that should take place when the menu can is destroyed")] [SerializeField]
    private MenuAction Action;

    [Tooltip("For making the title appear awesome")] [SerializeField]
    private TextMeshProUGUI[] DongClangBoom;

    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Fade ForegroundFade;

    private int _dcbIndex;

    public void Hit()
    {
        if (Action == MenuAction.Quit)
            return;
        
        DongClangBoom[_dcbIndex].enabled = true;
        _dcbIndex++;
    }
    
    public void DoYourThing()
    {
        StartCoroutine(DoYourThingCoroutine());
    }

    private IEnumerator DoYourThingCoroutine()
    {
        Text.enabled = false;
        ForegroundFade.Play();
        yield return new WaitForSeconds(SceneLoadDelay);

        if (Action == MenuAction.StartGame)
            SceneManager.LoadScene(1);
        else if(Action == MenuAction.Quit)
            Application.Quit();
    }
}
