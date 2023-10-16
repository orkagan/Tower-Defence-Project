using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggresiveEntity : Entity
{
    #region Fields
    private Collider target;
    #endregion

    #region Methods
    public abstract void Attack(); //depending on how actually implementing things goes, this very may well need to be virtual, not abstract, because having a basic implementation for attack might be useful
    public abstract Collider FindTarget();
    #endregion
}
