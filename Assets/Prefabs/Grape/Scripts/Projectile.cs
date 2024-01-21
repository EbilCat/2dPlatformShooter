using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.photonView.IsMine)
        {
            PhotonView otherPhotonView = collision.gameObject.GetComponent<PhotonView>();
            if (CollisionWithProjectileOwner(otherPhotonView)) { return; }

            Debug.Log(collision.gameObject.name);

            PlayerData playerHealth = collision.gameObject.GetComponent<PlayerData>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        Vector3 velocity = (Vector3)instantiationData[0];
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private bool CollisionWithProjectileOwner(PhotonView otherPhotonView)
    {
        return otherPhotonView != null && otherPhotonView.Owner == this.photonView.Owner;
    }
}