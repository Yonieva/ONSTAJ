using UnityEngine;

public class NoteRythmique : MonoBehaviour
{
    public GameObject prefabCorpsNoteLongue;
    public GameObject prefabJaugeRafale;
    public GameObject prefabRemplissageRafale;
    public GameObject prefabEffetReussite;
    public GameObject prefabEffetReussiteAccent;
    public HorlogeMusicale horlogeMusicale;
    public Sprite spriteNoteSimple;
    public Sprite spriteNoteAccent;
    public int numeroPiste = 1;
    public int objectif = 0;
    public int frappesRafale = 0;
    public float tempsMusicalCible = 8f;
    public float tempsMusicauxAvantFrappe = 4f;
    public float tailleAccent = 1.25f;
    public float duree = 0f;
    public float PositionY => transform.position.y;
    public float tempsFinMaintien = 0f;
    public float tempsFinRafale = 0f;
    public float tempsDerniereFrappeRafale = 0f;
    public float delaiMaximumEntreFrappes = 0.9f;
    public string typeNote = "simple";
    public bool estAccent = false;
    public bool enCoursDeMaintien = false;
    public bool rafaleEnCours = false;


    private GameObject corpsNoteLongue;
    private GameObject jaugeRafale;
    private GameObject remplissageRafale;
    private Vector3 echelleInitialeJaugeRafale;
    private Vector3 echelleInitialeCorps;
    private Vector3 echelleInitialeRemplissageRafale;
    private SpriteRenderer renduSprite;
    private float basCorpsNoteLongue;
    private float basRemplissageRafale;
    


    void Start()
    {
        renduSprite = GetComponent<SpriteRenderer>();

        if (horlogeMusicale == null)
        {
            horlogeMusicale = FindFirstObjectByType<HorlogeMusicale>();
        }
    }

    void Update()
    {
        if (horlogeMusicale == null)
            return;

        if (enCoursDeMaintien)
        {
            transform.position = new Vector3(
                transform.position.x,
                -3f,
                transform.position.z
            );

            if (corpsNoteLongue != null && duree > 0f)
            {
                float tempsRestant =
                    tempsFinMaintien - (float)horlogeMusicale.positionMusique;

                float proportionRestante =
                    Mathf.Clamp01(tempsRestant / duree);

                corpsNoteLongue.transform.localScale = new Vector3(
                    echelleInitialeCorps.x,
                    echelleInitialeCorps.y * proportionRestante,
                    echelleInitialeCorps.z
                );
                SpriteRenderer renduCorps =
                    corpsNoteLongue.GetComponent<SpriteRenderer>();

                float hauteurActuelle =
                    renduCorps.sprite.bounds.size.y *
                    corpsNoteLongue.transform.localScale.y;

                corpsNoteLongue.transform.localPosition = new Vector3(
                    0f,
                    basCorpsNoteLongue + 2.5f + hauteurActuelle / 2f,
                    0f
                );
            }

            return;
        }

        if (rafaleEnCours)
        {
            transform.position = new Vector3(
                transform.position.x,
                -3f,
                transform.position.z
            );

            return;
        }
                
        float tempsDepart = tempsMusicalCible - tempsMusicauxAvantFrappe;

        if (horlogeMusicale.positionEnTemps < tempsDepart)
        {
            renduSprite.enabled = false;

            if (corpsNoteLongue != null)
                corpsNoteLongue.SetActive(false);

            if (jaugeRafale != null)
                jaugeRafale.SetActive(false);

            if (remplissageRafale != null)
                remplissageRafale.SetActive(false);

            return;
        }

        renduSprite.enabled = true;

        if (corpsNoteLongue != null)
            corpsNoteLongue.SetActive(true);


        if (jaugeRafale != null)
            jaugeRafale.SetActive(true);

        if (remplissageRafale != null)
            remplissageRafale.SetActive(true);


        float progression = (float)(
            (horlogeMusicale.positionEnTemps - tempsDepart)
            / tempsMusicauxAvantFrappe
        );

        float positionY = Mathf.LerpUnclamped(3f, -3f, progression);

        transform.position = new Vector3(
            transform.position.x,
            positionY,
            transform.position.z
        );
    }

    public void ConfigurerNote(
    int nouvellePiste,
    float nouveauTempsMusical,
    string nouveauType,
    float nouvelleDuree,
    int nouvelObjectif)
    {
        numeroPiste = nouvellePiste;
        tempsMusicalCible = nouveauTempsMusical;
        typeNote = nouveauType;
        duree = nouvelleDuree;
        objectif = nouvelObjectif;

        estAccent = typeNote == "accent";

        AppliquerTypeNote();
    }

