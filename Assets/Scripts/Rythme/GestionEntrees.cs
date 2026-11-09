using UnityEngine;
using UnityEngine.InputSystem;

public class GestionEntrees : MonoBehaviour
{
    public DetectionZoneFrappe zoneFrappe;
    public GestionScore gestionScore;
    public EffetPiste effetPiste1;
    public EffetPiste effetPiste2;
    public EffetPiste effetPiste3;
    public EffetPiste effetPiste4;
    public float dureeProtectionApresRafale = 0.6f;

    private NoteRythmique[] notesLonguesEnCours = new NoteRythmique[5];
    private NoteRythmique[] rafalesEnCours = new NoteRythmique[5];
    private float[] protectionApresRafaleJusqua = new float[5];

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            effetPiste1.JouerEffet();

            if (rafalesEnCours[1] != null)
                AjouterFrappeRafale(1);
            else
                TesterFrappe(1);
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            effetPiste2.JouerEffet();

            if (rafalesEnCours[2] != null)
                AjouterFrappeRafale(2);
            else
                TesterFrappe(2);
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            effetPiste3.JouerEffet();

            if (rafalesEnCours[3] != null)
                AjouterFrappeRafale(3);
            else
                TesterFrappe(3);
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            effetPiste4.JouerEffet();

            if (rafalesEnCours[4] != null)
                AjouterFrappeRafale(4);
            else
                TesterFrappe(4);
        }

        VerifierMaintien(1, Keyboard.current.dKey.isPressed);
        VerifierMaintien(2, Keyboard.current.fKey.isPressed);
        VerifierMaintien(3, Keyboard.current.jKey.isPressed);
        VerifierMaintien(4, Keyboard.current.kKey.isPressed);
        VerifierRafale(1);
        VerifierRafale(2);
        VerifierRafale(3);
        VerifierRafale(4);
    }

    void TesterFrappe(int numeroPiste)
    {
        if (Time.time < protectionApresRafaleJusqua[numeroPiste])
        {
            protectionApresRafaleJusqua[numeroPiste] =
                Time.time + dureeProtectionApresRafale;

            return;
        }

        NoteRythmique note = zoneFrappe.ObtenirNoteDansZone(numeroPiste);

        if (note == null)
        {
            Debug.Log("Raté ! Piste " + numeroPiste);
            gestionScore.AjouterRate();
            return;
        }

        if (note.typeNote == "longue")
        {
            note.enCoursDeMaintien = true;

            note.tempsFinMaintien =
                (float)note.horlogeMusicale.positionMusique + note.duree;

            notesLonguesEnCours[numeroPiste] = note;

            Debug.Log("MAINTIEN DÉMARRÉ ! Piste " + numeroPiste);

            return;
        }

        if (note.typeNote == "rafale")
        {
            if (note.rafaleEnCours)
                return;
            note.rafaleEnCours = true;
            note.frappesRafale = 1;
            note.MettreAJourJaugeRafale();

            note.tempsFinRafale =
                (float)note.horlogeMusicale.positionMusique + note.duree;
            note.tempsDerniereFrappeRafale =
                (float)note.horlogeMusicale.positionMusique;

            rafalesEnCours[numeroPiste] = note;
            zoneFrappe.RetirerNote(numeroPiste);

            Debug.Log("RAFALE DÉMARRÉE ! Piste " + numeroPiste);

            return;
        }

        float distance = Mathf.Abs(
            note.PositionY - zoneFrappe.transform.position.y
        );

        if (distance <= 0.10f)
        {
            if (note.estAccent)
            {
                Debug.Log("ACCENT ! Piste " + numeroPiste);
                gestionScore.AjouterAccent();
            }
            else
            {
                Debug.Log("PARFAIT ! Piste " + numeroPiste);
                gestionScore.AjouterParfait();
            }
        }
        else
        {
            Debug.Log("BIEN ! Piste " + numeroPiste);
            gestionScore.AjouterBien();
        }

        note.JouerEffetReussite();
        zoneFrappe.RetirerNote(numeroPiste);
        Destroy(note.gameObject);
    }

    void AjouterFrappeRafale(int numeroPiste)
    {
        NoteRythmique note = rafalesEnCours[numeroPiste];

        if (note == null)
            return;
    
        note.frappesRafale++;
        note.tempsDerniereFrappeRafale =
            (float)note.horlogeMusicale.positionMusique;
        note.MettreAJourJaugeRafale();
        if (note.frappesRafale >= note.objectif)
        {
            Debug.Log("RAFALE RÉUSSIE ! Piste " + numeroPiste);

            gestionScore.AjouterParfait();

            protectionApresRafaleJusqua[numeroPiste] =
                Time.time + dureeProtectionApresRafale;

            rafalesEnCours[numeroPiste] = null;

            note.JouerEffetReussite();

            Destroy(note.gameObject, 0.3f);

            return;
        }

    }

    void VerifierMaintien(int numeroPiste, bool toucheMaintenue)
    {
        NoteRythmique note = notesLonguesEnCours[numeroPiste];

        if (note == null)
            return;

        if (!toucheMaintenue)
        {
            Debug.Log("RATÉ ! Maintien interrompu - piste " + numeroPiste);

            gestionScore.AjouterRate();

            notesLonguesEnCours[numeroPiste] = null;

            Destroy(note.gameObject);

            return;
        }

        if (note.horlogeMusicale.positionMusique >= note.tempsFinMaintien)
        {
            Debug.Log("NOTE LONGUE RÉUSSIE ! Piste " + numeroPiste);

            gestionScore.AjouterParfait();

            notesLonguesEnCours[numeroPiste] = null;

            note.JouerEffetReussite();

            Destroy(note.gameObject);
        }
    }

    void VerifierRafale(int numeroPiste)
    {
        NoteRythmique note = rafalesEnCours[numeroPiste];

        if (note == null)
            return;

        float tempsActuel =
            (float)note.horlogeMusicale.positionMusique;

        // Le joueur a arrêté de tapoter trop longtemps
        if (tempsActuel - note.tempsDerniereFrappeRafale
            >= note.delaiMaximumEntreFrappes)
        {
            Debug.Log(
                "RATÉ ! Rafale interrompue - piste " +
                numeroPiste
            );

            gestionScore.AjouterRate();

            note.rafaleEnCours = false;
            rafalesEnCours[numeroPiste] = null;

            Destroy(note.gameObject);
            return;
        }

        // La durée maximale de la rafale est terminée
        if (tempsActuel >= note.tempsFinRafale)
        {
            Debug.Log(
                "RATÉ ! Fin de rafale - piste " +
                numeroPiste
            );

            gestionScore.AjouterRate();

            note.rafaleEnCours = false;
            rafalesEnCours[numeroPiste] = null;

            Destroy(note.gameObject);
        }
    }


}