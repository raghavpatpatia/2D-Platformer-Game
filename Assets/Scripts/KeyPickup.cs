using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().PickupKey();
            // Destroy(gameObject);
            StartCoroutine(KeyFade());
        }
    }

    IEnumerator KeyFade()
    {
        Vector2 initialPosition = transform.position;
        float initialAlpha = spriteRenderer.color.a;

        Vector2 targetPosition = initialPosition + Vector2.up * moveDistance;
        Color targetColor = spriteRenderer.color;
        targetColor.a = 0f;

        float t = 0f;
        while (t < 1f)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, t);

            spriteRenderer.color = Color.Lerp(new Color(1f, 1f, 1f, initialAlpha), targetColor, t);

            t += Time.deltaTime / moveDuration;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
