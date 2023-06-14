using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelDisplay: MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextLevelPanel;
    public ScoreManager scoreManager;
    [SerializeField]int levelScore;
    private Animator animator;
    private int nextSceneIndex;

    private void Awake()
    {
        nextLevelPanel.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void SaveScene()
    {
        nextSceneIndex = (SceneManager.GetActiveScene().buildIndex) + 1;
        PlayerPrefs.SetInt("LoadLevel", nextSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (scoreManager.score >= levelScore)
            {
                SaveScene();
                animator.SetBool("isScoreMax", true);
                nextLevelPanel.SetActive(true);
                text.text = "Congrats!! You have cleared the level " + SceneManager.GetActiveScene().buildIndex.ToString();
                LevelManager.Instance.MarkCurrentLevelComplete();
            }
        }
    }
}
