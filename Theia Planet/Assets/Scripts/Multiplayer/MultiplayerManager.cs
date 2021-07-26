using UnityEngine;
using Photon.Pun;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab01;
    [SerializeField] private Transform playerPosition1;
    [SerializeField] private Transform playerPosition2;

    // enemy spawn
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyPosition;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnObject(playerPrefab01, playerPosition1);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            SpawnObject(playerPrefab01, playerPosition2);
        }

        SpawnObject(enemyPrefab, enemyPosition);
    }

    void SpawnObject(GameObject objectPrefab, Transform spawnPosition)
    {
        PhotonNetwork.Instantiate(objectPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }
}
