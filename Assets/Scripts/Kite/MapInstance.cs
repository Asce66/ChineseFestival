using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    public GameObject rightEagle;
    public GameObject leftEagle;
    public GameObject cloud;
    public GameObject butterfly;

    public float eagleTimeMax = 13;
    public float eagleTimeMin = 9;
    public float min = 3;
    public float changeInsSpeed = 0.0001f;

    private float leftInsTime = 0;
    private float rightInsTime = 0;
    private float cloudInsTime = 0;
    private float butterflyInsTime = 0;

    private float leftInsTimer = 0;
    private float rightInsTimer = 0;
    private float cloudInsTimer = 0;
    private float butterflyInsTimer = 0;

    private GameObject kite;

    [HideInInspector]
    public float eagleSpeedIncr=0;
    [SerializeField] float eagleSpeedRatio;

    AudioClip ac;
    AudioSource ads;

    void Start()
    {
        leftInsTime = Random.Range(eagleTimeMin, eagleTimeMax);
        rightInsTime = Random.Range(eagleTimeMin, eagleTimeMax);
        kite = GameObject.Find("Kite");
        ac = Resources.Load<AudioClip>("AudioClips/EagleVoice");
        ads = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager_kite.isStop || GameManager_kite.useStop)
            return;
        eagleSpeedIncr += Time.deltaTime * eagleSpeedRatio;
        if(eagleTimeMin > min)
        {
            eagleTimeMax -= Time.deltaTime * changeInsSpeed;
            eagleTimeMin -= Time.deltaTime * changeInsSpeed;
        }
        LeftIns();
        RightIns();
        CloudIns();
        ButterflyIns();
    }

    void LeftIns()
    {
        leftInsTimer += Time.deltaTime;
        if(leftInsTimer > leftInsTime)
        {
            leftInsTimer = 0;
            leftInsTime = Random.Range(eagleTimeMin, eagleTimeMax);
            if (kite.transform.position.x > 0)
            {
                Vector3 pos = GetLeftPosition();
                Instantiate(leftEagle, pos, leftEagle.transform.rotation).GetComponent<EagleMovement>().mapInstance = this;
            }
            else
            {
                Vector3 pos = GetRightPosition();
                Instantiate(rightEagle, pos, rightEagle.transform.rotation).GetComponent<EagleMovement>().mapInstance=this;
            }
            ads.PlayOneShot(ac);
        }
    }

    void RightIns()
    {
        rightInsTimer += Time.deltaTime;
        if (rightInsTimer > rightInsTime)
        {
            
            rightInsTimer = 0;
            rightInsTime = Random.Range(eagleTimeMin, eagleTimeMax);

            if (kite.transform.position.x < 0)
            {
                Vector3 pos = GetRightPosition();
                GameObject go = Instantiate(rightEagle, pos, rightEagle.transform.rotation,transform);
                go.GetComponent<EagleMovement>().mapInstance = this;
            }
            else
            {
                Vector3 pos = GetLeftPosition();
                GameObject go = Instantiate(leftEagle, pos, leftEagle.transform.rotation,transform);
                go.GetComponent<EagleMovement>().mapInstance = this; ;
            }
            ads.PlayOneShot(ac);
        }
    }

    void CloudIns()
    {
        cloudInsTimer += Time.deltaTime;
        if (cloudInsTimer > cloudInsTime)
        {
            cloudInsTimer = 0;
            cloudInsTime = Random.Range(7, 12);
            Instantiate(cloud, GetCloudPosition(), Quaternion.identity);
        }
    }

    void ButterflyIns()
    {
        butterflyInsTimer += Time.deltaTime;
        if (butterflyInsTimer > butterflyInsTime)
        {
            butterflyInsTimer = 0;
            butterflyInsTime = Random.Range(7, 12);
            Instantiate(butterfly, GetCloudPosition(), Quaternion.identity);
        }
    }

    Vector2 GetLeftPosition()
    {
        Vector3 pos = leftEagle.transform.localPosition;
        pos.x += Random.Range(-50, 50);
        return pos;
        float flag = Random.Range(1, 2);
        float xPosition = Random.Range(-100, -230);
        if (flag >= 1 && kite.transform.position.x > xPosition)
        {
            return new Vector2(xPosition, 230);
        }
        else
        {
            return new Vector2(-230, Random.Range(75, 150));
        }
    }

    Vector2 GetRightPosition()
    {
        Vector3 pos = rightEagle.transform.localPosition;
        pos.x += Random.Range(-50, 50);
        return pos;
        float flag = Random.Range(1, 2);
        float xPosition = Random.Range(100, 230);
        if (flag >= 1 && kite.transform.position.x < xPosition)
        {
            return new Vector2(xPosition, 230);
        }
        else
        {
            return new Vector2(230, Random.Range(75, 150));
        }
    }

    Vector3 GetCloudPosition()
    {
        return new Vector2(Random.Range(-200, 200), 230);
    }

    public void ResetTime()
    {
        eagleTimeMax = 15;
        eagleTimeMin = 7;
        leftInsTimer = 0;
        rightInsTimer = 0;
        cloudInsTimer = 0;
    }
}
