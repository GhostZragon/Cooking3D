using System;

public partial class CookwareRecipeHandle
{
    [Serializable]
    public struct RecipeStructure
    {
        public Recipes Recipes;
        public bool isComplete;

        public void CompleteFood()
        {
            isComplete = true;
        }
        public override bool Equals(object obj)
        {
            if(obj is Recipes)
            {

                return Recipes == obj as Recipes;
            }
            return false;
        }
    }
}