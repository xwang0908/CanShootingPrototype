using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class VolleyManager : MonoBehaviour
{
    public enum SpawnerSide
    {
        Left,
        Right,
        Random,
        DoNotSpawn
    }

    public enum EndRound
    {
        AllDestroyed,
        Timed
    }

    [Serializable]
    public class Round
    {
        public List<ThrowCan> Cans;
        public float DowntimeAfterRound;
        public EndRound WhenToEndRound;
    }
    
    [Serializable]
    public class ThrowCan
    {
        public SpawnerSide Spawner;
        public float DowntimeAfterThrow;
    }

    [Tooltip("The order of things that will happen once the game starts")] [SerializeField]
    private List<Round> Schedule;

    [Tooltip("The left spawner")] [SerializeField]
    private CanSpawner LeftSpawner;

    [Tooltip("The right spawner")] [SerializeField]
    private CanSpawner RightSpawner;

    [Tooltip("Factor to reduce the delay by each time the entire schedule is completed")] [SerializeField]
    private float DelayReductionFactor;

    private Coroutine _throwCans;

    void Start()
    {
        StartThrowingCans();
    }
    
    public void StartThrowingCans()
    {
        _throwCans = StartCoroutine(RunSchedule());
    }

    public void StopThrowingCans()
    {
        StopCoroutine(_throwCans);
    }

    public IEnumerator RunSchedule()
    {
        float delayReduction = 1.0f;
        
        while (true)
        {
            foreach(Round r in Schedule)
            {
                foreach (ThrowCan tc in r.Cans)
                {
                    if(tc.Spawner == SpawnerSide.DoNotSpawn)
                        continue;
                    
                    CanSpawner spawner = LeftSpawner;
                    if (tc.Spawner == SpawnerSide.Right || (tc.Spawner == SpawnerSide.Random && Random.Range(0, 2) == 0))
                        spawner = RightSpawner;
                    spawner.SpawnCan();

                    yield return new WaitForSeconds(tc.DowntimeAfterThrow * delayReduction);
                }
                
                if(r.WhenToEndRound == EndRound.AllDestroyed)
                    while (CanManager.Instance.NumActiveCans() > 0)
                        yield return null;

                yield return new WaitForSeconds(r.DowntimeAfterRound);
            }

            delayReduction *= DelayReductionFactor;
        }
    }
}
