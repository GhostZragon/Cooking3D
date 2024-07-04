using System;
using UnityEngine;

public partial class RecipeOrderProcessor
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

        public void TriggerUIOrderRelease()
        {
            Debug.Log("Hide");
            UIOrderRecipe.Hide();
        }
        public void TriggerUIOrderShow()
        {
            Debug.Log("Show");
            UIOrderRecipe.Show();
        }
        public bool IsMatchingRecipe(Recipes recipes1)
        {
            UIOrderRecipe.ShowYesTick();
            return recipes == recipes1;

        }

        public bool HasTimerEnded()
        {
            return CountDownTime <= 0;
        }

        public void SetEnableUI(bool enable)
        {
            UIOrderRecipe.gameObject.SetActive(enable);
        }

        public ScoreGrade GetScoreGradeOfTimer()
        {
            float percentage = CountDownTime / MaxCountDownTime;

            if (percentage >= 0.75f)
            {
                return ScoreGrade.High;
            }
            else if (percentage >= 0.5f)
            {
                return ScoreGrade.Medium;
            }
            else if (percentage > 0)
            {
                return ScoreGrade.Low;
            }
            else
            {
                return ScoreGrade.None;
            }
        }
    }
}
