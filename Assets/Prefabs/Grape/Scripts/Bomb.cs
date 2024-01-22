using Photon.Pun;
using UnityEngine;

public class Bomb : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    private int sourceViewId = -1;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        this.sourceViewId = (int)instantiationData[0];
        Vector3 velocity = (Vector3)instantiationData[1];
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int groundMask = LayerMask.NameToLayer("Ground");

        if (collision.gameObject.layer == groundMask) { return; }

        if (this.photonView.IsMine)
        {
            PhotonView otherPhotonView = collision.gameObject.GetComponent<PhotonView>();
            if (CollisionWithProjectileOwner(otherPhotonView)) { return; }

            DamageHandler damageHandler = collision.gameObject.GetComponent<DamageHandler>();
            if (damageHandler != null)
            {
                damageHandler.TakeDamage(sourceViewId, 5);
            }

            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    private bool CollisionWithProjectileOwner(PhotonView otherPhotonView)
    {
        return otherPhotonView != null && otherPhotonView.Owner == this.photonView.Owner;
    }
}