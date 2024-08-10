using Application.Data;
using Domain.Customers;
using FluentValidation;
using MediatR;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerCommand> validator
        )
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
            throw new Exceptions.ValidationException(result.Errors);

        var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Email, request.Email);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
