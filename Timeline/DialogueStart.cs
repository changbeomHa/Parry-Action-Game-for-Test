using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    [SerializeField] InteractionEvent theIE;
    [SerializeField] DialogueManager theDM;
    // Start is called before the first frame update
    void Start()
    {
        theDM.ShowDIalogue(theIE.GetDialogue()); 
    }

    public void CharacterImageChange()
    {

    }
}
