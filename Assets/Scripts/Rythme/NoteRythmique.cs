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

    public float decalageVerticalLongue = 0f;
    public float decalageVerticalRafale = 0f;


    private GameObject corpsNoteLongue;
    private GameObject jaugeRafale;
    private GameObject remplissageRafale;

    private Vector3 echelleInitialeCorps;
    private Vector3 echelleInitialeJaugeRafale;
    private Vector3 echelleInitialeRemplissageRafale;

    private SpriteRenderer renduSprite;

    private float basCorpsNoteLongue;
    private float basRemplissageRafale;


    // NOTES

    private float multiplicateurLargeurNoteSimple = 1f;
    private float multiplicateurHauteurNoteSimple = 1f;

    private float multiplicateurLargeurNoteAccent = 1f;
    private float multiplicateurHauteurNoteAccent = 1f;


    // LONGUE

    private float multiplicateurLargeurCorpsNoteLongue = 1f;
    private float multiplicateurHauteurCorpsNoteLongue = 1f;


    // RAFALE

    private float multiplicateurLargeurJaugeRafale = 1f;
    private float multiplicateurHauteurJaugeRafale = 1f;

    private float multiplicateurLargeurRemplissageRafale = 1f;
    private float multiplicateurHauteurRemplissageRafale = 1f;


    // EFFETS

    private float multiplicateurLargeurEffetReussite = 1f;
    private float multiplicateurHauteurEffetReussite = 1f;

    private float multiplicateurLargeurEffetReussiteAccent = 1f;
    private float multiplicateurHauteurEffetReussiteAccent = 1f;


    void Start()
    {
        renduSprite = GetComponent<SpriteRenderer>();

        if (horlogeMusicale == null)
        {
            horlogeMusicale =
                FindFirstObjectByType<HorlogeMusicale>();
        }
    }


    void Update()
    {
        if (horlogeMusicale == null)
            return;


        // NOTE LONGUE EN COURS

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
                    tempsFinMaintien -
                    (float)horlogeMusicale.positionMusique;

                float proportionRestante =
                    Mathf.Clamp01(
                        tempsRestant / duree
                    );

                corpsNoteLongue.transform.localScale =
                    new Vector3(
                        echelleInitialeCorps.x,
                        echelleInitialeCorps.y *
                        proportionRestante,
                        echelleInitialeCorps.z
                    );

                SpriteRenderer renduCorps =
                    corpsNoteLongue.GetComponent<SpriteRenderer>();

                float hauteurActuelle =
                    renduCorps.sprite.bounds.size.y *
                    corpsNoteLongue.transform.localScale.y;

                corpsNoteLongue.transform.localPosition =
                    new Vector3(
                        0f,
                        basCorpsNoteLongue +
                        hauteurActuelle / 2f,
                        0f
                    );
            }

            return;
        }


        // RAFALE EN COURS

        if (rafaleEnCours)
        {
            transform.position = new Vector3(
                transform.position.x,
                -3f,
                transform.position.z
            );

            return;
        }


        float tempsDepart =
            tempsMusicalCible -
            tempsMusicauxAvantFrappe;


        // NOTE PAS ENCORE VISIBLE

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


        // NOTE VISIBLE

        renduSprite.enabled = true;

        if (corpsNoteLongue != null)
            corpsNoteLongue.SetActive(true);

        if (jaugeRafale != null)
            jaugeRafale.SetActive(true);

        if (remplissageRafale != null)
            remplissageRafale.SetActive(true);


        float progression =
            (float)(
                (
                    horlogeMusicale.positionEnTemps -
                    tempsDepart
                )
                /
                tempsMusicauxAvantFrappe
            );


        float positionY =
            Mathf.LerpUnclamped(
                3f,
                -3f,
                progression
            );


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
        int nouvelObjectif
    )
    {
        numeroPiste = nouvellePiste;
        tempsMusicalCible = nouveauTempsMusical;
        typeNote = nouveauType;
        duree = nouvelleDuree;
        objectif = nouvelObjectif;

        estAccent =
            typeNote == "accent";

        AppliquerTypeNote();
    }


    public void AppliquerTypeNote()
    {
        if (renduSprite == null)
        {
            renduSprite =
                GetComponent<SpriteRenderer>();
        }


        // NOTE SIMPLE PAR DÉFAUT

        if (spriteNoteSimple != null)
        {
            renduSprite.sprite =
                spriteNoteSimple;
        }


        Vector3 echelleBaseNote =
            transform.localScale;


        // ACCENT

        if (typeNote == "accent")
        {
            transform.localScale =
                new Vector3(
                    echelleBaseNote.x *
                    multiplicateurLargeurNoteAccent,

                    echelleBaseNote.y *
                    multiplicateurHauteurNoteAccent,

                    echelleBaseNote.z
                );

            if (spriteNoteAccent != null)
            {
                renduSprite.sprite =
                    spriteNoteAccent;
            }

            renduSprite.color =
                Color.white;
        }


        // LONGUE

        else if (typeNote == "longue")
        {
            transform.localScale =
                new Vector3(
                    echelleBaseNote.x *
                    multiplicateurLargeurNoteSimple,

                    echelleBaseNote.y *
                    multiplicateurHauteurNoteSimple,

                    echelleBaseNote.z
                );

            renduSprite.color =
                Color.white;


            if (prefabCorpsNoteLongue != null)
            {
                corpsNoteLongue =
                    Instantiate(
                        prefabCorpsNoteLongue,
                        transform
                    );


                Vector3 echelleBaseCorps =
                    corpsNoteLongue.transform.localScale;


                echelleInitialeCorps =
                    new Vector3(
                        echelleBaseCorps.x *
                        multiplicateurLargeurCorpsNoteLongue,

                        echelleBaseCorps.y *
                        multiplicateurHauteurCorpsNoteLongue,

                        echelleBaseCorps.z
                    );


                corpsNoteLongue.transform.localScale =
                    echelleInitialeCorps;


                SpriteRenderer renduCorps =
                    corpsNoteLongue.GetComponent<SpriteRenderer>();


                float hauteurCorps =
                    renduCorps.sprite.bounds.size.y *
                    corpsNoteLongue.transform.localScale.y;


                basCorpsNoteLongue =
                    ObtenirHautNote() +
                    decalageVerticalLongue;


                corpsNoteLongue.transform.localPosition =
                    new Vector3(
                        0f,
                        basCorpsNoteLongue +
                        hauteurCorps / 2f,
                        0f
                    );
            }
        }


        // RAFALE

        else if (typeNote == "rafale")
        {
            transform.localScale =
                new Vector3(
                    echelleBaseNote.x *
                    multiplicateurLargeurNoteSimple,

                    echelleBaseNote.y *
                    multiplicateurHauteurNoteSimple,

                    echelleBaseNote.z
                );

            renduSprite.color =
                Color.white;


            // CADRE JAUGE

            if (prefabJaugeRafale != null)
            {
                jaugeRafale =
                    Instantiate(
                        prefabJaugeRafale,
                        transform
                    );


                Vector3 echelleBaseJauge =
                    jaugeRafale.transform.localScale;


                echelleInitialeJaugeRafale =
                    new Vector3(
                        echelleBaseJauge.x *
                        multiplicateurLargeurJaugeRafale,

                        echelleBaseJauge.y *
                        multiplicateurHauteurJaugeRafale,

                        echelleBaseJauge.z
                    );


                jaugeRafale.transform.localScale =
                    echelleInitialeJaugeRafale;


                SpriteRenderer renduJauge =
                    jaugeRafale.GetComponent<SpriteRenderer>();


                float hauteurJauge =
                    renduJauge.sprite.bounds.size.y *
                    jaugeRafale.transform.localScale.y;


                float basJauge =
                    ObtenirHautNote() +
                    decalageVerticalRafale;


                jaugeRafale.transform.localPosition =
                    new Vector3(
                        0f,
                        basJauge +
                        hauteurJauge / 2f,
                        0f
                    );
            }


            // REMPLISSAGE

            if (prefabRemplissageRafale != null)
            {
                remplissageRafale =
                    Instantiate(
                        prefabRemplissageRafale,
                        transform
                    );


                Vector3 echelleBaseRemplissage =
                    remplissageRafale.transform.localScale;


                echelleInitialeRemplissageRafale =
                    new Vector3(
                        echelleBaseRemplissage.x *
                        multiplicateurLargeurRemplissageRafale,

                        echelleBaseRemplissage.y *
                        multiplicateurHauteurRemplissageRafale,

                        echelleBaseRemplissage.z
                    );


                remplissageRafale.transform.localScale =
                    new Vector3(
                        echelleInitialeRemplissageRafale.x,
                        0f,
                        echelleInitialeRemplissageRafale.z
                    );


                SpriteRenderer renduRemplissage =
                    remplissageRafale.GetComponent<SpriteRenderer>();


                float hauteurInitialeRemplissage =
                    renduRemplissage.sprite.bounds.size.y *
                    echelleInitialeRemplissageRafale.y;


                basRemplissageRafale =
                    ObtenirHautNote() +
                    decalageVerticalRafale;


                remplissageRafale.transform.localPosition =
                    new Vector3(
                        0f,
                        basRemplissageRafale +
                        hauteurInitialeRemplissage / 2f,
                        0f
                    );
            }
        }


        // SIMPLE

        else
        {
            transform.localScale =
                new Vector3(
                    echelleBaseNote.x *
                    multiplicateurLargeurNoteSimple,

                    echelleBaseNote.y *
                    multiplicateurHauteurNoteSimple,

                    echelleBaseNote.z
                );

            renduSprite.color =
                Color.white;
        }
    }


    public void MettreAJourJaugeRafale()
    {
        if (
            remplissageRafale == null ||
            objectif <= 0
        )
        {
            return;
        }


        float progression =
            Mathf.Clamp01(
                (float)frappesRafale /
                objectif
            );


        float nouvelleEchelleY =
            echelleInitialeRemplissageRafale.y *
            progression;


        remplissageRafale.transform.localScale =
            new Vector3(
                echelleInitialeRemplissageRafale.x,
                nouvelleEchelleY,
                echelleInitialeRemplissageRafale.z
            );


        SpriteRenderer renduRemplissage =
            remplissageRafale.GetComponent<SpriteRenderer>();


        float hauteurActuelle =
            renduRemplissage.sprite.bounds.size.y *
            remplissageRafale.transform.localScale.y;


        remplissageRafale.transform.localPosition =
            new Vector3(
                0f,
                basRemplissageRafale +
                hauteurActuelle / 2f,
                0f
            );
    }


    private float ObtenirHautNote()
    {
        if (
            renduSprite == null ||
            renduSprite.sprite == null
        )
        {
            return 0f;
        }

        return renduSprite.sprite.bounds.extents.y;
    }


    public void JouerEffetReussite()
    {
        GameObject effetAUtiliser =
            prefabEffetReussite;

        float multiplicateurLargeur =
            multiplicateurLargeurEffetReussite;

        float multiplicateurHauteur =
            multiplicateurHauteurEffetReussite;


        if (
            typeNote == "accent" &&
            prefabEffetReussiteAccent != null
        )
        {
            effetAUtiliser =
                prefabEffetReussiteAccent;

            multiplicateurLargeur =
                multiplicateurLargeurEffetReussiteAccent;

            multiplicateurHauteur =
                multiplicateurHauteurEffetReussiteAccent;
        }


        if (effetAUtiliser == null)
            return;


        GameObject effetCree =
            Instantiate(
                effetAUtiliser,
                transform.position,
                Quaternion.identity
            );


        Vector3 echelleBaseEffet =
            effetCree.transform.localScale;


        effetCree.transform.localScale =
            new Vector3(
                echelleBaseEffet.x *
                multiplicateurLargeur,

                echelleBaseEffet.y *
                multiplicateurHauteur,

                echelleBaseEffet.z
            );
    }


    public void AppliquerSkin(
        SkinGameplay skin
    )
    {
        if (skin == null)
            return;


        // SPRITES

        spriteNoteSimple =
            skin.spriteNoteSimple;

        spriteNoteAccent =
            skin.spriteNoteAccent;


        // PREFABS

        prefabCorpsNoteLongue =
            skin.prefabCorpsNoteLongue;

        prefabJaugeRafale =
            skin.prefabJaugeRafale;

        prefabRemplissageRafale =
            skin.prefabRemplissageRafale;

        prefabEffetReussite =
            skin.prefabEffetReussite;

        prefabEffetReussiteAccent =
            skin.prefabEffetReussiteAccent;


        // NOTE SIMPLE

        multiplicateurLargeurNoteSimple =
            skin.multiplicateurLargeurNoteSimple;

        multiplicateurHauteurNoteSimple =
            skin.multiplicateurHauteurNoteSimple;


        // NOTE ACCENT

        multiplicateurLargeurNoteAccent =
            skin.multiplicateurLargeurNoteAccent;

        multiplicateurHauteurNoteAccent =
            skin.multiplicateurHauteurNoteAccent;


        // LONGUE

        multiplicateurLargeurCorpsNoteLongue =
            skin.multiplicateurLargeurCorpsNoteLongue;

        multiplicateurHauteurCorpsNoteLongue =
            skin.multiplicateurHauteurCorpsNoteLongue;


        // JAUGE RAFALE

        multiplicateurLargeurJaugeRafale =
            skin.multiplicateurLargeurJaugeRafale;

        multiplicateurHauteurJaugeRafale =
            skin.multiplicateurHauteurJaugeRafale;


        // REMPLISSAGE RAFALE

        multiplicateurLargeurRemplissageRafale =
            skin.multiplicateurLargeurRemplissageRafale;

        multiplicateurHauteurRemplissageRafale =
            skin.multiplicateurHauteurRemplissageRafale;


        // EFFET NORMAL

        multiplicateurLargeurEffetReussite =
            skin.multiplicateurLargeurEffetReussite;

        multiplicateurHauteurEffetReussite =
            skin.multiplicateurHauteurEffetReussite;


        // EFFET ACCENT

        multiplicateurLargeurEffetReussiteAccent =
            skin.multiplicateurLargeurEffetReussiteAccent;

        multiplicateurHauteurEffetReussiteAccent =
            skin.multiplicateurHauteurEffetReussiteAccent;
    }
}