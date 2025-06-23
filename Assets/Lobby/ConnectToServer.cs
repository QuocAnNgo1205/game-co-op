using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // Kết nối đến Photon server
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Photon server...");
    }

    // Callback khi kết nối thành công
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon server successfully!");
        // Có thể thêm logic để tham gia phòng hoặc tạo phòng ở đây
        PhotonNetwork.JoinLobby(); // Tham gia vào lobby mặc định        
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby successfully!");
        SceneManager.LoadScene("Host"); // Tải cảnh Lobby
    }

    // Callback khi kết nối thất bại
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon server: " + cause);
    }

}
