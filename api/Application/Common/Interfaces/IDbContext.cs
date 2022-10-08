﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Common.Interfaces;

public interface IDbContext
{
    DbSet<Medic> Medics { get; }
    
    DbSet<Patient> Patients { get; }

    DbSet<Symptom> Symptoms { get; }
}

