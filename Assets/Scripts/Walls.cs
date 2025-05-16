using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    bool game_time = true;

    float cd = 2;
    float current = 0;

    const int n = 15;
    GameObject[] _walls = new GameObject[n];
    SpriteRenderer[] _sprites = new SpriteRenderer[n];
    bool[] _using = new bool[n];

    [SerializeField]
    GameObject wall;
    private void OnEnable() {
        Flappy.game_over += GameOver;
    }

    private void OnDisable() {
        Flappy.game_over -= GameOver;
    }

    private void Start() {
        for (int i = 0; i < n; i++) {
            _walls[i] = Instantiate(wall);
            _using[i] = false;
            _sprites[i] = _walls[i].GetComponent<SpriteRenderer>();
            _walls[i].SetActive(false);
        }
    }

    void Update()
    {
        if (current <= 0 && game_time) {
            RandomWall();
        }
        else {
            current -= Time.deltaTime;
        }

        for (int i = 0; i < n; i++) {
            if (_using[i] && _walls[i].transform.position.x < -18.13) {
                _using[i] = false;
                _walls[i].SetActive(false);
            }
        }
    }

    void RandomWall() {
        GameObject high = wall;
        GameObject low = wall;
        GameObject point = wall;
        int count = 0;
        for (int i = 0; i < n; i++) {
            if (_using[i] == false) {
                _using[i] = true;
                count++;

                if(count < 2) {
                    high = _walls[i];
                    _sprites[i].color = Color.white;
                }
                else if (count < 3) {
                    point = _walls[i];
                    _sprites[i].color = Color.clear;
                }
                else {
                    low = _walls[i];
                    _sprites[i].color = Color.white;
                    break;
                }
            }
        }

        if (count == 3) {
            float space = Random.Range(3f, 5.5f);
            float position = Random.Range(0.25f + space/2, 5.25f + space/2);

            point.transform.localScale = new Vector3(-.5f, space, 1);

            high.tag = "Wall";
            point.tag = "Point";
            low.tag = "Wall";

            high.transform.position = new Vector3(-.5f, position + space/2 + 5f, 0);
            point.transform.position = new Vector3(-.5f, position, 0);
            low.transform.position = new Vector3(-.5f, position - space / 2 - 5f, 0);

            high.SetActive(true);
            point.SetActive(true);
            low.SetActive(true);
            current = cd;
        }

        return;
    }

    void GameOver() {
        game_time = false;
    }
}
