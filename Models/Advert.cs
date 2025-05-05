using System.ComponentModel.DataAnnotations;

namespace naboo_final_project.Models;

// Represents an advertisement entity in the database
public class Advert
{
    public int Id { get; set; } // Unique identifier for the advertisement

    [Required]
    public string Title { get; set; } = string.Empty; // Title of the advertisement

    public string ImagePath { get; set; } = string.Empty; // Path to the advertisement's image file

    [Required]
    public string Link { get; set; } = string.Empty; // URL link associated with the advertisement

    [Required]
    public int Location { get; set; } // Location identifier for categorizing the advertisement

    public string Description { get; set; } = string.Empty; // Optional description of the advertisement

    public bool IsActive { get; set; } = true; // Indicates whether the advertisement is active
}