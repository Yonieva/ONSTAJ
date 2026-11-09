using UnityEngine;

public class LecteurPartitionJson : MonoBehaviour
{
    [System.Serializable]
    public class NoteJson
    {
        public float temps;
        public string type;
        public float duree;
        public int objectif;
    }

    [System.Serializable]
    public class PartitionJson
    {
        public float bpm;
        public float duree;
        public string instrument;
        public string subdivision;
        public float decalage_grille;
        public int nombre_notes;
        public NoteJson[] notes;
    }


    public TextAsset fichierJson;
    public PartitionJson partitionChargee;
    public GameObject prefabNote;
    public GestionnaireSkin gestionnaireSkin;

    private float[] pisteOccupeeJusqua = new float[5];
    private int pisteSecondaireRafale = 0;
    private float rafaleOccupeeJusqua = 0f;


    void Start()
    {
        if (fichierJson == null)
        {
            Debug.LogError("Aucun fichier JSON assigné.");
            return;
        }

        partitionChargee =
            JsonUtility.FromJson<PartitionJson>(fichierJson.text);

        Debug.Log(
            "Partition chargée : " +
            partitionChargee.nombre_notes +
            " notes à " +
            partitionChargee.bpm +
            " BPM"
        );


        foreach (NoteJson noteJson in partitionChargee.notes)
        {
            int numeroPisteActuelle;
            int tentatives = 0;

            if (noteJson.temps < rafaleOccupeeJusqua)
            {
                // Pendant une rafale, on n'autorise que des notes simples ou accent
                // sur UNE seule piste secondaire.
                if (noteJson.type == "longue" || noteJson.type == "rafale")
                {
                    continue;
                }

                if (noteJson.temps >= pisteOccupeeJusqua[pisteSecondaireRafale])
                {
                    numeroPisteActuelle = pisteSecondaireRafale;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                do
                {
                    numeroPisteActuelle = Random.Range(1, 5);
                    tentatives++;
                }
                while (
                    noteJson.temps < pisteOccupeeJusqua[numeroPisteActuelle]
                    && tentatives < 20
                );
            }

            if (noteJson.type == "rafale")
            {
                do
                {
                    pisteSecondaireRafale = Random.Range(1, 5);
                }
                while (pisteSecondaireRafale == numeroPisteActuelle);

                rafaleOccupeeJusqua = noteJson.temps + noteJson.duree + 1f;
            }

            float positionX = -3f;

            switch (numeroPisteActuelle)
            {
                case 1:
                    positionX = -4.5f;
                    break;

                case 2:
                    positionX = -2.5f;
                    break;

                case 3:
                    positionX = 2.5f;
                    break;

                case 4:
                    positionX = 4.5f;
                    break;
            }

            GameObject nouvelleNote = Instantiate(
                prefabNote,
                new Vector3(positionX, 3f, 0f),
                Quaternion.identity
            );

            NoteRythmique noteRythmique =
                nouvelleNote.GetComponent<NoteRythmique>();

            if (
                gestionnaireSkin != null &&
                gestionnaireSkin.skinActuel != null
            )
            {
                noteRythmique.AppliquerSkin(
                    gestionnaireSkin.skinActuel
                );
            }

            noteRythmique.ConfigurerNote(
                numeroPisteActuelle,
                noteJson.temps * partitionChargee.bpm / 60f,
                noteJson.type,
                noteJson.duree,
                noteJson.objectif
            );

            if (noteJson.type == "longue" || noteJson.type == "rafale")
            {
                pisteOccupeeJusqua[numeroPisteActuelle] =
                    noteJson.temps + noteJson.duree + 1f;
            }

        }
    }

}