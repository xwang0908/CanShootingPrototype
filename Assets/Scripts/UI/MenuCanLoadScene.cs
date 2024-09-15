using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuCanLoadScene : CanHitEffect
{

    [Tooltip("The name of the scene to load when this can is destroyed")] [SerializeField]
    private string SceneToLoad;

    [Tooltip("The amount of time to wait before reloading the scene")] [SerializeField]
    private float Delay;

    public override void Hit(Vector2 hitPos)
    {
        StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSeconds(Delay);

        SceneManager.LoadScene(SceneToLoad);
    }
}
