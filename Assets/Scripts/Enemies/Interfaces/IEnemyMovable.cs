using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovable 
{
    
    Rigidbody2D _rb { get; set; }

    bool isFacingRight {  get; set; }

    void MoveEnemy(Vector2 velocity);

    void CheckforLeftOrRightFacing(Vector2 velocity);

}
