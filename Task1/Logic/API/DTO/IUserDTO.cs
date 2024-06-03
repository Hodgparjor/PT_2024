using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API.DTO
{
    internal interface IUserDTO
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
