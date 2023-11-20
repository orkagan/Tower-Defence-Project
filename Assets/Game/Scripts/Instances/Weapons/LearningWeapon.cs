using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningWeapon : Weapon
{

    public void Awake()
    { 
    this.hitbox = GetComponent<Collider>();
    }

    
}


