using UnityEngine;
using System.Collections;

public class S_UltimateFog : MonoBehaviour
{
    [SerializeField] private float fogDensityUlt = 0f;
    [SerializeField] private float dureeUlt = 5f;

    private float _defaultFogDensity;
    private void Awake()
    {
        _defaultFogDensity = RenderSettings.fogDensity;
    }

    public void ActiverUlt()
    {
        StopAllCoroutines();
        StartCoroutine(Ultime());
    }

    IEnumerator Ultime()
    {
        float duration = 2f;
        float startDensity = RenderSettings.fogDensity;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            RenderSettings.fogDensity = Mathf.Lerp(startDensity, 0f, timer / duration);

            yield return null;
        }

        RenderSettings.fogDensity = 0f;
    }
}