using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    float speed = 2f;
    bool game_time = true;
    private void OnEnable() {
        Flappy.game_over += GameOver;
    }
    private void OnDisable() {
        Flappy.game_over -= GameOver;
    }

    void Update()
    {
        if (game_time) {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
    }

    void GameOver() {
        game_time = false;
    }
}
