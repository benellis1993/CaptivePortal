using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NabooFinalProject.Models 
{
    public class User
    {
        // [Key] annotation explicitly marks this as the primary key.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")] // Explicitly map to the 'UserId' column name
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [Column("Email")] // Explicitly map to the 'Email' column name
        public string ? Email { get; set; }

        [Column("CreatedAt")] // Explicitly map to the 'CreatedAt' column name
        public DateTime CreatedAt { get; set; }

        // Add properties for email validation
        [Column("IsEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; } = false; // To track if the email is validated

        [Column("EmailConfirmationToken")]
        public string? EmailConfirmationToken { get; set; } // To store the unique token

        [Column("EmailConfirmationTokenExpires")]
        public DateTime? EmailConfirmationTokenExpires { get; set; } // To store token expiration time
    }
}
