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
        if (photonView.IsMine)
        {
            initialPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));
            currentPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
            transform.position = this.currentPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPosition(Vector3 deltaPosition) {
        currentPosition.x += deltaPosition.x;
        currentPosition.y += deltaPosition.y;
        currentPosition.z += deltaPosition.z;

        transform.position = currentPosition;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.id);
            stream.SendNext(this.initialPosition);
            stream.SendNext(this.currentPosition);
        }

        else
        {
            SetId((string)stream.ReceiveNext());

            this.initialPosition = (Vector3)stream.ReceiveNext();

            this.currentPosition = (Vector3)stream.ReceiveNext();
            transform.position = this.currentPosition;
        }
    }

    public void SetId(string _id) {
        this.id = _id;
        gameObject.name = "ShadowPlayer_" + this.id;
    }
}
