using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    private int sourceViewId = -1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.photonView.IsMine)
        {
            PhotonView otherPhotonView = collision.gameObject.GetComponent<PhotonView>();
            if (CollisionWithProjectileOwner(otherPhotonView)) { return; }

            DamageHandler damageHandler = collision.gameObject.GetComponent<DamageHandler>();
            if (damageHandler != null)
            {
                damageHandler.TakeDamage(sourceViewId, 1);
            }

            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        this.sourceViewId = (int)instantiationData[0];
        Vector3 velocity = (Vector3)instantiationData[1];
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private bool CollisionWithProjectileOwner(PhotonView otherPhotonView)
    {
        return otherPhotonView != null && otherPhotonView.Owner == this.photonView.Owner;
    }
}