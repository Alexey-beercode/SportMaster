namespace SportMaster.BLL.Dtos.Request;

public class FoodLogRequestDTO
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string MealType { get; set; }
    public string FoodName { get; set; }
    public decimal Calories { get; set; }
    public decimal Protein { get; set; }
    public decimal Carbs { get; set; }
    public decimal Fat { get; set; }
}
