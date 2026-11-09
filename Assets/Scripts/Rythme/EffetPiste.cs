using System.Collections;
using UnityEngine;

public class EffetPiste : MonoBehaviour
{
    private SpriteRenderer renduSprite;
    private Color couleurInitiale;

    public Color couleurFrappe = Color.yellow;
    public float dureeEffet = 0.1f;

    private GameObject prefabEffetTouche;

    void Start()
    {
        renduSprite = GetComponent<SpriteRenderer>();
        couleurInitiale = renduSprite.color;
    }

    public void AppliquerSkin(SkinGameplay skin)
    {
        if (skin == null)
            return;

        couleurInitiale = skin.couleurPiste;
        couleurFrappe = skin.couleurEffetTouche;
        prefabEffetTouche = skin.prefabEffetTouche;

        if (renduSprite != null)
        {
            renduSprite.color = couleurInitiale;
        }
    }

    public void JouerEffet()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());

        if (prefabEffetTouche != null)
        {
            Instantiate(
                prefabEffetTouche,
                transform.position,
                Quaternion.identity
            );
        }
    }

    IEnumerator Flash()
    {
        renduSprite.color = couleurFrappe;

        yield return new WaitForSeconds(dureeEffet);

        renduSprite.color = couleurInitiale;
    }
}