using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int maxPlanes = 10;
    private int numberOfPlanes=0;
    public Text mEnemyCountText = null;
    public int mPlanesDestroyed = 0;
    public Text mEggCountText=null;
    public int mEggsPresent = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)||Input.GetKey(KeyCode.Q)){
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying=false;
            #else
                Application.Quit();
            #endif
        }
        if(numberOfPlanes<maxPlanes){
            //Bounds myBound = GetComponent<Renderer>().bounds;
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            e.transform.localPosition = transform.localPosition;
            Vector3 pos;
            pos.x = s.GetWorldBound().min.x +Random.value*s.GetWorldBound().size.x;
            pos.y = s.GetWorldBound().min.y +Random.value*s.GetWorldBound().size.y;
            pos.z = 0;
            e.transform.localPosition=pos;
            ++numberOfPlanes;
        }
    }

    public void EnemyDestroyed(){
        mPlanesDestroyed = mPlanesDestroyed + 1;
        mEnemyCountText.text="Planes Destroyed = " + mPlanesDestroyed;
        --numberOfPlanes;
    }

    public void newEgg(bool e){
        if(e){
            ++mEggsPresent;
        } else {
            --mEggsPresent;
        }
        mEggCountText.text="Eggs Present = " + mEggsPresent;
    }
}
