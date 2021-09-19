using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MidiPlayerTK;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Instruments instruments;

    public int TOTAL_SCORE = 0;

    public Text scoreText;

    public SpriteChanger sc;

    int scoringMargin = 100; // Max margin of error (ms) for the user to score points
    public Dictionary<string, List<int>> drumMidiKeys; // How long it will take the circular indicator to converge with the drum
    public float indicatorOffset;

    // Start is called before the first frame update
    void Start()
    {
        drumMidiKeys = new Dictionary<string, List<int>>()
        {
            {"Bass", new List<int>(){35}},
            {"Hihat", new List<int>(){42, 44, 46}},
            {"Crash", new List<int>(){49, 55}},
            {"Snare", new List<int>(){37, 38}}
        };
    }

    // Update is called once per frame
    void Update()
    {
        int totalScoreForFrame = 0;
        float sentiment = 0;

        // Change these inputs to the drum hits later
        if (Input.GetKeyDown("space")) // Bass Drum
        {
            StartCoroutine(instruments.AnimationCountdown(0.3f, "Bass"));

            bool hit = false;
            foreach (MPTKEvent _event in instruments.midiEvents)
            {
                float scoreDiff = Mathf.Abs(_event.RealTime+200 - ((float)instruments.midiFilePlayer.MPTK_RealTime + indicatorOffset));
                if (scoreDiff <= scoringMargin && (_event.Value == 35))
                {
                    hit = true;
                    int localScore = (int)Mathf.Ceil((scoringMargin - scoreDiff) / 50);
                    foreach (Transform child in GameObject.FindGameObjectWithTag("IndicatorParent").transform)
                    {
                        if (child.GetComponent<IndicatorBox>().drumType == "Bass" && Mathf.Abs(child.position.y - 3.061279f) <= 0.5f)
                        {
                            totalScoreForFrame += localScore;
                            sentiment += (int)(scoringMargin - scoreDiff);
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                    break;
                }
            }

            if (!hit)
            {
                totalScoreForFrame -= 2;
                sentiment -= 40;
            }
        }
        
        if (Input.GetKeyDown("z")) // Hi-hat
        {
            StartCoroutine(instruments.AnimationCountdown(0.1f, "Hihat"));
            bool hit = false;
            foreach(MPTKEvent _event in instruments.midiEvents)
            {
                float scoreDiff = Mathf.Abs(_event.RealTime - ((float)instruments.midiFilePlayer.MPTK_RealTime + indicatorOffset));
                if (scoreDiff <= scoringMargin && (_event.Value == 42 || _event.Value == 44 || _event.Value == 46))
                {
                    hit = true;
                    int localScore = (int)Mathf.Ceil((scoringMargin - scoreDiff) / 50);
                    foreach (Transform child in GameObject.FindGameObjectWithTag("IndicatorParent").transform)
                    {
                        if (child.GetComponent<IndicatorBox>().drumType == "Hihat" && Mathf.Abs(child.position.y - 3.061279f) <= 0.7f)
                        {
                            totalScoreForFrame += localScore;
                            sentiment += (int)(scoringMargin - scoreDiff);
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                    break;
                }
            }

            if (!hit)
            {
                totalScoreForFrame -= 2;
                sentiment -= 40;
            }
        }
        
        if (Input.GetKeyDown("x")) // Snare Drum
        {
            StartCoroutine(instruments.AnimationCountdown(0.3f, "Snare"));

            bool hit = false;
            foreach (MPTKEvent _event in instruments.midiEvents)
            {
                float scoreDiff = Mathf.Abs(_event.RealTime+200 - ((float)instruments.midiFilePlayer.MPTK_RealTime + indicatorOffset));
                if (scoreDiff <= scoringMargin && (_event.Value == 37 || _event.Value == 38))
                {
                    hit = true;
                    int localScore = (int)Mathf.Ceil((scoringMargin - scoreDiff) / 50);
                    foreach (Transform child in GameObject.FindGameObjectWithTag("IndicatorParent").transform)
                    {
                        if (child.GetComponent<IndicatorBox>().drumType == "Snare" && Mathf.Abs(child.position.y - 3.061279f) <= 0.5f)
                        {
                            totalScoreForFrame += localScore;
                            sentiment += (int)(scoringMargin - scoreDiff);
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                    break;
                }
            }

            if (!hit)
            {
                totalScoreForFrame -= 2;
                sentiment -= 40;
            }
        }
        
        if (Input.GetKeyDown(",")) // Crash Cymbal
        {
            StartCoroutine(instruments.AnimationCountdown(0.3f, "Crash"));

            bool hit = false;
            foreach (MPTKEvent _event in instruments.midiEvents)
            {
                float scoreDiff = Mathf.Abs(_event.RealTime - ((float)instruments.midiFilePlayer.MPTK_RealTime + indicatorOffset));
                if (scoreDiff <= scoringMargin && (_event.Value == 49 || _event.Value == 55))
                {
                    hit = true;
                    int localScore = (int)Mathf.Ceil((scoringMargin - scoreDiff) / 50);
                    foreach (Transform child in GameObject.FindGameObjectWithTag("IndicatorParent").transform)
                    {
                        if (child.GetComponent<IndicatorBox>().drumType == "Crash" && Mathf.Abs(child.position.y - 3.061279f) <= 0.5f)
                        {
                            totalScoreForFrame += localScore;
                            sentiment += (int)(scoringMargin - scoreDiff);
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                    break;
                }
            }

            if (!hit)
            {
                totalScoreForFrame -= 2;
                sentiment -= 60;
            }
        }

        TOTAL_SCORE += totalScoreForFrame;
        scoreText.text = TOTAL_SCORE.ToString();

        // Show helpful text
        StartCoroutine(sc.changeText(sentiment));
    }
}
