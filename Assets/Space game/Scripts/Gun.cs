using UnityEngine;

public class Gun : MonoBehaviour, IPoolObject
{
    public GameObject bullet;
    bulletPooler currentBullet;
    AudioSource shootingSound; 

    public float shootForce, upwardForce;

    bool shooting, reloading;

    public Camera cam;
    public Transform attackPoint;

    private void Start() 
    { 
        currentBullet = bulletPooler.Instance;
        shootingSound = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update() 
    {
        MyInput();
    }
    private void MyInput() 
    {
       if (shooting = Input.GetKeyDown(KeyCode.Mouse0)) {Shoot(); };
    }

    public void Shoot() 
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit)) { targetPoint = hit.point; }
        else { targetPoint = ray.GetPoint(75); }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        shootingSound.Play();

        currentBullet.SpawnFromPool("Bullet", attackPoint.position, Quaternion.identity);// = Instantiate(bullet, attackPoint.position, Quaternion.identity);
      

        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
    }
}
