using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] public float holeSize = 1f;
    [SerializeField] private float MaxMinOffset = 1;

    private Coroutine CR_Spawn;

    private void Start() {
        StartSpawn();
    }

    void StartSpawn() {
        if(CR_Spawn == null) {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }

    void StopSpawn() {
        if (CR_Spawn != null) {
            StopCoroutine(CR_Spawn);
        }
    }

    void SpawnPipe() {
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));
        newPipeUp.gameObject.SetActive(true);

        Pipe newpipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);
        newpipeDown.gameObject.SetActive(true);

        newPipeUp.transform.position += Vector3.up * (holeSize / 2);
        newpipeDown.transform.position += Vector3.down * (holeSize / 2);

        float y = MaxMinOffset * Mathf.Cos(Time.time);
        newPipeUp.transform.position += Vector3.up * y;
        newpipeDown.transform.position += Vector3.up * y;
    }

    IEnumerator IeSpawn() {
        while (true) {
            if(bird.IsDead()) {
                StopSpawn();
            }

            SpawnPipe();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
