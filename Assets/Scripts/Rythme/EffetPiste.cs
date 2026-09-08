using System.Collections;
using UnityEngine;

public class EffetPiste : MonoBehaviour
{
    private SpriteRenderer renduSprite;
    private Color couleurInitiale;

    public Color couleurFrappe = Color.yellow;
    public float dureeEffet = 0.1f;

    void Start()
    {
        renduSprite = GetComponent<SpriteRenderer>();
        couleurInitiale = renduSprite.color;
    }

    public void JouerEffet()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        renduSprite.color = couleurFrappe;

        yield return new WaitForSeconds(dureeEffet);

        renduSprite.color = couleurInitiale;
    }
}