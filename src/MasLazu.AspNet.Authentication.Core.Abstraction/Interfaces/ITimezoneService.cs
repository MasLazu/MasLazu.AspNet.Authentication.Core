using MasLazu.AspNet.Framework.Application.Interfaces;
using MasLazu.AspNet.Authentication.Core.Abstraction.Models;

namespace MasLazu.AspNet.Authentication.Core.Abstraction.Interfaces;

public interface ITimezoneService : ICrudService<TimezoneDto, CreateTimezoneRequest, UpdateTimezoneRequest>
{
}
