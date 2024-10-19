using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSceneDialogueController : MonoBehaviour
{
    [SerializeField] DialogueManager theDM;
    [SerializeField] InteractionEvent theIE;

    public void DialogueStart()
    {
        theDM.ShowDIalogue(theIE.GetDialogue()); 
    }
}
