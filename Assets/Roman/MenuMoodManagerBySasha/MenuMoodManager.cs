using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuMoodManager : MonoBehaviour
{
    [Header("MenuItems")]
    public TMP_Text moodtext;
    public TMP_Text moodquotetext;
    public Image moodemoji;

    [Header("InfoFromManager")]
    public Sprite moodsprite;
    public string curmoodtostring;
    public string curquote;

    [Header("Ref")]
    public GameObject moodManagerRef;
    public MoodManager mm;
    

    // Start is called before the first frame update
    void Start()
    {
        moodManagerRef = GameObject.Find("MoodManager");
        if(moodManagerRef != null )
        {
            mm = moodManagerRef.GetComponent<MoodManager>();
            moodtext.text = mm.currentMood.ToString();
            moodquotetext.text = mm.currentMoodQuote;
            moodemoji.sprite = mm.currentmoodsprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
