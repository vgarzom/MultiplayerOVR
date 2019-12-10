using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedPlayer : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private GameObject avatar;

    [SerializeField]
    private Transform playerGlobal;

    [SerializeField]
    private Transform playerLocal;

    [SerializeField]
    private ShadowPlayer shadow;

    [SerializeField]
    private OVRBoundaryReporter currentBoundary;

    private Vector3 initialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Networked player has been instantiated!");
        currentBoundary = GameObject.Find("OVRBoundaryReporter").GetComponent<OVRBoundaryReporter>();
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
            //this.shadow.AddPosition(deltaPosition);
            this.initialPosition = playerLocal.localPosition;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("trying to send...");
        if (currentBoundary != null)
        {
            float w = Mathf.Abs(currentBoundary.boundaryMaxLimit.x - currentBoundary.boundaryMinLimit.x);
            float h = Mathf.Abs(currentBoundary.boundaryMaxLimit.z - currentBoundary.boundaryMinLimit.z);

            if (stream.IsWriting && photonView.IsMine)
            {
                //stream.SendNext(playerLocal.localPosition);
                float x = (2 * playerLocal.position.x / w) + (2 * currentBoundary.boundaryMinLimit.x / w) + 1;
                float z = (2 * playerLocal.position.z / h) + (2 * currentBoundary.boundaryMinLimit.z / h) + 1;
                Debug.Log("sending --> (x,z) (" + x + ", " + z + ")");
                stream.SendNext(new Vector3(x, playerLocal.position.y, z));
                stream.SendNext(playerLocal.localRotation);
            }

            else if (stream.IsReading)
            {
                Vector3 pos0 = (Vector3)stream.ReceiveNext();
                this.transform.rotation = (Quaternion)stream.ReceiveNext();

                Debug.Log("receiving --> (x,y,z) (" + pos0.x + ", " + pos0.y + ", " + pos0.z + ")");

                float x = (w / 2) * (pos0.x - 1) - currentBoundary.boundaryMinLimit.x;
                float z = (h / 2) * (pos0.z - 1) - currentBoundary.boundaryMinLimit.z;

                if (!photonView.IsMine)
                {
                    this.transform.localPosition = new Vector3(x, pos0.y, z);
                }

            }
        }
    }

    public void SetShadow(ShadowPlayer _shadow)
    {
        this.shadow = _shadow;
    }
}
