using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{

    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] public int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private UnityEvent OnJump, OnDead;
    [SerializeField] private UnityEvent OnAddPoint;

    private Rigidbody2D rb2d;
    private Animator animator;
    private AudioSource birdAudio;

    private void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        birdAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.enabled = false;
    }

    private void Update() {
        if(!isDead && Input.GetMouseButtonDown(0)) {
            Jump();
        }
    }

    public bool IsDead() {
        return isDead;
    }

    public void Dead() {
        if (!isDead && OnDead != null) {
            OnDead.Invoke();
        }
        isDead = true;
    }

    void Jump() {
        if (rb2d) {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, upForce));
        }

        if(OnJump != null) {
            
            OnJump.Invoke();
        }
    }

    public void AddScore(int value) {
        score += value;
        scoreText.text = score.ToString();
        if(OnAddPoint != null) {
            OnAddPoint.Invoke();
        }
    }



}
