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
            StartCoroutine(sweep_right((int)plasticBoss.position.x + 30, (int)plasticBoss.position.y - 10, 1.2f));
            StartCoroutine(sweep_right((int)plasticBoss.position.x + 15, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_right((int)plasticBoss.position.x, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_right((int)plasticBoss.position.x - 15, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_right((int)plasticBoss.position.x - 30, (int)plasticBoss.position.y - 10, 1.2f));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(sweep_left((int)plasticBoss.position.x + 30, (int)plasticBoss.position.y - 10, 1.2f));
            StartCoroutine(sweep_left((int)plasticBoss.position.x + 15, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_left((int)plasticBoss.position.x, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_left((int)plasticBoss.position.x - 15, (int)plasticBoss.position.y - 20, 1.2f));
            StartCoroutine(sweep_left((int)plasticBoss.position.x - 30, (int)plasticBoss.position.y - 10, 1.2f));        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(pincer(1.2f));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(fan(1.4f));
        }

        if (Input.GetKeyDown(KeyCode.G)){
            StartCoroutine(burst((int)plasticBoss.position.x, (int)plasticBoss.position.y - 5));
        }
    }

    private IEnumerator fan(float sec){
        Vector3 target =  plasticBoss.position;
        target.y = target.y - 100;
        target = target.normalized;
        target = new Vector3 (player.position.x + (target.x * 5), player.position.y + (target.y * 5), 0);
        for (int i = 0; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                if (i % 2 == 0){
                    JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                    jellyCopy1.Spawn((int)target.x, (int)target.y, 3, 0);
                    JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                    jellyCopy2.Spawn((int)target.x + 30, (int)target.y, 3, 0);
                    JellyfishController jellyCopy3 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                    jellyCopy3.Spawn((int)target.x - 30, (int)target.y, 3, 0);                    
                } else {
                    JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                    jellyCopy1.Spawn((int)target.x - 12, (int)target.y, 3, 0);
                    JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                    jellyCopy2.Spawn((int)target.x + 12, (int)target.y, 3, 0);    
                }
        }
    }

    private IEnumerator sweep_right(int x, int y, float sec){
        float index = 0;
        for (int i = 5; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                index += Time.deltaTime;
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy1.Spawn(x + (int)(i * 6 * Mathf.Cos(index)) - 40, y, 3, 0);
        }
    }

   private IEnumerator sweep_left(int x, int y, float sec){
        float index = 0;
        for (int i = 5; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                index += Time.deltaTime;
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                jellyCopy1.Spawn(x - (int)(i * 6 * Mathf.Cos(index)) + 40, y, 3, 0);
        }
    }

    private IEnumerator pincer(float sec){
        int x = (int)plasticBoss.position.x;
        int y1 = (int)plasticBoss.position.y - 5;
        int y2 = (int)plasticBoss.position.y - 7;
        int y3 = (int)plasticBoss.position.y - 10;

        for (int i = 0; i < 10; i++){
            yield return new WaitForSecondsRealtime(sec);
                JellyfishController jellyCopy1 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy2 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy3 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy4 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy5 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                JellyfishController jellyCopy6 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
                
                jellyCopy1.Spawn(x, y1, 1, 0.01f);
                jellyCopy2.Spawn(x, y2, 1, 0.002f);
                jellyCopy3.Spawn(x, y3, 1, 0.0005f);
                jellyCopy4.Spawn(x, y1, 2, 0.01f);
                jellyCopy5.Spawn(x, y2, 2, 0.002f);
                jellyCopy6.Spawn(x, y3, 2, 0.0005f);
        }
    }

    private IEnumerator burst(int x, int y){
        JellyfishController jellyCopy0 = Instantiate(jelly, plasticBoss.position, plasticBoss.rotation);
        jellyCopy0.Spawn(x, y, 4, 0);
        
        float sec = Mathf.Sqrt(Mathf.Pow(plasticBoss.position.x - x, 2) + Mathf.Pow(plasticBoss.position.y - y, 2)) / 0.003f;
        
        for (int i=0; i < sec; i++){
            yield return null;
        }

        JellyfishController jellyCopy1 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy1.Spawn(x + 100, y, 3, 0);
        JellyfishController jellyCopy2 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy2.Spawn(x - 100, y, 3, 0);
        JellyfishController jellyCopy3 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy3.Spawn(x, y + 100, 3, 0);
        JellyfishController jellyCopy4 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy4.Spawn(x, y - 100, 3, 0);
        JellyfishController jellyCopy5 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy5.Spawn(x + 100, y + 100, 3, 0);
        JellyfishController jellyCopy6 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy6.Spawn(x - 100, y + 100, 3, 0);
        JellyfishController jellyCopy7 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy7.Spawn(x + 100, y - 100, 3, 0);
        JellyfishController jellyCopy8 = Instantiate(jelly, new Vector3(x, y, 0), plasticBoss.rotation);
        jellyCopy8.Spawn(x - 100, y - 100, 3, 0); 
    }
}