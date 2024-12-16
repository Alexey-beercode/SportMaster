using SportMaster.Domain.Enums;
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class FoodLog : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public MealType MealType { get; set; }
    public string FoodName { get; set; }
    public decimal Calories { get; set; }
    public decimal Protein { get; set; }
    public decimal Carbs { get; set; }
    public decimal Fat { get; set; }
}