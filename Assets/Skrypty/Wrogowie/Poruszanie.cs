using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poruszanie : MonoBehaviour
{
    public Transform wroga_Transform;
    public SpriteRenderer sprite_wroga;
    public Sprite stanie;
    public Sprite poruszanie;
    public Rigidbody2D rigidbody2d;
    public static Poruszanie instance;
    private void Awake()
    {
        //gameObject.SetActive(false);
        instance = this;
        wroga_Transform = transform.Find("sprite");
        sprite_wroga = wroga_Transform.GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update


    // Update is called once per frame

    public void PoruszanieWPrawo()
    {
        sprite_wroga.sprite = poruszanie;
        wroga_Transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void PoruszanieWGore()
    {
        sprite_wroga.sprite = poruszanie;
        wroga_Transform.rotation= Quaternion.Euler(0,0,90f);
    }
    public void PoruszanieWLewo()
    {
        sprite_wroga.sprite = poruszanie;
        wroga_Transform.rotation = Quaternion.Euler(0, 180f, 0f);
    }
    public void PoruszanieWDol()
    {
        sprite_wroga.sprite = poruszanie;
        wroga_Transform.rotation = Quaternion.Euler(0, 180f, -90f);
    }
}
