using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Plastic Plastic;
    public TurtleController Turtle;
    public UrchinController Urchin;

    public Transform PlasticBoss;
    public Transform Player;


    void Update()
    {
		if(PauseMenu.GameIsPaused == false)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Plastic.Spawn(transform.position);
			}

			if (Input.GetKeyDown(KeyCode.T))
			{
				TurtleAttack();
			}

			if (Input.GetKeyDown(KeyCode.U))
			{
				UrchinAttack();
			}

			if (Input.GetKeyDown(KeyCode.H))
			{
				FanAttack(1.4f,10,5);
			}

			if (Input.GetKeyDown(KeyCode.J))
			{
				SweepRightLayered();
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				SweepLeftLayered();
			}

			if (Input.GetKeyDown(KeyCode.L))
			{
				PincerAttack();
			}	
		}
    }

    // Performs a single sweep left projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator SweepLeft(int xOffset, int yOffset, float sec, int numWaves)
    {
        float index = 0;
        for (int i = 5; i < 5 + numWaves; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(xOffset - (int)(i * 6 * Mathf.Cos(index) - 40), yOffset, 0), 3, 0);
        }
    }

    // Layers the sweep left attack by calling the SweepLeft with different offsets and with numProjectiles projectiles per wave; numProjectiles should be an ODD NUMBER
    public void SweepLeftLayered(int numProjectiles = 5)
    {
        // Generates the xOffset and yOffset values to achieve the layered effect
        int split = numProjectiles / 2;
        for (int i = split; i >= -split; i--)
        {
            if (i == split || i == -split)
                StartCoroutine(SweepLeft(i * 15, -10, 1.2f, 5));

            else
                StartCoroutine(SweepLeft(i * 15, -20, 1.2f, 5));
        }
    }

    // Performs a single sweep right projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator SweepRight(int xOffset, int yOffset, float sec, int numWaves)
    {
        float index = 0;
        for (int i = 5; i < 5 + numWaves; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            index += Time.deltaTime;
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(xOffset + (int)(i * 6 * Mathf.Cos(index) - 40),yOffset,0),3,0);
        }
    }

    // Layers the sweep right attack by calling the SweepRight with different offsets and with numProjectiles projectiles per wave; numProjectiles should be an ODD NUMBER
    public void SweepRightLayered(int numProjectiles=5)
    {
        // Generates the xOffset and yOffset values to achieve the layered effect
        int split = numProjectiles / 2;
        for (int i = split; i >= -split; i--)
        {
            if (i == split||i == -split)
                StartCoroutine(SweepRight(i*15, -10, 1.2f, 5));

            else
                StartCoroutine(SweepRight(i * 15, -20, 1.2f, 5));
        }
    }

    // Performs the fan pattern projectile attack with a sec delay between each wave with numProjectiles projectiles per wave; numProjectiles should be an ODD NUMBER
    private IEnumerator Fan(float sec, int numWaves, int numProjectiles)
    {
        Vector3 target = PlasticBoss.position;
        target.y = target.y - 100;
        target = target.normalized;
        target = new Vector3(Player.position.x + (target.x * 5), Player.position.y + (target.y * 5), 0);
        for (int i = 0; i < numWaves; i++)
        {
            // Alternates between shooting three projectiles and two projectiles at a time
            yield return new WaitForSecondsRealtime(sec);
            if (i % 2 == 0)
            {
                // Generates the xOffset to achieve the fan effect
                int split = numProjectiles / 2;
                for (int j = split; j >= -split; j--)
                {
                    Plastic.Spawn(PlasticBoss.position, target + new Vector3(j*15, 0, 0), 3, 0);
                }
            }
            else
            {
                // Generates the xOffset to achieve the fan effect
                int split = numProjectiles / 2;
                for (int j = split; j >= -split; j--)
                {
                    // Ignore the middle spawn
                    if (j != 0)
                        Plastic.Spawn(PlasticBoss.position, target + new Vector3(j * 6, 0, 0), 3, 0);
                }
            }
        }
    }

    // Calls the IEnumerator fan function
    public void FanAttack(float sec = 1.4f, int numWaves = 10, int numProjectiles = 3)
    {
        StartCoroutine(Fan(sec,numWaves,numProjectiles));
    }

    // Performs the pincer pattern projectile attack with a sec delay between each wave
    private IEnumerator Pincer(float sec, int numWaves)
    {
        int x = (int)PlasticBoss.position.x;
        int y1 = (int)PlasticBoss.position.y - 5;
        int y2 = (int)PlasticBoss.position.y - 7;
        int y3 = (int)PlasticBoss.position.y - 10;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y1, 0), 1, 0.01f);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y2, 0), 1, 0.002f);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y3, 0), 1, 0.0005f);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y1, 0), 2, 0.01f);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y2, 0), 2, 0.002f);
            Plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x, y3, 0), 2, 0.0005f);
        }
    }

    // Calls the IEnumerator pincer function
    public void PincerAttack(int numWaves = 7)
    {
        StartCoroutine(Pincer(1.2f, numWaves));
    }

    // Summons the turtle spawn boss attack with a given minMovementSpeed and maxMovementSpeed
    public void TurtleAttack(float minMovementSpeed=3.5f, float maxMovementSpeed = 5f)
    {
        Turtle.Spawn(minMovementSpeed, maxMovementSpeed);
    }

    // Summons the urchin spawn boss attack that shoots out numPlasticSpawn projectiles
    public void UrchinAttack(int numPlasticSpawn = 9)
    {
        Urchin.Spawn(numPlasticSpawn);
    }
}
