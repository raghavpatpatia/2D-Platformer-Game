using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextLevelPanel;
    public ScoreManager scoreManager;
    [SerializeField]int levelScore;
    private Animator animator;

    private void Awake()
    {
        nextLevelPanel.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (scoreManager.score >= levelScore)
            {
                animator.SetBool("isScoreMax", true);
                nextLevelPanel.SetActive(true);
                text.text = "Congrats!! You have cleared the level " + SceneManager.GetActiveScene().buildIndex.ToString();
            }
        }
    }
}
