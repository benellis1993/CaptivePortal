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
        [Column("FirstName")] // Explicitly map to the 'FirstName' column name
        public string ? FirstName { get; set; }

        [Required]
        [Column("LastName")] // Explicitly map to the 'LastName' column name
        public string ? LastName { get; set; }

        [Required]
        [Column("Email")] // Explicitly map to the 'Email' column name
        public string ? Email { get; set; }

        [Column("IPAddress")] // Explicitly map to the 'IPAddress' column name
        public string ? IPAddress { get; set; }

        [Column("CreatedAt")] // Explicitly map to the 'CreatedAt' column name
        public DateTime CreatedAt { get; set; }
    }
}
