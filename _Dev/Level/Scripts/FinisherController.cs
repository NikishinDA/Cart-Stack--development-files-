using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    private List<CartController> _cartChain;
    [SerializeField] private FinisherSectionController[] finisherSections;
    [SerializeField] private float timeAddition = 0.25f;
    [SerializeField] private CinemachineVirtualCamera finisherCamera;
    [SerializeField] private float sectionLength = 3.842f;
    [SerializeField] private float zOffset = 3.842f;
    [SerializeField] private float popupShowDelay = 4f;
    private int _activeSectors = 0;

    private void Awake()
    {
        EventManager.AddListener<FinisherTakeAwayCartEvent>(OnFinisherTakeAwayCartEvent);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherTakeAwayCartEvent>(OnFinisherTakeAwayCartEvent);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        //var finisherCameraPosition = finisherCamera.transform.position;
        //finisherCameraPosition.z += (_activeSectors + 1) * sectionLength - zOffset;
        //finisherCamera.position = finisherCameraPosition;
        VarSaver.Multiplier = 1f + (_activeSectors) * 0.2f;
    }

    private void OnFinisherTakeAwayCartEvent(FinisherTakeAwayCartEvent obj)
    {
        _activeSectors++;
    }

    public void ActivateFinisher(Transform playerTransform)
    {
        //_cartChain = chain;
        finisherCamera.gameObject.SetActive(true);
        finisherCamera.Follow = playerTransform;
        finisherCamera.LookAt = playerTransform;
        //DistributeCarts();
        //StartCoroutine(PutPlayerInPos(playerTransform, 1f));
    }


   /* private void DistributeCarts()
    {
        float time = 1f;
        int j = 0;
        int i;
        for (i =0; i < _cartChain.Count; i++)
        {
            _cartChain[i].StopFollowing();
            finisherSections[i].SetCart(_cartChain[i],time);
            //_cartChain[i].transform.SetParent(cartHolders[i]);
            //_cartChain[i].SetCartInPosition(time);
            time += timeAddition;
            if (_cartChain[i].GetComponent<CartContentManager>().IsFull)
            {
                j = i;
            }
        }

        var finisherCameraPosition = finisherCamera.position;
        finisherCameraPosition.z += (j + 1) * sectionLength - zOffset;
        finisherCamera.position = finisherCameraPosition;
        VarSaver.Multiplier = 1f + (i-1) * 0.2f;
    }*/

    private IEnumerator PutPlayerInPos(Transform playerTransform, float time)
    {
        float oldPosX = transform.position.x;
        Vector3 newPos;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            newPos = playerTransform.position;
            newPos.x = Mathf.Lerp(oldPosX, 0, t/ time);
            playerTransform.position = newPos;
            yield return null;
        }
    }
}
