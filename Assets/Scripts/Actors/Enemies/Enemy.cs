using DG.Tweening;
using Interface;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    [SelectionBase]
    [RequireComponent(typeof(MoverToPosition))]
    public class Enemy : ExtendedMono, IMoveAfterSpawn
    {
#pragma warning disable 649
        [SerializeField] private float routeMargin;
        [Header("Assignables")]
        [SerializeField] private Transform bodyPivot;
        [SerializeField] private EnemyGun gun;
    
        [HideInInspector] public bool onShootPosition;
        private bool canShoot;
        private MoverToPosition mover;
        private Tween onPositionRoute;
#pragma warning restore 649

        public void InitializeMoving(PositionPointsManager positionManager)
        {
            positionManager.RequestFreePositionWhenAvailable(this);
        }

        public void FreePositionAvailable(PositionPointsManager.PositionPoint targetPosition)
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
            canShoot = true;
        
            onPositionRoute = transform.DOMoveX(transform.position.x + routeMargin , 3f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);

            gun.CanShoot = true;
        }

        private void OnDisable()
        {
            onShootPosition = false;
            canShoot = false;
            onPositionRoute.Kill();
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
