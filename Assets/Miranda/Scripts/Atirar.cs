using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire; //Se o jogador pode atirar ou n�o
    private float timer = 0;
    public float timeBetweenFiring = 10f;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
        }

        if (canFire && Input.GetMouseButton(1))
        {
            canFire = false;
            timer = 0;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        if (timer >= timeBetweenFiring)
        {
            canFire = true;
        }
    }
}
