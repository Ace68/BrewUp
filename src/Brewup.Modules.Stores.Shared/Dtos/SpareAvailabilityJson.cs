﻿namespace Brewup.Modules.Stores.Shared.Dtos;

public record SpareAvailabilityJson(string SpareId, double Stock, double Availability, double ProductionCommitted, double SalesCommitted, double SupplierOrdered);