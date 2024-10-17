﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    public decimal Amount { get; set; } 
    public DateTime Date { get; set; } 
    public string Description { get; set; } 
    
    public int CategoryId { get; set; } 
    public Category Category { get; set; } 

    public int UserId { get; set; } 
    public User User { get; set; } 
}
