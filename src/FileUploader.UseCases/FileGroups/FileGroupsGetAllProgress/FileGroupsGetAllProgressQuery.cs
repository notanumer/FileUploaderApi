using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.UseCases.FileGroups.FileGroupsGetAllProgress;

public record FileGroupsGetAllProgressQuery : IRequest<double>;
