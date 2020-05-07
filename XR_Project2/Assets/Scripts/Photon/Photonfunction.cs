using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Photonfunction : MonoBehaviourPunCallbacks, IPunObservable
{
    public string getMessage;
    public Text PunText;
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        PunText.text = getMessage;
    }
    [PunRPC]
    public void Send(string OldMassage)
    {
        getMessage = OldMassage;
    }
    public void SendMassage(string Massage)
    {
        photonView.RPC("Send", RpcTarget.Others, Massage);
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            Debug.Log("false");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){ }
}
