using UnityEngine;
using TMPro;
using System.Collections;

public class GestionScore : MonoBehaviour
{
    public int score = 0;
    public int combo = 0;
    public int meilleurCombo = 0;
    public int nombreParfaits = 0;
    public int nombreBiens = 0;
    public int nombreRates = 0;

    public float echelleJugement = 1.2f;
    public float echelleCombo = 1.15f;

    public TextMeshProUGUI texteScore;
    public TextMeshProUGUI texteCombo;
    public TextMeshProUGUI texteJugement;

    public Color couleurParfait = Color.green;
    public Color couleurBien = Color.yellow;
    public Color couleurRate = Color.red;

    private Vector2 positionInitialeJugement;
    private Vector3 echelleInitialeJugement;
    private Vector3 echelleInitialeCombo;
    private Coroutine animationJugementEnCours;
    private float tailleInitialeJugement;

    void Start()
    {
        echelleInitialeJugement = texteJugement.transform.localScale;
        tailleInitialeJugement = texteJugement.fontSize;
        echelleInitialeCombo = texteCombo.transform.localScale;
        positionInitialeJugement =
            texteJugement.rectTransform.anchoredPosition;

        ActualiserAffichage();
    }

    public void AjouterParfait()
    {
        score += 100;
        combo++;
        nombreParfaits++;

        if (combo > meilleurCombo)
        {
            meilleurCombo = combo;
        }

        texteJugement.text = "PARFAIT";
        texteJugement.color = couleurParfait;

        ActualiserAffichage();
        LancerAnimationJugement();
        StartCoroutine(AnimerCombo());
    }

    public void AjouterBien()
    {
        score += 50;
        combo++;
        nombreBiens++;

        if (combo > meilleurCombo)
        {
            meilleurCombo = combo;
        }

        texteJugement.text = "BIEN";
        texteJugement.color = couleurBien;

        ActualiserAffichage();
        LancerAnimationJugement();
        StartCoroutine(AnimerCombo());
    }

    public void AjouterRate()
    {
        combo = 0;
        nombreRates++;

        texteJugement.text = "RATÉ";
        texteJugement.color = couleurRate;

        ActualiserAffichage();
        LancerAnimationJugement();
    }

    public void AjouterAccent()
    {
        score += 150;
        combo++;

        if (combo > meilleurCombo)
        {
            meilleurCombo = combo;
        }

        nombreParfaits++;

        texteJugement.text = "PARFAIT";
        texteJugement.color = couleurParfait;

        ActualiserAffichage();
        LancerAnimationJugement();
        StartCoroutine(AnimerCombo());
    }

    public float PrecisionPourcentage
    {
        get
        {
            int totalNotes =
                nombreParfaits +
                nombreBiens +
                nombreRates;

            if (totalNotes == 0)
                return 0f;

            float pointsPrecision =
                (nombreParfaits * 1f) +
                (nombreBiens * 0.5f);

            return (pointsPrecision / totalNotes) * 100f;
        }
    }

    void ActualiserAffichage()
    {
        texteScore.text = "Score\n" + score.ToString("D4");
        texteCombo.text = "Combo\n" + combo.ToString("D3");
    }

    void LancerAnimationJugement()
    {
        if (texteJugement == null)
            return;

        if (animationJugementEnCours != null)
        {
            StopCoroutine(animationJugementEnCours);
        }

        positionInitialeJugement =
            texteJugement.rectTransform.anchoredPosition;

        texteJugement.fontSize =
            tailleInitialeJugement;

        Color couleur =
            texteJugement.color;

        couleur.a = 1f;

        texteJugement.color =
            couleur;

        animationJugementEnCours =
            StartCoroutine(AnimerJugement());
    }

    IEnumerator AnimerJugement()
    {

        if (texteJugement == null)
            yield break;

        float duree = 0.35f;
        float temps = 0f;

        Vector2 positionDepart = positionInitialeJugement;

        Color couleurDepart = texteJugement.color;

        // Sécurité : toujours démarrer visible
        couleurDepart.a = 1f;
        texteJugement.color = couleurDepart;

        while (temps < duree)
        {
            temps += Time.deltaTime;

            float progression =
                Mathf.Clamp01(temps / duree);


            Color couleur = couleurDepart;

            couleur.a = Mathf.Lerp(
                1f,
                0f,
                progression
            );

            texteJugement.color = couleur;

            yield return null;
        }

        texteJugement.rectTransform.anchoredPosition =
            positionDepart;

        Color couleurFinale = couleurDepart;
        couleurFinale.a = 1f;

        texteJugement.color = couleurFinale;
        texteJugement.text = "";

        animationJugementEnCours = null;
    }

    IEnumerator AnimerCombo()
    {
        texteCombo.transform.localScale =
            echelleInitialeCombo * echelleCombo;

        yield return new WaitForSeconds(0.08f);

        texteCombo.transform.localScale =
            echelleInitialeCombo;
    }
}