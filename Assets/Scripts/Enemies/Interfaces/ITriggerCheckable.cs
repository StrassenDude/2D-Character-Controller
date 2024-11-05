using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{

    bool IsAggroed { get; set; }
    bool IsWhithinStrikingDistance { get; set; }

    void SetAggroState(bool isAggroed);
    void SetWhithinStrikingDistance(bool isWhithinStrikingDistance);

}
