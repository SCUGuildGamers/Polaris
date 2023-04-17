using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Plastic _plastic;
    public Plastic _lanePlastic;
    public TurtleController Turtle;
    public UrchinController Urchin;
    public platform platform;

    public Animator animator;

    public Transform PlasticBoss;
    public Transform Player;

    private float _waveHeight = 5.5f;
    private float _screenHeight = 9f;

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(SetTrajectoryWaves(5, 0.5f, 3, 5));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(HomingShots(3, 5f));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LaneShots(3, 4f));
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                platform.Spawn(new Vector3(-10, -6, 0), new Vector3(-10, 3, 0));
                platform.Spawn(new Vector3(-10, -12, 0), new Vector3(-10, -3, 0));
                platform.Spawn(new Vector3(-4, -9, 0), new Vector3(-4, 0, 0));

            }

        }
    }

    // Performs a single sweep left projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator SweepLeft(int xOffset, int yOffset, float sec, int numWaves)
    {
        float index = 0;
        for (int i = 5; i < 5 + numWaves; i++)
        {
            yield return new WaitForSeconds(sec);
            index += Time.deltaTime;
            Vector3 target = PlasticBoss.position + new Vector3(xOffset, yOffset - (int)(i * 6 * Mathf.Cos(index) - 40), 0);
            _plastic.Spawn(PlasticBoss.position, target, transform, 3);
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
            {
                StartCoroutine(SweepLeft(-10, i * 15, 1.2f, 5));
            }
            else
            {
                StartCoroutine(SweepLeft(-20, i * 15, 1.2f, 5));
            }
        }
    }

    // Performs a single sweep right projectile attack with a sec delay between each wave adjusted by a xOffset and yOffset
    private IEnumerator SweepRight(int xOffset, int yOffset, float sec, int numWaves)
    {
        float index = 0;
        for (int i = 5; i < 5 + numWaves; i++)
        {
            yield return new WaitForSeconds(sec);
            index += Time.deltaTime;
            Vector3 target = PlasticBoss.position + new Vector3(xOffset, yOffset + (int)(i * 6 * Mathf.Cos(index) - 40), 0);
            _plastic.Spawn(PlasticBoss.position, target, transform, 3);
        }
    }

    // Layers the sweep right attack by calling the SweepRight with different offsets and with numProjectiles projectiles per wave; numProjectiles should be an ODD NUMBER
    public void SweepRightLayered(int numProjectiles = 5)
    {
        // Generates the xOffset and yOffset values to achieve the layered effect
        int split = numProjectiles / 2;
        for (int i = split; i >= -split; i--)
        {
            if (i == split || i == -split)
            {
                StartCoroutine(SweepRight(-10, i * 15, 1.2f, 5));
            }
            else
            {
                StartCoroutine(SweepRight(-20, i * 15, 1.2f, 5));
            }
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
            yield return new WaitForSeconds(sec);
            if (i % 2 == 0)
            {
                // Generates the xOffset to achieve the fan effect
                int split = numProjectiles / 2;
                for (int j = split; j >= -split; j--)
                {
                    _plastic.Spawn(PlasticBoss.position, target + new Vector3(0, j * 15, 0), transform, 3);
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
                        _plastic.Spawn(PlasticBoss.position, target + new Vector3(0, j * 6, 0), transform, 3);
                }
            }
        }
    }

    // Calls the IEnumerator fan function
    public void FanAttack(float sec = 1.4f, int numWaves = 10, int numProjectiles = 3)
    {
        StartCoroutine(Fan(sec, numWaves, numProjectiles));
    }

    // Performs the pincer pattern projectile attack with a sec delay between each wave
    private IEnumerator Pincer(float sec, int numWaves)
    {
        int y = (int)PlasticBoss.position.y;
        int x1 = (int)PlasticBoss.position.x - 5;
        int x2 = (int)PlasticBoss.position.x - 7;
        int x3 = (int)PlasticBoss.position.x - 10;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(sec);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x1, y, 0), transform, 1, false, 0.01f);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x2, y, 0), transform, 1, false, 0.002f);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x3, y, 0), transform, 1, false, 0.0005f);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x1, y, 0), transform, 2, false, 0.01f);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x2, y, 0), transform, 2, false, 0.002f);
            _plastic.Spawn(PlasticBoss.position, PlasticBoss.position + new Vector3(x3, y, 0), transform, 2, false, 0.0005f);
        }
    }

    // Calls the IEnumerator pincer function
    public void PincerAttack(int numWaves = 7)
    {
        StartCoroutine(Pincer(1.2f, numWaves));
    }

    // Summons the turtle spawn boss attack with a given minMovementSpeed and maxMovementSpeed
    public void TurtleAttack(float minMovementSpeed = 3.5f, float maxMovementSpeed = 5f)
    {
        Turtle.Spawn(minMovementSpeed, maxMovementSpeed);
    }

    // Summons the urchin spawn boss attack that shoots out numPlasticSpawn projectiles
    public void UrchinAttack(int numPlasticSpawn = 9)
    {
        Urchin.Spawn(numPlasticSpawn, Player.position);
    }

    public void cluster()
    {
        _plastic.Spawn(PlasticBoss.position, Player.position, transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, .5f, 0), transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, 1f, 0), transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, 1.5f, 0), transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, -.5f, 0), transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, -1f, 0), transform, 3, false, 0.1f);
        _plastic.Spawn(PlasticBoss.position, Player.position + new Vector3(0, -1.5f, 0), transform, 3, false, 0.1f);
    }

    public void Minefield(int num, float sec)
    {
        StartCoroutine(PlantMines(num, sec));
    }

    private IEnumerator PlantMines(int num, float sec)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSecondsRealtime(sec);
            Urchin.Spawn(12, PlasticBoss.position + new Vector3(-5 * (i + 1), 4, 0));
            Urchin.Spawn(12, PlasticBoss.position + new Vector3(-5 * (i + 1), -4, 0));
        }
    }

    // Spawns a set trajectory shot moving from right to left spawning at the Boss' position with offset by y_offset
    private void SetTrajectoryShot(float y_offset)
    {
        // Spawn position of the trajectory shot
        Vector3 spawn_position = transform.position + new Vector3(0, y_offset, 0);

        _plastic.Spawn(spawn_position, spawn_position + new Vector3(-1, 0, 0), transform, 3);
    }

    // Spawns a set trajectory wave with num_projectiles projectiles and a wait of projectile_wait between each projectile
    private IEnumerator SetTrajectoryWave(int num_projectiles, float projectile_wait)
    {
        animator.SetTrigger("bossTossTrigger");
        for (int i = 0; i < num_projectiles; i++)
        {
            SetTrajectoryShot(GenerateWaveOffset());
            yield return new WaitForSecondsRealtime(projectile_wait);
        }
    }

    // Generates a random y offset for the SetTrajectoryShot to spawn the projectile at
    private float GenerateWaveOffset()
    {
        System.Random rand = new System.Random();
        double val = (rand.NextDouble() * (_waveHeight + _waveHeight) - _waveHeight);
        return (float)val;
    }

    // Spawns num_waves number of waves with wave_wait between each wave; each wave has num_projectiles projectiles with a wait of projectile_wait between them
    private IEnumerator SetTrajectoryWaves(int num_projectiles, float projectile_wait, int num_waves, float wave_wait)
    {
        for (int i = 0; i < num_waves; i++) {
            StartCoroutine(SetTrajectoryWave(num_projectiles, projectile_wait));
            yield return new WaitForSecondsRealtime(wave_wait);
        }
    }

    // Spawns a single homing projectile that moves towards the player until they deflect it / collide with the player
    private void HomingShot() {
        _plastic.Spawn(transform.position, transform.position, transform, 5, true);
    }

    // Spawns multiple homing projectiles depending on the parameters passed
    private IEnumerator HomingShots(int num_shots, float shot_wait) {
        animator.SetTrigger("bossTossTrigger");

        for (int i = 0; i < num_shots; i++) {
            HomingShot();
            yield return new WaitForSecondsRealtime(shot_wait);
        }
    }

    // Spawns a single lane shot which covers a certain vertical lane of the screen
    private void LaneShot(float y_offset) {
        animator.SetTrigger("bossTossTrigger");

        // Spawn position of the trajectory shot
        Vector3 spawn_position = transform.position + new Vector3(0, y_offset, 0);

        _lanePlastic.Spawn(spawn_position, spawn_position + new Vector3(-1, 0, 0), transform, 3, false, 0, true, 0.015f);
    }

    // Spawns multiple lane projectiles depending on the parameters passed
    private IEnumerator LaneShots(int num_shots, float shot_wait) {
        for (int i = 0; i < num_shots; i++) {
            LaneShot(GenerateLaneOffset());
            yield return new WaitForSecondsRealtime(shot_wait);
        }
    }

    // Generates a random y offset for the LaneShot to spawn the projectile at
    private float GenerateLaneOffset()
    {
        // Random number generation between 0-2
        System.Random rand = new System.Random();
        int num = rand.Next(0, 3);

        // Depending on the number rolled, spawn in the upper, middle, or lower quadrant
        if (num == 0)
            return (float)_screenHeight / 2f;

        else if (num == 1)
            return 0;

        else
            return -(float)_screenHeight / 2f;
    }
}
