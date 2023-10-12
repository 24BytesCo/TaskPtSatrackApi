using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Exceptions;
using Tareas.Application.Features.Categories.Commands.CreateCategory;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandled : IRequestHandler<CreateTaskCommand, TaskVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Constructor de la clase para inyectar las dependencias necesarias.
        public CreateTaskCommandHandled(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Método para manejar el comando de creación de una tarea.
        public async Task<TaskVm> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            // Validar la existencia de la categoría antes de crear la tarea.
            if (!(await ValidateCategoryExistence(request.CategoryId)))
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            // Validar que la fecha límite no sea en el pasado.
            if (request.Deadline < DateTime.Now)
            {
                throw new BadRequestException("La fecha actual no puede ser superior a la de la fecha límite");
            }

            // Mapear el objeto de solicitud a una entidad de tarea.
            var taskNew = _mapper.Map<ScheduledTask>(request);

            // Agregar la tarea a través del repositorio del Unit of Work.
            await _unitOfWork.Repository<ScheduledTask>().AddAsync(taskNew);

            // Mapear y devolver la tarea creada como resultado.
            return _mapper.Map<TaskVm>(taskNew);
        }

        // Método para validar la existencia de una categoría.
        private async Task<bool> ValidateCategoryExistence(Guid categoryId)
        {
            return await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId) != null;
        }
    }
}
