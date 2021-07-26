using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    /* LOBBY */

    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI startButtonText;
    [SerializeField] private int roomSize;


    private void Start()
    {
        startButtonText.text = "LOADING...";
        startButton.interactable = false;

        PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");

        PhotonNetwork.AutomaticallySyncScene = true;

        startButtonText.text = "PLAY ONLINE";
        startButton.interactable = true;
        startButton.onClick.AddListener(DelayStart);
    }

    public void DelayStart()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 1000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room, trying again...");
        CreateRoom();
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startButtonText.text = "PLAY ONLINE";
    }

    /* ROOM */

    [SerializeField] private int waitingRoomSceneIndex;

    public override void OnEnable()
    {
        // register to photon callback functions
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        // unregister to photon callback functions
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
