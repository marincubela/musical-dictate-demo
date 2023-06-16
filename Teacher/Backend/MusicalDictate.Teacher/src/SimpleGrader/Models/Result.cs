namespace SimpleGrader.Models;

public class Result
{
    private Result() { }

    public Grade Grade { get; private set; }

    public Teacher Teacher { get; private set; }

    public string TeacherId { get; private set; }

    public string Comment { get; private set; }

    public double Percentage { get; private set; }

}