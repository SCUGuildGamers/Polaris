using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTime : MonoBehaviour
{
    private bool bt = false;
    private bool cd = false;

    void Update (){
        if (bt == false && cd == false){
            if(Input.GetKeyDown("z"))
    		{
			    btime();
		    }
        }		
	}

    void btime(){
        Debug.Log("btime");
        bt = true;
        cd = true;
        Time.timeScale = 0.4f;
        Invoke("btimeover", 1f);
    }

    void btimeover(){
        Debug.Log("btime over");

        Time.timeScale = 1f;
        bt = false;
        Invoke("cdover", 5f);
    }

    void cdover(){
        cd = false;
    }
}
