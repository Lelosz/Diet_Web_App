namespace WebAPI.Models
{
    public class CalculatorModel
    {
        public int Id { get; set; }
        public String Result{ get; set; }

        public CalculatorType CalculatorType{ get; set; }
        public DateTime Created { get; set; }

        public int? UserId { get; set; }
        public UserModel? User { get; set; }
    }

    public enum CalculatorType 
    {
        BMI, WHR, PPMCPM, WeightPlanner, PhysicalActivity, FoodCalories
    }
}
