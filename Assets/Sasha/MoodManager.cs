using DataManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.IO;
using UnityEngine.Serialization;

public enum Moods { Щастя, Сум, Гнів, Страх, Радість, Відраза, Здивування, Відчай, Захват, Спокій };//id 1 - 10

public class MoodManager : MonoBehaviour, IMoodManager
{
    [SerializeField] private CardSpriteContainer _spriteContainer;
    
    private Dictionary<Moods, List<string>> _moodQuotes = new Dictionary<Moods, List<string>>();
    public Dictionary<int,Moods> moodsbyid = new Dictionary<int, Moods>();

    [Header("Current")]
    public Moods currentMood;
    public string currentMoodQuote;
    public Sprite currentmoodsprite;

    [Header("Quotes Lists")]
    public List<string> HappinessQ;
    public List<string> SadnessQ;
    public List<string> AngerQ;
    public List<string> FearQ;
    public List<string> JoynessQ;
    public List<string> DisgustionQ;
    public List<string> SurprisdedQ;
    public List<string> DespairQ;
    public List<string> DelightnessQ;
    public List<string> CalmQ;
    
    //saving
    private DataManagementSystem<MoodsData> dataManagementSystem;
    
    public Dictionary<Moods, List<string>> GetMoodsAndQuotes()
    {
        return _moodQuotes;
    }

    public void SaveMoodData()
    {
        MoodsData data = new MoodsData();
        data.Mood = currentMood;
        data.Quote = currentMoodQuote;
        data.currSprite = currentmoodsprite;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/saveMoodDataFile.json", json);
    }
    
    public void LoadMoodData()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveMoodDataFile.json");
        MoodsData data = JsonUtility.FromJson<MoodsData>(json);

        currentMood = data.Mood;
        currentMoodQuote = data.Quote;
        currentmoodsprite = data.currSprite;
    }
    
    public void SaveNewReasonAndMood(Moods mood, string quote)
    {

    }

    public void SaveReason()
    {
        
    }

    public void SetMoodQuote()
    {
        if(_moodQuotes.ContainsKey(currentMood))
        {
            if(_moodQuotes.TryGetValue(currentMood, out List<string> currentmoodquotes))
            {
                System.Random random = new System.Random();
                int randomIndex = random.Next(0, currentmoodquotes.Count);
                currentMoodQuote = currentmoodquotes[randomIndex];
            }
        }
    }
    
    public void SetMoodByButton(Moods mood)
    {
        currentMood = mood;
        currentmoodsprite = _spriteContainer.GetSprite(currentMood);
        SetMoodQuote();
    }

    public Sprite GetSpriteForMood(Moods mood)
    {
        return _spriteContainer.GetSprite(mood);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        HappinessQ.Add("Справжнє щастя полягає у радості від невеликих речей.");
        HappinessQ.Add("Щасливі миті — це краплі роси на квітці життя.");
        HappinessQ.Add("Життя – це прекрасний шлях до безмежного щастя.");

        SadnessQ.Add("Сльози — це мова, якою серце розповідає про біль, що словами не виражається.");
        SadnessQ.Add("Сум - це перерва між двома радощами, яка допомагає цінувати їх навіть більше.");
        SadnessQ.Add("Сум може бути плачем душі, який допомагає вилити біль та очистити серце.");

        AngerQ.Add("Гнів – це вогонь, який може зігрівати, але і знищити, якщо не контролювати його.");
        AngerQ.Add("Гнів – це сила, яку можна перетворити на позитивні зміни, а не просто виливати на інших.");
        AngerQ.Add("Краще стримана гідність, ніж вибухи гніву.");

        FearQ.Add("Страх – це тінь невідомого, яка розходиться перед сонячним промінням рішучості.");
        FearQ.Add("Страх – це ланцюг, який можна розірвати, поклавши на нього віру у свої сили.");
        FearQ.Add("Страх – це лише перешкода на шляху до досягнення мрій.");

        JoynessQ.Add("Радість – це музика душі, яка виграє у відповідь на події, що дарують щастя.");
        JoynessQ.Add("Справжня радість полягає не в тому, що ми маємо, а в тому, ким ми стаємо.");
        JoynessQ.Add("Радість розмножується, коли її ділитися з іншими.");

        DisgustionQ.Add("Відраза – це вітер, що дме в обличчя, але можна вибратися з нього, перетворивши його на вітерець.");
        DisgustionQ.Add("Відраза – це вказівник на те, що важливо працювати над внутрішнім спокоєм.");
        DisgustionQ.Add("Відраза – це навчання приймати світ таким, яким він є, і не давати йому впливати на внутрішню гармонію.");

        SurprisdedQ.Add("Справжнє здивування відкриває нам невидимі реалії життя.");
        SurprisdedQ.Add("Здивування – це ключ до воріт до найцікавіших відкриттів.");
        SurprisdedQ.Add("Здивування робить світ яскравішим і більш чарівним.");

        DespairQ.Add("Відчай – це плинність, яка відступить, дозволяючи надії вийти на передній план.");
        DespairQ.Add("Відчай – це перехідний стан між тим, що було, і тим, що може бути.");
        DespairQ.Add("Відчай дає можливість знайти нові шляхи, коли звичайні стають недосяжними.");

        DelightnessQ.Add("Захват – це момент, коли серце відчуває, що воно живе повними ритмами.");
        DelightnessQ.Add("Захват – це свідчення того, що світ не припиняє дивувати нас своєю красою.");
        DelightnessQ.Add("Захват – це найкращий наповнювач життя емоціями.");

        CalmQ.Add("Спокій – це внутрішнє озеро, на поверхні якого розцвітають лілії гармонії.");
        CalmQ.Add("Спокій – це заслужена винагорода за мудрість і терпіння.");
        CalmQ.Add("Спокій – це центр, який допомагає в нас виживати в кипучому потоці подій.");
        
        _moodQuotes.Add(Moods.Щастя, HappinessQ);
        _moodQuotes.Add(Moods.Сум, SadnessQ);
        _moodQuotes.Add(Moods.Гнів, AngerQ);
        _moodQuotes.Add(Moods.Страх, FearQ);
        _moodQuotes.Add(Moods.Радість, JoynessQ);
        _moodQuotes.Add(Moods.Відраза, DisgustionQ);
        _moodQuotes.Add(Moods.Здивування, SurprisdedQ);
        _moodQuotes.Add(Moods.Відчай, DespairQ);
        _moodQuotes.Add(Moods.Захват, DelightnessQ);
        _moodQuotes.Add(Moods.Спокій, CalmQ);

        moodsbyid.Add(1, Moods.Щастя);
        moodsbyid.Add(2, Moods.Сум);
        moodsbyid.Add(3, Moods.Гнів);
        moodsbyid.Add(4, Moods.Страх);
        moodsbyid.Add(5, Moods.Радість);
        moodsbyid.Add(6, Moods.Відраза);
        moodsbyid.Add(7, Moods.Здивування);
        moodsbyid.Add(8, Moods.Відчай);
        moodsbyid.Add(9, Moods.Захват);
        moodsbyid.Add(10, Moods.Спокій);
    }
}

[Serializable]
public class MoodsData : GameData
{
    public Moods Mood;
    public string Quote;
    public Sprite currSprite;

    public override void Reset()
    {
       
    }
}
