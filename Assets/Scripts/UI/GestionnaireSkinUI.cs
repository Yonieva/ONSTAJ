using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionnaireSkin : MonoBehaviour
{
    public SkinGameplay skinActuel;


    // =========================================================
    // REFERENCES
    // =========================================================

    [Header("Fond gameplay")]
    public SpriteRenderer fondGameplay;


    [Header("Pistes")]
    public SpriteRenderer[] pistes;
    public SpriteRenderer[] contoursPistes;


    [Header("Zone de frappe")]
    public SpriteRenderer zoneFrappe;


    [Header("Touches")]
    public SpriteRenderer[] fondsTouches;


    [Header("UI Score / Combo")]
    public Image fondScore;
    public Image fondCombo;


    [Header("Polices")]
    public TextMeshProUGUI texteScore;
    public TextMeshProUGUI texteCombo;
    public TextMeshProUGUI texteJugement;
    public TextMeshProUGUI texteCompteRebours;


    [Header("Gestion du score")]
    public GestionScore gestionScore;


    [Header("Effets des pistes")]
    public EffetPiste[] effetsPistes;

    [Header("Textes touches")]
    public TMP_Text[] textesTouches;


    // =========================================================
    // VALEURS DE BASE DE LA SCENE
    // =========================================================

    private Vector3 echelleInitialeFond;

    private Vector3[] echellesInitialesPistes;
    private Vector3[] echellesInitialesContours;

    private Vector3 echelleInitialeZoneFrappe;

    private Vector3[] echellesInitialesTouches;

    private Vector3 echelleInitialeFondScore;
    private Vector3 echelleInitialeFondCombo;


    // Tailles de texte déjà réglées dans Unity

    private float tailleInitialeScore;
    private float tailleInitialeCombo;
    private float tailleInitialeJugement;
    private float tailleInitialeCompteRebours;


    // =========================================================
    // UNITY
    // =========================================================

    void Awake()
    {
        MemoriserValeursInitiales();
    }


    void Start()
    {
        AppliquerSkin();
    }


    // =========================================================
    // MEMORISATION DES VALEURS DE BASE
    // =========================================================

    private void MemoriserValeursInitiales()
    {
        // FOND

        if (fondGameplay != null)
        {
            echelleInitialeFond =
                fondGameplay.transform.localScale;
        }


        // PISTES

        if (pistes != null)
        {
            echellesInitialesPistes =
                new Vector3[pistes.Length];

            for (int i = 0; i < pistes.Length; i++)
            {
                if (pistes[i] != null)
                {
                    echellesInitialesPistes[i] =
                        pistes[i].transform.localScale;
                }
            }
        }


        // CONTOURS

        if (contoursPistes != null)
        {
            echellesInitialesContours =
                new Vector3[contoursPistes.Length];

            for (int i = 0; i < contoursPistes.Length; i++)
            {
                if (contoursPistes[i] != null)
                {
                    echellesInitialesContours[i] =
                        contoursPistes[i].transform.localScale;
                }
            }
        }


        // ZONE DE FRAPPE

        if (zoneFrappe != null)
        {
            echelleInitialeZoneFrappe =
                zoneFrappe.transform.localScale;
        }


        // TOUCHES

        if (fondsTouches != null)
        {
            echellesInitialesTouches =
                new Vector3[fondsTouches.Length];

            for (int i = 0; i < fondsTouches.Length; i++)
            {
                if (fondsTouches[i] != null)
                {
                    echellesInitialesTouches[i] =
                        fondsTouches[i].transform.localScale;
                }
            }
        }


        // FOND SCORE

        if (fondScore != null)
        {
            echelleInitialeFondScore =
                fondScore.rectTransform.localScale;
        }


        // FOND COMBO

        if (fondCombo != null)
        {
            echelleInitialeFondCombo =
                fondCombo.rectTransform.localScale;
        }


        // TAILLE SCORE

        if (texteScore != null)
        {
            tailleInitialeScore =
                texteScore.fontSize;
        }


        // TAILLE COMBO

        if (texteCombo != null)
        {
            tailleInitialeCombo =
                texteCombo.fontSize;
        }


        // TAILLE JUGEMENT

        if (texteJugement != null)
        {
            tailleInitialeJugement =
                texteJugement.fontSize;
        }


        // TAILLE COMPTE A REBOURS

        if (texteCompteRebours != null)
        {
            tailleInitialeCompteRebours =
                texteCompteRebours.fontSize;
        }
    }


    // =========================================================
    // APPLICATION DU SKIN
    // =========================================================

    public void AppliquerSkin()
    {
        if (skinActuel == null)
        {
            Debug.LogWarning(
                "Aucun skin gameplay sélectionné."
            );

            return;
        }


        // =====================================================
        // FOND
        // =====================================================

        if (fondGameplay != null)
        {
            if (skinActuel.fondGameplay != null)
            {
                fondGameplay.sprite =
                    skinActuel.fondGameplay;
            }


            fondGameplay.transform.localScale =
                new Vector3(
                    echelleInitialeFond.x *
                    skinActuel.multiplicateurLargeurFondGameplay,

                    echelleInitialeFond.y *
                    skinActuel.multiplicateurHauteurFondGameplay,

                    echelleInitialeFond.z
                );
        }


        // =====================================================
        // PISTES
        // =====================================================

        if (pistes != null)
        {
            for (int i = 0; i < pistes.Length; i++)
            {
                if (pistes[i] == null)
                    continue;


                if (skinActuel.spritePiste != null)
                {
                    pistes[i].sprite =
                        skinActuel.spritePiste;
                }


                pistes[i].color =
                    skinActuel.couleurPiste;


                if (
                    echellesInitialesPistes != null &&
                    i < echellesInitialesPistes.Length
                )
                {
                    pistes[i].transform.localScale =
                        new Vector3(
                            echellesInitialesPistes[i].x *
                            skinActuel.multiplicateurLargeurPiste,

                            echellesInitialesPistes[i].y *
                            skinActuel.multiplicateurHauteurPiste,

                            echellesInitialesPistes[i].z
                        );
                }
            }
        }


        // =====================================================
        // CONTOURS
        // =====================================================

        if (contoursPistes != null)
        {
            for (int i = 0; i < contoursPistes.Length; i++)
            {
                if (contoursPistes[i] == null)
                    continue;


                if (skinActuel.spriteContourPiste != null)
                {
                    contoursPistes[i].sprite =
                        skinActuel.spriteContourPiste;
                }


                contoursPistes[i].color =
                    skinActuel.couleurContourPiste;


                if (
                    echellesInitialesContours != null &&
                    i < echellesInitialesContours.Length
                )
                {
                    contoursPistes[i].transform.localScale =
                        new Vector3(
                            echellesInitialesContours[i].x *
                            skinActuel.multiplicateurLargeurContourPiste,

                            echellesInitialesContours[i].y *
                            skinActuel.multiplicateurHauteurContourPiste,

                            echellesInitialesContours[i].z
                        );
                }
            }
        }


        // =====================================================
        // ZONE DE FRAPPE
        // =====================================================

        if (zoneFrappe != null)
        {
            if (skinActuel.spriteZoneFrappe != null)
            {
                zoneFrappe.sprite =
                    skinActuel.spriteZoneFrappe;
            }


            zoneFrappe.transform.localScale =
                new Vector3(
                    echelleInitialeZoneFrappe.x *
                    skinActuel.multiplicateurLargeurZoneFrappe,

                    echelleInitialeZoneFrappe.y *
                    skinActuel.multiplicateurHauteurZoneFrappe,

                    echelleInitialeZoneFrappe.z
                );
        }


        // =====================================================
        // TOUCHES D / F / J / K
        // =====================================================

        if (fondsTouches != null)
        {
            for (int i = 0; i < fondsTouches.Length; i++)
            {
                if (fondsTouches[i] == null)
                    continue;


                if (skinActuel.spriteFondTouche != null)
                {
                    fondsTouches[i].sprite =
                        skinActuel.spriteFondTouche;
                }


                if (
                    echellesInitialesTouches != null &&
                    i < echellesInitialesTouches.Length
                )
                {
                    fondsTouches[i].transform.localScale =
                        new Vector3(
                            echellesInitialesTouches[i].x *
                            skinActuel.multiplicateurLargeurFondTouche,

                            echellesInitialesTouches[i].y *
                            skinActuel.multiplicateurHauteurFondTouche,

                            echellesInitialesTouches[i].z
                        );
                }
            }
        }

        if (texteScore != null)
        {
            texteScore.color =
                skinActuel.couleurTexteScore;
        }

        if (texteCombo != null)
        {
            texteCombo.color =
                skinActuel.couleurTexteCombo;
        }

        if (texteJugement != null)
        {
            texteJugement.color =
                skinActuel.couleurTexteJugement;
        }

        if (textesTouches != null)
        {
            foreach (TMP_Text texteTouche in textesTouches)
            {
                if (texteTouche != null)
                {
                    texteTouche.color =
                        skinActuel.couleurTexteTouches;
                }
            }
        }


        // =====================================================
        // FOND SCORE
        // =====================================================

        if (fondScore != null)
        {
            if (skinActuel.spriteFondScore != null)
            {
                fondScore.sprite =
                    skinActuel.spriteFondScore;
            }


            fondScore.rectTransform.localScale =
                new Vector3(
                    echelleInitialeFondScore.x *
                    skinActuel.multiplicateurLargeurFondScore,

                    echelleInitialeFondScore.y *
                    skinActuel.multiplicateurHauteurFondScore,

                    echelleInitialeFondScore.z
                );
        }


        // =====================================================
        // FOND COMBO
        // =====================================================

        if (fondCombo != null)
        {
            if (skinActuel.spriteFondCombo != null)
            {
                fondCombo.sprite =
                    skinActuel.spriteFondCombo;
            }


            fondCombo.rectTransform.localScale =
                new Vector3(
                    echelleInitialeFondCombo.x *
                    skinActuel.multiplicateurLargeurFondCombo,

                    echelleInitialeFondCombo.y *
                    skinActuel.multiplicateurHauteurFondCombo,

                    echelleInitialeFondCombo.z
                );
        }


        // =====================================================
        // POLICE SCORE / COMBO / JUGEMENT
        // =====================================================

        if (skinActuel.policeInterface != null)
        {
            if (texteScore != null)
            {
                texteScore.font =
                    skinActuel.policeInterface;
            }


            if (texteCombo != null)
            {
                texteCombo.font =
                    skinActuel.policeInterface;
            }


            if (texteJugement != null)
            {
                texteJugement.font =
                    skinActuel.policeInterface;
            }
        }


        // =====================================================
        // TAILLE SCORE
        // =====================================================

        if (texteScore != null)
        {
            texteScore.fontSize =
                tailleInitialeScore *
                skinActuel.multiplicateurTaillePoliceScore;
        }


        // =====================================================
        // TAILLE COMBO
        // =====================================================

        if (texteCombo != null)
        {
            texteCombo.fontSize =
                tailleInitialeCombo *
                skinActuel.multiplicateurTaillePoliceCombo;
        }


        // =====================================================
        // TAILLE JUGEMENT
        // =====================================================

        if (texteJugement != null)
        {
            texteJugement.fontSize =
                tailleInitialeJugement *
                skinActuel.multiplicateurTaillePoliceJugement;
        }


        // =====================================================
        // COMPTE A REBOURS
        // =====================================================

        if (texteCompteRebours != null)
        {
            if (skinActuel.policeCompteRebours != null)
            {
                texteCompteRebours.font =
                    skinActuel.policeCompteRebours;
            }


            texteCompteRebours.fontSize =
                tailleInitialeCompteRebours *
                skinActuel.multiplicateurTailleCompteRebours;
        }


        // =====================================================
        // COULEURS JUGEMENTS
        // =====================================================

        if (gestionScore != null)
        {
            gestionScore.couleurParfait =
                skinActuel.couleurParfait;


            gestionScore.couleurBien =
                skinActuel.couleurBien;


            gestionScore.couleurRate =
                skinActuel.couleurRate;
        }


        // =====================================================
        // EFFETS DES PISTES
        // =====================================================

        if (effetsPistes != null)
        {
            foreach (EffetPiste effetPiste in effetsPistes)
            {
                if (effetPiste != null)
                {
                    effetPiste.AppliquerSkin(
                        skinActuel
                    );
                }
            }
        }
    }
}