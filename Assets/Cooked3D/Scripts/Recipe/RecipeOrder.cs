﻿using System;
using UnityEngine;

public partial class RecipeOrderHandle
{
    [Serializable]
    public class RecipeOrder
    {
        [SerializeField] Recipes recipes;
        [SerializeField] UIOrderRecipe UIOrderRecipe;
        [SerializeField] float MaxCountDownTime;
        [SerializeField] float CountDownTime;

        public RecipeOrder(Recipes _recipes, UIOrderRecipe _uiOrderRecipe)
        {
            recipes = _recipes;
            UIOrderRecipe = _uiOrderRecipe;
            UIOrderRecipe.UpdateDataFromRecipe(recipes);
        }
        public void Counter(float deltaTime)
        {
            CountDownTime -= deltaTime;
            if (CountDownTime <= 0)
            {
                CountDownTime = 0;
                return;
            }
            UIOrderRecipe.UpdateFillValue(CountDownTime / MaxCountDownTime);
        }

        public void SetTimer(float maxTimer)
        {
            MaxCountDownTime = maxTimer;
            CountDownTime = maxTimer;
        }

        public void RemoveUI()
        {
            UIOrderRecipe.OnRelease();
        }

        public bool IsMatchRecipe(Recipes recipes1)
        {
            return recipes == recipes1;
        }

        public bool IsTimeOut()
        {
            return CountDownTime <= 0;
        }

        public void SetEnableUI(bool enable)
        {
            UIOrderRecipe.gameObject.SetActive(enable);
        }

    }
}