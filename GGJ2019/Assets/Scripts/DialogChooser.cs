using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogChooser : MonoBehaviour {

    [System.Serializable]
    public class KeyValuePair{
        public string Key;
        public Response Response;
    }

    [System.Serializable]
    public class Response{
        public string Answer;
        public bool ChoosePrompt;
    }

    public Text Output;
    public List<string> Prompts;
    public string defaultPrompt;
    [SerializeField] public List<KeyValuePair> ResponseList;
    Dictionary<string, Response> Responses;
    public Button[] InitialButtons;
    public Button[] AllButtons;

    List<string> ActiveKey;

	public void Start()
	{
        ActiveKey = new List<string>();
        Responses = new Dictionary<string, Response>();
        foreach(KeyValuePair kvp in ResponseList){
            Responses.Add(kvp.Key, kvp.Response);
        }
        AllButtons = FindObjectsOfType<Button>();
        ResetButtons();
	}

	public void AddToString(string seg)
    {
        ActiveKey.Add(seg);
    }

    public void BackWord(){
        ActiveKey.RemoveAt(ActiveKey.Count - 1);
    }

    public void EvaluateKey(){
        string key = "";
        foreach(string str in ActiveKey){
            key += str;
        }
        Response response = Responses[key];
        string dialog = response.Answer;
        if(response.ChoosePrompt){
            dialog += " " + GetPrompt();
        }
        ActiveKey.Clear();
        Output.text = dialog;

        ResetButtons();
    }

    private string GetPrompt(){
        if (Prompts.Count>0)
        {
            int index = Random.Range(0, Prompts.Count);
            string res = Prompts[index];
            Prompts.RemoveAt(index);
            return res;
        }else{
            return defaultPrompt;
        }
    }

    void ResetButtons(){
        foreach (Button b in AllButtons)
        {
            b.gameObject.SetActive(false);
        }
        foreach (Button b in InitialButtons)
        {
            b.gameObject.SetActive(true);
        }
    }
}
