using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab của người chơi
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        PhotonNetwork.Instantiate(
            playerPrefab.name, // Tên prefab phải trùng với tên trong Photon
            randomPosition,
            Quaternion.identity
        );
    }    
}
