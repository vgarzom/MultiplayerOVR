using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShadowPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private Vector3 initialPosition;

    private Vector3 currentPosition;

    [SerializeField]
    private string id;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Vector3.zero;
        currentPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPosition(Vector3 deltaPosition) {
        currentPosition.x += deltaPosition.x;
        currentPosition.y += deltaPosition.y;
        currentPosition.z += deltaPosition.z;


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.id);
        }

        else
        {
            this.id = (string)stream.ReceiveNext();
        }
    }

    public void SetId(string _id) {
        this.id = _id;
    }
}
