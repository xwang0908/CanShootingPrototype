using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    private int _numThrownCans;
    private int _numCompletedRounds;

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
    
    public int NumScheduledCans()
    {
        int total = 0;
        foreach (Round r in Schedule)
            total += r.Cans.Count;
        return total + total * _numCompletedRounds;
    }

    public int NumThrownCans()
    {
        return _numThrownCans;
    }

    public IEnumerator RunSchedule()
    {
        float delayReduction = 1.0f;
        bool loopAgain = true;
        _numCompletedRounds = -1;
        
        while (loopAgain)
        {
            _numCompletedRounds++;
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
                    
                    _numThrownCans++;

                    yield return new WaitForSecondsRealtime(tc.DowntimeAfterThrow * delayReduction);
                }
                
                if(r.WhenToEndRound == EndRound.AllDestroyed)
                    while (CanManager.Instance.NumActiveCans() > 0)
                        yield return null;

                // No waiting after completing the schedule the first time
                if(_numCompletedRounds == 0)
                    yield return new WaitForSecondsRealtime(r.DowntimeAfterRound);
            }
            
            delayReduction *= DelayReductionFactor;
            loopAgain = ScoreManager.Instance.GetScore() == _numThrownCans;
        }

        GameManager.Instance.EndGame();
    }
}
