using Photon.Pun;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileEmitter;
    [SerializeField] private float projectileForce = 10.0f;
    [SerializeField] private float weaponFireInterval_Secs = 0.1f;

    private object[] projectileInitData = new object[1];

    private float coolDown_Secs = 0.0f;

    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();
    }

    private void Update()
    {
        if (this.GetComponent<PhotonView>().IsMine)
        {
            coolDown_Secs -= Time.deltaTime;
            if (coolDown_Secs < 0.0f) { coolDown_Secs = 0.0f; }

            if (Input.GetMouseButton(0) && Mathf.Approximately(coolDown_Secs, 0.0f))
            {
                projectileInitData[0] = this.projectileEmitter.forward * projectileForce;
                GameObject projectileInstance = PhotonNetwork.Instantiate(this.projectile.name, this.projectileEmitter.position, Quaternion.identity, 0, projectileInitData);
                coolDown_Secs = weaponFireInterval_Secs;
            }
        }
    }
}