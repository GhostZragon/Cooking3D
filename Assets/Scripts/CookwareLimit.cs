using System;
using UnityEngine;

public partial class CookwareManager
{
    [Serializable]
    public class CookwareLimit
    {
        public CookwareLimit(int _amount = 0, int _limitCount = 3)
        {
            amount = _amount;
            limitCount = _limitCount;
        }
        [SerializeField] private int amount;
        [SerializeField] private int limitCount;
        public void Increase()
        {
            amount += 1;
        }
        public void Descrease()
        {
            amount -= 1;
        }
        public bool CanSpawnMore()
        {
            return amount < limitCount;
        }
    }

}
