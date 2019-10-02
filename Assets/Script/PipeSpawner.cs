using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] public float spawnInterval = 1;
    [SerializeField] public float holeSize = 1f;
    [SerializeField] private float MaxMinOffset = 1;
    [SerializeField] private Point point;
    [SerializeField] public float speed = 0.5f;

    private Coroutine CR_Spawn;

    private void Start() {
        StartSpawn();
    }

    private void Update() {
        if (!bird.IsDead()) {
            holeSize = Random.Range(0.7f, 2);
        }
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

        //for pipe
        float y = MaxMinOffset * Mathf.Sin(Time.time);
        newPipeUp.transform.position += Vector3.up * y;
        newpipeDown.transform.position += Vector3.up * y;

      
        // for point
        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(holeSize);
        newPoint.transform.position += Vector3.up * y;
        Debug.Log(holeSize);

    
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
