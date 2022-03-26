using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private TextMeshProUGUI coinTextScore;
    private AudioSource audioManager;
    private int scoreCount = 0;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("Coin Text").GetComponent<TextMeshProUGUI>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.COIN_TAG) {
            target.gameObject.SetActive(false);
            IncreaseScore();
        }
    }

    public void IncreaseScore()
    {
        scoreCount++;
        coinTextScore.text = "x " + scoreCount;
        audioManager.Play();
    }
}
