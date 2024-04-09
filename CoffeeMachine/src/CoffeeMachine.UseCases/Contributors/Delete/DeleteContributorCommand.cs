using Ardalis.Result;
using Ardalis.SharedKernel;

namespace CoffeeMachine.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
