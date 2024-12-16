namespace SportMaster.BLL.Dtos;

public class RecommendationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RecommendationText { get; set; }
    public DateTime Date { get; set; }
}