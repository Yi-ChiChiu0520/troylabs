using System.ComponentModel.DataAnnotations;

namespace TroyLabs.Models;
public class Survey
{
    public long SurveyId { get; set; }
    
    [Required] // Other fields are required
    public string CustomerName { get; set; }

    [Required]
    public string CompanyName { get; set; }

    [Required]
    public string Position { get; set; }

    [Range(1, 10)]
    public int GiftSatisfaction { get; set; }

    [Range(1, 10)]
    public int EaseofReceiving { get; set; }

    [Range(1, 10)]
    public int GiftImpact { get; set; }

    // Comments can be null
    public string? Comments { get; set; }
}