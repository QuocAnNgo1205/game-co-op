using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnOrcs : MonoBehaviour
{
    public GameObject orcPrefab; // Prefab của Orc
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int numberOfOrcs = 1; // Số lượng Orc muốn spawn

    private void Start()
    {
        for (int i = 0; i < numberOfOrcs; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(orcPrefab.name, randomPosition, Quaternion.identity);
            }
        }
    }
}
