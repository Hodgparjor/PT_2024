﻿using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    internal class UserModel : IUserModel
    {
        public UserModel(int id, string name) {
            Id = id;
            Name = name;
        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}