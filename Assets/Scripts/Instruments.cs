using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;
using MidiPlayerTK;
using System.Text;
using UnityEngine.UI;

public class Instruments : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer;
    public MidiLoad midiloaded;
    public List<MPTKEvent> midiEvents;
    public InputManager input;

    public Sprite bassSprite, hihatSprite, snareSprite, crashSprite, bassSprite2, hihatSprite2, snareSprite2, crashSprite2;

    public Dictionary<string, Sprite> instrumentAnimations;

    public Transform indicatorPrefab;

    // Start is called before the first frame update
    void Start()
    {

        instrumentAnimations = new Dictionary<string, Sprite>()
        {
            {"Bass 1", bassSprite},
            {"Hihat 1", hihatSprite},
            {"Snare 1", snareSprite},
            {"Crash 1", crashSprite},
            {"Bass 2", bassSprite2},
            {"Hihat 2", hihatSprite2},
            {"Snare 2", snareSprite2},
            {"Crash 2", crashSprite2}
        };

        // Select a MIDI from the MIDI DB (with exact name)
        midiFilePlayer.MPTK_MidiName = "Instant_Crush";
        midiFilePlayer.OnEventNotesMidi.AddListener(NewNote);
        midiloaded = midiFilePlayer.MPTK_Load();
        midiEvents = midiloaded.MPTK_ReadMidiEvents();

        StartCoroutine(CountdownGame(3));
    }


    IEnumerator CountdownGame(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            GameObject.FindGameObjectWithTag("CountdownTimer").GetComponent<Text>().text = count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        midiFilePlayer.MPTK_Play();
        GameObject.FindGameObjectWithTag("CountdownTimer").SetActive(false);
        StartCoroutine(Wait(1.7f));
        
        
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // count down is finished...
        gameObject.GetComponent<AudioSource>().Play();
    }

    public IEnumerator AnimationCountdown(float seconds, string name)
    {
        GameObject inst = GameObject.FindGameObjectWithTag(name);
        SpriteRenderer sr = inst.GetComponent<SpriteRenderer>();
        sr.sprite = instrumentAnimations[name + " 2"];
        yield return new WaitForSeconds(seconds);

        // count down is finished...
        sr.sprite = instrumentAnimations[name + " 1"];
    }

    public void NewNote(List<MPTKEvent> mptkEvents)
    {
        foreach (MPTKEvent mptkEvent in mptkEvents)
        {
            // Log if event is a note on
            if (mptkEvent.Command == MPTKCommand.NoteOn)
            {
                var obj = Instantiate(indicatorPrefab, new Vector3(0, 4.99f, 0), new Quaternion(0, 0, 0, 0));
                //Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");
                if (input.drumMidiKeys["Bass"].Contains(mptkEvent.Value))
                {  
                    obj.transform.parent = GameObject.FindGameObjectWithTag("IndicatorParent").transform;
                    obj.GetComponent<IndicatorBox>().setInitialValues("Bass");
                } else if (input.drumMidiKeys["Hihat"].Contains(mptkEvent.Value))
                {
                    obj.transform.parent = GameObject.FindGameObjectWithTag("IndicatorParent").transform;
                    obj.GetComponent<IndicatorBox>().setInitialValues("Hihat");
                } else if (input.drumMidiKeys["Snare"].Contains(mptkEvent.Value))
                {
                    obj.transform.parent = GameObject.FindGameObjectWithTag("IndicatorParent").transform;
                    obj.GetComponent<IndicatorBox>().setInitialValues("Snare");
                } else if (input.drumMidiKeys["Crash"].Contains(mptkEvent.Value))
                {
                    obj.transform.parent = GameObject.FindGameObjectWithTag("IndicatorParent").transform;
                    obj.GetComponent<IndicatorBox>().setInitialValues("Crash");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
