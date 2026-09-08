using UnityEngine;

public class HorlogeMusicale : MonoBehaviour
{
    public AudioSource sourceAudio;
    public float bpm = 120f;
    public double positionMusique { get; private set; }
    public double positionEnTemps { get; private set; }
    public bool morceauTermine { get; private set; } = false;

    private double tempsDebutMusique;
    private double secondesParTemps;
    private bool musiqueADemarre = false;


    void Start()
    {
        secondesParTemps = 60.0 / bpm;

        tempsDebutMusique = AudioSettings.dspTime;

        sourceAudio.PlayScheduled(tempsDebutMusique);

    }

    void Update()
    {
        positionMusique = AudioSettings.dspTime - tempsDebutMusique;

        if (!musiqueADemarre && sourceAudio.isPlaying)
        {
            musiqueADemarre = true;
        }

        if (musiqueADemarre && !sourceAudio.isPlaying && !morceauTermine)
        {
            morceauTermine = true;

            Debug.Log("Morceau terminé");
        }

        positionEnTemps = positionMusique / secondesParTemps;
    }
}