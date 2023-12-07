using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class TestingScript : MonoBehaviour
{
    GameObject game;
    Player player;
    float wait = 2f;

    [UnitySetUp]
    public void SetUp()
    {
        game = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Tower Defence Game"));

        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), game.transform).GetComponent<Player>();

        GameObject.Find("HUD").GetComponent<HUDManager>().MakePlayer = player;
    }

    [UnityTest]
    public IEnumerator PlayModeTest()
    {
        Assert.IsTrue(UnityEditor.EditorApplication.isPlaying);

        yield return new WaitForSeconds(wait);
    }

    [UnityTest]
    public IEnumerator PlayerCurrencyIsTen()
    {
        SetUp();

        int money = player.Currency;

        yield return new WaitForSeconds(wait);

        Assert.That(money == 10);

        TearDown();
    }

    [UnityTest]
    public IEnumerator PlaceTower()
    {
        SetUp();

        Vector3 pos = Vector3.zero;

        if (Physics.Raycast(player.transform.position, -player.transform.up, out RaycastHit rayHit, Mathf.Infinity, LayerMask.NameToLayer("Buildable Layer")))
        {
            pos = rayHit.point;
        }

        TowerCreationManager towerC = game.GetComponentInChildren<TowerCreationManager>();

        Object.Instantiate(towerC.GetChosenTower, pos, Quaternion.identity);

        yield return new WaitForSeconds(wait);

        Assert.IsTrue(GameObject.Find("Tower 1 Prefab (Placeholder)(Clone)"));

        TearDown();
    }

    [UnityTest]
    public IEnumerator Is_Enemy_Count_In_Round_One_Less_Than_Round_Two()
    {
        SetUp();

        //wait a bit after setting up game scene
        yield return new WaitForSeconds(wait);

        EnemySpawner spawner = Object.FindAnyObjectByType<EnemySpawner>();

        //records the number of enemies that have spawned each round
        int temp = 0, temp2 = 0, roundThreeCount = 0;
        spawner.onFinishSpawning.AddListener(() => temp = spawner.enemiesInScene.Length);
        //kill all enemies in game scene
        spawner.onFinishSpawning.AddListener(spawner.KillAllEnemies);

        //switches game state from build phase to attack phase
        GameStateHandler.Instance.SwitchState();

        //wait 
        yield return new WaitForSeconds(10f);

        int roundOneCount = temp;

        spawner.onFinishSpawning.AddListener(() => temp2 = spawner.enemiesInScene.Length);

        GameStateHandler.Instance.SwitchState();

        yield return new WaitForSeconds(10f);

        int roundTwoCount = temp2;

        spawner.onFinishSpawning.AddListener(() => roundThreeCount = spawner.enemiesInScene.Length);

        GameStateHandler.Instance.SwitchState();

        yield return new WaitForSeconds(15f);

        Debug.Log($"Round 1: Enemies = {roundOneCount:00}\n" +
            $"Round 2: Enemies = {roundTwoCount:00}\n" +
            $"Round 3: Enemies = {roundThreeCount:00}\n ");

        //checks whether enemy count in round 1 is less than that of round 2
        Assert.IsTrue(roundOneCount < roundThreeCount);

        TearDown();
    }

    [UnityTearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
