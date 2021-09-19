using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite perfect;
    public Sprite good;
    public Sprite ok;
    public Sprite miss;

    bool showingText = false;

    SpriteRenderer spriteSetter;

    // Start is called before the first frame update
    void Start()
    {
        spriteSetter = GetComponent<SpriteRenderer>();
        spriteSetter.sprite = null;
    }

    public IEnumerator changeText(float score)
    {
        if (showingText) yield break;

        

        showingText = true;
        if (score == 0)
        {
            showingText = false;
            spriteSetter.sprite = null;
            yield break;
        } else if (score >= 50)
        {
            spriteSetter.sprite = perfect;
        } else if (score >= 30)
        {
            spriteSetter.sprite = good;
        } else if (score >= 1)
        {
            spriteSetter.sprite = ok;
        } else if (score <= -1)
        {
            spriteSetter.sprite = miss;
        }
        yield return new WaitForSeconds(1);
        showingText = false;

        yield break;
    }
}
