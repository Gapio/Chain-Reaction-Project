using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public float recoilAngle = 10f; // the angle by which to rotate the gun
    public float recoilTime = 0.2f; // how long the recoil animation should take
    public AudioClip gunShot; // the audio clip to play on collision
    public float volume = 1f; // the volume of the audio clip

    private bool isRecoiling = false; // flag to track whether the gun is currently recoiling
    private Quaternion originalRotation; // the original rotation of the gun
    private Quaternion recoilRotation; // the target rotation of the gun during recoil
    private float recoilTimer = 0f; // the current time during the recoil animation

    private void Start()
    {
        originalRotation = transform.localRotation;
        recoilRotation = Quaternion.Euler(-recoilAngle, 0f, 0f);
    }

    private void Update()
    {
        if (isRecoiling)
        {
            // update the recoil animation
            recoilTimer += Time.deltaTime;
            float t = recoilTimer / recoilTime;
            transform.localRotation = Quaternion.Slerp(originalRotation, originalRotation * recoilRotation, t);

            if (t >= 1f)
            {
                // end the recoil animation
                isRecoiling = false;
                recoilTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fuse") && !isRecoiling)
        {
            // start the recoil animation
            isRecoiling = true;
            AudioSource.PlayClipAtPoint(gunShot, transform.position, volume);
        }
    }
}