using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter
{
    public class EnemyManager : MonoBehaviour
    {
        private Coroutine waveRoutine;

        [SerializeField]
        private List<GameObject> enemyPrefabs;

        [SerializeField]
        private List<Rect> spawnAreas;

        [SerializeField]
        private Color gizmoColor = new Color(1, 0, 0, .3f);

        private List<EnemyController> activeEnemies = new List<EnemyController>();

        private bool enemySpawnComplete = false;

        [SerializeField]
        private float timeBetweenSpawns = 0.2f;

        [SerializeField]
        private float timeBetweenWaves = 1f;

        private GameManager gameManager;

        public void Init(GameManager instance)
        {
            gameManager = instance;
        }

        public void StartWave(int waveCount)
        {
            if (waveCount <= 0)
            {
                gameManager.EndOfWave();
                return;
            }
            if (waveRoutine != null)
            {
                StopCoroutine(waveRoutine);
            }
            waveRoutine = StartCoroutine(SpawnWave(waveCount));
        }

        public void StopWave()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnWave(int waveCount)
        {
            enemySpawnComplete = false;
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int i = 0; i < waveCount; i++)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                SpawnRandomEnemy();
            }

            enemySpawnComplete = true;
        }

        private void SpawnRandomEnemy()
        {
            if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
            {
                Debug.LogWarning("Doesn't setting Enemy Prefabs or Spawn Areas");
                return;
            }

            GameObject randPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

            Vector2 randomPos = new Vector2(Random.Range(randomArea.xMax, randomArea.xMax), Random.Range(randomArea.yMin, randomArea.yMax));

            GameObject go = Instantiate(randPrefab, randomPos, Quaternion.identity);
            EnemyController enemyCon = go.GetComponent<EnemyController>();
            enemyCon.Init(this, gameManager.player.transform);

            activeEnemies.Add(enemyCon);
        }

        private void OnDrawGizmosSelected()
        {
            if (spawnAreas == null) return;

            Gizmos.color = gizmoColor;
            foreach (var area in spawnAreas)
            {
                Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
                Vector3 size = new Vector3(area.width, area.height);

                Gizmos.DrawCube(center, size);
            }
        }


        public void RemoveEnemyOnDeath(EnemyController enemy)
        {
            activeEnemies.Remove(enemy);
            if (enemySpawnComplete && activeEnemies.Count == 0)
            {
                gameManager.EndOfWave();
            }
        }

    }
}
