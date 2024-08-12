using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMovable 
{
    Rigidbody2D rb { get; set; }
    void MoveCharacter();
}
