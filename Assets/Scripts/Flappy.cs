using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flappy : MonoBehaviour
{
    [SerializeField]
    private float gravity;
    private Rigidbody2D body;

    private bool alive;
    private float last_vel;

    public delegate void DirChange();
    public static event DirChange change_sprite;

    public delegate void GameOver();
    public static event GameOver game_over;

    private int points;
    [SerializeField]
    private TMPro.TextMeshProUGUI score;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        alive = true;
        last_vel = -1f;
        points = 0;
    }

    void Update() {
        if (alive && Input.GetMouseButtonDown(0)) {
            body.velocity = -0.5f * body.gravityScale * gravity * Vector2.up;
        }

        if (body.velocity.y * last_vel < 0) {
            change_sprite?.Invoke();
        }

        last_vel = body.velocity.y;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Wall") {
            alive = false;
            game_over?.Invoke();
        }
        else if (collision.tag == "Point") {
            points += 1;
            score.text = points.ToString();
        }
    }
}
