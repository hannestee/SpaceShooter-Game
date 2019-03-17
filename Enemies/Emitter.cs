using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emitter : MonoBehaviour {

    // Store Wave Prefab
    public GameObject[] waves;
    public int waveWait;
    public int startWait;

    // Current Wave
    private int currentWave;
    
    void Start()
    {
        StartCoroutine(WaveSpawn());
    }

    IEnumerator WaveSpawn ()
    {
        yield return new WaitForSeconds(startWait);
        // Terminate the coroutine if Wave does not exist
        if (waves.Length == 0)
        {
            yield break;
        }

        while (true)
        {

            // Create Wave
            GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);

            // Make Wave a child element of Emitter
            wave.transform.parent = transform;

            // Wait until all enemies of child elements of Wave are deleted
            while (wave.transform.childCount != 0 ) {
                yield return new WaitForSeconds(waveWait);
            }

            // Delete Wave
            Destroy(wave);

            // When we execute all the stored wave, we set currentWave to 0 (from the beginning -> loop)
            if (waves.Length <= ++currentWave)
            {
                currentWave = 0;
            }

        }
    }
}