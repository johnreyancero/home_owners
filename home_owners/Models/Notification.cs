// Models/Notification.cs
using System;
using System.ComponentModel.DataAnnotations;

public class Notification
{
    public int Id { get; set; }

    [Required]
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsRead { get; set; } = false;

    public string Username { get; set; } // for session-based lookup
}
