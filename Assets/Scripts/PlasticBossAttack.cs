using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBossAttack : MonoBehaviour
{
    public GameObject plasticDrop;
    public TurtleController turtle;

    public JellyfishController jelly;


    public Transform plasticBoss;
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject plasticDropCopy = Instantiate(plasticDrop, plasticBoss.position, plasticBoss.rotation);
            plasticDropCopy.SetActive(true);
            plasticDropCopy.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TurtleController turtleCopy = Instantiate(turtle, plasticBoss.position, plasticBoss.rotation);
            turtleCopy.Spawn();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(ccw_spiral((int)plasticBoss.position.x, (int)plasticBoss.position.y - 10, 3, 0.6f));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(cw_spiral((int)plasticBoss.position.x, (int)plasticBoss.position.y - 10, 3, 0.6f));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(pincer(0.3f));
        }
    }

    private IEnumerator ccw_spiral(int x, int y, int mode, float sec){
        for (int i = -5; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy1.Spawn(x + (i * 6) - 20, y, mode, 0);
                JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy2.Spawn(x + (i * 6) - 10, y, mode, 0);
                JellyfishController jellyCopy3 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy3.Spawn(x + (i * 6), y, mode, 0);
                JellyfishController jellyCopy4 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy4.Spawn(x + (i * 6) + 10, y, mode, 0);
                JellyfishController jellyCopy5 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy5.Spawn(x + (i * 6) + 20, y, mode, 0);
        }
    }

    private IEnumerator cw_spiral(int x, int y, int mode, float sec){
        for (int i = -5; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy1.Spawn(x - (i * 6) + 20, y, mode, 0);
                JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy2.Spawn(x - (i * 6) + 10, y, mode, 0);
                JellyfishController jellyCopy3 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy3.Spawn(x - (i * 6), y, mode, 0);
                JellyfishController jellyCopy4 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy4.Spawn(x - (i * 6) - 10, y, mode, 0);
                JellyfishController jellyCopy5 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy5.Spawn(x - (i * 6) - 20, y, mode, 0);
        }
    }

    private IEnumerator pincer(float sec){
        int x = (int)player.position.x;
        int y = (int)player.position.y / 2;
        for (int i = 0; i < 20; i++){
            yield return new WaitForSecondsRealtime(sec);
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);

                if (i % 2 == 0){
                    jellyCopy1.Spawn(x, y, 1, 0.06f);
                    jellyCopy2.Spawn(x, y, 2, 0.002f);
                } else {
                    jellyCopy1.Spawn(x, y, 2, 0.06f);
                    jellyCopy2.Spawn(x, y, 1, 0.002f);
                }
        }
    }
}