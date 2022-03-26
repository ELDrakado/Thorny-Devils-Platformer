using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private TextMeshProUGUI lifeText;
    private int lifeScoreCount;
    private bool canDamage;

    void Awake()
    {
        lifeText = GameObject.Find("Life Text").GetComponent<TextMeshProUGUI>();
        lifeScoreCount = 3;
        lifeText.text = "x " + lifeScoreCount;

        canDamage = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage() 
    {
        if (canDamage) {
            lifeScoreCount--;

            if (lifeScoreCount >= 0) {
                lifeText.text = "x " + lifeScoreCount;
            }

            if (lifeScoreCount == 0) {
                // RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
    }
}
