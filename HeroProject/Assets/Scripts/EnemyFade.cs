using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFade : MonoBehaviour
{
    Renderer rend;
    private int hitPoints=4;
    private GameController mGameController = null;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
        mGameController=FindObjectOfType<GameController>();
    }

    IEnumerator Fade(GameObject f){
        rend = f.GetComponent<Renderer>();
        Color c = rend.material.color;
        c.a*=.8f;
        rend.material.color=c;
        yield return new WaitForSeconds(.8f);
    }

    void Update()
    {

    }

    public void starFading(GameObject f){
        StartCoroutine("Fade",f);
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Bullet"){
            --hitPoints;
            starFading(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Faded");
            if(hitPoints<=0){
                Destroy(gameObject);
                mGameController.EnemyDestroyed();
            }
        } else if(collision.gameObject.tag == "Bullet2"){
            hitPoints=hitPoints-2;
            starFading(gameObject);
            starFading(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Faded");
            if(hitPoints<=0){
                Destroy(gameObject);
                mGameController.EnemyDestroyed();
            }
        }
    }
}