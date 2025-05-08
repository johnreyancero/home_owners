public class Booking
{
    public int Id { get; set; }
    public string GuestName { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string? SpecialRequest { get; set; }
    public string? RoomName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pending";
    
}
