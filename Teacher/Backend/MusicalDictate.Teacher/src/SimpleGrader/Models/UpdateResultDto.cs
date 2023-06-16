namespace SimpleGrader.Models;

public class UpdateResultDto
{
    public string StudentSolutionId { get; set; }
    public int Grade { get; set; }
    public double Percentage { get; set; }
    public string Comment { get; set; }
}