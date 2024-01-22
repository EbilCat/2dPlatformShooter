using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private void Update()
    {
        if(PlayerData.LocalPlayer != null)
        {
            Vector3 lookAtPos = PlayerData.LocalPlayer.transform.position;
            lookAtPos.z = this.transform.position.z;

            this.transform.position = Vector3.Lerp(this.transform.position, lookAtPos, Time.deltaTime * 2);
        }
    }
}
