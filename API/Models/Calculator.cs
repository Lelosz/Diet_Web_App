namespace WebAPI.Models
{
    public class Calculator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Result{ get; set; }
        public CalculatorType CalculatorType{ get; set; }
        public DateTime Created { get; set; }
    }

    public enum CalculatorType 
    {
        BMI, WHR, PPM, CPM, WeightPlanner, PhysicalActivity, FoodCalories
    }
}
