using UnityEngine;

public class Gun : MonoBehaviour, IPoolObject
{
    public GameObject bullet;
    bulletPooler currentBullet;

    public float shootForce, upwardForce;

    //public float timebetweenShooting, spread, timeBetweenShots;
    //public int bulletsPerTap;

    //int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera cam;
    public Transform attackPoint;

    private void Start() 
    { 
        currentBullet = bulletPooler.Instance;
    }

    private void Awake() 
    {
        readyToShoot = true; 
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

        if (Physics.Raycast(ray, out hit)) { targetPoint = hit.point; Debug.Log("SHOT"); }
        else { targetPoint = ray.GetPoint(75); }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        currentBullet.SpawnFromPool("Bullet", attackPoint.position, Quaternion.identity);// = Instantiate(bullet, attackPoint.position, Quaternion.identity);
       
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
    }
}
