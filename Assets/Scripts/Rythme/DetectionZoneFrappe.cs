using UnityEngine;

public class DetectionZoneFrappe : MonoBehaviour
{
    private NoteRythmique[] notesDansZone = new NoteRythmique[5];

    public GestionScore gestionScore;

    public NoteRythmique ObtenirNoteDansZone(int numeroPiste)
    {
        return notesDansZone[numeroPiste];
    }

    public void RetirerNote(int numeroPiste)
    {
        notesDansZone[numeroPiste] = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            NoteRythmique note = collision.GetComponent<NoteRythmique>();

            if (note != null)
            {
                notesDansZone[note.numeroPiste] = note;
                Debug.Log("Une note entre dans la zone - piste " + note.numeroPiste);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            NoteRythmique note = collision.GetComponent<NoteRythmique>();

            if (note != null && notesDansZone[note.numeroPiste] == note)
            {
                notesDansZone[note.numeroPiste] = null;

                // Une note longue déjà attrapée continue son maintien
                // même lorsqu'elle quitte physiquement la zone de frappe.
                if (note.enCoursDeMaintien || note.rafaleEnCours)
                {
                    return;
                }

                Debug.Log("RATÉ ! Piste " + note.numeroPiste);

                if (gestionScore != null)
                {
                    gestionScore.AjouterRate();
                }

                Destroy(note.gameObject);
            }
        }
    }
}