using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private bool _isPinned = false;
    private bool _isLaunched = false;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!_isPinned && _isLaunched)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isPinned = true;
        if (other.gameObject.CompareTag("Target"))
        {
            GameObject childObject = transform.GetChild(0).gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;
            transform.SetParent(other.gameObject.transform);
            GameManager.Instance.DecreaseGoal();
            GameManager.Instance.AddComBo();
            GameManager.Instance.audioSource.PlayOneShot(GameManager.Instance.audioClip);
        }
        else if (other.gameObject.CompareTag("Pin"))
        {
            GameManager.Instance.SetGameOver(false);
        }
    }

    public void Launch()
    {
        _isLaunched = true;
    }
}
