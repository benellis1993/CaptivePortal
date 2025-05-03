using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace naboo_final_project.Models
{
    public class AdminUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AdminUserId")]
        public int AdminUserId { get; set; }

        [Required]
        [EmailAddress]
        [Column("Email")]
        public string Email { get; set; }

        [Required]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; } // Store hashed passwords

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("Role")]
        public string Role { get; set; } = "Admin"; // Default role is "Admin"
    }
}