using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private CustomerState _currentState;
    private Animator _animator;

    public bool hasClothes = false;

    private Vector3 _outPosition = new Vector3(0, 1.5f, 25);

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hasClothes = false;
        StartCoroutine(WaitUntilFindClothes());
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            _animator.SetBool("Walk",true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }

    public void CallFindClothesCoroutine()
    {
        StartCoroutine(WaitUntilFindClothes());
    }

    private IEnumerator WaitUntilFindClothes()
    {
        _currentState = CustomerState.LOOKING_FOR_CLOTHES;
        _navMeshAgent.SetDestination(transform.position);
        while (true)
        {
            var standPosition = findClothesStand();
            if (findClothesStand() != Vector3.zero)
            {
                GoToNearestNonEmptyClothesStand(standPosition);
                break;
            }
            yield return new WaitForSeconds(5);
        }
        yield return null;
    }

    private Vector3 findClothesStand()
    {
        var stands = GameObject.FindGameObjectsWithTag("Stand");
        foreach (var stand in stands)
        {
            if (stand.GetComponent<ClothesStand>().CurrentClothesAmount > 0)
            {
                return stand.transform.position;
            }
        }
        return Vector3.zero;
    }

    public void GoToNearestNonEmptyClothesStand(Vector3 standPosition)
    {
        _currentState = CustomerState.GO_TO_CLOTHES_STAND;
        _navMeshAgent.SetDestination(standPosition);
    }

    public void TakeClothes()
    {
        hasClothes = true;
        GoToCheckout();
    }

    public void GoToCheckout()
    {
        var checkout = GameObject.FindGameObjectWithTag("Checkout");
        _navMeshAgent.SetDestination(checkout.transform.position);
        _currentState = CustomerState.GO_TO_CHECKOUT;
    }

    public void WaitInQueue(Vector3 waitPos)
    {
        _currentState = CustomerState.IN_QUEUE;
        _navMeshAgent.SetDestination(waitPos);
    }

    public void GoOut()
    {
        // TODO: Give money on checkout
        _currentState = CustomerState.GO_OUT;
        _navMeshAgent.SetDestination(_outPosition);
    }

    private enum CustomerState
    {
        LOOKING_FOR_CLOTHES,
        GO_TO_CLOTHES_STAND,
        GO_TO_CHECKOUT,
        IN_QUEUE,
        GO_OUT
    }
}
