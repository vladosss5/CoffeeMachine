﻿namespace CoffeeMachine.API.DTOs.Machine;

public class MachineEditDto
{
    public long Id { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public int Balance { get; set; }  
}