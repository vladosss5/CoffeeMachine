﻿using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CoffeeMachine.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
