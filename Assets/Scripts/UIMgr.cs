using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIMgr : MonoBehaviour
{
    public UIDocument uIDocument;
    public float scoreMultiplier = 10f;

    float elapsedTime = 0f;
    float score = 0f;
    float highScore = 0f;
    Label scoreText;
    Label highScoreText;
    Button restartButton;
    bool isGameOver = false;

    void Start()
    {
        VisualElement ve = uIDocument.rootVisualElement;
        scoreText = ve.Q<Label>("ScoreLabel");
        highScoreText = ve.Q<Label>("HighScoreLabel");
        highScoreText.style.display = DisplayStyle.None;
        restartButton = ve.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

void ReloadScene()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (isGameOver) return;
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = $" Score : {score} ";
    }

    public void GameOver()
    {
        isGameOver = true;
        restartButton.style.display = DisplayStyle.Flex;
        // 하이스코어 보이기
        highScore = PlayerPrefs.GetFloat("HIGHSCORE", 0f);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HIGHSCORE", score);
            StartCoroutine(MakeBlink());
        }
        highScoreText.text = $"High : {highScore}";
        highScoreText.style.display = DisplayStyle.Flex;
    }

    IEnumerator MakeBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            highScoreText.style.display = DisplayStyle.None;
            yield return new WaitForSeconds(0.3f);
            highScoreText.style.display = DisplayStyle.Flex;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
