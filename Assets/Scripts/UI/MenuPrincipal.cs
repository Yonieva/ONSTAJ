using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using TMPro;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panneauOptions;

    public void Jouer()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OuvrirOptions()
    {
        panneauOptions.SetActive(true);
    }

    public void FermerOptions()
    {
        panneauOptions.SetActive(false);
    }

    public void QuitterJeu()
    {
        Debug.Log("Quitter le jeu");

        Application.Quit();
    }

    public TMP_Dropdown dropdownLangue;

    public void ChangerLangue(int indexLangue)
    {
        StartCoroutine(AppliquerLangue(indexLangue));
    }

    private IEnumerator AppliquerLangue(int indexLangue)
    {
        yield return LocalizationSettings.InitializationOperation;

        if (
            indexLangue >= 0 &&
            indexLangue < LocalizationSettings.AvailableLocales.Locales.Count
        )
        {
            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[indexLangue];
        }
    }
}