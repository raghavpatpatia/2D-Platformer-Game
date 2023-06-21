using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelDisplay: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject nextLevelPanel;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int levelScore;
    private Animator animator;
    private ParticleSystem levelComplete;

    private void Awake()
    {
        nextLevelPanel.SetActive(false);
        animator = GetComponent<Animator>();
        levelComplete = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (scoreManager.score >= levelScore)
            {
                SoundManager.Instance.Play(Sounds.LevelDoor);
                levelComplete.Play();
                animator.SetBool("isScoreMax", true);
                nextLevelPanel.SetActive(true);
                text.text = "Congrats!! You have cleared the level " + SceneManager.GetActiveScene().buildIndex.ToString();
                LevelManager.Instance.MarkCurrentLevelComplete();
            }
        }
    }
}
