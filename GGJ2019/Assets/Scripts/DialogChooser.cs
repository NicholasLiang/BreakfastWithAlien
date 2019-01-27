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
    public Animator animator;
    public AudioClip Music;
    public AudioClip TalkingSound;
    public AudioSource Speaker;
    public Text Output;
    public List<string> Prompts;
    public List<int> ActivePrompts;
    public string defaultPrompt;
    [SerializeField] public List<KeyValuePair> ResponseList;
    Dictionary<string, Response> Responses;
    public Button[] InitialButtons;
    public Button[] AllButtons;

    Coroutine GameEnd;
    Coroutine Animating;



    List<string> ActiveKey;

	public void Start()
	{
        ActiveKey = new List<string>();
        Responses = new Dictionary<string, Response>();
        foreach(KeyValuePair kvp in ResponseList){
            Responses.Add(kvp.Key, kvp.Response);
        }
        AllButtons = FindObjectsOfType<Button>();

        ActivePrompts = new List<int>();
        PopulatePrompts();

        ResetButtons();
	}

    void PopulatePrompts(){
        for (int i = 0; i < Prompts.Count; i++)
        {
            ActivePrompts.Add(i);
        }
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
        Response response;
        Responses.TryGetValue(key, out response);
        if (response == null)
        {
            Debug.Log(key + " not found.");
            Output.text = (key + " not found.");
        }
        else
        {
            string dialog = response.Answer;
            if (response.ChoosePrompt)
            {
                dialog += "\n " + GetPrompt();
            }

            Output.text = dialog;
            StartCoroutine(Animate(dialog.Length));
        }
        if (key == "WhoMadeGame")
        {
            StartCoroutine(ClearVisuals());
        }
        else
        {
            ResetButtons();
        }

        ActiveKey.Clear();
        
    }

    private string GetPrompt(){
        if (Prompts.Count>0)
        {
            int index = Random.Range(0, ActivePrompts.Count);
            string res = Prompts[ActivePrompts[index]];
            ActivePrompts.RemoveAt(index);
            return res;
        }else{
            return defaultPrompt;
        }
    }

    void ResetGame(){
        ActivePrompts.Clear();
        PopulatePrompts();
        ResetButtons();
    }

    void ResetButtons(){
        HideButtons();
        foreach (Button b in InitialButtons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void HideButtons(){
        foreach (Button b in AllButtons)
        {
            b.gameObject.SetActive(false);
        }
    }

    IEnumerator Animate(float length){
        //Start animation
        animator.SetBool("Talking", true);
        yield return new WaitForSeconds(length * 0.01f);
        animator.SetBool("Talking", false);
        //End Animation
    }

    IEnumerator ClearVisuals()
    {
        yield return new WaitForSeconds(60);
        ResetGame();
    }
}
