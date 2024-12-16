namespace SportMaster.BLL.Dtos.Request;

public class RecommendationRequestDTO
{
    public Guid UserId { get; set; }
    public string RecommendationText { get; set; }
    public DateTime Date { get; set; }
}
