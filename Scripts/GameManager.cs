using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    public  TileBoard boad;
    public CanvasGroup gameoverUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hisscoreText;
    private int score;

    private void Start()
    {
       NewGame();
    }
    public void NewGame()
    {
        SetScore(0);
        hisscoreText.text = LoadHiscore().ToString(); 

        gameoverUI.alpha = 0f;
        gameoverUI.interactable = false;



        boad.ClearBoard();
        boad.CreateTile();
        boad.CreateTile();
        
        boad.enabled = true;
    }
    public void GameOver()
    {
        boad.enabled = false;
        StartCoroutine(Fade(gameoverUI, 1f, 1f));
        gameoverUI.interactable = true;

    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += duration;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
    public void IncreaseScore(int point)
    {
        SetScore(score + point);
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHisscore();

    }
    private void SaveHisscore()
    {
        int hiscore = LoadHiscore();
        if(score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);

        }
    }
    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

 /*   public void ResetScore()
    {
       retur  PlayerPrefs.DeleteKey("hiscore");
        PlayerPrefs.Save();
        Debug.Log("Điểm số đã được đặt lại về 0.");
    }*/

}

