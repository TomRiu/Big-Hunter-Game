using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int bulletAmount;
    public float fireRate;
    private float currentFireRate;
    private bool isShot, emptyShot, reloading;

    public GameObject viewFinder;
    private GameObject viewFinderClone;
    public Animator camAnim;

    public GameObject ViewFinderClone { get => viewFinderClone; set => viewFinderClone = value; }

    private void Awake()
    {
        instance = this;
        isShot = false;
        emptyShot = false;
        reloading = false;
        bulletAmount = 5;
        currentFireRate = fireRate;
        Infor.instance.ReloadAmmo();
    }

    // Start is called before the first frame update
    void Start()
    {
        ViewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManger.instance.IsGameover)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && isShot == false && reloading == false)
            {
                bulletAmount -= 1;
                Debug.Log(bulletAmount);
                if (bulletAmount >= 0)
                {
                    isShot = true;
                    Shoot(mousePos);
                    CamShake();
                    Infor.instance.ammo[bulletAmount].SetActive(false);
                    AudioController.instance.PlaySound("shoot");
                }
                else
                {
                    Debug.Log("Reload!");
                    emptyShot = true;
                    AudioController.instance.PlaySound("empty gun");
                }
            }

            if (isShot)
            {
                currentFireRate -= Time.deltaTime;
                if (currentFireRate <= 0)
                {
                    isShot = false;
                    currentFireRate = fireRate;
                }
            }
            else if(emptyShot)
            {
                emptyShot = false;
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletAmount < 5)
            {
                bulletAmount = 5;
                reloading = true;
                StartCoroutine(ReloadAmmo());
                AudioController.instance.PlaySound("fast reloading");
            }

            ViewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            Infor.instance.fireRateSlider.fillAmount = currentFireRate / fireRate;
        }
        else Destroy(ViewFinderClone);
    }

    IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(2);
        Infor.instance.ReloadAmmo();
        reloading = false;
    }

    void Shoot(Vector3 mousePos)
    {
        Vector3 shootDir = Camera.main.transform.position - mousePos;
        shootDir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    Debug.Log(hit.collider.name);

                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird && !GameManger.instance.IsGameover)
                    {
                        bird.Die();
                        GameManger.instance.Score += bird.point;
                        GameManger.instance.CurPlayTime += 1;
                    }
                }
            }
        }
    }

    private void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }
}
