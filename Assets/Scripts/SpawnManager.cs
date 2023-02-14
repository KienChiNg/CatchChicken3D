using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalList;
    [SerializeField] private int amoutSpawn;
    [SerializeField] float spawnRange = 20;
    [SerializeField] float timeSpawnAgain = 6;
    [SerializeField] MoveRandomly[] all;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amoutSpawn; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SpawnRandomAnimal(j,i);
            }
        }
    }

    void SpawnRandomAnimal(int index, int i)
    {
        // int animalIndex = Random.Range(0,animalList.Length);
        Instantiate(animalList[index], GenerateSpawnPosition(), animalList[index].transform.rotation);
        // track.name = $"chicken{i.ToString()}{index.ToString()}";
    }
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.78f, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        all = GameObject.FindObjectsOfType<MoveRandomly> (true);
        for (int i = 0; i < all.Length; i++)
        {
            if(!all[i].gameObject.activeSelf && !all[i].isDead){
                all[i].isDead = true;
                StartCoroutine(ReturnAnimal(all[i]));
                // Debug.Log("aloo");
            }
        }
    }
    IEnumerator ReturnAnimal(MoveRandomly animal){
        yield return new WaitForSeconds(timeSpawnAgain);
        animal.Return();
        animal.gameObject.transform.position = GenerateSpawnPosition();
    }
}
