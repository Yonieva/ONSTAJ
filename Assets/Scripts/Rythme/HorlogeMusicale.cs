using UnityEngine;
using TMPro;

public class HorlogeMusicale : MonoBehaviour
{
    public AudioSource sourceAudio;
    public TextMeshProUGUI texteCompteRebours;
    public AudioSource sourceEffetsCompteRebours;
    public AudioClip sonCompteReboursComplet;
    public EffetLogoDepart effetLogoDepart;

    public float bpm = 120f;

    public float tempsAffichage3 = 0.1f;
    public float tempsAffichage2 = 0.4f;
    public float tempsAffichage1 = 0.7f;
    public float tempsAffichagePlay = 1.0f;

    public double positionMusique { get; private set; }
    public double positionEnTemps { get; private set; }
    public bool morceauTermine { get; private set; } = false;

    private double tempsDebutMusique;
    private double secondesParTemps;
    private bool musiqueADemarre = false;
    private double tempsDebutCompteRebours;
    private bool logoDepartJoue = false;

    void Start()
    {
        secondesParTemps = 60.0 / bpm;

        double maintenant = AudioSettings.dspTime;

        tempsDebutCompteRebours = maintenant;

        if (
            sourceEffetsCompteRebours != null &&
            sonCompteReboursComplet != null
        )
        {
            sourceEffetsCompteRebours.clip =
                sonCompteReboursComplet;

            sourceEffetsCompteRebours.Play();

            tempsDebutMusique =
                maintenant + sonCompteReboursComplet.length;
        }
        else
        {
            Debug.LogError(
                "Compte à rebours mal configuré : AudioSource ou AudioClip manquant."
            );

            tempsDebutMusique = maintenant;
        }

        sourceAudio.PlayScheduled(tempsDebutMusique);
    }

    void Update()
    {
        positionMusique =
            AudioSettings.dspTime - tempsDebutMusique;

        float tempsEcouleCompteRebours =
            (float)(
                AudioSettings.dspTime -
                tempsDebutCompteRebours
            );

        if (tempsEcouleCompteRebours < tempsAffichage3)
        {
            texteCompteRebours.text = "";
        }
        else if (tempsEcouleCompteRebours < tempsAffichage2)
        {
            texteCompteRebours.text = "3";
        }
        else if (tempsEcouleCompteRebours < tempsAffichage1)
        {
            texteCompteRebours.text = "2";
        }
        else if (tempsEcouleCompteRebours < tempsAffichagePlay)
        {
            texteCompteRebours.text = "1";
        }
        else
        {
            texteCompteRebours.text = "";

            if (!logoDepartJoue && effetLogoDepart != null)
            {
                logoDepartJoue = true;
                effetLogoDepart.Jouer();
            }
        }

        if (!musiqueADemarre && sourceAudio.isPlaying)
        {
            musiqueADemarre = true;
        }

        if (
            musiqueADemarre &&
            !sourceAudio.isPlaying &&
            !morceauTermine
        )
        {
            morceauTermine = true;

            Debug.Log("Morceau terminé");
        }

        positionEnTemps =
            positionMusique / secondesParTemps;
    }
}