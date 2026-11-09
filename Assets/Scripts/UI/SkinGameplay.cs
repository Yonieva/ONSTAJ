using UnityEngine;
using TMPro;

[CreateAssetMenu(
    fileName = "NouveauSkinGameplay",
    menuName = "ON STAJ/Skin Gameplay"
)]
public class SkinGameplay : ScriptableObject
{
    // =========================================================
    // FOND
    // =========================================================

    [Header("Fond")]
    public Sprite fondGameplay;

    public float multiplicateurLargeurFondGameplay = 1f;
    public float multiplicateurHauteurFondGameplay = 1f;


    // =========================================================
    // COMPTE A REBOURS
    // =========================================================

    [Header("Compte à rebours")]
    public TMP_FontAsset policeCompteRebours;

    public float multiplicateurTailleCompteRebours = 1f;


    // =========================================================
    // PISTES
    // =========================================================

    [Header("Pistes")]
    public Sprite spritePiste;
    public Sprite spriteContourPiste;

    public float multiplicateurLargeurPiste = 1f;
    public float multiplicateurHauteurPiste = 1f;

    public float multiplicateurLargeurContourPiste = 1f;
    public float multiplicateurHauteurContourPiste = 1f;


    // =========================================================
    // COULEURS PISTES
    // =========================================================

    [Header("Couleurs pistes")]
    public Color couleurPiste = Color.gray;
    public Color couleurContourPiste = Color.cyan;


    // =========================================================
    // ZONE DE FRAPPE
    // =========================================================

    [Header("Zone de frappe")]
    public Sprite spriteZoneFrappe;

    public float multiplicateurLargeurZoneFrappe = 1f;
    public float multiplicateurHauteurZoneFrappe = 1f;


    // =========================================================
    // NOTES
    // =========================================================

    [Header("Notes")]
    public Sprite spriteNoteSimple;
    public Sprite spriteNoteAccent;

    public float multiplicateurLargeurNoteSimple = 1f;
    public float multiplicateurHauteurNoteSimple = 1f;

    public float multiplicateurLargeurNoteAccent = 1f;
    public float multiplicateurHauteurNoteAccent = 1f;


    // =========================================================
    // NOTE LONGUE
    // =========================================================

    [Header("Longue")]
    public GameObject prefabCorpsNoteLongue;

    public float multiplicateurLargeurCorpsNoteLongue = 1f;
    public float multiplicateurHauteurCorpsNoteLongue = 1f;


    // =========================================================
    // RAFALE
    // =========================================================

    [Header("Rafale")]
    public GameObject prefabJaugeRafale;
    public GameObject prefabRemplissageRafale;

    public float multiplicateurLargeurJaugeRafale = 1f;
    public float multiplicateurHauteurJaugeRafale = 1f;

    public float multiplicateurLargeurRemplissageRafale = 1f;
    public float multiplicateurHauteurRemplissageRafale = 1f;


    // =========================================================
    // TOUCHES D / F / J / K
    // =========================================================

    [Header("Touches")]
    public Sprite spriteFondTouche;

    public float multiplicateurLargeurFondTouche = 1f;
    public float multiplicateurHauteurFondTouche = 1f;


    // =========================================================
    // UI SCORE / COMBO / JUGEMENT
    // =========================================================

    [Header("UI")]
    public Sprite spriteFondScore;
    public Sprite spriteFondCombo;

    public TMP_FontAsset policeInterface;

    public float multiplicateurLargeurFondScore = 1f;
    public float multiplicateurHauteurFondScore = 1f;

    public float multiplicateurLargeurFondCombo = 1f;
    public float multiplicateurHauteurFondCombo = 1f;


    [Header("Taille des textes UI")]
    public float multiplicateurTaillePoliceScore = 1f;
    public float multiplicateurTaillePoliceCombo = 1f;
    public float multiplicateurTaillePoliceJugement = 1f;


    [Header("Couleurs textes UI")]
    public Color couleurTexteScore = Color.white;
    public Color couleurTexteCombo = Color.white;
    public Color couleurTexteJugement = Color.white;
    public Color couleurTexteTouches = Color.white;


    // =========================================================
    // COULEURS JUGEMENTS
    // =========================================================

    [Header("Couleurs jugements")]
    public Color couleurParfait = Color.cyan;
    public Color couleurBien = Color.white;
    public Color couleurRate = Color.red;


    // =========================================================
    // EFFETS
    // =========================================================

    [Header("Effets")]
    public GameObject prefabEffetReussite;
    public GameObject prefabEffetReussiteAccent;
    public GameObject prefabEffetTouche;

    public Color couleurEffetTouche = Color.white;


    public float multiplicateurLargeurEffetReussite = 1f;
    public float multiplicateurHauteurEffetReussite = 1f;


    public float multiplicateurLargeurEffetReussiteAccent = 1f;
    public float multiplicateurHauteurEffetReussiteAccent = 1f;


    public float multiplicateurLargeurEffetTouche = 1f;
    public float multiplicateurHauteurEffetTouche = 1f;
}