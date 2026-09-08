using UnityEngine;
using TMPro;

public class GestionFinMorceau : MonoBehaviour
{
    public HorlogeMusicale horlogeMusicale;
    public GestionScore gestionScore;
    public GameObject panneauFinMorceau;
    public TextMeshProUGUI texteScoreFinal;
    public TextMeshProUGUI texteComboFinal;
    public TextMeshProUGUI texteResultatsFinal;

    void Start()
    {
        panneauFinMorceau.SetActive(false);
    }

    void Update()
    {
        if (horlogeMusicale.morceauTermine && !panneauFinMorceau.activeSelf)
        {
            texteScoreFinal.text = "Score : " + gestionScore.score;
            texteComboFinal.text = "Meilleur combo : " + gestionScore.meilleurCombo;
            texteResultatsFinal.text =
                "Parfaits : " + gestionScore.nombreParfaits + "\n" +
                "Biens : " + gestionScore.nombreBiens + "\n" +
                "Ratés : " + gestionScore.nombreRates + "\n" +
                "Précision : " + gestionScore.PrecisionPourcentage.ToString("F1") + " %";
                
            panneauFinMorceau.SetActive(true);
        }
    }
}