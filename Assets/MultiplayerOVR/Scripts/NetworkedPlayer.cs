using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private GameObject avatar;

    [SerializeField]
    private Transform playerGlobal;

    [SerializeField]
    private Transform playerLocal;

    [SerializeField]
    private ShadowPlayer shadow;

    private Vector3 initialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Networked player has been instantiated!");

        if (photonView.IsMine)
        {
            this.initialPosition = new Vector3(0.0f, 0.0f, 0.0f);

            playerGlobal = GameObject.Find("OVRPlayerController").transform;
            playerGlobal.position = this.initialPosition;

            playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            transform.SetParent(playerLocal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector3 deltaPosition = playerGlobal.position - this.initialPosition;
            this.shadow.AddPosition(deltaPosition);
            this.initialPosition = playerGlobal.position;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting) {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
            stream.SendNext(this.initialPosition);
        }

        else {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            this.initialPosition = (Vector3)stream.ReceiveNext();
        }
    }

    public void SetShadow(ShadowPlayer _shadow) {
        this.shadow = _shadow;
    }
}
