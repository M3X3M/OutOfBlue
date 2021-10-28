using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthBarController healthbar;
    [SerializeField] private GameObject enemy_group;
    [SerializeField] private GameObject loady_prefab;
    [SerializeField] private GameObject responder_prefab;
    [SerializeField] private GameObject aoa_prefab;
    private bool wave_is_active = false;
    private int wave_type = 0;
    private float[] aoa_properties = {0.1f, 1f, -0.07f};
    private float[] responder_properties = {0.05f, 1.5f, -0.1f};

    // Start is called before the first frame update
    void Start()
    {
        //resetting the global score
        GameData.ResetScore();
        StartCoroutine(LoadyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_group.transform.childCount <= 0)
        {
            wave_is_active = false;
        }
        

        if(!wave_is_active)
        {
            SpawnNewWave();            
            wave_is_active = true;
        }
    }

    public void PlayerWasHit(int new_health)
    {
        healthbar.onHealthChanged(new_health);
        if(new_health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator SpawnLoadyWave(int amount, float speed_multiplier, float wait_time)
    {
        List<LoadyController> loady_controllers = new List<LoadyController>();
        float top_thresh = loady_prefab.GetComponent<LoadyController>().getThreshholdTop();
        float bot_thresh = loady_prefab.GetComponent<LoadyController>().getThreshholdBot();

        float start_pos = GetFloatInRange(bot_thresh, top_thresh);

        for(int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(loady_prefab, new Vector2(9.1f, 4.3f), Quaternion.identity);
            LoadyController loady_ctrl = go.GetComponent<LoadyController>();
            loady_controllers.Add(loady_ctrl);
            loady_ctrl.setSpeedMultiplier(speed_multiplier);
            loady_ctrl.setStartPos(start_pos);
        }

        yield return new WaitForSeconds(1f);

        foreach (LoadyController l_ctrl in loady_controllers)
        {
            l_ctrl.startMoving();
            yield return new WaitForSeconds(wait_time);
        }

        yield return new WaitForSeconds(10f);
        wave_is_active = false;
    }

    private void SpawnVerticalEnemy(GameObject vertical_enemy_prefab)
    {
        GameObject go = Instantiate(vertical_enemy_prefab, new Vector2(11f, 0f), Quaternion.identity);
        go.transform.parent = enemy_group.transform;
    }
    private void SpawnVerticalEnemy(GameObject vertical_enemy_prefab, float vert_speed, float bullet_cooldown, float bullet_speed)
    {
        GameObject go = Instantiate(vertical_enemy_prefab, new Vector2(11f, 0f), Quaternion.identity);
        go.transform.parent = enemy_group.transform;
        VerticalEnemyController vectrl = go.GetComponent<VerticalEnemyController>();
        vectrl.SetDifficulty(vert_speed, bullet_cooldown, bullet_speed);
    }
    private float GetFloatInRange(float bot, float top)
    {
        float range = top - bot;
        System.Random random = new System.Random();
        double val = (random.NextDouble() * range + bot);
        return (float)val;
    }

    private IEnumerator LoadyTimer()
    {
        int counter = 0;
        int amount = 5;
        float speed_multiplier = 1f;
        while(true)
        {
            if(((counter % 5) == 0) && (amount < 30))
            {
                counter = 0;
                amount = amount++;
                speed_multiplier = speed_multiplier + 0.05f;
            }
            yield return new WaitForSeconds(20);
            StartCoroutine(SpawnLoadyWave(5, 1f, .3f));
            ++counter;
        }
    }

    private void SpawnNewWave()
    {
        switch (wave_type)
        {
            case 0:
            {
                SpawnVerticalEnemy(responder_prefab, responder_properties[0], responder_properties[1], responder_properties[2]);
                if(responder_properties[1] > 0.2f)
                {
                    responder_properties[1] -= 0.05f;
                }
                if(responder_properties[2] > (-0.3f))
                {
                    responder_properties[2] -= 0.05f;
                }
                wave_type = 1;
                break;
            }
            default:
            {
                SpawnVerticalEnemy(aoa_prefab, aoa_properties[0], aoa_properties[1], aoa_properties[2]);
                if(aoa_properties[1] > 0.2f)
                {
                    aoa_properties[1] -= 0.05f;
                }
                if(aoa_properties[2] > (-0.3f))
                {
                    aoa_properties[2] -= 0.05f;
                }
                wave_type = 0;
                break;
            }
        }
    }
}
