using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class S_MushroomGlow : MonoBehaviour
{
    [SerializeField] private Renderer hatRenderer;
    [SerializeField] private Renderer stemRenderer;
    [SerializeField] private float glowTime = 5f;
    [SerializeField] private VisualEffect sporesVFX;
    [SerializeField] private Color particleColor = Color.cyan;

    private Material hatMat;
    private Material stemMat;
    private Coroutine glowCoroutine;
    private bool permanentGlow = false;
    void Start()
    {
        sporesVFX.Stop();
    }
    void Awake()
    {
        hatMat = hatRenderer.material;
        stemMat = stemRenderer.material;

        SetGlow(0f, 3.3f, 0f, 0f, -1f, 0f);
    }

    void SetGlow(float glow, float glow1, float glow2, float glow_bolet, float glow1_bolet, float glow2_bolet)
    {
        // Chapeau
        hatMat.SetFloat("_Glow", glow);
        hatMat.SetFloat("_Glow2", glow2);

        hatMat.SetFloat("_Glow_Bolet", glow_bolet);
        hatMat.SetFloat("_Glow2_Bolet", glow2_bolet);

        // Tige
        stemMat.SetFloat("_Glow1", glow1);
        stemMat.SetFloat("_Glow1_Bolet", glow1_bolet);
    }
    private void PlayVFX()
    {
        if (sporesVFX == null)
            return;

        sporesVFX.SetVector4("ParticleColor", particleColor);
        sporesVFX.Reinit();
        sporesVFX.Play();
    }
    public void LightUp()
    {
        if (permanentGlow)
            return;

        PlayVFX();

        if (glowCoroutine != null)
            StopCoroutine(glowCoroutine);

        glowCoroutine = StartCoroutine(GlowRoutine());
    }
    public void EnablePermanentGlow()
    {
        permanentGlow = true;

        if (glowCoroutine != null)
            StopCoroutine(glowCoroutine);

        SetGlow(0.5f, -1f, -8f, 0.5f, -2.5f, -8f);
    }

    public void DisablePermanentGlow()
    {
        permanentGlow = false;

        SetGlow(0f, 3.3f, -1f, 0f, -1f, 0f);
    }
    IEnumerator GlowRoutine()
    {
        SetGlow(0.5f, -1f, -8f, 0.5f, -2.5f, -8f);

        yield return new WaitForSeconds(glowTime);

        float fadeDuration = 2f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float t = timer / fadeDuration;

            float glow = Mathf.Lerp(0.5f, 0f, t);
            float glow1 = Mathf.Lerp(-1f, 3.3f, t);
            float glow2 = Mathf.Lerp(-8f, -1f, t);

            float glow_bolet = Mathf.Lerp(0.5f, 0f, t);
            float glow1_bolet = Mathf.Lerp(-2.5f, -1f, t);
            float glow2_bolet = Mathf.Lerp(-8f, 0f, t);

            SetGlow(glow, glow1, glow2, glow_bolet, glow1_bolet, glow2_bolet);

            yield return null;
        }

        SetGlow(0f, 3.3f, -1f, 0f, -1f, 0f);
    }
}