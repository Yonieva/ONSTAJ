using UnityEngine;

public class PartitionTest : MonoBehaviour
{
    [System.Serializable]
    public class DonneeNote
    {
        public int numeroPiste;
        public float tempsMusicalCible;
    }

    public DonneeNote[] notes;
    public GameObject prefabNote;

    void Start()
    {
        foreach (DonneeNote donneeNote in notes)
        {
            float positionX = 0f;

            switch (donneeNote.numeroPiste)
            {
                case 1:
                    positionX = -3f;
                    break;

                case 2:
                    positionX = -1f;
                    break;

                case 3:
                    positionX = 1f;
                    break;

                case 4:
                    positionX = 3f;
                    break;
            }

            GameObject nouvelleNote = Instantiate(
                prefabNote,
                new Vector3(positionX, 3f, 0f),
                Quaternion.identity
            );

            NoteRythmique noteRythmique =
                nouvelleNote.GetComponent<NoteRythmique>();

            noteRythmique.numeroPiste = donneeNote.numeroPiste;
            noteRythmique.tempsMusicalCible = donneeNote.tempsMusicalCible;
        }
    }
}