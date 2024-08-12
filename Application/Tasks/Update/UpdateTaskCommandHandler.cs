using Application.Data;
using Domain.Products;
using Domain.TaskDetails;
using MediatR;

namespace Application.TaskDetails.Update;

internal sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ITaskRepository taskRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);

        if (task is null)
        {
            throw new TaskNotFoundException(request.TaskId);
        }

        task.Update(request.Name, request.Descriptions, request.Status);

        _taskRepository.Update(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
