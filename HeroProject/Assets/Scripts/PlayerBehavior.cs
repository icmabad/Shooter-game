using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Text mModeText = null;
    public float speed = 20f;
    public float mHeroRotateSpeed=90f/2f;

    public bool mFollowMousePosition = true;

    //public int mPlanesTouched=0;
    private float eCoolDown=0.2f;
    private float eFireTime=0f;
    private float bFireTime=0f;
    private GameController mGameController = null;

    // Start is called before the first frame update
    void Start()
    {
        mGameController=FindObjectOfType<GameController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            mFollowMousePosition = !mFollowMousePosition;
            if(mFollowMousePosition){
                mModeText.text="Control Mode: Mouse";
            } else {
                mModeText.text="Control Mode: Keyboard";
            }
        }

        Vector3 pos = transform.position;

        if(mFollowMousePosition){
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Position is " + pos);
            pos.z=0f;
        } else {

            if (Input.GetKey(KeyCode.W))
            {
                pos += (speed * Time.smoothDeltaTime)*transform.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= (speed * Time.smoothDeltaTime)*transform.up;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.forward,-mHeroRotateSpeed *Time.smoothDeltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.forward,mHeroRotateSpeed *Time.smoothDeltaTime);
            }
        }

        if (Input.GetKey(KeyCode.Space)&&Time.time>eFireTime){
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            Rigidbody2D re = e.GetComponent<Rigidbody2D>();
            //Debug.Log("Spawn Eggs: " + e.transform.localPosition);
            eFireTime=Time.time+eCoolDown;
            mGameController.newEgg(true);
        }

        if (Input.GetKey(KeyCode.B)&&Time.time>bFireTime){
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg2") as GameObject);
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            Rigidbody2D re = e.GetComponent<Rigidbody2D>();
            //Debug.Log("Spawn Eggs: " + e.transform.localPosition);
            bFireTime=Time.time+(eCoolDown*5);
            mGameController.newEgg(true);
        }

        transform.position = pos;
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy"){
            Debug.Log("Hero x Plane: OnTrigger2D");
            //mPlanesTouched = mPlanesTouched + 1;
            //mEnemyCountText.text="Planes touched = " + mPlanesTouched;
            Destroy(collision.gameObject);
            mGameController.EnemyDestroyed();
        }
    }

    public void OnTriggerStay2D(Collider2D collision){
        Debug.Log("Hero x Plane: OnTriggerStay2D");
    }
}
