using Photon.Pun;
using UnityEngine;

public class FireSecondaryWeapon : MonoBehaviour
{
    private PlayerData playerData;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileEmitter;
    [SerializeField] private float projectileForce = 10.0f;
    [SerializeField] private float weaponFireInterval_Secs = 0.1f;

    private object[] projectileInitData = new object[2];

    private float coolDown_Secs = 0.0f;

    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();
    }

    private void Update()
    {
        if (this.playerData.IsLocalPlayer && this.playerData.IsDead == false)
        {
            coolDown_Secs -= Time.deltaTime;
            if (coolDown_Secs < 0.0f) { coolDown_Secs = 0.0f; }

            if (Input.GetMouseButton(1) && Mathf.Approximately(coolDown_Secs, 0.0f))
            {
                projectileInitData[0] = playerData.photonView.ViewID;
                projectileInitData[1] = (this.projectileEmitter.forward + this.projectileEmitter.up) * projectileForce;
                PhotonNetwork.Instantiate(this.projectile.name, this.projectileEmitter.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), 0, projectileInitData);
                coolDown_Secs = weaponFireInterval_Secs;
            }
        }
    }
}