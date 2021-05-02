namespace LevelComponents.SolutionElements.LevelSolutions.RoyalWoodsSolutionEvents
{
    public class RoyalWoodsMainSolution : LevelSolutionEvent
    {
        public override void OnLevelSolution()
        {
            Portal.OpenPortal();
        }

        public override void OnNoLevelSolution()
        {
            Portal.ClosePortal();
        }
    }
}