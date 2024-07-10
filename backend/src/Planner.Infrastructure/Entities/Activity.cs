﻿using Planner.Infrastructure.Enums;

namespace Planner.Infrastructure.Entities;
public class Activity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public ActivityStatus Status { get; set; }
    public Guid TripId { get; set; }
}
