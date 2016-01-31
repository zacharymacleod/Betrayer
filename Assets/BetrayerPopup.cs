using UnityEngine;
using System.Collections;

public class BetrayerPopup : MonoBehaviour
{

    float timer;
    float expire;
    float opacity = 0f;
    bool ready = false;
    bool notified = false;

    // Use this for initialization
    void Start()
    {
        timer = 3f;
        expire = 7f;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (GameManager.State() == GameManager.state.Initial)
        //{
        //    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacity);
        //    GetComponent<SpriteRenderer>().enabled = true;
        //    if (ready)
        //    {
        //        timer -= Time.deltaTime;
        //        expire -= Time.deltaTime;
        //    }
        //    else { opacity += Time.deltaTime; if (opacity > 1) { ready = true; opacity = 1; } }

        //    if (timer < 0 & !notified)
        //    {
        //        GameManager.find().BetrayerNotify();
        //        notified = true;
        //    }
        //    if (expire < 0)
        //    {
        //        opacity -= Time.deltaTime;
        //    }
        //    if (opacity < 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
