using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Exceptions;
using Tareas.Application.Features.Tasks.Commands.CreateTask;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandled : IRequestHandler<UpdateTaskCommand, TaskVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Constructor de la clase para inyectar las dependencias necesarias.
        public UpdateTaskCommandHandled(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskVm> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToUpdate = await GetTask(request.TaskId);

            if (taskToUpdate == null)
            {
                throw new NotFoundException("Tarea", request.TaskId);
            }
           

            _mapper.Map(request, taskToUpdate, typeof(UpdateTaskCommand), typeof(ScheduledTask));
            await _unitOfWork.Repository<ScheduledTask>().UpdateAsync(taskToUpdate);

            return _mapper.Map<TaskVm>(taskToUpdate);

        }

        // Método para consultar una tarea
        private async Task<ScheduledTask> GetTask(Guid taskId)
        {
            return await _unitOfWork.Repository<ScheduledTask>().GetByIdAsync(taskId);
        }
    }
}
