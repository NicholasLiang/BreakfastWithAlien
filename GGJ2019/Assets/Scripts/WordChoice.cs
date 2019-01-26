using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordChoice : MonoBehaviour {

    DialogChooser dialog;
    public string value;
    public bool EndSentence;
    public List<Button> NextWords;


	// Use this for initialization
	void Start () {
        dialog = FindObjectOfType<Canvas>().GetComponent<DialogChooser>(); //Ugly hack but it's a jam.
	}

    public void AddWord(){
        dialog.AddToString(value);
        if(EndSentence){
            dialog.EvaluateKey();
        }else{
            RevealNext();
        }
    }

    //public void Back(){
    //    dialog.BackWord();
    //}

    public void RevealNext(){
        foreach(Button b in NextWords){
            b.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    //public void RevealPrevious(){
        
    //}

}
