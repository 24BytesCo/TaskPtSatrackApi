using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandle : IRequestHandler<CreateCategoryCommand, CategoryVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateCategoryCommandHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<CategoryVm> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var categoryNew = _mapper.Map<Category>(request);

            await _unitOfWork.Repository<Category>().AddAsync(categoryNew);

            return _mapper.Map<CategoryVm>(categoryNew);
        }
    }
}
