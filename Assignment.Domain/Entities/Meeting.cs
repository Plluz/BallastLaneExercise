namespace Assignment.Domain.Entities;

public class Meeting
{
    public Meeting()
    {
        
    }

    public Meeting(Guid id, string title, DateTime startDate, DateTime endDate)
    {
        Id = id;
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
