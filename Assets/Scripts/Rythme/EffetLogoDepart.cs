using UnityEngine;
using System.Collections;

public class EffetLogoDepart : MonoBehaviour
{
    public GameObject logoDepart;
    public ParticleSystem explosionLogo;

    public float dureeApparition = 0.25f;
    public float dureeDisparition = 0.45f;
    public float grossissementExplosion = 1.2f;

    public void Jouer()
    {
        StartCoroutine(JouerSequence());
    }

    private IEnumerator JouerSequence()
    {
        logoDepart.SetActive(true);

        RectTransform rectTransform =
            logoDepart.GetComponent<RectTransform>();

        CanvasGroup canvasGroup =
            logoDepart.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup =
                logoDepart.AddComponent<CanvasGroup>();
        }

        Vector3 echelleInitiale =
            rectTransform.localScale;

        canvasGroup.alpha = 1f;

        float temps = 0f;

        while (temps < dureeApparition)
        {
            temps += Time.deltaTime;

            yield return null;
        }

        if (explosionLogo != null)
        {
            explosionLogo.Play();
        }

        temps = 0f;

        while (temps < dureeDisparition)
        {
            temps += Time.deltaTime;

            float progression =
                temps / dureeDisparition;

            rectTransform.localScale =
                Vector3.Lerp(
                    echelleInitiale,
                    echelleInitiale * grossissementExplosion,
                    progression
                );

            canvasGroup.alpha =
                Mathf.Lerp(
                    1f,
                    0f,
                    progression
                );

            yield return null;
        }

        logoDepart.SetActive(false);

        rectTransform.localScale =
            echelleInitiale;

        canvasGroup.alpha = 1f;
    }
}