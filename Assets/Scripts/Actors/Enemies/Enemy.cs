using DG.Tweening;
using DTOs;
using Enumerations;
using Interface;
using UnityEngine;
using Utilities.BaseClasses;
using Utilities.Managers;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    [SelectionBase]
    [RequireComponent(typeof(MoverToPosition))]
    public class Enemy : EnemyBase, IMoveToPointSubscriber
    {
#pragma warning disable 649
        [SerializeField] private float routeMargin;
        [Header("Assignables")]
        [SerializeField] private Transform bodyPivot;
        [SerializeField] private EnemyGun gun;
        [SerializeField] private EnemyDeathHandler deathHandler;
    
        [HideInInspector] public bool onShootPosition;
        private MoverToPosition mover;
        private Tween onPositionRoute;
#pragma warning restore 649

        private void Awake()
        {
            deathHandler.scoreValue = scoreValue;
        }

        public void InitializeMoving(PositionPointsManager positionManager)
        {
            positionManager.RequestFreePositionWhenAvailable(this, PositionPointType.Ship);
        }

        public void FreePositionAvailable(PositionPoint targetPosition)
        {
            if (!mover)
            {
                mover = GetComponent<MoverToPosition>();
            }

            transform.parent = targetPosition.transform;
            
            mover.OnPositionReached.AddListener(OnPositionReached);
            mover.StartMovingToPosition(targetPosition);
        }

        private void OnPositionReached()
        {
            onShootPosition = true;
        
            onPositionRoute = transform.DOMoveX(transform.position.x + routeMargin , 3f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);

            gun.CanShoot = true;
        }

        protected void OnDisable()
        {
            onShootPosition = false;
            onPositionRoute.Kill();
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
