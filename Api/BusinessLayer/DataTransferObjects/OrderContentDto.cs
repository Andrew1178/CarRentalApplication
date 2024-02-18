﻿namespace BusinessLayer;

public class OrderContentDto
{
    public int Id { get; set; }
    public decimal VehicleUsDollarRatePerDay { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public VehicleDto Vehicle { get; set; } = null!;
}