    public void AppliquerTypeNote()
    {
        if (renduSprite == null)
        {
            renduSprite = GetComponent<SpriteRenderer>();
        }

        if (spriteNoteSimple != null)
        {
            renduSprite.sprite = spriteNoteSimple;
        }

        if (typeNote == "accent")
        {
            if (spriteNoteAccent != null)
            {
                renduSprite.sprite = spriteNoteAccent;
            }

            renduSprite.color = Color.white;
        }
        else if (typeNote == "longue")
        {
            renduSprite.color = Color.white;

            if (prefabCorpsNoteLongue != null)
            {
                corpsNoteLongue = Instantiate(
                    prefabCorpsNoteLongue,
                    transform
                );
                echelleInitialeCorps = corpsNoteLongue.transform.localScale;
                corpsNoteLongue.transform.localPosition = new Vector3(
                    0f,
                    11f,
                    0f
                );
            }
            SpriteRenderer renduCorps =
                corpsNoteLongue.GetComponent<SpriteRenderer>();

            float hauteurInitiale =
                renduCorps.sprite.bounds.size.y *
                corpsNoteLongue.transform.localScale.y;

            basCorpsNoteLongue =
                corpsNoteLongue.transform.localPosition.y -
                hauteurInitiale / 2f;
        }
        else if (typeNote == "rafale")
        {
            renduSprite.color = Color.white;

            if (prefabJaugeRafale != null)
            {
                jaugeRafale = Instantiate(prefabJaugeRafale, transform);

                echelleInitialeJaugeRafale = jaugeRafale.transform.localScale;

                jaugeRafale.transform.localScale = new Vector3(
                    echelleInitialeJaugeRafale.x,
                    echelleInitialeJaugeRafale.y,
                    echelleInitialeJaugeRafale.z
                );

                jaugeRafale.transform.localPosition = new Vector3(
                    0f,
                    11f,
                    0f
                );
            }

            if (prefabRemplissageRafale != null)
            {
                remplissageRafale = Instantiate(prefabRemplissageRafale, transform);

                echelleInitialeRemplissageRafale =
                    remplissageRafale.transform.localScale;

                remplissageRafale.transform.localScale = new Vector3(
                    echelleInitialeRemplissageRafale.x,
                    0f,
                    echelleInitialeRemplissageRafale.z
                );

                remplissageRafale.transform.localPosition = new Vector3(
                    0f,
                    11f,
                    0f
                );
            }

            SpriteRenderer renduRemplissage =
                remplissageRafale.GetComponent<SpriteRenderer>();

            float hauteurInitialeRemplissage =
                renduRemplissage.sprite.bounds.size.y *
                echelleInitialeRemplissageRafale.y;

            basRemplissageRafale =
                remplissageRafale.transform.localPosition.y -
                hauteurInitialeRemplissage / 2f;
                    }
    }




    public void MettreAJourJaugeRafale()
    {
        Debug.Log(
            "MAJ JAUGE | frappes = " + frappesRafale +
            " / " + objectif +
            " | remplissage existe = " + (remplissageRafale != null)
        );

        if (remplissageRafale == null || objectif <= 0)
            return;

        float progression = Mathf.Clamp01(
            (float)frappesRafale / objectif
        );

        float nouvelleEchelleY =
            echelleInitialeRemplissageRafale.y * progression;

        remplissageRafale.transform.localScale = new Vector3(
            echelleInitialeRemplissageRafale.x,
            nouvelleEchelleY,
            echelleInitialeRemplissageRafale.z
        );

        SpriteRenderer renduRemplissage =
            remplissageRafale.GetComponent<SpriteRenderer>();

        float hauteurActuelle =
            renduRemplissage.sprite.bounds.size.y *
            remplissageRafale.transform.localScale.y;

        remplissageRafale.transform.localPosition = new Vector3(
            0f,
            basRemplissageRafale + hauteurActuelle / 2f,
            0f
        );
    }

    public void JouerEffetReussite()
    {
        GameObject effetAUtiliser = prefabEffetReussite;

        if (typeNote == "accent" && prefabEffetReussiteAccent != null)
        {
            effetAUtiliser = prefabEffetReussiteAccent;
        }

        if (effetAUtiliser == null)
            return;

        Instantiate(
            effetAUtiliser,
            transform.position,
            Quaternion.identity
        );
    }
}