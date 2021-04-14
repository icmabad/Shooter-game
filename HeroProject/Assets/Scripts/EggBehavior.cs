using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    private const float kEggSpeed = 40f;
    //private const int kLifeTime=4000;
    //private int mLifeCount=0;
    private GameController mGameController = null;
    // Start is called before the first frame update
    void Start()
    {
        //mLifeCount=kLifeTime;
        mGameController=FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=transform.up*(kEggSpeed*Time.smoothDeltaTime);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
        mGameController.newEgg(false);
    }

}
